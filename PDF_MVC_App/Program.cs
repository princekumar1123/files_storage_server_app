using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PDF_MVC_App.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PDF_MVC_AppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PDF_MVC_AppContext") ?? throw new InvalidOperationException("Connection string 'PDF_MVC_AppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=PdfDocuments}/{action=Index}/{id?}");

app.Run();

//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//public class Startup
//{
//    public Startup(IConfiguration configuration)
//    {
//        Configuration = configuration;
//    }

//    public IConfiguration Configuration { get; }

//    //public void ConfigureServices(IServiceCollection services)
//    //{
//    //    services.AddDbContext<AppDbContext>(options =>
//    //        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//    //    services.AddControllersWithViews();
//    //}

//    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//    {
//        if (env.IsDevelopment())
//        {
//            app.UseDeveloperExceptionPage();
//        }
//        else
//        {
//            app.UseExceptionHandler("/Home/Error");
//            app.UseHsts();
//        }

//        app.UseHttpsRedirection();
//        app.UseStaticFiles();

//        app.UseRouting();

//        app.UseAuthorization();

//        app.UseEndpoints(endpoints =>
//        {
//            endpoints.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");
//        });
//    }
//}

