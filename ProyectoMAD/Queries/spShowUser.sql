USE DB_Proyecto
GO

CREATE OR ALTER VIEW editUserView
AS
	SELECT id_usuario, nombre_completo, email, password, genero, idioma, tama�o_texto
		FROM Usuarios;
GO

CREATE OR ALTER PROCEDURE spShowUser
	@id SMALLINT,
	@nombre VARCHAR(50) OUTPUT,
	@email VARCHAR(50) OUTPUT,
	@password VARCHAR(50) OUTPUT,
	@genero VARCHAR(15) OUTPUT,
	@idioma VARCHAR(10) OUTPUT,
	@tama�o SMALLINT OUTPUT
AS
BEGIN
	BEGIN TRY
		SELECT @nombre = nombre_completo, @email = email, @password = password,
			@genero = genero, @idioma = idioma, @tama�o = tama�o_texto
			FROM editUserView
			WHERE id_usuario = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
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

CREATE OR ALTER PROCEDURE spUpdateUser
	@id SMALLINT,
	@nombre VARCHAR(50),
	@email VARCHAR(50),
	@password VARCHAR(50),
	@genero VARCHAR(15),
	@idioma VARCHAR(10),
	@tama�o SMALLINT
AS
BEGIN
	BEGIN TRY
		UPDATE editUserView
			SET nombre_completo = @nombre,
				email = @email,
				password = @password,
				genero = @genero,
				idioma = @idioma,
				tama�o_texto = @tama�o
			WHERE id_usuario = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

SELECT * FROM Usuarios;
UPDATE Usuarios SET estatus = 1 WHERE id_usuario = 2;