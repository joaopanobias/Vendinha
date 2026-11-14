IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'VendinhaDb')
BEGIN
    CREATE DATABASE VendinhaDb;
END
GO

USE VendinhaDb;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Clientes')
BEGIN
    CREATE TABLE Clientes (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        NomeCompleto VARCHAR(150) NOT NULL,
        Cpf VARCHAR(14) NOT NULL,
        DataNascimento DATE NOT NULL,
        Email VARCHAR(150) NOT NULL,
        Ativo BIT NOT NULL DEFAULT 1
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Dividas')
BEGIN
    CREATE TABLE Dividas (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ClienteId INT NOT NULL FOREIGN KEY REFERENCES Clientes(Id),
        Valor DECIMAL(18,2) NOT NULL,
        Paga BIT NOT NULL DEFAULT 0,
        DataCriacao DATETIME NOT NULL,
        DataPagamento DATETIME NULL
    );
END
GO