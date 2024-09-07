using Repository.Interface;
using Repository.SqlServer;
using Services;
using UnitOfWork.Interfaces;
using UnitOfWork.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork,UnitOfWorkSqlServer>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWorkSqlServer>();
builder.Services.AddScoped<IClienteService,ClienteService>();
//builder.Services.AddScoped<IClienteRepository,ClienteRepository>();


var app = builder.Build();

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
    pattern: "{controller=Cliente}/{action=Index}/{id?}");

app.Run();
