using Loans.Domain.Repositories;
using Loans.Helpers.Identity;
using Loans.Service.Calculations;
using Loans.Service.Calculations.CalculationPipeline;
using Loans.Service.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Loans.Database;
using Microsoft.EntityFrameworkCore;
using Loans.Domain.Entities.Identity;
using Loans.Domain.Repositories.Contracts;
using Loans.Helpers.CosmosDb;
using Loans.Helpers.Validation;
using Loans.Helpers.Validation.Contracts;
using Loans.Service.Calculations.CalculationPipeline.Contracts;
using Loans.Service.Calculations.Contracts;
using Loans.Service.Data.Contracts;
using Loans.Helpers.Identity.Contracts;
using Loans.Helpers.Response;
using Loans.Helpers.Response.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LoansDbContext>(option =>
{
    option.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnectionString"));
});

builder.Services.AddIdentityCore<User>(option => {
    option.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<LoansDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddLogging();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    SymmetricSecurityKey token = AuthenticationOptions.GetSymmetricSecurityKey();

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = token,

        ValidateIssuer = true,
        ValidIssuer = AuthenticationOptions.ISSUER,

        ValidateAudience = true,
        ValidAudience = AuthenticationOptions.AUDIENCE,

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
    options.SaveToken = true;
});


builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Lockout.AllowedForNewUsers = false;

    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    options.User.RequireUniqueEmail = true;
    options.Password.RequiredUniqueChars = 1;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
});

builder.Services.AddAuthorization(o => o.AddPolicy(
    "RequireAuthenticatedUserPolicy",
    builder => builder.RequireAuthenticatedUser()));

builder.Services.AddControllers();
builder.Services.AddScoped<ICosmosClientFactory, CosmosClientFactory>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<IValidatorBuilderFactory, ValidatorBuilderFactory>();
builder.Services.AddScoped<ICalculationMethodFactory, CalculationMethodFactory>();
builder.Services.AddScoped<IPaymentGraphGeneratorFactory, PaymentGraphGeneratorFactory>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IJwtTokenManager, JwtTokenManager>();
builder.Services.AddScoped<IHttpResponseModelFactory, HttpResponseModelFactory>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod()
                );

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
