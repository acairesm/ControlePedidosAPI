using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlePedidosAPI.Models
{
    // Modelo que representa uma pizza do cardápio
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        // Nome da pizza — campo obrigatório
        public string Nome { get; set; } = string.Empty;

        // Descrição opcional da pizza (ingredientes, etc.)
        public string? Descricao { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        // Preço da pizza — deve ser maior que zero
        public decimal Preco { get; set; }

        // Indica se a pizza está disponível no cardápio
        public bool Disponivel { get; set; } = true;

        // Chave estrangeira para a categoria
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // Propriedade de navegação — referência à categoria da pizza
        public Category? Category { get; set; }
    }
}
