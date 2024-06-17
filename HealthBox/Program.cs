using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQLiteConnecn;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles(); // Enable serving static files

// Define the path to the SQLite database.
string dbPath = "/Users/loveone/Desktop/HealthBox/HealthBoxDB.db"; 
var connect = new Connect(dbPath);

app.MapPost("/users", async (User user) =>
{
    connect.InsertUser(user);
    return Results.Ok("User inserted successfully.");
})
.WithName("InsertUser")
.WithOpenApi();

app.Run();
