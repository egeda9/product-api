CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN
    SELECT
        Id,
        Username,
        Email,
        FirstName,
        LastName,
        CreatedAt,
        UpdatedAt,
		IsActive
    FROM
        [User]
    WHERE IsActive = 1
END;
GO

