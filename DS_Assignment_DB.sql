USE [master]
GO
/****** Object:  Database [DSI_Assignment_DB]    Script Date: 6/13/2024 12:33:22 AM ******/
CREATE DATABASE [DSI_Assignment_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DSI_Assignment_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DSI_Assignment_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DSI_Assignment_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DSI_Assignment_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DSI_Assignment_DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DSI_Assignment_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DSI_Assignment_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DSI_Assignment_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DSI_Assignment_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DSI_Assignment_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DSI_Assignment_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [DSI_Assignment_DB] SET  MULTI_USER 
GO
ALTER DATABASE [DSI_Assignment_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DSI_Assignment_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DSI_Assignment_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DSI_Assignment_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DSI_Assignment_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DSI_Assignment_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DSI_Assignment_DB', N'ON'
GO
ALTER DATABASE [DSI_Assignment_DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [DSI_Assignment_DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DSI_Assignment_DB]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[SupplierId] [int] NOT NULL,
	[RefID] [nvarchar](50) NOT NULL,
	[PoNo] [nvarchar](50) NOT NULL,
	[PoDate] [datetime2](7) NOT NULL,
	[ExpectedDate] [datetime2](7) NULL,
	[Remarks] [nvarchar](250) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Qty] [int] NOT NULL,
	[Rate] [float] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [varchar](150) NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Item] ON 

INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (1, N'Chips')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (2, N'Juice')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (3, N'Biscuits')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (4, N'Apple')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (5, N'Banana')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (6, N'Orange')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (7, N'Chocolate')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (8, N'Bread')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (9, N'Cake')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (10, N'Candy')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (11, N'Mango')
INSERT [dbo].[Item] ([ItemId], [ItemName]) VALUES (12, N'Fish')
SET IDENTITY_INSERT [dbo].[Item] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (11, 1, N'011', N'8521', CAST(N'2024-06-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (12, 2, N'012', N'8587', CAST(N'2024-06-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-20T00:00:00.0000000' AS DateTime2), N'Test data')
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (13, 4, N'013', N'789', CAST(N'2024-06-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-14T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (14, 5, N'014', N'36518', CAST(N'2024-06-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-27T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (15, 6, N'015', N'DS9654', CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (16, 3, N'016', N'T-965', CAST(N'2024-06-21T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-29T00:00:00.0000000' AS DateTime2), N'ok')
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (17, 2, N'017', N'8521', CAST(N'2024-06-20T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-20T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (18, 4, N'018', N'352', CAST(N'2024-06-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), N'oj')
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (20, 4, N'020', N'8525', CAST(N'2024-06-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-14T00:00:00.0000000' AS DateTime2), N'Testing')
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (21, 1, N'021', N'8521', CAST(N'2024-06-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (22, 3, N'022', N'T6548', CAST(N'2024-06-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (23, 5, N'023', N'6380', CAST(N'2024-06-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Order] ([OrderId], [SupplierId], [RefID], [PoNo], [PoDate], [ExpectedDate], [Remarks]) VALUES (24, 6, N'024', N'7410', CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-14T00:00:00.0000000' AS DateTime2), N'Test data')
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 

INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (17, 8, 16, 3, 2000)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (18, 3, 16, 3, 100)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (19, 3, 17, 3, 300)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (39, 7, 12, 2, 200)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (40, 3, 12, 1, 500)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (54, 4, 11, 2, 300)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (55, 8, 11, 2, 300)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (65, 9, 13, 5, 78)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (66, 10, 13, 87, 450)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (67, 5, 13, 70, 870)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (68, 8, 15, 7, 2000)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (69, 4, 15, 30, 2400)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (70, 9, 15, 8, 7000)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (71, 2, 15, 23, 4600)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (74, 4, 14, 6, 6005)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (75, 5, 14, 50, 452)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (79, 5, 22, 789, 200)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (80, 4, 22, 40, 50)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (81, 1, 22, 10, 50)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (86, 3, 18, 3, 2000)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (87, 2, 18, 3, 200)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (88, 5, 18, 60, 956)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (92, 7, 21, 2, 500)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (93, 4, 21, 50, 60)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (94, 7, 23, 50, 9340)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (95, 2, 23, 60, 630)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (96, 3, 20, 3, 300)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (97, 9, 20, 60, 890)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (98, 8, 20, 50, 9340)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ItemId], [OrderId], [Qty], [Rate]) VALUES (99, 4, 24, 50, 9340)
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Supplier] ON 

INSERT [dbo].[Supplier] ([SupplierId], [SupplierName]) VALUES (1, N'Pran Ltd')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName]) VALUES (2, N'ACI Ltd')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName]) VALUES (3, N'Bombay Sweets')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName]) VALUES (4, N'RFL Group')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName]) VALUES (5, N'Sabbir Traders')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName]) VALUES (6, N'RK PLC')
SET IDENTITY_INSERT [dbo].[Supplier] OFF
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Supplier]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Item] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetails_Item]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetails_Order]
GO
/****** Object:  StoredProcedure [dbo].[CreateOrderDetailProc]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[CreateOrderDetailProc]
    @ItemId INT,
    @OrderId INT,
    @Qty INT,
    @Rate FLOAT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[OrderDetail] (ItemId, OrderId, Qty, Rate)
    VALUES (@ItemId, @OrderId, @Qty, @Rate);

END;
GO
/****** Object:  StoredProcedure [dbo].[CreateOrderProc]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[CreateOrderProc]
    @SupplierId INT,
    @RefID NVARCHAR(50),
    @PoNo NVARCHAR(50),
    @PoDate DATETIME2,
    @ExpectedDate DATETIME2,
    @Remarks NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Order] (SupplierId, RefID, PoNo, PoDate, ExpectedDate, Remarks)
    VALUES (@SupplierId, @RefID, @PoNo, @PoDate, @ExpectedDate, @Remarks);

    SELECT SCOPE_IDENTITY() AS OrderId; -- Return the generated OrderId
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteOrderDetailsByOrderIdProc]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[DeleteOrderDetailsByOrderIdProc]
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [OrderDetail]  
    WHERE OrderId = @OrderId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteOrderProc]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteOrderProc]
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[Order]
    WHERE OrderId = @OrderId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GenerateOrderRefNoProc]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerateOrderRefNoProc]
AS
BEGIN
    DECLARE @refNo VARCHAR(50);
    DECLARE @totalRow INT = (isnull((SELECT top 1 OrderId FROM [Order] order by OrderId desc),0));

    IF @totalRow < 9 
        SET @refNo = '00' + CAST(@totalRow + 1 AS VARCHAR);
    ELSE IF @totalRow < 99
        SET @refNo = '0' + CAST(@totalRow + 1 AS VARCHAR);
    ELSE 
        SET @refNo = CAST(@totalRow + 1 AS VARCHAR);

    SELECT @refNo;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetOrderByIdProc]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetOrderByIdProc]
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT O.*, S.SupplierName 
    FROM [Order] O 
    INNER JOIN Supplier S ON O.SupplierId = S.SupplierId
    WHERE O.OrderId = @OrderId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetOrderDetailsByOrderIdProc]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetOrderDetailsByOrderIdProc]
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT OD.*, I.ItemName 
    FROM [OrderDetail] OD 
    INNER JOIN Item I ON OD.ItemId = I.ItemId
    WHERE OD.OrderId = @OrderId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersProc]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetOrdersProc]
    @PageSize INT,
    @PageNumber INT,
    @QueryString NVARCHAR(250) = NULL,
    @TotalRows INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Calculate the total number of rows that match the query string, or all rows if no query string is provided
    SELECT @TotalRows = COUNT(*)
    FROM [dbo].[Order] O
	INNER JOIN Supplier S ON O.SupplierId = S.SupplierId
    WHERE (@QueryString IS NULL OR RefID LIKE '%' + @QueryString + '%'
        OR PoNo LIKE '%' + @QueryString + '%'
        OR SupplierName LIKE '%' + @QueryString + '%');

    -- Select the rows for the specified page
    SELECT OrderId, RefID, PoNo, PoDate, ExpectedDate, SupplierName
    FROM (
        SELECT OrderId, RefID, PoNo, PoDate, ExpectedDate,SupplierName,
               ROW_NUMBER() OVER (ORDER BY OrderId desc) AS RowNum
        FROM [dbo].[Order] O
		INNER JOIN Supplier S ON O.SupplierId = S.SupplierId
        WHERE (@QueryString IS NULL OR RefID LIKE '%' + @QueryString + '%'
            OR PoNo LIKE '%' + @QueryString + '%'
            OR SupplierName LIKE '%' + @QueryString + '%')
    ) AS PagedOrders
    WHERE RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize
    ORDER BY OrderId desc;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderProc]    Script Date: 6/13/2024 12:33:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateOrderProc]
    @OrderId INT,
    @SupplierId INT,
    @RefID NVARCHAR(50),
    @PoNo NVARCHAR(50),
    @PoDate DATETIME2,
    @ExpectedDate DATETIME2,
    @Remarks NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Order]
    SET SupplierId = @SupplierId,
        RefID = @RefID,
        PoNo = @PoNo,
        PoDate = @PoDate,
        ExpectedDate = @ExpectedDate,
        Remarks = @Remarks
    WHERE OrderId = @OrderId;
END;
GO
USE [master]
GO
ALTER DATABASE [DSI_Assignment_DB] SET  READ_WRITE 
GO
