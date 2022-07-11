using MCQPuzzleGame.Context;
using MCQPuzzleGame.Exception;
using MCQPuzzleGame.Helpers;
using MCQPuzzleGame.Repositiories;
using MCQPuzzleGame.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCQPuzzleGame
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
            // Fetching connection from appsettings.json
            var connectstring = Configuration.GetConnectionString("Server");
            //EntityFramework
            services.AddDbContext<DbStoreContext>(options => options.UseSqlServer(connectstring));
            services.AddCors();
            services.AddControllers(option => option.Filters.Add(new LogAttribute()));// Add filter at global level
            services.AddControllers().AddJsonOptions(x=> x.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IJwtHelper, JwtHelpers>();
            services.AddTransient<IMcqQuestionRepo, McqQuestionsRepo>();
            //services.AddScoped<AuthCustomHandler>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var key = Encoding.UTF8.GetBytes(Configuration["JWT:key"]);
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //set clockskew to zero so token  expire exactly at token
                    //expiration time (instead of five minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });
            
            //services.AddCors(options =>
            //options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().
            //AllowAnyHeader().AllowAnyMethod()));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MCQPuzzleGame", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MCQPuzzleGame v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(x => x.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader()
            .AllowCredentials());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
