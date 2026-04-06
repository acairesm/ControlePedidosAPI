using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ControlePedidosAPI.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }

        [Required]
        public string ProdutoNome { get; set; } = string.Empty;

        // A quantidade deve ser no mínimo 1
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")]
        public int Quantidade { get; set; }

        // O preço unitário deve ser no mínimo R$ 0,01
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que 0.")]
        public decimal PrecoUnitario { get; set; }

        // Chave estrangeira — cada item pertence a um pedido
        [ForeignKey("Pedido")]
        public int PedidoId { get; set; }

        // Propriedade de navegação para o pedido
        // JsonIgnore evita que o pedido inteiro apareça dentro de cada item no JSON
        [JsonIgnore]
        public Pedido? Pedido { get; set; }
    }
}