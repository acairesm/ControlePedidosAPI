using Microsoft.AspNetCore.Mvc;
using ControlePedidosAPI.Models;
using ControlePedidosAPI.Repository;

namespace ControlePedidosAPI.Controllers
{
    // Esse controller gerencia os pedidos feitos pelos clientes
    [ApiController]
    [Route("api/pedido")]
    public class PedidoController : ControllerBase
    {
        // Repositório injetado pelo ASP.NET para acessar o banco de dados
        private readonly IPedidoRepository _pedidoRepository;

        // Construtor: recebe o repositório por injeção de dependência
        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        // GET api/pedido
        // Retorna todos os pedidos cadastrados, incluindo os itens de cada um
        [HttpGet]
        public IActionResult ListarTodos()
        {
            var pedidos = _pedidoRepository.GetAll();
            return Ok(pedidos);
        }

        // GET api/pedido/{id}
        // Busca um pedido específico pelo ID
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var pedido = _pedidoRepository.GetById(id);

            if (pedido == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return Ok(pedido);
        }

        // GET api/pedido/{id}/total
        // Endpoint extra: retorna o valor total calculado do pedido
        // O total é a soma de (quantidade * preço unitário) de cada item
        [HttpGet("{id}/total")]
        public IActionResult BuscarTotal(int id)
        {
            // Primeiro verifica se o pedido existe antes de calcular
            var pedido = _pedidoRepository.GetById(id);

            if (pedido == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            var total = _pedidoRepository.GetTotal(id);

            // Retorna um objeto com o id e o total para ficar mais claro na resposta
            return Ok(new { pedidoId = id, total });
        }

        // POST api/pedido
        // Cria um novo pedido — valida nome do cliente e a presença de itens
        [HttpPost]
        public IActionResult Criar([FromBody] Pedido pedido)
        {
            // Não faz sentido um pedido sem saber quem está pedindo
            if (string.IsNullOrWhiteSpace(pedido.ClienteNome))
                return BadRequest(new { mensagem = "O nome do cliente é obrigatório." });

            // Um pedido sem itens não tem utilidade
            if (pedido.Itens == null || pedido.Itens.Count == 0)
                return BadRequest(new { mensagem = "O pedido deve ter pelo menos um item." });

            try
            {
                var pedidoCriado = _pedidoRepository.Create(pedido);
                return CreatedAtAction(nameof(BuscarPorId), new { id = pedidoCriado!.Id }, pedidoCriado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // PATCH api/pedido/{id}/status
        // Atualiza apenas o status do pedido (ex: Pendente → Pago → Enviado)
        // Usamos PATCH porque estamos alterando só um campo, não o pedido inteiro
        [HttpPatch("{id}/status")]
        public IActionResult AtualizarStatus(int id, [FromBody] StatusPedido status)
        {
            // Valida se o valor recebido é um status válido do enum
            if (!Enum.IsDefined(typeof(StatusPedido), status))
                return BadRequest(new { mensagem = "Status inválido." });

            var pedidoAtualizado = _pedidoRepository.UpdateStatus(id, status);

            if (pedidoAtualizado == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return Ok(pedidoAtualizado);
        }

        // DELETE api/pedido/{id}
        // Remove um pedido do sistema
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var removido = _pedidoRepository.Delete(id);

            if (!removido)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return NoContent();
        }
    }
}
