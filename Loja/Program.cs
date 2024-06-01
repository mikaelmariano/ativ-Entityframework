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

app.UseHttpsRedirection();

app.MapPost("/createproduto", async (LojaDbContext DbContext, Produto newProduto) =>
{
    DbContext.Produtos.Add(newProduto);
    await DbContext.SaveChangesAsync();
    return Results.Created($"/createproduto/{newProduto.Id}", newProduto);
    });

app.MapGet("/produtos", async(LojaDbContext dbContext) => 
{
    var produtos = await dbContext.Produtos.ToListAsync();
    return Results.Ok(produtos);
    });

app.MapGet("/produtos/{id}", async(int id, LojaDbContext dbContext) => 
{
    var produtos = await dbContext.Produtos.FindAsync(id);
    if (produtos == null)
    {
        return Results.NotFound($"Produto with ID {id} not found.");
    }
    return Results.Ok(produtos);
    });




app.Run();
