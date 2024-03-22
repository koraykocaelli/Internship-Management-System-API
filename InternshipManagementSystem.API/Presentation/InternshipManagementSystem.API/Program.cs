using FluentValidation.AspNetCore;
using InternshipManagementSystem.Application;
using InternshipManagementSystem.Application.Validators.Advisor;
using InternshipManagementSystem.Application.Validators.Student;
using InternshipManagementSystem.Domain.Entities.Identity;
using InternshipManagementSystem.Infrastructure;
using InternshipManagementSystem.Infrastructure.Filters;
using InternshipManagementSystem.Persistence;
using InternshipManagementSystem.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:", "http://localhost:")));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateAdvisorValidator>())
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<UpdateAdvisorValidator>())
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateStudentValidator>())
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<UpdateStudentValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
    //.AddJsonOptions(options =>
    //{
    //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    //    options.JsonSerializerOptions.MaxDepth = 0; // Set the maximum depth to 0 to disable circular reference handling
    //});


builder.Services.AddInfrastuctureServices();
builder.Services.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistanceService();
builder.Services.AddAuthentication("Student")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))

        };

    });
builder.Services.AddAuthorization();
builder.Services.AddAuthenticationCore();
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    // Disable email verification
    options.SignIn.RequireConfirmedEmail = false;

    // Disable lockout
    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
})
    .AddEntityFrameworkStores<InternshipManagementSystemDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();
app.UseCookiePolicy(new CookiePolicyOptions
{
    Secure = CookieSecurePolicy.None, // Set to None for non-HTTPS requests
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None
    // Other cookie policy options...
}) ;
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
Console.WriteLine(app.Environment.IsDevelopment());
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
