-- Script SQL para criação das tabelas do banco de dados da Pizzaria
-- Banco: SQLite (pizzeria.db)

-- Tabela de categorias de pizza
CREATE TABLE IF NOT EXISTS Categories (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL
);

-- Tabela de pizzas do cardápio
CREATE TABLE IF NOT EXISTS Pizzas (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    Descricao TEXT,
    Preco REAL NOT NULL,
    Disponivel INTEGER NOT NULL DEFAULT 1,
    CategoryId INTEGER NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Tabela de pedidos dos clientes
CREATE TABLE IF NOT EXISTS Pedidos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ClienteNome TEXT NOT NULL,
    DataPedido TEXT NOT NULL,
    Status INTEGER NOT NULL DEFAULT 0,
    Total REAL NOT NULL DEFAULT 0
);

-- Tabela de itens de cada pedido
CREATE TABLE IF NOT EXISTS ItensPedido (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ProdutoNome TEXT NOT NULL,
    Quantidade INTEGER NOT NULL,
    PrecoUnitario REAL NOT NULL,
    PedidoId INTEGER NOT NULL,
    FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id)
);

-- Dados iniciais: categorias
INSERT INTO Categories (Id, Nome) VALUES (1, 'Tradicional');
INSERT INTO Categories (Id, Nome) VALUES (2, 'Especial');
INSERT INTO Categories (Id, Nome) VALUES (3, 'Doce');

-- Dados iniciais: pizzas
INSERT INTO Pizzas (Id, Nome, Descricao, Preco, Disponivel, CategoryId) VALUES (1, 'Margherita', 'Molho de tomate, mussarela e manjericão', 35.00, 1, 1);
INSERT INTO Pizzas (Id, Nome, Descricao, Preco, Disponivel, CategoryId) VALUES (2, 'Calabresa', 'Calabresa fatiada, cebola e azeitona', 38.00, 1, 1);
INSERT INTO Pizzas (Id, Nome, Descricao, Preco, Disponivel, CategoryId) VALUES (3, 'Quatro Queijos', 'Mussarela, provolone, parmesão e gorgonzola', 45.00, 1, 2);
INSERT INTO Pizzas (Id, Nome, Descricao, Preco, Disponivel, CategoryId) VALUES (4, 'Frango com Catupiry', 'Frango desfiado com catupiry cremoso', 42.00, 1, 2);
INSERT INTO Pizzas (Id, Nome, Descricao, Preco, Disponivel, CategoryId) VALUES (5, 'Chocolate com Morango', 'Chocolate ao leite com morangos frescos', 40.00, 1, 3);
