USE [DB_Proyecto]
GO

CREATE OR ALTER PROCEDURE [dbo].[InsertarUsuario]
    @nombre_completo VARCHAR(50),
    @email VARCHAR(50),
    @password VARCHAR(50),
    @fecha_nacimiento DATE,
    @genero VARCHAR(15),
    @fecha_registro DATETIME,
    @pregunta_seguridad VARCHAR(100),
    @respuesta_seguridad VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF EXISTS (SELECT 1 FROM Usuarios WHERE email = @email)
		BEGIN
			RAISERROR('El usuario ya existe', 16, 1);
			RETURN;
		END
		ELSE
		BEGIN
			INSERT INTO Usuarios (nombre_completo, email, password, fecha_nacimiento, genero, 
			fecha_registro, pregunta_seguridad, respuesta_seguridad, habilitado, estatus)
			VALUES (@nombre_completo, @Email, @Password, @fecha_nacimiento, @genero, 
			@fecha_registro, @pregunta_seguridad, @respuesta_seguridad, 1, 1);

			PRINT 'Usuario registrado';
		END
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO