using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codex.SalarySurvey.API.AspCoreExtensions;
using Codex.SalarySurvey.Data;
using Codex.SalarySurvey.Data.Infrastructure;
using Codex.SalarySurvey.Data.Repositories;
using Codex.SalarySurvey.Domain;
using Codex.SalarySurvey.Domain.Contracts;
using Codex.SalarySurvey.Domain.Contracts.Repositories;
using Codex.SalarySurvey.Domain.Contracts.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Codex.SalarySurvey.Integration;
using Codex.SalarySurvey.Common.Settings;
using Microsoft.IdentityModel.Tokens;
using Codex.SalarySurvey.Domain.Auth;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace Codex.SalarySurvey.API
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers(config => config.Filters.Add(typeof(ValidateModelAttribute), -3000));
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"))).
                AddScoped<AppDbContext>();

            // Register configuration intance containing the application settings.
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // Configure the JSON Serializer to use the Camel-Case serialization for the property names.
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            // Register the Swagger generator, defining 1 or more Swagger documents.
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Salary Survey APIs", Version = "v1" });

                // Define the BearerAuth scheme that's in use.
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
            });

            // Jwt wire up.
            // Get options from app settings.
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions.
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // Register HttpContextAccessor.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Register AutoMapper.
            services.AddAutoMapper();

            // Register JwtFactory.
            services.AddTransient<IJwtFactory, JwtFactory>();

            // Register UnitOfWork.
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Register Services.
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IEmployerService, EmployerService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISurveyService, SurveyService>();
            services.AddTransient<ISurveyQuestionService, SurveyQuestionService>();

            // Register Repositories.
            services.AddTransient<IEmployerRepository, EmployerRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISurveyRepository, SurveyRepository>();
            services.AddTransient<ISurveyQuestionRepository, SurveyQuestionRepository>();
            services.AddTransient<IQuestionAnswerRepository, QuestionAnswerRepository>();

            // Register Others.
            services.AddTransient<ICodexResourceManager, CodexResourceManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Salary Survey APIs V1");
            });

            app.UseSwagger();
        }
    }
}
