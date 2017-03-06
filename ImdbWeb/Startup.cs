﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ImdbWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc(routes => {
                routes.MapRoute("Movies by genre route", "Movie/Genre/{genrename}",
                    new { Controller = "Movie", Action = "MoviesByGenre" });

                routes.MapRoute("Person details", "Person/{id}",
                    new { Controller = "Person", Action = "Details" },
                    new { id=@"\d+"});

                routes.MapRoute("Movie cover route", "Image/{format}/{id}.jpg",
                    new { Controller="Image", Action="CreateImage"});

                routes.MapRoute("Default route", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}