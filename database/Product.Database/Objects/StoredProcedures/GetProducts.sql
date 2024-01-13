CREATE PROCEDURE [dbo].[GetProducts]
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
    WHERE IsActive = 1
END;
GO

