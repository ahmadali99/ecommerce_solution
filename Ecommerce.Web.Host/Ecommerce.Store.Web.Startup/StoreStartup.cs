using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace Ecommerce.Store.Web.Startup
{
    public static class StoreStartup
    {
        public static void AddStoreServices(this IServiceCollection services)
        {
            var assembly = typeof(StoreStartup).Assembly;
            var assemblyPath = Path.GetDirectoryName(assembly.Location)!;
            var viewsPath = Path.Combine(Path.GetDirectoryName(typeof(StoreStartup).Assembly.Location)!, "Views").Replace("\\", "/");

            services.AddControllersWithViews()
                .AddApplicationPart(assembly)
                .AddRazorRuntimeCompilation(options =>
                 {
                     if (Directory.Exists(viewsPath))
                     {
                         options.FileProviders.Add(new PhysicalFileProvider(viewsPath));
                         Console.WriteLine($"✅ Razor views added from: {viewsPath}");
                     }
                     else
                     {
                         Console.WriteLine($"❌ Views path not found: {viewsPath}");
                     }
                 });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
        }



        public static void UseStoreServices(this WebApplication app)
        {
            // Serve static files from the class library's wwwroot
            var wwwrootPath = Path.Combine(Path.GetDirectoryName(typeof(StoreStartup).Assembly.Location)!, "wwwroot");

            if (Directory.Exists(wwwrootPath))
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(wwwrootPath),
                    RequestPath = ""
                });
            }

            // Configure routing
            app.UseRouting();
            app.UseAuthorization();

            // Map default controller route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
