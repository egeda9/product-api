CREATE PROCEDURE [dbo].[DeleteUser]
	@UserId INT
AS
BEGIN
    UPDATE [User]
    SET
        IsActive = 0
    WHERE
        Id = @UserId;
END;