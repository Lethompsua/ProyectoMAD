USE DB_Proyecto
GO

CREATE OR ALTER VIEW editUserView
AS
	SELECT id_usuario, nombre_completo, email, password, genero, idioma, tamaño_texto
		FROM Usuarios;
GO

CREATE OR ALTER PROCEDURE spShowUser
	@id SMALLINT,
	@nombre VARCHAR(50) OUTPUT,
	@email VARCHAR(50) OUTPUT,
	@password VARCHAR(50) OUTPUT,
	@genero VARCHAR(15) OUTPUT,
	@idioma VARCHAR(10) OUTPUT,
	@tamaño SMALLINT OUTPUT
AS
BEGIN
	BEGIN TRY
		SELECT @nombre = nombre_completo, @email = email, @password = password,
			@genero = genero, @idioma = idioma, @tamaño = tamaño_texto
			FROM editUserView
			WHERE id_usuario = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END