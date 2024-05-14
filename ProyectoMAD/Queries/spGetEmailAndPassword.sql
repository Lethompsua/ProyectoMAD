USE DB_Proyecto
GO

CREATE OR ALTER PROCEDURE GetEmailAndPassword
	@id SMALLINT,
	@email VARCHAR(50) OUTPUT,
	@password VARCHAR(50) OUTPUT
AS
BEGIN
	SELECT @email = COUNT(id_usuario), @password = COUNT(id_usuario)
		FROM Usuarios
		WHERE id_usuario = @id;
END
GO