
using Database.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// допустим я буду использовать файл ресурсов. Монтировать нужные строки из него в список который буду отправлять на страницу.
// Add services to the container.
// первым делом подключим базу данных .В ней будут храниться данные пользователя. Кроме того , попробуем записать туда так же и продукты.
builder.Services.AddControllersWithViews();
//
builder.Services.AddScoped<Database.Contexts.AppContext>();
//builder.Services.AddScoped<UserContext>();
 

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")??throw new ApplicationException("you can't get connection from appsettings");
//
//builder.Services.AddDbContext<UserContext>(options=>options.UseSqlServer(connectionString));
builder.Services.AddDbContext<Database.Contexts.AppContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("PortableFitnessApp")));
var app = builder.Build();
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Подключение к базе данных успешно установлено!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка подключения: " + ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
