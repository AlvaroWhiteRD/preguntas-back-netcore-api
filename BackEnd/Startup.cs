using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Persistence.Context;
using BackEnd.Persistence.Repositories;
using BackEnd.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd
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
            services.AddDbContext<AplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConectionString")));

            // Service
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IQuestionnaireService, QuestionnaireServices>();
            services.AddScoped<IAnswerQuestionnaireServices, AnswerQuestionnaireServices>();

            // Repository
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
            services.AddScoped<IAnswerQuestionnaireRepository, AnswerQuestionnaireRepository>();

            //cors
            services.AddCors(options => options.AddPolicy("AllowWepApp",
                                        builder=> builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()));

            //autenticacion jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters{ 
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["jwt:Issuer"],
                    ValidAudience = Configuration["jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero

                });

            services.AddControllers().AddNewtonsoftJson(options=>
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowWepApp");

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
