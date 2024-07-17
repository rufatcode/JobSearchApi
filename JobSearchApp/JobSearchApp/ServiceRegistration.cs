using System;
using Data;
using Data.DAL;
using Domain.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Helpers;
using Service.Helpers.Interfaces;
using System.Text;
using Service.Validators.PositionValidators;
using Service.Profiles;
using Service.Services.Interfaces;
using Service.Services;
using MediatR;

namespace JobSearchApp
{
	public static class ServiceRegistration
	{
        public static void Registration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
               .AddFluentValidation(d => d.RegisterValidatorsFromAssemblyContaining<PostPositionValidator>())
               .AddNewtonsoftJson(opt => {
                   opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
               });
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(orign => true);
                    policy.WithOrigins("http://localhost:8080", "https://mypathacademy.com");
                });
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379");
            services.AddDbContext<DataContext>(context =>
            {
                context.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    opt =>

                        opt.MigrationsAssembly("Data")
                );

            });
            services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = true;
                option.SignIn.RequireConfirmedPhoneNumber = false;
                option.SignIn.RequireConfirmedAccount = true;
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Lockout.AllowedForNewUsers = true;
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddAuthorization();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "JobSearch app", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });
            services.Configure<DataProtectionTokenProviderOptions>(option =>
            {
                option.TokenLifespan = TimeSpan.FromMinutes(10);

            });
            //services.AddMediatR(typeof(Program).Assembly);
            services.AddAutoMapper(typeof(AdvertaismetProfile).Assembly);
            services.AddScoped<ISendEmail, SendEmail>();
            services.AddScoped<UrlHelperService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddHttpContextAccessor();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.AddScoped<IFileService, FileService>();

            services.AddScoped<IAdvertaismetService, AdvertaismetService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IEmploymentTypeService, EmploymentTypeService>();
            services.AddScoped<IJobInformationService, JobInformationService>();
            services.AddScoped<IJobInformationTypeService, JobInformationTypeService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IPhoneNumberHeadlingService, PhoneNumberHeadlingService>();
            services.AddScoped<IPhoneNumberService, PhoneNumberService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IWishListService, WishListService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();


            //services.AddHostedService<UserCleanUpService>();
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
        }
    }
}

