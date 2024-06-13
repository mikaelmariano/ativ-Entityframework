// using Microsoft.EntityFrameworkCore;
// using loja.data;
// using loja.models;
// using loja.Models;

// var builder = WebApplication.CreateBuilder(args);

// // Configurar a conexão com o BD
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<LojaDbContext>(options => 
//     options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)))
// );

// var app = builder.Build();

// app.UseHttpsRedirection();

// app.MapPost("/createproduto", async (LojaDbContext DbContext, Produto newProduto) =>
// {
//     DbContext.Produtos.Add(newProduto);
//     await DbContext.SaveChangesAsync();
//     return Results.Created($"/createproduto/{newProduto.Id}", newProduto);
//     });

// app.MapGet("/produtos", async(LojaDbContext dbContext) => 
// {
//     var produtos = await dbContext.Produtos.ToListAsync();
//     return Results.Ok(produtos);
//     });

// app.MapGet("/produtos/{id}", async(int id, LojaDbContext dbContext) => 
// {
//     var produtos = await dbContext.Produtos.FindAsync(id);
//     if (produtos == null)
//     {
//         return Results.NotFound($"Produto with ID {id} not found.");
//     }
//     return Results.Ok(produtos);
//     });

// // Endpoint para atualizar um Produto existente
// app.MapPut("/produtos/{id}", async (int id, LojaDbContext dbContext, Produto updatedProduto) =>
// {
//     // Verifica se o produto existe na base, conforme o id informado
//     // Se o produto existir na base, será retornado para dentro do objeto existingProduto
//     var existingProduto = await dbContext.Produtos.FindAsync(id);
//     if (existingProduto == null)
//     {
//         return Results.NotFound($"Produto with ID {id} not found.");
//     }

//     // Atualiza os dados do existingProduto
//     existingProduto.Nome = updatedProduto.Nome;
//     existingProduto.Preco = updatedProduto.Preco;
//     existingProduto.Fornecedor = updatedProduto.Fornecedor;

//     // Salva no banco de dados
//     await dbContext.SaveChangesAsync();

//     // Retorna para o cliente que invocou o endpoint
//     return Results.Ok(existingProduto);
// });

// app.MapPost("/createcliente", async (LojaDbContext DbContext, Cliente newCliente) =>
// {
//     DbContext.Clientes.Add(newCliente);
//     await DbContext.SaveChangesAsync();
//     return Results.Created($"/createcliente/{newCliente.Id}", newCliente);
//     });

// app.MapGet("/clientes", async(LojaDbContext dbContext) => 
// {
//     var clientes = await dbContext.Clientes.ToListAsync();
//     return Results.Ok(clientes);
//     });

// app.MapGet("/clientes/{id}", async(int id, LojaDbContext dbContext) => 
// {
//     var clientes = await dbContext.Clientes.FindAsync(id);
//     if (clientes == null)
//     {
//         return Results.NotFound($"Cliente with ID {id} not found.");
//     }
//     return Results.Ok(clientes);
//     });

//  // Endpoint para atualizar um Cliente existente
// app.MapPut("/clientes/{id}", async (int id, LojaDbContext dbContext, Cliente updatedCliente) =>
// {
//     // Verifica se o produto existe na base, conforme o id informado
//     // Se o produto existir na base, será retornado para dentro do objeto existingProduto
//     var existingCliente = await dbContext.Clientes.FindAsync(id);
//     if (existingCliente== null)
//     {
//         return Results.NotFound($"Cliente with ID {id} not found.");
//     }

//     // Atualiza os dados do existingProduto
//     existingCliente.Nome = updatedCliente.Nome;
//     existingCliente.Cpf = updatedCliente.Cpf;
//     existingCliente.Email = updatedCliente.Email;

//     // Salva no banco de dados
//     await dbContext.SaveChangesAsync();

//     // Retorna para o cliente que invocou o endpoint
//     return Results.Ok(existingCliente);
// });

// //Implementações Desafio 

// app.MapPost("/createfornecedor", async (LojaDbContext DbContext, Fornecedor newFornecedor) =>
// {
//     DbContext.Fornecedores.Add(newFornecedor);
//     await DbContext.SaveChangesAsync();
//     return Results.Created($"/createfornecedor/{newFornecedor.Id}", newFornecedor);
//     });
   
// app.MapGet("/fornecedores", async(LojaDbContext dbContext) => 
// {
//     var fornecedores = await dbContext.Fornecedores.ToListAsync();
//     return Results.Ok(fornecedores);
//     });

//  app.MapGet("/fornecedores/{id}", async(int id, LojaDbContext dbContext) => 
// {
//     var fornecedores = await dbContext.Fornecedores.FindAsync(id);
//     if (fornecedores == null)
//     {
//         return Results.NotFound($"Fornecedor with ID {id} not found.");
//     }
//     return Results.Ok(fornecedores);
//     });

//  // Endpoint para atualizar um Fornecedor existente
// app.MapPut("/fornecedor/{id}", async (int id, LojaDbContext dbContext, Fornecedor updatedFornecedor) =>
// {
//     // Verifica se o produto existe na base, conforme o id informado
//     // Se o produto existir na base, será retornado para dentro do objeto existingFornecedor
//     var existingFornecedor = await dbContext.Fornecedores.FindAsync(id);
//     if (existingFornecedor== null)
//     {
//         return Results.NotFound($"Cliente with ID {id} not found.");
//     }

//     // Atualiza os dados do existingFornecedor
//     existingFornecedor.Nome= updatedFornecedor.Nome;
//     existingFornecedor.Cnpj = updatedFornecedor.Cnpj;
//     existingFornecedor.Endereco = updatedFornecedor.Endereco;
//     existingFornecedor.Email = updatedFornecedor.Email;
//     existingFornecedor.Telefone = updatedFornecedor.Telefone;

//     // Salva no banco de dados
//     await dbContext.SaveChangesAsync();

//     // Retorna para o cliente que invocou o endpoint
//     return Results.Ok(existingFornecedor);
// });

// app.Run();


using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using loja.models;
using loja.services;
using loja.data; // Adicione essa linha
using System.Collections.Generic;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Adicione o DbContext ao contêiner de serviços
builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
    new MySqlServerVersion(new Version(8, 0, 26))));

// Adicione o ProductService ao contêiner de serviços
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// Configurar as requisições HTTP 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/produtos", async (ProductService productService) =>
{
    var produtos = await productService.GetAllProductsAsync();
    return Results.Ok(produtos);
});

app.MapGet("/produtos/{id}", async (int id, ProductService productService) =>
{
    var produto = await productService.GetProductByIdAsync(id);
    if (produto == null)
    {
        return Results.NotFound($"Product with ID {id} not found.");
    }
    return Results.Ok(produto);
});

app.MapPost("/produtos", async (Produto produto, ProductService productService) =>
{
    await productService.AddProductAsync(produto);
    return Results.Created($"/produtos/{produto.Id}", produto);
});

app.MapPut("/produtos/{id}", async (int id, Produto produto, ProductService productService) =>
{
    if (id != produto.Id)
    {
        return Results.BadRequest("Product ID mismatch.");
    }

    await productService.UpdateProductAsync(produto);
    return Results.Ok();
});

app.MapDelete("/produtos/{id}", async (int id, ProductService productService) =>
{
    await productService.DeleteProductAsync(id);
    return Results.Ok();
});

app.Run();
