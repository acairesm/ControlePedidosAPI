using Microsoft.AspNetCore.Mvc;
using ControlePedidosAPI.Models;
using ControlePedidosAPI.Repository;

namespace ControlePedidosAPI.Controllers
{
    [ApiController]
    [Route("api/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _repository;

        public PedidoController(IPedidoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _repository.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _repository.GetByIdAsync(id);
            if (pedido == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return Ok(pedido);
        }

        [HttpGet("{id}/total")]
        public async Task<IActionResult> GetTotal(int id)
        {
            var pedido = await _repository.GetByIdAsync(id);
            if (pedido == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            var total = await _repository.GetTotalAsync(id);
            return Ok(new { total });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pedido pedido)
        {
            if (string.IsNullOrWhiteSpace(pedido.ClienteNome))
                return BadRequest(new { mensagem = "O nome do cliente é obrigatório." });

            if (pedido.Itens == null || pedido.Itens.Count == 0)
                return BadRequest(new { mensagem = "O pedido deve conter ao menos um item." });

            var criado = await _repository.CreateAsync(pedido);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusPedido status)
        {
            var atualizado = await _repository.UpdateStatusAsync(id, status);
            if (atualizado == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removido = await _repository.DeleteAsync(id);
            if (!removido)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return NoContent();
        }
    }
}
