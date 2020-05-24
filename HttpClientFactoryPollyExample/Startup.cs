using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientFactoryPollyExample.Configuration;
using HttpClientFactoryPollyExample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;

namespace HttpClientFactoryPollyExample
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
            services.Configure<SuperHeroApiConfig>(
                Configuration.GetSection(nameof(SuperHeroApiConfig)));

            // Make the SuperHeroApiConfig available to dependancy injcection
            services.AddSingleton<ISuperHeroApiConfig>(sp =>
                sp.GetRequiredService<IOptions<SuperHeroApiConfig>>().Value);


            // Create the retry policy we want
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError() // HttpRequestException, 5XX and 408
                                        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

            // register the Service and apply the retry policy
            services.AddHttpClient<ISuperHeroService, SuperHeroService>(o => 
                                    o.BaseAddress = new Uri(Configuration["SuperHeroApiConfig:BaseUrl"]))
                .AddPolicyHandler(retryPolicy);


            services.AddControllers();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
