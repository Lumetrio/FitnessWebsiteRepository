
using Database.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// �������� � ���� ������������ ���� ��������. ����������� ������ ������ �� ���� � ������ ������� ���� ���������� �� ��������.
// Add services to the container.
// ������ ����� ��������� ���� ������ .� ��� ����� ��������� ������ ������������. ����� ���� , ��������� �������� ���� ��� �� � ��������.
builder.Services.AddControllersWithViews();
//

//builder.Services.AddScoped<UserContext>();
 

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")??throw new ApplicationException("you can't get connection from appsettings");

// ������������ ��� Scoped �� ���������?
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),x=>x.MigrationsAssembly("PortableFitnessApp")));


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
