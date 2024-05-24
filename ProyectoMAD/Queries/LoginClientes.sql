USE DB_Proyecto;
GO

CREATE OR ALTER PROCEDURE VerificarLogin
    @Email VARCHAR(50),
    @Password VARCHAR(50),
	@id SMALLINT OUTPUT,
	@usuarioDesactivado BIT OUTPUT,
	@usuarioActivo BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UsuarioExiste BIT;
    DECLARE @IntentosFallidos INT;
	DECLARE @ContraseñaCorrecta BIT;

	SET @id = 0;
	SET @usuarioDesactivado = 0;
	SET @usuarioActivo = 1;

	SELECT @UsuarioExiste = COUNT(id_usuario)
	FROM Usuarios
	WHERE email = @Email;

	IF @UsuarioExiste = 1
	BEGIN
		SELECT @IntentosFallidos = intentos
	    FROM Usuarios
		WHERE email = @email;

		IF @IntentosFallidos < 3
		BEGIN
			SELECT @ContraseñaCorrecta = COUNT(id_usuario)
			FROM Usuarios
			WHERE email = @Email AND password = @Password;

			IF @ContraseñaCorrecta = 1
			BEGIN
				UPDATE Usuarios
				SET intentos = 0
				WHERE email = @Email;

				SELECT @UsuarioActivo = estatus
					FROM Usuarios
					WHERE email = @Email;

				SET @id = dbo.GetUser(@Email);

				IF @UsuarioActivo = 0
				BEGIN
					PRINT('El usuario se encuentra dado de baja');
					RETURN;
				END

				PRINT('Login exitoso');
				RETURN;
			END
			ELSE
			BEGIN
				UPDATE Usuarios
					SET intentos = intentos + 1
					WHERE email = @Email;
				SET @IntentosFallidos = @IntentosFallidos + 1;

				IF @IntentosFallidos = 3
				BEGIN
					UPDATE Usuarios
						SET habilitado = 0
						WHERE email = @Email;

					SET @id = dbo.GetUser(@Email);
					SET @usuarioDesactivado = 1;
					PRINT(CONCAT('El usuario ha sido desactivado', @usuarioDesactivado));
				END
				ELSE
				BEGIN
					RAISERROR('La contraseña ingresada es incorrecta', 16, 1);
				END

				SET @usuarioActivo = 1;
				RETURN
			END
		END
		ELSE
		BEGIN
			SELECT @ContraseñaCorrecta = COUNT(id_usuario)
				FROM Usuarios
				WHERE email = @Email AND contraseña_temporal = @Password;

			IF @ContraseñaCorrecta = 1
			BEGIN
				UPDATE Usuarios
					SET habilitado = 1, 
						intentos = 0,
						contraseña_temporal = NULL
					WHERE email = @Email;

				SET @id = dbo.GetUser(@Email);
				SET @usuarioDesactivado = 0;
			END
			ELSE
			BEGIN
				SET @id = dbo.GetUser(@Email);
				SET @usuarioDesactivado = 1;
				PRINT(CONCAT('El usuario se encuentra desactivado', @usuarioDesactivado));
			END

			SET @usuarioActivo = 1;
			RETURN;
		END
	END
	ELSE
	BEGIN
		RAISERROR('El usuario ingresado no existe', 16, 1);
		RETURN;
	END
END;
GO

CREATE OR ALTER PROCEDURE spAltaUsuario
	@id SMALLINT
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		UPDATE Usuarios
			SET estatus = 1
			WHERE id_usuario = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
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