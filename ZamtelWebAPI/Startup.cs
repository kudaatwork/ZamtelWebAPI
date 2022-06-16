using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZamtelWebAPI.Models;
using FluentValidation.AspNetCore;

namespace ZamtelWebAPI
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
            services.AddControllers().AddFluentValidation(fv =>
            {                
                //fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddDbContext<ZamtelContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ZamtelDb")));

            services.AddSwaggerGen(gen => gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Zamtel KYC Web API", Version = "v1.0"}));

            // /swagger/v1.0/swagger.json
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();           

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(ui => { ui.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Zamtel KYC Web API Endpoint"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
