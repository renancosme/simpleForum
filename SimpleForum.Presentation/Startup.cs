using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleForum.Domain;
using SimpleForum.Domain.Repository;
using SimpleForum.Infrastructure;
using SimpleForum.Infrastructure.Repository;
using System;

namespace SimpleForum.Presentation
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
            services.AddDbContext<SimpleForumContext>(
                options => options.UseSqlServer(Configuration.GetValue<string>("SimpleForumContext"),
                p => p.UseRowNumberForPaging()));
                        
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ITopicRepository, TopicRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMvc();

            // Sets the default authentication scheme for the app.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Login/Login";
                        options.LogoutPath = "/Login/Logout";
                        options.ExpireTimeSpan = TimeSpan.FromSeconds(20);
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Invoke the Authentication Middleware
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
