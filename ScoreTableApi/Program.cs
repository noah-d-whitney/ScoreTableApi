using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ScoreTableApi;
using ScoreTableApi.Configurations;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;
using ScoreTableApi.Repository;
using ScoreTableApi.Services;
using Serilog;
using DatabaseContext = ScoreTableApi.Data.DatabaseContext;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting ScoreTable Server");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddDbContext<DatabaseContext>();
    builder.Services.AddAuthentication();
    builder.Services.Configure<CookieAuthenticationOptions>
        (IdentityConstants.ApplicationScheme, o => o.Cookie.SameSite = SameSiteMode.None );
    builder.Services.AddIdentityApiEndpoints<User>()
        .AddEntityFrameworkStores<DatabaseContext>();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
            builder.WithOrigins("http://localhost:3000")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
    builder.Services.AddAutoMapper(typeof(MapperInitializer));

    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IAuthManager, AuthManager>();
    builder.Services.AddScoped<IGameService, GameService>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = SecuritySchemeType.OAuth2.ToString(),
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
        options.SwaggerDoc("v1",
            new OpenApiInfo { Title = "ScoreTable", Version = "v1" });
    });
    builder.Services.AddControllers().AddNewtonsoftJson(options => options
        .SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapIdentityApi<User>();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseCors("AllowAll");

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "ScoreTable Server Terminated Unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}




