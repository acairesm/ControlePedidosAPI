using Microsoft.EntityFrameworkCore;
using ControlePedidosAPI.Data;
using ControlePedidosAPI.Models;

namespace ControlePedidosAPI.Repository
{
    // Implementação do repositório de pizzas usando Entity Framework Core com SQLite
    public class PizzaRepository : IPizzaRepository
    {
        // Contexto do banco de dados injetado pelo ASP.NET
        private readonly AppDbContext _context;

        // Construtor: recebe o contexto por injeção de dependência
        public PizzaRepository(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todas as pizzas do banco, incluindo a categoria de cada uma
        public IEnumerable<Pizza> GetAll()
        {
            return _context.Pizzas.Include(p => p.Categoria).ToList();
        }

        // Busca uma pizza pelo ID, incluindo a categoria
        public Pizza? GetById(int id)
        {
            return _context.Pizzas.Include(p => p.Categoria).FirstOrDefault(p => p.Id == id);
        }

        // Retorna apenas as pizzas disponíveis no cardápio
        public IEnumerable<Pizza> GetDisponiveis()
        {
            return _context.Pizzas.Include(p => p.Categoria).Where(p => p.Disponivel).ToList();
        }

        // Cadastra uma nova pizza no banco de dados
        public Pizza Create(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            _context.SaveChanges();
            return pizza;
        }

        // Atualiza os dados de uma pizza existente
        public Pizza? Update(int id, Pizza pizza)
        {
            var pizzaExistente = _context.Pizzas.Find(id);

            if (pizzaExistente == null)
                return null;

            // Atualiza os campos da pizza existente com os novos valores
            pizzaExistente.Nome = pizza.Nome;
            pizzaExistente.Descricao = pizza.Descricao;
            pizzaExistente.Preco = pizza.Preco;
            pizzaExistente.Disponivel = pizza.Disponivel;
            pizzaExistente.CategoriaId = pizza.CategoriaId;

            _context.SaveChanges();
            return pizzaExistente;
        }

        // Remove uma pizza do banco de dados
        public bool Delete(int id)
        {
            var pizza = _context.Pizzas.Find(id);

            if (pizza == null)
                return false;

            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
            return true;
        }
    }
}
