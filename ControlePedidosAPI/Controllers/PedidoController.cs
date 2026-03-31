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
        public async Task<IActionResult> ListarTodos()
        {
            var pedidos = await _pedidoRepository.GetAllAsync();
            return Ok(pedidos);
        }

        // GET api/pedido/{id}
        // Busca um pedido específico pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);

            if (pedido == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return Ok(pedido);
        }

        // GET api/pedido/{id}/total
        // Endpoint extra: retorna o valor total calculado do pedido
        // O total é a soma de (quantidade * preço unitário) de cada item
        [HttpGet("{id}/total")]
        public async Task<IActionResult> BuscarTotal(int id)
        {
            // Primeiro verifica se o pedido existe antes de calcular
            var pedido = await _pedidoRepository.GetByIdAsync(id);

            if (pedido == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            var total = await _pedidoRepository.GetTotalAsync(id);

            // Retorna um objeto com o id e o total para ficar mais claro na resposta
            return Ok(new { pedidoId = id, total });
        }

        // POST api/pedido
        // Cria um novo pedido — valida nome do cliente e a presença de itens
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Pedido pedido)
        {
            // Não faz sentido um pedido sem saber quem está pedindo
            if (string.IsNullOrWhiteSpace(pedido.ClienteNome))
                return BadRequest(new { mensagem = "O nome do cliente é obrigatório." });

            // Um pedido sem itens não tem utilidade
            if (pedido.Itens == null || pedido.Itens.Count == 0)
                return BadRequest(new { mensagem = "O pedido deve ter pelo menos um item." });

            var pedidoCriado = await _pedidoRepository.CreateAsync(pedido);

            // Retorna 201 Created com o caminho para acessar o pedido criado
            return CreatedAtAction(nameof(BuscarPorId), new { id = pedidoCriado.Id }, pedidoCriado);
        }

        // PATCH api/pedido/{id}/status
        // Atualiza apenas o status do pedido (ex: Pendente → Pago → Enviado)
        // Usamos PATCH porque estamos alterando só um campo, não o pedido inteiro
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] StatusPedido status)
        {
            var pedidoAtualizado = await _pedidoRepository.UpdateStatusAsync(id, status);

            if (pedidoAtualizado == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return Ok(pedidoAtualizado);
        }

        // DELETE api/pedido/{id}
        // Remove um pedido do sistema
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var removido = await _pedidoRepository.DeleteAsync(id);

            if (!removido)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return NoContent();
        }
    }
}
