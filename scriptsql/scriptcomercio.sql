USE [master]
GO
/****** Object:  Database [Comercio]    Script Date: 12/5/2024 09:54:18 ******/
CREATE DATABASE [Comercio]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Comercio', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Comercio.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Comercio_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Comercio_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Comercio] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Comercio].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Comercio] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Comercio] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Comercio] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Comercio] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Comercio] SET ARITHABORT OFF 
GO
ALTER DATABASE [Comercio] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Comercio] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Comercio] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Comercio] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Comercio] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Comercio] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Comercio] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Comercio] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Comercio] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Comercio] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Comercio] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Comercio] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Comercio] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Comercio] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Comercio] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Comercio] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Comercio] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Comercio] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Comercio] SET  MULTI_USER 
GO
ALTER DATABASE [Comercio] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Comercio] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Comercio] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Comercio] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Comercio] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Comercio] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Comercio] SET QUERY_STORE = ON
GO
ALTER DATABASE [Comercio] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Comercio]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 12/5/2024 09:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[id] [int] NOT NULL,
	[nombre] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 12/5/2024 09:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[id] [int] NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[precio] [decimal](10, 2) NOT NULL,
	[fecha_carga] [date] NOT NULL,
	[categoria_id] [int] NOT NULL,
 CONSTRAINT [PK__Producto__3213E83F564385AB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 12/5/2024 09:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id_usuario] [int] NOT NULL,
	[nombre_usuario] [nvarchar](50) NULL,
	[contraseña] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK__Productos__categ__403A8C7D] FOREIGN KEY([categoria_id])
REFERENCES [dbo].[Categorias] ([id])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK__Productos__categ__403A8C7D]
GO
/****** Object:  StoredProcedure [dbo].[spAutenticarUsuario]    Script Date: 12/5/2024 09:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spAutenticarUsuario]
    @nombre_usuario NVARCHAR(50),
    @contraseña NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @contraseña_almacenada NVARCHAR(100);

 
    SELECT @contraseña_almacenada = Contraseña
    FROM Usuarios
    WHERE nombre_usuario = @nombre_usuario;

 
    IF @contraseña_almacenada IS NOT NULL AND @contraseña_almacenada = @contraseña
    BEGIN
       
        SELECT 'Autenticado' AS Estado;
    END
    ELSE
    BEGIN
      
        SELECT 'Error de autenticación' AS Estado;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[spEliminarProducto]    Script Date: 12/5/2024 09:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spEliminarProducto]
	@IdProducto as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	Delete from Productos where Id = @IdProducto
END


GO
/****** Object:  StoredProcedure [dbo].[spInsertarProducto]    Script Date: 12/5/2024 09:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

	CREATE PROCEDURE [dbo].[spInsertarProducto]
	@Nombre as varchar (100),
    @Precio decimal(18, 2),
    @FechaCarga datetime,
    @CategoriaId int
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Productos (nombre,precio, fecha_carga, categoria_id)
    VALUES (@Nombre, @Precio, @FechaCarga, @CategoriaId);

    -- Obtener el ID del producto insertado
    DECLARE @ProductoId int;
    SET @ProductoId = SCOPE_IDENTITY();

    SELECT @ProductoId AS 'IdProductoInsertado';
END

GO
/****** Object:  StoredProcedure [dbo].[spModificarProducto]    Script Date: 12/5/2024 09:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spModificarProducto] 
    @Id AS INT,
    @Nombre AS VARCHAR(100),
    @Precio AS VARCHAR(100), 	
    @FechaCarga AS DATETIME, 
    @CategoriaId AS INT
 
AS
BEGIN
   
    SET NOCOUNT ON;

    UPDATE Productos 
    SET	    
        Nombre = @Nombre,
        precio = @Precio,
        fecha_carga= @FechaCarga,
		categoria_id = @CategoriaId
      
    WHERE 
        Id = @Id;

   
END
GO
/****** Object:  StoredProcedure [dbo].[spObtenerProductos]    Script Date: 12/5/2024 09:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObtenerProductos]
AS
BEGIN
   
    SELECT id, nombre, precio, fecha_carga, categoria_id
    FROM Productos;
END
GO
USE [master]
GO
ALTER DATABASE [Comercio] SET  READ_WRITE 
GO
