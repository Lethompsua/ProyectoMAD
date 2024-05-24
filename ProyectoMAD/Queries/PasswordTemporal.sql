USE DB_Proyecto;
GO

CREATE OR ALTER PROCEDURE spValidarRespuesta
	@Respuesta VARCHAR(100),
	@Contraseña VARCHAR(50),
	@ID SMALLINT
AS
BEGIN
	DECLARE @RespuestaCorrecta BIT;

	SELECT @RespuestaCorrecta = COUNT(id_usuario)
		FROM Usuarios
		WHERE @Respuesta = respuesta_seguridad;

	IF @RespuestaCorrecta = 1
	BEGIN
		UPDATE Usuarios
			SET contraseña_temporal = @Contraseña
			WHERE id_usuario = @ID;

		PRINT(CONCAT('Tu contraseña temporal es: ', @Contraseña));
	END
	ELSE
	BEGIN
		RAISERROR('La respuesta es incorrecta', 16, 1);
		RETURN;
	END
END
GO

CREATE OR ALTER PROCEDURE spGetQuestion
	@id SMALLINT,
	@question VARCHAR(100) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		DECLARE @tempQuestion VARCHAR(100);
        SET @tempQuestion = dbo.GetQuestion(@id);
        SET @question = @tempQuestion;

		RETURN 0
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO

/*
DECLARE @question VARCHAR(100);
EXEC spGetQuestion 1, @question OUTPUT;
SELECT @question;
*/