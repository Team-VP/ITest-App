using AutoMapper;
using ITestApp.Common.Providers;
using ITestApp.Data;
using ITestApp.Data.DataSeed;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services;
using ITestApp.Services.Contracts;
using ITestApp.Web.Models;
using ITestApp.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ITestApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.Configuration = configuration;
            this.Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.RegisterData(services);
            this.RegisterAuthentication(services);
            this.RegisterServices(services);
            this.RegisterInfrastructure(services);
        }

        private void RegisterData(IServiceCollection services)
        {
            services.AddDbContext<ITestAppDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ISaver, EntitySaver>();
        }

        private void RegisterAuthentication(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ITestAppDbContext>()
                .AddDefaultTokenProviders();

            if (this.Environment.IsDevelopment())
            {
                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
                    options.Lockout.MaxFailedAccessAttempts = 999;
                });
            }
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IAnswersService, AnswersService>();
            services.AddTransient<ITestsService, TestsService>();
            services.AddTransient<ICategoryService, CategoriesService>();
            services.AddTransient<IStatusesService, StatusesService>();
            services.AddTransient<IResultService, ResultService>();
            services.AddTransient<IAdminService, AdminService>();
        }

        private void RegisterInfrastructure(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();
            services.AddScoped<IMappingProvider, MappingProvider>();
            services.AddAntiforgery(options => options.HeaderName = "__RequestVerificationToken");
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            if (this.Environment.IsDevelopment())
            {
                //DataSeeder.Seed(serviceProvider);
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areaRoute",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //CreateUserRoles(serviceProvider).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();


            IdentityResult roleResult;
            //Adding Addmin Role  
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");

            if (!roleCheck)
            {
                //create the roles and seed them to the database  
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //Assign Admin role to the main User here we have given our newly loregistered login id for Admin management  
            //User user = await UserManager.FindByEmailAsync("w@w.com");
            User user = await UserManager.FindByEmailAsync("w@w.com");

            var User = new User();
            await UserManager.AddToRoleAsync(user, "Admin");
        }
    }
}
