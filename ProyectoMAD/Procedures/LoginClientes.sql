CREATE PROCEDURE VerificarLogin
    @NombreUsuario VARCHAR(50),
    @Contraseña VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UsuarioExiste INT;
    DECLARE @IntentosFallidos INT;

    -- Obtener el número de intentos fallidos del usuario
    SELECT @IntentosFallidos = IntentosFallidos
    FROM Usuarios
    WHERE NombreUsuario = @NombreUsuario;

    -- Si el usuario existe y aún no ha sido desactivado
    IF @IntentosFallidos IS NOT NULL AND @IntentosFallidos < 3
    BEGIN
        -- Verificar si el usuario y contraseña son correctos
        SELECT @UsuarioExiste = COUNT(*)
        FROM Usuarios
        WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña;

        -- Si el login es correcto
        IF @UsuarioExiste = 1
        BEGIN
            -- Reiniciar el contador de intentos fallidos
            UPDATE Usuarios
            SET IntentosFallidos = 0
            WHERE NombreUsuario = @NombreUsuario;

            -- Devolver 1 para indicar login correcto
            SELECT 1 AS LoginCorrecto;
        END
        ELSE
        BEGIN
            -- Incrementar el contador de intentos fallidos
            UPDATE Usuarios
            SET IntentosFallidos = @IntentosFallidos + 1
            WHERE NombreUsuario = @NombreUsuario;

            -- Devolver 0 para indicar login incorrecto
            SELECT 0 AS LoginCorrecto;
        END
    END
    ELSE
    BEGIN
        -- Si el usuario no existe o ha sido desactivado, devolver 0
        SELECT 0 AS LoginCorrecto;
    END

    -- Desactivar el usuario si ha excedido el límite de intentos fallidos
    IF @IntentosFallidos >= 2
    BEGIN
        UPDATE Usuarios
        SET Activo = 0
        WHERE NombreUsuario = @NombreUsuario;
    END
END;
