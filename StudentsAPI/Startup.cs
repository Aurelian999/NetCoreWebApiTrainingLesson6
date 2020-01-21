using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StudentsAPI.Core.Entities;
using StudentsAPI.Middleware;
using StudentsAPI.V2.Filters;
using StudentsAPI.V2.Services;
using StudentsAPI.V2.Services.Extensions;
using StudentsAPI.V2.Services.Interfaces;

namespace StudentsAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStudentsService();
            services.AddSingleton<IEventsService, EventsService>();
            services.AddSingleton<ICodeCommitsService, CodeCommitsService>();

            services.AddControllers(options => {
                options.Filters.Add(typeof(LogAction));
            }).AddNewtonsoftJson();

            services.AddAutoMapper(typeof(Startup));
            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer( options =>
                {
                    options.Authority = "https://localhost:5000";
                    options.Audience = StudentsAPIResource.APIName;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireClaim("Scope", new string[] { StudentsAPIScopes.Admin }));

                options.AddPolicy("RegularUser", policy => 
                    policy.RequireClaim("Scope", new string[] { StudentsAPIScopes.Admin, StudentsAPIScopes.User }));

            });

            services.AddScoped<LogAction>();
            services.AddScoped<ValidateStudentId>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    await context.Response.WriteAsync("Unexpected server error. Please contact admin@localhost.com.");
                }));
            }

            loggerFactory.AddFile(Configuration.GetSection("Logging")["LogFile"]);

            app.UseHttpsRedirection();
            app.UseLicensing();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
