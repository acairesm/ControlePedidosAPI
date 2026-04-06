using Microsoft.EntityFrameworkCore;
using ControlePedidosAPI.Data;
using ControlePedidosAPI.Models;

namespace ControlePedidosAPI.Repository
{
    // Implementação do repositório de pedidos usando Entity Framework Core com SQLite
    public class PedidoRepository : IPedidoRepository
    {
        // Contexto do banco de dados injetado pelo ASP.NET
        private readonly AppDbContext _context;

        // Construtor: recebe o contexto por injeção de dependência
        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos os pedidos do banco, incluindo os itens de cada pedido
        public IEnumerable<Pedido> GetAll()
        {
            return _context.Pedidos.Include(p => p.Itens).ToList();
        }

        // Busca um pedido pelo ID, incluindo seus itens
        public Pedido? GetById(int id)
        {
            return _context.Pedidos.Include(p => p.Itens).FirstOrDefault(p => p.Id == id);
        }

        // Calcula o valor total do pedido somando (quantidade * preço unitário) de cada item
        public decimal GetTotal(int id)
        {
            var pedido = _context.Pedidos.Include(p => p.Itens).FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return 0;

            return pedido.Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
        }

        // Cria um novo pedido no banco de dados
        public Pedido Create(Pedido pedido)
        {
            // Calcula o total automaticamente com base nos itens
            pedido.Total = pedido.Itens.Sum(i => i.Quantidade * i.PrecoUnitario);

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
            return pedido;
        }

        // Atualiza apenas o status de um pedido existente
        public Pedido? UpdateStatus(int id, StatusPedido status)
        {
            var pedido = _context.Pedidos.Include(p => p.Itens).FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return null;

            pedido.Status = status;
            _context.SaveChanges();
            return pedido;
        }

        // Remove um pedido e seus itens do banco de dados
        public bool Delete(int id)
        {
            var pedido = _context.Pedidos.Include(p => p.Itens).FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return false;

            _context.Pedidos.Remove(pedido);
            _context.SaveChanges();
            return true;
        }
    }
}
