USE [DB_Proyecto]
GO

-- Comprobamos si el procedimiento almacenado ya existe y lo borramos si es así
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'InsertarUsuario')
BEGIN
    DROP PROCEDURE [dbo].[InsertarUsuario]
END
GO

-- Creamos el nuevo procedimiento almacenado con las modificaciones necesarias
CREATE PROCEDURE [dbo].[InsertarUsuario]
    @nombre_completo VARCHAR(50),
    @email VARCHAR(50),
    @password VARCHAR(8),
    @fecha_nacimiento DATE,
    @id_genero SMALLINT,
    @fecha_registro DATETIME,
    @pregunta_seguridad VARCHAR(50),
    @respuesta_seguridad TEXT
AS
BEGIN
    DECLARE @edad INT
    SET @edad = DATEDIFF(YEAR, @fecha_nacimiento, GETDATE())
    
    IF (@edad >= 18)  -- Verifica si el usuario es mayor de edad
    BEGIN
        -- Validar el formato del correo electrónico
        IF (CHARINDEX('@', @email) > 0 AND LEN(@email) - CHARINDEX('@', @email) > 0 
            AND CHARINDEX('.', REVERSE(@email)) > 0 AND LEN(@email) - CHARINDEX('.', REVERSE(@email)) > 1)
        BEGIN
            -- Validar el nombre para que no contenga números ni caracteres especiales
            IF (@nombre_completo NOT LIKE '%[^a-zA-Z ]%')
            BEGIN
                -- Insertar datos del usuario si tanto el correo como el nombre son válidos
                INSERT INTO Usuarios (nombre_completo, email, password, fecha_nacimiento, id_genero, fecha_registro, pregunta_seguridad, respuesta_seguridad, habilitado, estatus)
                VALUES (@nombre_completo, @email, @password, @fecha_nacimiento, @id_genero, @fecha_registro, @pregunta_seguridad, @respuesta_seguridad, 1, 1);
            END
            ELSE
                print 'El nombre no puede contener números ni caracteres especiales.'
        END
        ELSE
            print 'El correo electrónico proporcionado no tiene un formato válido.'
    END
    ELSE
        print 'El usuario es menor de edad y no puede ser registrado.'
END;
