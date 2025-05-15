using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;
using System.Text.Json;
using TombolaTest.Data;
using TombolaTest.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = null
};
var json = await File.ReadAllTextAsync("AllTheBeans.json");
//var beans = JsonSerializer.Deserialize<List<CoffeeBean>>(json, options);
var beans = System.Text.Json.JsonSerializer.Deserialize<List<CoffeeBean>>(json);

builder.Services.AddDbContext<DataContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSeeding((context, _) =>
    {
        
        
        if (context.Set<CoffeeBean>().Any())
        {
            //load json into db here
            context.SaveChanges();
        }
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
