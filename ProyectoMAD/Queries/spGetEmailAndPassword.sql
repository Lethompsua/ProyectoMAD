USE DB_Proyecto
GO

CREATE OR ALTER PROCEDURE spGetEmailAndPassword
	@id SMALLINT,
	@email VARCHAR(50) OUTPUT,
	@password VARCHAR(50) OUTPUT
AS
BEGIN
	BEGIN TRY
		SELECT @email = email, @password = password
			FROM Usuarios
			WHERE id_usuario = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO