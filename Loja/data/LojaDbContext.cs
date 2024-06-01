using loja.models;
using loja.Models;
using Microsoft.EntityFrameworkCore;


namespace loja.data{
    public class LojaDbContext : DbContext{

        public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options){}
        public DbSet<Produto> Produtos {get;set;}
        public DbSet<Cliente> Clientes {get;set;}
        public DbSet<Fornecedor> Fornecedores {get;set;}
}
}