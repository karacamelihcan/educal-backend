using System.Reflection;
using Educal.Core.Dtos;
using Educal.Database;
using Educal.Database.Repositories.CustomerRepositories;
using Educal.Database.Repositories.InstructorRepositories;
using Educal.Database.Repositories.ManagerRepositories;
using Educal.Database.Repositories.RegistrarRepositories;
using Educal.Database.Repositories.StudentRepositories;
using Educal.Database.Repositories.TokenRepositories;
using Educal.Database.UnitOfWorks;
using Educal.Services.Services.AuthenticationServices;
using Educal.Services.Services.CustomerServices;
using Educal.Services.Services.InstructorServices;
using Educal.Services.Services.ManagerServices;
using Educal.Services.Services.RegistrarServices;
using Educal.Services.Services.SignServices;
using Educal.Services.Services.StudentServices;
using Educal.Services.Services.TokenServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// OptionsPattern for TokenOptions
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection(TokenOptions.OptionSectionName));


// Inversion of Control
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IRegistrarRepository, RegistrarRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IRegistrarService, RegistrarService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// DbContext
builder.Services.AddDbContext<EducalDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("EducalDb"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(EducalDbContext)).GetName().Name);
    });
});

//JwtAuthentication
var tokenOptions = builder.Configuration.GetSection(TokenOptions.OptionSectionName).Get<TokenOptions>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1",new OpenApiInfo{
        Version = "V1",
        Title = "Educal API",
        Description = "Main API Documantation of Educal API"
    });

    var securityScheme = new OpenApiSecurityScheme(){
        Description = "Please insert your JWT Token into field",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };
    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            new string[] {}
        }
    };

    options.AddSecurityDefinition("bearerAuth",securityScheme);
    options.AddSecurityRequirement(securityRequirement);
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpLogging();
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();

app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/V1/swagger.json","Main API Documantation of Educal API");
    options.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
