using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ControlePedidosAPI.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }

        // Chave estrangeira para a pizza do cardápio
        [Required]
        [ForeignKey("Pizza")]
        public int PizzaId { get; set; }

        // Preenchido automaticamente com o nome da pizza ao criar o pedido
        public string ProdutoNome { get; set; } = string.Empty;

        // A quantidade deve ser no mínimo 1
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")]
        public int Quantidade { get; set; }

        // Preenchido automaticamente com o preço da pizza ao criar o pedido
        public decimal PrecoUnitario { get; set; }

        // Chave estrangeira — cada item pertence a um pedido
        [ForeignKey("Pedido")]
        public int PedidoId { get; set; }

        [JsonIgnore]
        public Pedido? Pedido { get; set; }

        [JsonIgnore]
        public Pizza? Pizza { get; set; }
    }
}