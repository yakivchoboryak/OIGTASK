using Microsoft.EntityFrameworkCore;
using SurveyManagement.App.Interfaces;
using SurveyManagement.App.Services;
using SurveyManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<ISurveyService, SurveyService>();

var connectionString = builder.Configuration.GetConnectionString("SurveyDbConnection");
builder.Services.AddDbContext<SurveyDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("SurveyManagement.Infrastructure")));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SurveyDbContext>();
    dbContext.Database.Migrate();
}

// TODO: add exception handling middleware
// Add logging

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

app.MapBlazorHub();
app.Run();
