
using Business_logic_Layer;
using Database.Contexts;
using Database.DBModelCommands;
using FitnessLogic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PortableFitnessApp.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// допустим я буду использовать файл ресурсов. Монтировать нужные строки из него в список который буду отправлять на страницу.
// Add services to the container.
// первым делом подключим базу данных .В ней будут храниться данные пользователя. Кроме того , попробуем записать туда так же и продукты.

//БДШКА
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ApplicationException("you can't get connection from appsettings");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, x => x.MigrationsAssembly("PortableFitnessApp")));


//IDENTITY

builder.Services.AddAuthentication(options =>
{
}).AddCookie();// возможно ЭТО НЕЛЬЗЯ
builder.Services.AddAuthorization();
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
    // - брутфорс
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    //options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Срок действия куки  
    options.ExpireTimeSpan = TimeSpan.FromHours(1);

    // Автоматическое продление срока действия при активности
    options.SlidingExpiration = true;

    //HTTPS запрос для доп защиты.
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    // Запрет доступа к куки через js
    options.Cookie.HttpOnly = true;

    //options.Cookie.Expiration = TimeSpan.FromHours(2);

    // Защита от CSRF-атак
    options.Cookie.SameSite = SameSiteMode.Strict;

    // Пути при ошибках аутентификации
    options.LoginPath = "/User/Login";
    options.AccessDeniedPath = "/User/Login"; //  ну либо можно вводить js сообщение что тебе туда нельзя.


});



// сделать в будущем сброс пароля. и email хотя для этого придётся нотификация , так что можно по проверке спец вопроса? который тоже будет хэшироваться??

//builder.Services.AddScoped<UserContext>();

// регистрирует как Scoped по умолчанию

//СЕРВИСЫ
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();







// добавляем swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    //// Добавляем XML-документацию
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllersWithViews();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

if (app.Environment.IsDevelopment())
{
    //БЕЗОПАСНОСТЬ +Swagger
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}






app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
