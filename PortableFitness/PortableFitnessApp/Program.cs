
using Business_logic_Layer;
using Database.Contexts;
using Database.DBModelCommands;
using FitnessLogic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PortableFitnessApp.DTO;
using System.Reflection;


 var builder = WebApplication.CreateBuilder(args);

// �������� � ���� ������������ ���� ��������. ����������� ������ ������ �� ���� � ������ ������� ���� ���������� �� ��������.
// Add services to the container.
// ������ ����� ��������� ���� ������ .� ��� ����� ��������� ������ ������������. ����� ���� , ��������� �������� ���� ��� �� � ��������.

//�����
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ApplicationException("you can't get connection from appsettings");
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connectionString, x => x.MigrationsAssembly("PortableFitnessApp")));


//IDENTITY

builder.Services.AddAuthentication().AddCookie("cookie");// �������� ��� ������
builder.Services.AddAuthorization();
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireLowercase = true;
})
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();
// ������� � ������� ����� ������. � email ���� ��� ����� ������� ����������� , ��� ��� ����� �� �������� ���� �������? ������� ���� ����� ������������??

//builder.Services.AddScoped<UserContext>();

// ������������ ��� Scoped �� ���������

//�������
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();







// ��������� swagger
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

	//// ��������� XML-������������
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
    pattern: "{controller=User}/{action=Register}/{id?}");

app.Run();
