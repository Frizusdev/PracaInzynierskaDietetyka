using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracaInzynierskaDietetyka.Data;
using PracaInzynierskaDietetyka.Repository;
using PracaInzynierskaDietetyka.Services.ConnectorServices;
using PracaInzynierskaDietetyka.Services.DishesServices;
using PracaInzynierskaDietetyka.Services.UserDataServices;
using PracaInzynierskaDietetyka.Services.WorkoutService;
using PracaInzynierskaDietetyka.Services.Dish_TypesSerivces;
using PracaInzynierskaDietetyka.Services.WorkoutConnectorServices;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 27))));

//builder.Services.AddDefaultIdentity<XUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<XUser, XRole>(options =>
{
    options.Stores.MaxLengthForKeys = 128;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders()
.AddRoles<XRole>();

builder.Services.AddScoped<IDishesService, DishesService>();
builder.Services.AddScoped<IDishesRepository, DishesRepository>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<IUserDataService, UserDataService>();
builder.Services.AddScoped<IUserDataRepository, UserDataRepository>();
builder.Services.AddScoped<IConnectorService, ConnectorService>();
builder.Services.AddScoped<IConnectorRepository, ConnectorRepository>();
builder.Services.AddScoped<IDish_TypesService, Dish_TypesService>();
builder.Services.AddScoped<IDish_TypesRepository, Dish_TypesRepository>();
builder.Services.AddScoped<IWorkoutConnectorService, WorkoutConnectorService>();
builder.Services.AddScoped<IWorkoutConnectorRepository, WorkoutConnectorRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();

