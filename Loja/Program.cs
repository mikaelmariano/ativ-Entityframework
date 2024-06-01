using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

var builder = WebApplication.CreateBuilder(args);

// Configurar a conexão com o BD
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options => 
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)))
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
