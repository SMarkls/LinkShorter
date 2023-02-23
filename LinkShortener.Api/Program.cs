using LinkShortener.Api.Data.DataBase;
using LinkShortener.Api.Services.Implementations;
using LinkShortener.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescs => apiDescs.First());
});
builder.Services.AddDbContext<ApiDbContext>(ops =>
    ops.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services
    .AddTransient<IShortenService, ShortenService>()
    .AddTransient<IAccountService, AccountService>()
    .AddTransient<IHashCalculator, HashCalculator>();

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