using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportAPI.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SportAPI.Services;
using SportAPI.Middlewares;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace SportAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SportContext>(options => options
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(connection));

            //var connectionString = new SqlConnectionStringBuilder()
            //{
            //    DataSource = Environment.GetEnvironmentVariable("DB_HOST"),   
            //    // Set Host to 'cloudsql' when deploying to App Engine Flexible environment
            //    UserID = Environment.GetEnvironmentVariable("DB_USER"),        
            //    Password = Environment.GetEnvironmentVariable("DB_PASS"),      
            //    InitialCatalog = Environment.GetEnvironmentVariable("DB_NAME"), 

            //    Encrypt = false,
            //};
            //connectionString.Pooling = true;

            //string connection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<SportContext>(options => options.UseSqlServer(connectionString.ConnectionString));


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = false,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };  
                    });

           
            services.AddCustomServices();
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });






        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();




            app.UseRouting();
            app.UseMiddleware<ErrorHandlerMiddleware>();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();







            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    
}
