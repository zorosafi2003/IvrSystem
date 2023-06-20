using IvrSystem.Application.IvrFeatures.CallManagerFeatures.Commands;
using IvrSystem.Persistence.IProviders;
using IvrSystem.Persistence.Providers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IvrSystem.Api
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

            services.AddHttpContextAccessor();

            services.AddScoped<IHttpProvider, HttpProvider>();

            services.AddMediatR(typeof(Call_CallManagerCommand).GetTypeInfo().Assembly);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IvrSystem.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IvrSystem.Api v1"));

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                  .SetPreflightMaxAge(TimeSpan.FromSeconds(6000)));

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
