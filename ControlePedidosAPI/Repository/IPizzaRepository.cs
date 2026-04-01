using ControlePedidosAPI.Models;

namespace ControlePedidosAPI.Repository
{
    // Interface do repositório de pizzas — define os métodos de acesso ao banco
    public interface IPizzaRepository
    {
        IEnumerable<Pizza> GetAll();
        Pizza? GetById(int id);
        IEnumerable<Pizza> GetDisponiveis();
        Pizza Create(Pizza pizza);
        Pizza? Update(int id, Pizza pizza);
        bool Delete(int id);
    }
}
