using System.ComponentModel.DataAnnotations;

namespace ControlePedidosAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required]
        public string ClienteNome { get; set; } = string.Empty;

        public DateTime DataPedido { get; set; } = DateTime.Now;

        public StatusPedido Status { get; set; } = StatusPedido.Pendente;

        public decimal Total { get; set; }

        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}