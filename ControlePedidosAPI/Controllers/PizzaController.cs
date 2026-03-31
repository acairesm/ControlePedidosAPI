using Microsoft.AspNetCore.Mvc;
using ControlePedidosAPI.Models;
using ControlePedidosAPI.Repository;

namespace ControlePedidosAPI.Controllers
{
    [ApiController]
    [Route("api/pizza")]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaRepository _repository;

        public PizzaController(IPizzaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pizzas = await _repository.GetAllAsync();
            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pizza = await _repository.GetByIdAsync(id);
            if (pizza == null)
                return NotFound(new { mensagem = "Pizza não encontrada." });

            return Ok(pizza);
        }

        [HttpGet("disponiveis")]
        public async Task<IActionResult> GetDisponiveis()
        {
            var pizzas = await _repository.GetDisponiveisAsync();
            return Ok(pizzas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pizza pizza)
        {
            if (string.IsNullOrWhiteSpace(pizza.Nome))
                return BadRequest(new { mensagem = "O nome da pizza é obrigatório." });

            if (pizza.Preco <= 0)
                return BadRequest(new { mensagem = "O preço deve ser maior que zero." });

            var criada = await _repository.CreateAsync(pizza);
            return CreatedAtAction(nameof(GetById), new { id = criada.Id }, criada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pizza pizza)
        {
            if (string.IsNullOrWhiteSpace(pizza.Nome))
                return BadRequest(new { mensagem = "O nome da pizza é obrigatório." });

            if (pizza.Preco <= 0)
                return BadRequest(new { mensagem = "O preço deve ser maior que zero." });

            var atualizada = await _repository.UpdateAsync(id, pizza);
            if (atualizada == null)
                return NotFound(new { mensagem = "Pizza não encontrada." });

            return Ok(atualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removida = await _repository.DeleteAsync(id);
            if (!removida)
                return NotFound(new { mensagem = "Pizza não encontrada." });

            return NoContent();
        }
    }
}
