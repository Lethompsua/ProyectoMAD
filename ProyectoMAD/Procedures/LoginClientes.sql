USE DB_Proyecto;
GO

CREATE OR ALTER PROCEDURE VerificarLogin
    @Email VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UsuarioExiste INT;
    DECLARE @IntentosFallidos INT;

    -- Obtener el número de intentos fallidos del usuario
    SELECT @IntentosFallidos = intentos
    FROM Usuarios
    WHERE email = @email;

    -- Si el usuario existe y aún no ha sido desactivado
    IF @IntentosFallidos IS NOT NULL AND @IntentosFallidos < 3
	BEGIN
		-- Verificar si el usuario y contraseña son correctos
		SELECT @UsuarioExiste = COUNT(id_usuario)
		FROM Usuarios
		WHERE email = @Email AND password = @Password;

		-- Si el login es correcto
		IF @UsuarioExiste = 1
		BEGIN
			-- Reiniciar el contador de intentos fallidos
			UPDATE Usuarios
			SET intentos = 0
			WHERE email = @Email;

			PRINT('Login exitoso');
		END
		ELSE
		BEGIN
			-- Incrementar el contador de intentos fallidos
			UPDATE Usuarios
			SET intentos = @IntentosFallidos + 1
			WHERE email = @Email;

			PRINT('El correo o contraseña no coinciden');
		END
	END
    ELSE IF @IntentosFallidos IS NULL
	BEGIN
		PRINT('El usuario ingresado no existe');
	END
	ELSE IF @IntentosFallidos >= 3
	BEGIN
		PRINT('El usuario ingresado ha sido desactivado');
	END

    -- Desactivar el usuario si ha excedido el límite de intentos fallidos
    IF @IntentosFallidos >= 2
    BEGIN
        UPDATE Usuarios
        SET habilitado = 0
        WHERE email = @Email;
    END
END;