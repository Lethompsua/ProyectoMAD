USE DB_Proyecto;
GO

CREATE OR ALTER PROCEDURE VerificarLogin
    @Email VARCHAR(50),
    @Password VARCHAR(50),
	@id SMALLINT OUTPUT,
	@usuarioDesactivado BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UsuarioExiste BIT;
    DECLARE @IntentosFallidos INT;
	DECLARE @Contrase�aCorrecta BIT;

	SET @id = 0;
	SET @usuarioDesactivado = 0;

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
			SELECT @Contrase�aCorrecta = COUNT(id_usuario)
			FROM Usuarios
			WHERE email = @Email AND password = @Password;

			IF @Contrase�aCorrecta = 1
			BEGIN
				UPDATE Usuarios
				SET intentos = 0
				WHERE email = @Email;

				PRINT('Login exitoso');
				SET @id = dbo.GetUser(@Email);
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
					RETURN
				END
				ELSE
				BEGIN
					RAISERROR('La contrase�a ingresada es incorrecta', 16, 1);
				END

				RETURN
			END
		END
		ELSE
		BEGIN
			SET @id = dbo.GetUser(@Email);
			SET @usuarioDesactivado = 1;
			PRINT(CONCAT('El se encuentra desactivado', @usuarioDesactivado));
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