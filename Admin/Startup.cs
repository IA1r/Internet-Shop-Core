using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Core.Model;
using Admin.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Admin
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			string connection = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<InternetShopContext>(options => options.UseSqlServer(connection));

			services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<InternetShopContext>();

			services.RegisterDependencies();

			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				options.CookieName = ".User.Session";
				options.IdleTimeout = TimeSpan.FromMinutes(15);
			});

			// Add framework services.
			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

			app.UseStaticFiles();

			app.UseIdentity();

			app.UseSession();

			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

				routes.MapRoute(
					name: "searchProduct",
					template: "{controller=Home}/{action=Index}/{keyword?}");

				routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
