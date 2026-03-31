using ControlePedidosAPI.Models;

namespace ControlePedidosAPI.Repository
{
    public interface IPizzaRepository
    {
        Task<IEnumerable<Pizza>> GetAllAsync();
        Task<Pizza?> GetByIdAsync(int id);
        Task<IEnumerable<Pizza>> GetDisponiveisAsync();
        Task<Pizza> CreateAsync(Pizza pizza);
        Task<Pizza?> UpdateAsync(int id, Pizza pizza);
        Task<bool> DeleteAsync(int id);
    }
}
