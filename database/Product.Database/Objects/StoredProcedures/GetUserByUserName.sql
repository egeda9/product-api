CREATE PROCEDURE [dbo].[GetUserByUserName]
    @UserName NVARCHAR(50)
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
		IsActive,
		PasswordHash
    FROM
        [User]
    WHERE
        Username = @UserName
		AND IsActive = 1
END;
GO

