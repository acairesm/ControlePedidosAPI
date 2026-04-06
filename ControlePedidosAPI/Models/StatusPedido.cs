namespace ControlePedidosAPI.Models
{
    // Enum que representa os possíveis estados de um pedido no sistema
    public enum StatusPedido
    {
        Pendente,   // Pedido criado, aguardando pagamento
        Pago,       // Pagamento confirmado
        Enviado,    // Pedido saiu para entrega
        Cancelado   // Pedido cancelado pelo cliente ou sistema
    }
}