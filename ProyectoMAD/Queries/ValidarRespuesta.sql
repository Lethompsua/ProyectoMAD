USE DB_Proyecto;
GO

CREATE OR ALTER PROCEDURE spValidarRespuesta
	@Respuesta VARCHAR(100),
	@Contraseņa VARCHAR(50),
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
			SET contraseņa_temporal = @Contraseņa
			WHERE id_usuario = @ID;

		PRINT(CONCAT('Tu contraseņa temporal es: ', @Contraseņa));
	END
	ELSE
	BEGIN
		RAISERROR('La respuesta es incorrecta', 16, 1);
		RETURN;
	END
END

SELECT * FROM Usuarios;