CREATE PROCEDURE [dbo].[GetUser]
    @UserId INT
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
    WHERE
        Id = @UserId
		AND IsActive = 1
END;
GO

