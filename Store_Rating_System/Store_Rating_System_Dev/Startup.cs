using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Store_Rating_System_Dev.Models;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Text.RegularExpressions;

namespace Store_Rating_System_Dev
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["Data:Store_Rating_System_dev_connection_str:ConnectionString"];
            var NewConnectionString = GetUpdatedConnectionString(connectionString);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(NewConnectionString));

            services.AddTransient<IRepository, EFRepository>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAuthenticatedUser", policy =>
                       policy.RequireAuthenticatedUser());
            });
        }


        private string GetUpdatedConnectionString(string connection)
        {
            Regex RegexAttachDbFilename = new Regex("AttachDbFilename=(.+?);");
            string AttachDbFilename = RegexAttachDbFilename.Match(connection).Groups[1].ToString();

            Regex RegexDbName = new Regex(@"\\.+?$", RegexOptions.RightToLeft);
            Regex RegexRelativePath = new Regex(@"^(.+)\\.+?", RegexOptions.RightToLeft);
            string NameDB = RegexDbName.Match(AttachDbFilename).ToString();
            string RelativePath = RegexRelativePath.Match(AttachDbFilename).Groups[1].ToString();

            Directory.SetCurrentDirectory(RelativePath);
            string FullPath = Directory.GetCurrentDirectory() + NameDB;

            string NewConnection = RegexAttachDbFilename.Replace(connection, "AttachDbFilename=" + FullPath + ";");

            return NewConnection;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseDeveloperExceptionPage();
            app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            ApplicationDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
