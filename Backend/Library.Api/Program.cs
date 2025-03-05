using Library.Api.Extensions;
using Library.Core.Settings;
using Library.Data.Extensions;
using Library.Service.Extensions;
using Library.Service.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<SuperAdminSettings>(builder.Configuration.GetSection("SuperAdmin"));

// Data Katmanı extension'ları
builder.Services.AddDatabaseExt(builder.Configuration);


//Servis katmanı extension'ları
builder.Services.AddServicesExt();


// Identity ve Jwt Ekleme
builder.Services.AddSecurityExt(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request Method: {context.Request.Method}");
    Console.WriteLine($"Request Path: {context.Request.Path}");
    Console.WriteLine($"Request QueryString: {context.Request.QueryString}");
    
    await next.Invoke();
    
    Console.WriteLine($"Response Status: {context.Response.StatusCode}");
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var authService = serviceProvider.GetRequiredService<AuthService>();
    await authService.EnsureSuperAdminAsync();
}

app.Run();