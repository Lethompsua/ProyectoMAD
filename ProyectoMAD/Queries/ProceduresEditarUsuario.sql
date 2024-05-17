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
	@newPassword VARCHAR(50),
	@genero VARCHAR(15),
	@idioma VARCHAR(10),
	@tama�o SMALLINT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @currentPassword VARCHAR(50);
	DECLARE @oldPassword1 VARCHAR(50);
	DECLARE @oldPassword2 VARCHAR(50);

	BEGIN TRY
		SELECT @currentPassword = password
			FROM Usuarios
			WHERE id_usuario = @id;

		IF @currentPassword = @newPassword
		BEGIN
			UPDATE editUserView
				SET nombre_completo = @nombre,
					email = @email,
					genero = @genero,
					idioma = @idioma,
					tama�o_texto = @tama�o
				WHERE id_usuario = @id;
		END
		ELSE BEGIN
			SELECT TOP 2 @oldPassword1 = (SELECT password
											  FROM Contrase�asAntiguas
											  WHERE id_usuario = @id
											  ORDER BY id_contrase�a DESC --Entre mayor es el id, m�s reciente es la contrase�a
											  OFFSET 0 ROWS --No omite ninguna fila
											  FETCH NEXT 1 ROWS ONLY), --Solo devuelve la primera fila
                         @oldPassword2 = (SELECT password
											  FROM Contrase�asAntiguas
											  WHERE id_usuario = @id
											  ORDER BY id_contrase�a DESC 
											  OFFSET 1 ROWS --Omite la primera fila para poder obtener la segunda contrase�a
											  FETCH NEXT 1 ROWS ONLY);
			IF @newPassword = @oldPassword1 OR @newPassword = @oldPassword2
			BEGIN
				RAISERROR('La nueva contrase�a no puede ser igual a las �ltimas dos contrase�as', 16, 1);
				RETURN
			END

			UPDATE editUserView
				SET nombre_completo = @nombre,
					email = @email,
					password = @newPassword,
					genero = @genero,
					idioma = @idioma,
					tama�o_texto = @tama�o
				WHERE id_usuario = @id;

			INSERT INTO Contrase�asAntiguas (id_usuario, password)
			VALUES (@id, @currentPassword);
		END
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO