var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Add OpenAPI/Swagger generation (but not the UI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EtsyClient'ı DI container'a ekle
builder.Services.AddSingleton<MiuDigitalArt.Services.EtsyClient>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Enable OpenAPI doc generation (only JSON, no UI)
app.UseSwagger(); // ✅ Sadece JSON endpoint sağlar (swagger.json)


app.UseCors("AllowAll");

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

app.Run();