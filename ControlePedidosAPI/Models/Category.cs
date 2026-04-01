using System.ComponentModel.DataAnnotations;

namespace ControlePedidosAPI.Models
{
    // Modelo que representa uma categoria de pizza (ex: Tradicional, Especial, Doce)
    public class Category
    {
        public int Id { get; set; }

        [Required]
        // Nome da categoria — campo obrigatório
        public string Nome { get; set; } = string.Empty;

        // Lista de pizzas que pertencem a esta categoria
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
    }
}
