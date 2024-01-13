CREATE PROCEDURE [dbo].[InsertUser]
    @Username NVARCHAR(50),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255),
    @PasswordSalt NVARCHAR(255),
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @NewUserId INT OUTPUT
AS
BEGIN
    INSERT INTO [User] (
        Username, Email, PasswordHash, PasswordSalt, FirstName, LastName, CreatedAt, IsActive
    )
    VALUES (
        @Username, @Email, @PasswordHash, @PasswordSalt, @FirstName, @LastName, GETDATE(), 1
    );

	SET @NewUserId = SCOPE_IDENTITY();
END;
GO

