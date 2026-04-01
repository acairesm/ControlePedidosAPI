using Microsoft.AspNetCore.Mvc;
using ControlePedidosAPI.Models;
using ControlePedidosAPI.Repository;

namespace ControlePedidosAPI.Controllers
{
    // Esse controller cuida de tudo relacionado às pizzas do cardápio
    [ApiController]
    [Route("api/pizza")]
    public class PizzaController : ControllerBase
    {
        // O repositório é injetado pelo ASP.NET automaticamente via Program.cs
        private readonly IPizzaRepository _pizzaRepository;

        // Construtor: recebe o repositório por injeção de dependência
        public PizzaController(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        // GET api/pizza
        // Retorna a lista com todas as pizzas cadastradas no banco
        [HttpGet]
        public IActionResult ListarTodas()
        {
            var pizzas = _pizzaRepository.GetAll();
            return Ok(pizzas);
        }

        // GET api/pizza/{id}
        // Busca uma pizza pelo ID — se não existir, retorna 404
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var pizza = _pizzaRepository.GetById(id);

            if (pizza == null)
                return NotFound(new { mensagem = "Pizza não encontrada." });

            return Ok(pizza);
        }

        // GET api/pizza/disponiveis
        // Retorna apenas as pizzas com Disponivel = true
        [HttpGet("disponiveis")]
        public IActionResult ListarDisponiveis()
        {
            var pizzas = _pizzaRepository.GetDisponiveis();
            return Ok(pizzas);
        }

        // POST api/pizza
        // Cadastra uma nova pizza — valida nome e preço antes de salvar
        [HttpPost]
        public IActionResult Criar([FromBody] Pizza pizza)
        {
            // Valida se o nome foi informado
            if (string.IsNullOrWhiteSpace(pizza.Nome))
                return BadRequest(new { mensagem = "O nome da pizza é obrigatório." });

            // Valida se o preço faz sentido (não pode ser zero ou negativo)
            if (pizza.Preco <= 0)
                return BadRequest(new { mensagem = "O preço deve ser maior que zero." });

            var pizzaCriada = _pizzaRepository.Create(pizza);

            // Retorna 201 Created com o endereço do novo recurso no cabeçalho
            return CreatedAtAction(nameof(BuscarPorId), new { id = pizzaCriada.Id }, pizzaCriada);
        }

        // PUT api/pizza/{id}
        // Atualiza todos os dados de uma pizza existente
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Pizza pizza)
        {
            // Mesmas validações do cadastro
            if (string.IsNullOrWhiteSpace(pizza.Nome))
                return BadRequest(new { mensagem = "O nome da pizza é obrigatório." });

            if (pizza.Preco <= 0)
                return BadRequest(new { mensagem = "O preço deve ser maior que zero." });

            var pizzaAtualizada = _pizzaRepository.Update(id, pizza);

            // Se o repositório retornou null, é porque o ID não existe
            if (pizzaAtualizada == null)
                return NotFound(new { mensagem = "Pizza não encontrada." });

            return Ok(pizzaAtualizada);
        }

        // DELETE api/pizza/{id}
        // Remove uma pizza do cardápio
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var removida = _pizzaRepository.Delete(id);

            if (!removida)
                return NotFound(new { mensagem = "Pizza não encontrada." });

            // 204 NoContent: deu certo, mas não há nada para retornar
            return NoContent();
        }
    }
}
