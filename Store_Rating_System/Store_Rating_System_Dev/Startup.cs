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
            var connectionStringParameters = connection.Split(';');
            string relative_path_to_db = null;
            string NewConnectionString = "";
            foreach (var item in connectionStringParameters)
            {

                if (item.Contains("AttachDbFilename"))
                {
                    relative_path_to_db = item.Split('=')[1];
                    var path_splited = relative_path_to_db.Split("\\");
                    var relative_path_to_dir_db = path_splited[0] + "\\" + path_splited[1];
                    var name_of_db = path_splited[2];
                    Directory.SetCurrentDirectory(relative_path_to_dir_db);
                    var full_path_to_db = Directory.GetCurrentDirectory() + "\\" + name_of_db;

                    NewConnectionString += "AttachDbFilename=" + full_path_to_db + ";";
                }
                else
                {
                    NewConnectionString += item + ";";
                }
            }

            return NewConnectionString;

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


            app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            ApplicationDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
