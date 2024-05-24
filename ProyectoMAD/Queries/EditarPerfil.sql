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
	@tamaño SMALLINT
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
					tamaño_texto = @tamaño
				WHERE id_usuario = @id;
		END
		ELSE BEGIN
			SELECT TOP 2 @oldPassword1 = (SELECT password
											  FROM ContraseñasAntiguas
											  WHERE id_usuario = @id
											  ORDER BY id_contraseña DESC --Entre mayor es el id, más reciente es la contraseña
											  OFFSET 0 ROWS --No omite ninguna fila
											  FETCH NEXT 1 ROWS ONLY), --Solo devuelve la primera fila
                         @oldPassword2 = (SELECT password
											  FROM ContraseñasAntiguas
											  WHERE id_usuario = @id
											  ORDER BY id_contraseña DESC 
											  OFFSET 1 ROWS --Omite la primera fila para poder obtener la segunda contraseña
											  FETCH NEXT 1 ROWS ONLY);
			IF @newPassword = @oldPassword1 OR @newPassword = @oldPassword2
			BEGIN
				RAISERROR('La nueva contraseña no puede ser igual a las últimas dos contraseñas', 16, 1);
				RETURN
			END

			UPDATE editUserView
				SET nombre_completo = @nombre,
					email = @email,
					password = @newPassword,
					genero = @genero,
					idioma = @idioma,
					tamaño_texto = @tamaño
				WHERE id_usuario = @id;

			INSERT INTO ContraseñasAntiguas (id_usuario, password)
			VALUES (@id, @currentPassword);
		END
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO