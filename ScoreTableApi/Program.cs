using Microsoft.OpenApi.Models;
using ScoreTableApi;
using ScoreTableApi.Configurations;
using ScoreTableApi.Data;
using ScoreTableApi.IRepository;
using ScoreTableApi.Repository;
using Serilog;

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
    builder.Services.ConfigureIdentity();
    builder.Services.ConfigureJwt(builder.Configuration);
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
        );
    });
    builder.Services.AddAutoMapper(typeof(MapperInitializer));
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
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

    app.UseHttpsRedirection();

    app.UseAuthentication();

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




