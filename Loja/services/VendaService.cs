using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class VendaService
    {
        private readonly LojaDbContext _dbContext;

        public VendaService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para consultar todas as vendas
        public async Task<List<Venda>> GetAllVendasAsync()
        {
            return await _dbContext.Vendas.Include(v => v.Cliente).Include(v => v.Produto).ToListAsync();
        }

        // Método para consultar uma venda a partir do seu Id
        public async Task<Venda> GetVendaByIdAsync(int id)
        {
            return await _dbContext.Vendas.Include(v => v.Cliente).Include(v => v.Produto).FirstOrDefaultAsync(v => v.Id == id);
        }

        // Método para gravar uma nova venda
        public async Task AddVendaAsync(Venda venda)
        {
            var cliente = await _dbContext.Clientes.FindAsync(venda.ClienteId);
            var produto = await _dbContext.Produtos.FindAsync(venda.ProdutoId);

            if (cliente == null || produto == null)
            {
                throw new KeyNotFoundException("Cliente ou Produto não encontrado.");
            }

            _dbContext.Vendas.Add(venda);
            await _dbContext.SaveChangesAsync();
        }

        // Método para atualizar os dados de uma venda
        public async Task UpdateVendaAsync(Venda venda)
        {
            _dbContext.Entry(venda).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Método para excluir uma venda
        public async Task DeleteVendaAsync(int id)
        {
            var venda = await _dbContext.Vendas.FindAsync(id);
            if (venda != null)
            {
                _dbContext.Vendas.Remove(venda);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Método para consultar vendas por produto (detalhada)
        public async Task<List<object>> GetVendasPorProdutoDetalhadaAsync(int produtoId)
        {
            var vendas = await _dbContext.Vendas
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .Where(v => v.ProdutoId == produtoId)
                .Select(v => new
                {
                    v.Id,
                    ProdutoNome = v.Produto.Nome,
                    v.DataVenda,
                    ClienteNome = v.Cliente.Nome,
                    v.QuantidadeVendida,
                    v.PrecoUnitario
                }).ToListAsync();

            return vendas.Cast<object>().ToList();
        }

        // Método para consultar vendas por produto (sumarizada)
        public async Task<object> GetVendasPorProdutoSumarizadaAsync(int produtoId)
        {
            var vendas = await _dbContext.Vendas
                .Where(v => v.ProdutoId == produtoId)
                .GroupBy(v => v.Produto.Nome)
                .Select(g => new
                {
                    ProdutoNome = g.Key,
                    QuantidadeTotal = g.Sum(v => v.QuantidadeVendida),
                    PrecoTotal = g.Sum(v => v.QuantidadeVendida * v.PrecoUnitario)
                }).FirstOrDefaultAsync();

            return vendas;
        }

        // Método para consultar vendas por cliente (detalhada)
        public async Task<List<object>> GetVendasPorClienteDetalhadaAsync(int clienteId)
        {
            var vendas = await _dbContext.Vendas
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .Where(v => v.ClienteId == clienteId)
                .Select(v => new
                {
                    v.Id,
                    ProdutoNome = v.Produto.Nome,
                    v.DataVenda,
                    v.QuantidadeVendida,
                    v.PrecoUnitario
                }).ToListAsync();

            return vendas.Cast<object>().ToList();
        }

        // Método para consultar vendas por cliente (sumarizada)
        public async Task<object> GetVendasPorClienteSumarizadaAsync(int clienteId)
        {
            var vendas = await _dbContext.Vendas
                .Where(v => v.ClienteId == clienteId)
                .GroupBy(v => v.Cliente.Nome)
                .Select(g => new
                {
                    ClienteNome = g.Key,
                    QuantidadeTotal = g.Sum(v => v.QuantidadeVendida),
                    PrecoTotal = g.Sum(v => v.QuantidadeVendida * v.PrecoUnitario)
                }).FirstOrDefaultAsync();

            return vendas;
        }
    }
}
