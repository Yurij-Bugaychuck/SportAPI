using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using SportAPI.Services;
using SportAPI.Middlewares;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using SportAPI.Models;

namespace SportAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "localhost_origin";
        
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SportContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    options =>
                    {
                        options.RequireHttpsMetadata = false;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = false,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true
                        };
                    });
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: this.MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            services
                .AddCustomServices()
                .AddControllers()
                .AddNewtonsoftJson(
                    o =>
                    {
                        o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        o.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
                    });
            
            services
                .AddSwaggerGen(
                    options =>
                    {
                        var jwtSecurityScheme = new OpenApiSecurityScheme
                        {
                            Scheme = "bearer",
                            BearerFormat = "JWT",
                            Name = "Authentication",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.Http,
                            Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                            Reference = new OpenApiReference
                            {
                                Id = JwtBearerDefaults.AuthenticationScheme,
                                Type = ReferenceType.SecurityScheme
                            }
                        };

                        options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            { jwtSecurityScheme, new[]
                            {
                                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdDRAZ20udWEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJ1c2VyIiwibmJmIjoxNjM4NjIzMzA4LCJleHAiOjE2Mzg2ODMzMDgsImlzcyI6IlNwb3J0U2VydmVyIiwiYXVkIjoiU3BvcnRQQUlDbGllbnQifQ.IoPb3rGLAwKsF-cNqVgq1GE6JB4mu81NVKMhpgV-SKA"
                            } }
                        });
                    });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            
            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseMiddleware<ErrorHandlerMiddleware>()
                .UseAuthentication()
                .UseAuthorization()
                .UseStaticFiles()
                .UseSwagger()
                .UseSwaggerUI()
                .UseEndpoints(
                    endpoints =>
                    {
                        endpoints.MapControllers();
                    });
        }
    }
}
