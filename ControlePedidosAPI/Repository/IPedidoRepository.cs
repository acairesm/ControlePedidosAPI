using ControlePedidosAPI.Models;

namespace ControlePedidosAPI.Repository
{
    // Interface do repositório de pedidos — define os métodos de acesso ao banco
    public interface IPedidoRepository
    {
        IEnumerable<Pedido> GetAll();
        Pedido? GetById(int id);
        decimal GetTotal(int id);
        Pedido? Create(Pedido pedido);
        Pedido? UpdateStatus(int id, StatusPedido status);
        bool Delete(int id);
    }
}
