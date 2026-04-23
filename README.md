# ControlePedidosAPI

API para gerenciamento de pedidos de uma pizzaria. Permite cadastrar pizzas no cardápio, criar pedidos com itens e acompanhar o status de cada pedido.

## Tecnologias utilizadas

| Tecnologia | Versão |
|---|---|
| .NET | 10.0 |
| ASP.NET Core Web API | 10.0 |
| Entity Framework Core | 10.0.3 |
| SQLite | via EF Core (`Microsoft.EntityFrameworkCore.Sqlite` 10.0.3) |
| C# | 13 |

## Como rodar o projeto

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/ControlePedidosAPI.git
cd ControlePedidosAPI
```

2. Restaure as dependências:
```bash
dotnet restore
```

3. Rode a aplicação:
```bash
cd ControlePedidosAPI
dotnet run
```

4. A API vai rodar em:
   - HTTP: `http://localhost:5033`
   - HTTPS: `https://localhost:7119`

> O banco de dados SQLite (`pizzeria.db`) é criado automaticamente na primeira execução, já com dados iniciais de categorias e pizzas.

## Estrutura do projeto

```
ControlePedidosAPI/
├── ControlePedidosAPI.sln          # Arquivo de solução
├── schema.sql                      # Script SQL de referência do banco
├── README.md
└── ControlePedidosAPI/             # Projeto principal
    ├── ControlePedidosAPI.csproj   # Configuração do projeto e pacotes
    ├── Program.cs                  # Ponto de entrada da aplicação
    ├── appsettings.json            # Configurações (connection string, etc.)
    ├── pizzeria.db                 # Banco de dados SQLite
    ├── Controllers/                # Controllers da API (endpoints)
    │   ├── PizzaController.cs
    │   └── PedidoController.cs
    ├── Models/                     # Modelos/entidades do banco
    │   ├── Pizza.cs
    │   ├── Categoria.cs
    │   ├── Pedido.cs
    │   ├── ItemPedido.cs
    │   └── StatusPedido.cs         # Enum de status do pedido
    ├── Repository/                 # Repositórios (acesso ao banco)
    │   ├── IPizzaRepository.cs
    │   ├── PizzaRepository.cs
    │   ├── IPedidoRepository.cs
    │   └── PedidoRepository.cs
    ├── Data/                       # Contexto do Entity Framework
    │   └── AppDbContext.cs
    └── Properties/
        └── launchSettings.json
```

## Rotas da API (Endpoints)

### Pizza (`/api/pizza`)

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/pizza` | Lista todas as pizzas do cardápio |
| GET | `/api/pizza/{id}` | Busca uma pizza pelo ID |
| GET | `/api/pizza/disponiveis` | Lista apenas as pizzas disponíveis |
| POST | `/api/pizza` | Cadastra uma nova pizza |
| PUT | `/api/pizza/{id}` | Atualiza os dados de uma pizza |
| DELETE | `/api/pizza/{id}` | Remove uma pizza do cardápio |

**Exemplo de body para POST `/api/pizza`:**

O `id` **não deve ser enviado** — o banco gera automaticamente.
```json
{
  "nome": "Portuguesa",
  "descricao": "Presunto, ovo, cebola, azeitona e mussarela",
  "preco": 40.00,
  "disponivel": true,
  "categoriaId": 1
}
```

**Exemplo de body para PUT `/api/pizza/{id}`:**

O `id` vai na **URL**, não no body.
```json
{
  "nome": "Portuguesa",
  "descricao": "Presunto, ovo, cebola, azeitona e mussarela",
  "preco": 42.00,
  "disponivel": true,
  "categoriaId": 1
}
```

### Pedido (`/api/pedido`)

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/pedido` | Lista todos os pedidos com seus itens |
| GET | `/api/pedido/{id}` | Busca um pedido pelo ID |
| GET | `/api/pedido/{id}/total` | Retorna o valor total calculado do pedido |
| POST | `/api/pedido` | Cria um novo pedido com itens |
| PATCH | `/api/pedido/{id}/status` | Atualiza o status do pedido |
| DELETE | `/api/pedido/{id}` | Remove um pedido |

**Exemplo de body para POST `/api/pedido`:**

Você só precisa informar o `pizzaId` e a `quantidade`. O nome e o preço são preenchidos automaticamente com base no cadastro da pizza.
```json
{
  "clienteNome": "João Silva",
  "itens": [
    {
      "pizzaId": 1,
      "quantidade": 2
    },
    {
      "pizzaId": 3,
      "quantidade": 1
    }
  ]
}
```

> `produtoNome` e `precoUnitario` **não precisam ser enviados** — a API busca esses valores automaticamente pelo `pizzaId`.

Se quiser sobrescrever manualmente (não recomendado), pode enviar com todos os campos:
```json
{
  "clienteNome": "João Silva",
  "itens": [
    {
      "pizzaId": 1,
      "quantidade": 2,
      "produtoNome": "Margherita",
      "precoUnitario": 35.00
    }
  ]
}
```

**Exemplo de body para PATCH `/api/pedido/{id}/status`:**
```json
1
```
Os valores possíveis para o status são:
| Valor | Status | Descrição |
|---|---|---|
| 0 | Pendente | Pedido criado, aguardando pagamento |
| 1 | Pago | Pagamento confirmado |
| 2 | Enviado | Pedido saiu para entrega |
| 3 | Cancelado | Pedido cancelado |

## Banco de dados

O projeto usa **SQLite** como banco de dados. O arquivo do banco é o `pizzeria.db`, que fica dentro da pasta do projeto e é criado automaticamente ao rodar a aplicação.

### Tabelas

| Tabela | Descrição |
|---|---|
| `Pizzas` | Pizzas do cardápio (nome, descrição, preço, disponibilidade) |
| `Categorias` | Categorias de pizza (Tradicional, Especial, Doce) |
| `Pedidos` | Pedidos dos clientes (nome do cliente, data, status, total) |
| `ItensPedido` | Itens de cada pedido (produto, quantidade, preço unitário) |

### Relacionamentos

- Uma **Categoria** tem muitas **Pizzas**
- Um **Pedido** tem muitos **ItensPedido**

### Dados iniciais (seed)

O banco já vem com 3 categorias e 5 pizzas cadastradas automaticamente:

- **Categorias:** Tradicional, Especial, Doce
- **Pizzas:** Margherita, Calabresa, Quatro Queijos, Frango com Catupiry, Chocolate com Morango

## Status do projeto

Em desenvolvimento - Fase 1 (backend/API).

## Equipe

| Nome|
|---|
| [ Lorenzo Pedrozo ] |
| [ Aslam Rekik ] |
| [ André Caires ] |
