
using Database.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// допустим € буду использовать файл ресурсов. ћонтировать нужные строки из него в список который буду отправл€ть на страницу.
// Add services to the container.
// первым делом подключим базу данных .¬ ней будут хранитьс€ данные пользовател€.  роме того , попробуем записать туда так же и продукты.
builder.Services.AddControllersWithViews();
//

//builder.Services.AddScoped<UserContext>();
 

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")??throw new ApplicationException("you can't get connection from appsettings");

// регистрирует как Scoped по умолчанию?
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

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
