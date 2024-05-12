USE DB_Proyecto;
GO

CREATE OR ALTER PROCEDURE spValidarRespuesta
	@Respuesta VARCHAR(100),
	@Contrase�a VARCHAR(50),
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
			SET contrase�a_temporal = @Contrase�a
			WHERE id_usuario = @ID;

		PRINT(CONCAT('Tu contrase�a temporal es: ', @Contrase�a));
	END
	ELSE
	BEGIN
		RAISERROR('La respuesta es incorrecta', 16, 1);
		RETURN;
	END
END

SELECT * FROM Usuarios;