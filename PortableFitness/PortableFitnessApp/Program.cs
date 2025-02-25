
using Database.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// �������� � ���� ������������ ���� ��������. ����������� ������ ������ �� ���� � ������ ������� ���� ���������� �� ��������.
// Add services to the container.
// ������ ����� ��������� ���� ������ .� ��� ����� ��������� ������ ������������. ����� ���� , ��������� �������� ���� ��� �� � ��������.
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
        Console.WriteLine("����������� � ���� ������ ������� �����������!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("������ �����������: " + ex.Message);
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
