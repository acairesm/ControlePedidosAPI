using System.ComponentModel.DataAnnotations;

namespace ControlePedidosAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        // Nome do cliente que fez o pedido — campo obrigatório
        [Required]
        public string ClienteNome { get; set; } = string.Empty;

        // Data e hora em que o pedido foi criado
        public DateTime DataPedido { get; set; } = DateTime.Now;

        // Todo pedido começa com status Pendente
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;

        // Valor total do pedido, calculado automaticamente com base nos itens
        public decimal Total { get; set; }

        // Lista de itens que compõem o pedido
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}