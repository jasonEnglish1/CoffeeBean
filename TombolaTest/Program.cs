using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Text.Json;
using TombolaTest.Data;
using TombolaTest.Jobs;
using TombolaTest.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = null
};

var json = File.ReadAllText("AllTheBeans.json");
var data = JsonSerializer.Deserialize<List<CoffeeBean>>(json);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});


var contextOptions = new DbContextOptionsBuilder<DataContext>()
    .Options;

using var context = new DataContext(contextOptions);
if (!context.CoffeeBeans.Any())
{
    context.CoffeeBeans.AddRange(data);
    context.SaveChanges();
}


// Configure the Quartz job scheduler
builder.Services.AddQuartz(options =>
{
    var jobkey = JobKey.Create("BeanOfTheDayJob");
    options.AddJob<BeanOfTheDayJob>(jobkey)
        .AddTrigger(trigger =>
        trigger
            .WithCronSchedule("0 0 * * * ?")
            .ForJob(jobkey)
            );
});


// Builds are finished before program is closed
builder.Services.AddQuartzHostedService(option =>
{
    option.WaitForJobsToComplete = true;
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
