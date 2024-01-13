CREATE PROCEDURE [dbo].[DeleteProduct]
	@ProductId INT
AS
BEGIN
    UPDATE Product
    SET
        IsActive = 0
    WHERE
        Id = @ProductId;
END;
GO



