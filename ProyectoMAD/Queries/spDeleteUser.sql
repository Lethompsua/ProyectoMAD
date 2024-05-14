USE DB_Proyecto
GO

CREATE OR ALTER PROCEDURE spDeleteUser
	@id SMALLINT
AS
BEGIN
	BEGIN TRY
		UPDATE Usuarios
			SET estatus = 0,
				fecha_baja = GETDATE()
			WHERE id_usuario = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO