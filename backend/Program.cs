var builder = WebApplication.CreateBuilder(args);

// CORS policy name
var AllowedOrigins = "_allowedOrigins";

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowedOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Content-Disposition"); // Expose content disposition header
        });
});

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add helper to scope
builder.Services.AddScoped<PHIRedaction.Interfaces.IUploadHelper, PHIRedaction.Helpers.UploadHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(AllowedOrigins);

app.MapControllers();

app.Run();
