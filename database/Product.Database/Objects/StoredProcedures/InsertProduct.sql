CREATE PROCEDURE [dbo].[InsertProduct]
    @Name NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @Price DECIMAL(10, 2),
    @StockQuantity INT,
    @Manufacturer NVARCHAR(100),
    @Category NVARCHAR(50),
    @ReleaseDate DATE,
    @IsAvailable BIT,
    @NewProductId INT OUTPUT
AS
BEGIN
    INSERT INTO Product (
        Name, Description, Price, StockQuantity, Manufacturer, Category, ReleaseDate, IsAvailable, CreatedAt, IsActive
    )
    VALUES (
        @Name, @Description, @Price, @StockQuantity, @Manufacturer, @Category, @ReleaseDate, @IsAvailable, GETDATE(), 1
    );

	SET @NewProductId = SCOPE_IDENTITY();
END;
GO

