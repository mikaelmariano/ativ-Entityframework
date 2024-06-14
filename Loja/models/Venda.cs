using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loja.models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public string NumeroNotaFiscal { get; set; }
        
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
        
        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }
        
        public int QuantidadeVendida { get; set; }
        public double PrecoUnitario { get; set; }
    }
}
