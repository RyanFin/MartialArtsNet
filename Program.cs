using Microsoft.EntityFrameworkCore;
using MartialArtsNet.Models;
using MartialArtsNet.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the Dependency Injection (DI) container.
builder.Services.Configure<MartialArtsDatabaseSettings>(builder.Configuration.GetSection("MartialArtsDatabase"));

// MovesService takes a direct dependency on MongoClient
builder.Services.AddSingleton<MovesService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<MoveSetContext>(opt => opt.UseInMemoryDatabase("MoveSetList"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
