using Microsoft.EntityFrameworkCore;
using Walks.Api.Data;
using Walks.Api.Mapping;
using Walks.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Injecting db context.
builder.Services.AddDbContext<WalksDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("WalksDbConnection")));

builder.Services.AddScoped<IRegionRepository, RegionRepository>();

//Inject the Automapper.
builder.Services.AddAutoMapper(typeof(AutomapperProfiles)); 

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
