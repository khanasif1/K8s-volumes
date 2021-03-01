using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace K8s.Volumes
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
            // injects IOptionsMonitor<ValuesConfiguration>)
            services.Configure<APIConfiguration>(Configuration.GetSection(APIConfiguration.Position));
            APISecret _secret = new APISecret();
            try
            {
                //using (var reader = new StreamReader(@"secrets\App-Secret.txt"))
                using (var reader = new StreamReader(@"secrets/App-Secret"))
                {
                    _secret.Secret = reader.ReadToEnd();
                }
                if (string.IsNullOrEmpty(_secret.Secret))
                {
                    _secret.Secret = "data missing";
                }
            }
            catch (Exception ex)
            {
                _secret.Secret = ex.Message;
            }

            // injects ValuesConfiguration
            APIConfiguration apiConfiguration = new APIConfiguration();
            Configuration.GetSection(APIConfiguration.Position).Bind(apiConfiguration);
            services.AddSingleton<APIConfiguration>(apiConfiguration);
            services.AddSingleton<APISecret>(_secret);
            services.AddControllers();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
