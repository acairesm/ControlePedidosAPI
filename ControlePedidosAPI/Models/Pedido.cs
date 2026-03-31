using System.ComponentModel.DataAnnotations;

namespace ControlePedidosAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required]
        // Faz a validação para garantir que o campo seja obrigatório

        public string ClienteNome { get; set; } = string.Empty;
       // string.Empty inicializa a string vazia para evitar valor nulo


        public DateTime DataPedido { get; set; } = DateTime.Now;
        // DateTime é o tipo usado para armazenar data e hora
        // DateTime.Now pega a data e hora atuais no momento da criação do pedido

        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
        // StatusPedido é um enum com os possíveis status do pedido
        // StatusPedido.Pendente define que todo pedido começa como pendente
        public decimal Total { get; set; }

        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
        // List<ItemPedido> representa uma lista de itens do pedido
        // new List<ItemPedido>() cria a lista vazia para não dar erro e permitir adicionar itens depois
    }
}