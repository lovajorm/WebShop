using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebShop.Dal;
using WebShop.Log;
using WebShop.Web.Interfaces;
using WebShop.Web.Middleware;
using WebShop.Web.Models;
using WebShop.Web.Repositories;

namespace WebShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("C:\\Development\\Webshoppen\\WebShop\\WebShop.Log\\log4net.config"));
            services.AddSingleton<IMessageLogger, MessageLogger>();

            //services.AddSingleton(new LoggerFactory()
            //    .AddConsole(Configuration.GetSection("Logging"))
            //    .AddDebug()
            //    .AddLog4Net(Configuration.GetValue<string>("Log4NetConfigFile:Name")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShoppingCart.GetCart(sp));                                                         //Loads the shopping cart function.  

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection =
                @"Server=(localdb)\mssqllocaldb;Database=WebShop.DataModel.DbContext;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<WebShopDbContext>(o => o.UseSqlServer(connection));

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/WebShop/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMiddleware<RestLoggingMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "categoryFilter", template: "Product/{action}/{category?}", defaults: new { Controller = "Product", action = "List" });
                routes.MapRoute(name: "default", template: "{controller=Product}/{action=List}/{id?}");
            });
        }
    }
}
