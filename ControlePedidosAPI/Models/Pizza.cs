using System.ComponentModel.DataAnnotations;

namespace ControlePedidosAPI.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        public bool Disponivel { get; set; } = true;
    }
}
