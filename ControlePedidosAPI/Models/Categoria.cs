using System.ComponentModel.DataAnnotations;

namespace ControlePedidosAPI.Models
{
    // Modelo que representa uma categoria de pizza (ex: Tradicional, Especial, Doce)
    public class Categoria
    {
        public int Id { get; set; }

        // Nome da categoria — campo obrigatório
        [Required]
        public string Nome { get; set; } = string.Empty;

        // Lista de pizzas que pertencem a esta categoria
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
    }
}
