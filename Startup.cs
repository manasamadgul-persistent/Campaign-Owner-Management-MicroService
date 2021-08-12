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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using CampaignMgmt.Data;
using CampaignMgmt.Models;
using Microsoft.Extensions.Options;
using CampaignMgmt.Repository;
using CampaignMgmt.Extensions;
using CampaignMgmt.DbContext;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CampaignMgmt
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
            services.AddSingleton(typeof(IMongoDBContext<>),typeof(MongoDBContext<>));
            
            services.Configure<OwnerDBSettings>(
                Configuration.GetSection(nameof(OwnerDBSettings)));

            services.AddSingleton<IOwnerDBSettings>(sp =>
                sp.GetRequiredService<IOptions<OwnerDBSettings>>().Value);

            services.Configure<JWTTokenKey>(
                 Configuration.GetSection(nameof(JWTTokenKey)));

            services.AddSingleton<IJWTTokenKey>(sp =>
                sp.GetRequiredService<IOptions<JWTTokenKey>>().Value);

            services.AddSingleton(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddSingleton(typeof(IOwnerRepository), typeof(OwnerRepository));
            services.AddSingleton(typeof(ILoginRepository), typeof(LoginRepository));

            services.AddControllers();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                    x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection(nameof(JWTTokenKey)).ToString())),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CampaignMgmt", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CampaignMgmt v1"));
            //}
            app.ConfigurExceptionHandler(env);

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
