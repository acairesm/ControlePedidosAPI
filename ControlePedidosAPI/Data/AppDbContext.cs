using Microsoft.EntityFrameworkCore;
using ControlePedidosAPI.Models;

namespace ControlePedidosAPI.Data
{
    // Classe de contexto do Entity Framework — faz a ponte entre o código e o banco SQLite
    public class AppDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração (connection string, etc.)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tabela de pizzas no banco de dados
        public DbSet<Pizza> Pizzas { get; set; }

        // Tabela de categorias no banco de dados
        public DbSet<Categoria> Categorias { get; set; }

        // Tabela de pedidos no banco de dados
        public DbSet<Pedido> Pedidos { get; set; }

        // Tabela de itens de pedido no banco de dados
        public DbSet<ItemPedido> ItensPedido { get; set; }

        // Método chamado na criação do modelo — configura relacionamentos e dados iniciais
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do relacionamento: uma Categoria tem muitas Pizzas
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Pizzas)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);

            // Configuração do relacionamento: um Pedido tem muitos ItensPedido
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId);

            // Seed de categorias — dados iniciais para o banco
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Tradicional" },
                new Categoria { Id = 2, Nome = "Especial" },
                new Categoria { Id = 3, Nome = "Doce" }
            );

            // Seed de pizzas — dados iniciais para o banco
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza
                {
                    Id = 1,
                    Nome = "Margherita",
                    Descricao = "Molho de tomate, mussarela e manjericão",
                    Preco = 35.00m,
                    Disponivel = true,
                    CategoriaId = 1
                },
                new Pizza
                {
                    Id = 2,
                    Nome = "Calabresa",
                    Descricao = "Calabresa fatiada, cebola e azeitona",
                    Preco = 38.00m,
                    Disponivel = true,
                    CategoriaId = 1
                },
                new Pizza
                {
                    Id = 3,
                    Nome = "Quatro Queijos",
                    Descricao = "Mussarela, provolone, parmesão e gorgonzola",
                    Preco = 45.00m,
                    Disponivel = true,
                    CategoriaId = 2
                },
                new Pizza
                {
                    Id = 4,
                    Nome = "Frango com Catupiry",
                    Descricao = "Frango desfiado com catupiry cremoso",
                    Preco = 42.00m,
                    Disponivel = true,
                    CategoriaId = 2
                },
                new Pizza
                {
                    Id = 5,
                    Nome = "Chocolate com Morango",
                    Descricao = "Chocolate ao leite com morangos frescos",
                    Preco = 40.00m,
                    Disponivel = true,
                    CategoriaId = 3
                }
            );
        }
    }
}
