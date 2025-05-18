
using Gymawy.Application;
using Gymawy.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services
    .AddInfrastructure(config)
    .AddApplication();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});



builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend", policy =>
//    {
//        policy.WithOrigins("http://localhost:3000") // your frontend URL
//              .AllowAnyHeader()
//              .AllowAnyMethod()
//              .AllowCredentials(); // important for cookies
//    });
//});

//app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
