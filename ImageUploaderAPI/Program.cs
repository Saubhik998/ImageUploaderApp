using ImageUploaderAPI.Models;
using ImageUploaderAPI.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Configures MongoDB settings from appsettings.json.
/// </summary>
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    
    if (string.IsNullOrEmpty(settings.ConnectionString) || string.IsNullOrEmpty(settings.DatabaseName))
    {
        throw new ArgumentNullException("MongoDB connection settings are missing.");
    }

    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

/// <summary>
/// Enables CORS to allow requests from the React frontend.
/// </summary>
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000") // Allow frontend URL
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});

/// <summary>
/// Registers necessary services.
/// </summary>
builder.Services.AddSingleton<ImageService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/// <summary>
/// Enables Swagger for API documentation and testing.
/// </summary>
app.UseSwagger();
app.UseSwaggerUI();

/// <summary>
/// Applies CORS policy before authorization middleware.
/// </summary>
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
