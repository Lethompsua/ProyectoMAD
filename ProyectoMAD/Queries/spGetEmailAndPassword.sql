USE DB_Proyecto
GO

CREATE OR ALTER VIEW lastUserView
AS
	SELECT id_usuario, email, password
		FROM Usuarios;
GO

CREATE OR ALTER PROCEDURE spGetEmailAndPassword
	@id SMALLINT,
	@email VARCHAR(50) OUTPUT,
	@password VARCHAR(50) OUTPUT
AS
BEGIN
	BEGIN TRY
		SELECT @email = email, @password = password
			FROM lastUserView
			WHERE id_usuario = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO