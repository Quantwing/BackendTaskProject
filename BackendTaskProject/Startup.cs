using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using BackendTaskProject.Models;
using BackendTaskProject.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Web;

namespace BackendTaskProject
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
            services.AddMvc();
            services.AddDbContext<BookContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BookContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            bool openBrowser = Configuration.GetValue<bool>("openbrowser");
            if (openBrowser)
            {
                Process.Start(new ProcessStartInfo("http://localhost:50615/book") { UseShellExecute = true });
            }
        }
    }
}
