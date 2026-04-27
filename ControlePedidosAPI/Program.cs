using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ControlePedidosAPI.Data;
using ControlePedidosAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Registra os controllers e configura o JSON para ignorar referências circulares
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Evita erro de ciclo infinito entre Pizza → Category → Pizzas → Category...
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Configura o Entity Framework com SQLite usando a connection string do appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra os repositórios para injeção de dependência
// Scoped = uma instância por requisição HTTP
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

var app = builder.Build();

// Aplica as migrations pendentes ao iniciar a aplicação — cria o banco se não existir
// e atualiza o schema caso o modelo tenha mudado
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

// Mapeia os controllers para as rotas da API
app.MapControllers();

app.Run();
