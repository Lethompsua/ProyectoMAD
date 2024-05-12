USE [DB_Proyecto]
GO

CREATE OR ALTER PROCEDURE [dbo].[InsertarUsuario]
    @nombre_completo VARCHAR(50),
    @email VARCHAR(50),
    @password VARCHAR(50),
    @fecha_nacimiento DATE,
    @id_genero SMALLINT,
    @fecha_registro DATETIME,
    @pregunta_seguridad VARCHAR(100),
    @respuesta_seguridad TEXT,
	@usuarioExistente BIT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF EXISTS (SELECT 1 FROM Usuarios WHERE email = @email)
		BEGIN
			PRINT 'El usuario ya existe';
			SET @usuarioExistente = 1;
		END
		ELSE
		BEGIN
			INSERT INTO Usuarios (nombre_completo, email, password, fecha_nacimiento, id_genero, 
			fecha_registro, pregunta_seguridad, respuesta_seguridad, habilitado, estatus)
			VALUES (@nombre_completo, @Email, @Password, @fecha_nacimiento, @id_genero, 
			@fecha_registro, @pregunta_seguridad, @respuesta_seguridad, 1, 1);

			PRINT 'Usuario registrado';
			SET @usuarioExistente = 0;
		END
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO