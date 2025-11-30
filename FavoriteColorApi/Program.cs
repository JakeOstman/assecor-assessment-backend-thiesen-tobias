using FavoriteColorApi.Data;
using FavoriteColorApi.Repositories;
using FavoriteColorApi.Services;
using FavoriteColorApi.Services.DataLoader;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PersonDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<CsvDataLoader>(provider =>
    new CsvDataLoader("Data/sample-input.csv"));
builder.Services.AddSingleton<IColorNameProvider, ColorNameProvider>();

builder.Services.AddScoped<IPersonRepository, CsvPersonRepository>();

builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddSingleton<IColorNameProvider, ColorNameProvider>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
