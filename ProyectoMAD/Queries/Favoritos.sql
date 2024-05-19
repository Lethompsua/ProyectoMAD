USE DB_Proyecto
GO

CREATE OR ALTER VIEW FavsView
AS
	SELECT f.id_usuario, f.nombre, f.capitulo, v.texto
	FROM Favoritos f
	INNER JOIN DB_Bible.dbo.Versiculos v
	ON f.id_versiculo = v.Id_Vers;
GO

CREATE OR ALTER PROCEDURE spShowFavs
	@id_user SMALLINT
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		SELECT nombre, capitulo, texto
		FROM FavsView
		WHERE id_usuario = @id_user;
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END

/*
SELECT * FROM DB_Bible.dbo.Versiculos
SELECT * FROM DB_Bible.dbo.Libros
SELECT * FROM DB_Bible.dbo.Versiones

INSERT INTO Favoritos (nombre, fecha_registro, libro, capitulo, version, id_versiculo, id_usuario)
VALUES ('Mi favorito', GETDATE(), 'Génesis', '9', 'REINA VALERA 1960', 4252, 1);

SELECT * FROM Favoritos

EXEC spShowFavs 1
*/