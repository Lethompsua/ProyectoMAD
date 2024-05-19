USE DB_Proyecto
GO

CREATE OR ALTER VIEW FavsView
AS
	SELECT f.id_usuario,
		f.id_favorito AS #,
		f.nombre AS Nombre,
		f.version AS Version,
		f.libro AS Libro,
		f.capitulo AS capitulo, 
		v.texto AS Texto
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
		SELECT #, Nombre, Libro, Capitulo, Texto
		FROM FavsView
		WHERE id_usuario = @id_user;
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE spDeleteFav
	@id SMALLINT
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		DELETE FROM Favoritos
			WHERE id_favorito = @id;
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE spGetChapter
	@Libro VARCHAR(20),
    @NumeroCap TINYINT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Id_Libro SMALLINT

	BEGIN TRY
		SELECT @Id_Libro = Id_Libro
		FROM DB_Bible.dbo.Libros
		WHERE Nombre = @Libro;

		SELECT Texto
		FROM DB_Bible.dbo.Versiculos
		WHERE Id_Libro = @Id_Libro
		AND NumeroCap = @NumeroCap;
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO

/* PRUEBAS
SELECT * FROM DB_Bible.dbo.Versiculos
SELECT * FROM DB_Bible.dbo.Libros
SELECT * FROM DB_Bible.dbo.Versiones

INSERT INTO Favoritos (nombre, fecha_registro, libro, capitulo, version, id_versiculo, id_usuario)
VALUES ('Mi favorito', GETDATE(), 'Génesis', '3', 'REINA VALERA 1960', 4252, 1);
INSERT INTO Favoritos (nombre, fecha_registro, libro, capitulo, version, id_versiculo, id_usuario)
VALUES ('Mi favorito', GETDATE(), 'Génesis', '3', 'REINA VALERA 1960', 4252, 1);
INSERT INTO Favoritos (nombre, fecha_registro, libro, capitulo, version, id_versiculo, id_usuario)
VALUES ('Mi favorito', GETDATE(), 'Génesis', '3', 'REINA VALERA 1960', 4260, 1);

SELECT * FROM Favoritos
SELECT * FROM DB_Bible.dbo.Libros WHERE Nombre = 'Génesis'

EXEC spShowFavs 1
EXEC spGetChapter 9
*/