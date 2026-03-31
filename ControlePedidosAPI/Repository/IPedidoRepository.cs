using ControlePedidosAPI.Models;

namespace ControlePedidosAPI.Repository
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task<Pedido?> GetByIdAsync(int id);
        Task<decimal> GetTotalAsync(int id);
        Task<Pedido> CreateAsync(Pedido pedido);
        Task<Pedido?> UpdateStatusAsync(int id, StatusPedido status);
        Task<bool> DeleteAsync(int id);
    }
}
