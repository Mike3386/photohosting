using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Photohosting.Models;
using Microsoft.Extensions.PlatformAbstractions;
using log4net.Config;
using log4net;
using System.Reflection;
using System.IO;
using BusinessLogic;
using BusinessLogic.Models;

namespace Photohosting
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("Photohosting")));

            services.AddDbContext<ImageContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("Photohosting")));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Sessions"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
            DatabaseInitialize(app.ApplicationServices);
        }

        public void DatabaseInitialize(IServiceProvider serviceProvider)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            using (UserContext db = serviceProvider.GetRequiredService<UserContext>())
            {
                Role adminRole = db.Roles.FirstOrDefault(x => x.Name == adminRoleName);
                Role userRole = db.Roles.FirstOrDefault(x => x.Name == userRoleName);
                
                if (adminRole == null)
                {
                    adminRole = new Role { Name = adminRoleName };
                    db.Roles.Add(adminRole);
                }
                if (userRole == null)
                {
                    userRole = new Role { Name = userRoleName };
                    db.Roles.Add(userRole);
                }
                db.SaveChanges();
                
                User admin = db.Users.FirstOrDefault(u => u.Email == adminEmail);
                if (admin == null)
                {
                    db.Users.Add(new User { Email = adminEmail, Password = adminPassword, Role = adminRole });
                    db.SaveChanges();
                }
            }
        }
    }
}
