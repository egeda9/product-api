CREATE PROCEDURE [dbo].[UpdateProduct]
    @ProductId INT,
    @Name NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @Price DECIMAL(10, 2),
    @StockQuantity INT,
    @Manufacturer NVARCHAR(100),
    @Category NVARCHAR(50),
    @ReleaseDate DATE,
    @IsAvailable BIT
AS
BEGIN
    UPDATE Product
    SET
        Name = @Name,
        Description = @Description,
        Price = @Price,
        StockQuantity = @StockQuantity,
        Manufacturer = @Manufacturer,
        Category = @Category,
        ReleaseDate = @ReleaseDate,
        IsAvailable = @IsAvailable,
        UpdatedAt = GETDATE()
    WHERE
        Id = @ProductId;
END;
GO

