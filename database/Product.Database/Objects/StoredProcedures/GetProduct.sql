CREATE PROCEDURE [dbo].[GetProduct]
    @ProductId INT
AS
BEGIN
    SELECT
        Id,
        [Name],
        [Description],
        Price,
        StockQuantity,
        Manufacturer,
        Category,
        ReleaseDate,
        IsAvailable,
        CreatedAt,
        UpdatedAt,
		IsActive
    FROM
        Product
    WHERE
        Id = @ProductId
		AND IsActive = 1
END;
GO

