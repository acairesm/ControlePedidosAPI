using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlePedidosAPI.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }

        [Required]
        public string ProdutoNome { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")]
        public int Quantidade { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que 0.")]
        public decimal PrecoUnitario { get; set; }

        [ForeignKey("Pedido")]
        public int PedidoId { get; set; }

        public Pedido? Pedido { get; set; }
    }
}