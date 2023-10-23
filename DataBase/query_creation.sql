USE master
GO
 -- SI EXISTE LA BASE DE DATOS CON ESTE NOMBRE ENTONCES HAREMOS LO SIGUIENTE
IF EXISTS(SELECT * FROM DBO.SYSDATABASES WHERE NAME = 'LibreriaDB')
    BEGIN
		-- ELIMINAMOS LA BASE DE DATOS PORQ EXISTE
		DROP DATABASE LibreriaDB 
    END

GO
-- PROCEDEMOS A CREAR NUESTRA BASE DE DATOS
CREATE DATABASE LibreriaDB
GO

USE LibreriaDB
GO
-- PROCEDEMOS A CREAR NUESTRA TABLAS
Create Table [Editorial](
	[EditorialId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](150) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](12) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,

	CONSTRAINT [PK_Editorial_Primary] PRIMARY KEY CLUSTERED (EditorialId)
)

GO

CREATE TABLE [Book](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](80) NOT NULL,
	[Autor] [nvarchar](100) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[EditorialId] [int] NOT NULL,

	CONSTRAINT [PK_Book_Primary] PRIMARY KEY CLUSTERED (BookId),
	CONSTRAINT [PK_Editorial_Book] FOREIGN KEY (EditorialId) REFERENCES Editorial (EditorialId)
 )

 GO

 CREATE TABLE [Branch](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](150) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](12) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	CONSTRAINT [PK_Branch_Primary] PRIMARY KEY CLUSTERED (BranchId)
)

GO

CREATE TABLE [Inventary](
	[InventaryId] [int] IDENTITY(1,1) NOT NULL,
	[Stock] [decimal](18, 2) NOT NULL,
	[BookId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	CONSTRAINT [PK_Inventary_Primary] PRIMARY KEY CLUSTERED (InventaryId),
	CONSTRAINT [PK_Book_Inventary] FOREIGN KEY (BookId) REFERENCES Book (BookId),
	CONSTRAINT [PK_Branch_Inventary] FOREIGN KEY (BranchId) REFERENCES Branch (BranchId)
)