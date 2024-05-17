USE DB_Proyecto
GO

CREATE OR ALTER VIEW historyView
AS
	SELECT h.id_usuario,
		h.id_historial,
		h.palabra AS Palabra, 
		i.Nombre AS Idioma, 
		v.NombreVersion AS Version, 
		h.filtro_testamento AS Testamento, 
		h.filtro_libro AS Libro, 
		h.fecha AS Fecha
			FROM Historiales h
			INNER JOIN DB_Bible.dbo.Libros l ON l.Nombre = h.filtro_libro
			INNER JOIN DB_Bible.dbo.Idiomas i ON i.Id_Idioma = l.Id_Idioma
			INNER JOIN DB_Bible.dbo.Versiones v ON v.NombreVersion = h.filtro_version
GO

CREATE OR ALTER PROCEDURE spGetHistory
	@id_user SMALLINT,
	@id_historial SMALLINT OUTPUT,
	@Palabra VARCHAR(MAX) OUTPUT,
	@Idioma VARCHAR(20) OUTPUT,
	@Version VARCHAR(50) OUTPUT,
	@Testamento VARCHAR(50) OUTPUT,
	@Libro VARCHAR(50) OUTPUT,
	@Fecha DATETIME OUTPUT
AS BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		SELECT @id_historial = id_historial,
			@Palabra = Palabra,
			@Idioma = Idioma,
			@Version = Version,
			@Testamento = Testamento,
			@Libro = Libro,
			@Fecha = Fecha
				FROM historyView
				WHERE id_usuario = @id_user;
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO

/*
DECLARE @id_historial SMALLINT;
DECLARE @Palabra VARCHAR(MAX);
DECLARE @Idioma VARCHAR(20);
DECLARE @Version VARCHAR(50);
DECLARE @Testamento VARCHAR(50);
DECLARE @Libro VARCHAR(50);
DECLARE @Fecha DATETIME;

EXEC spGetHistory 
    @id_user = 1,
	@id_historial = @id_historial OUTPUT,
    @Palabra = @Palabra OUTPUT,
    @Idioma = @Idioma OUTPUT,
    @Version = @Version OUTPUT,
    @Testamento = @Testamento OUTPUT,
    @Libro = @Libro OUTPUT,
    @Fecha = @Fecha OUTPUT;

SELECT @id_historial AS id,
       @Palabra AS Palabra, 
       @Idioma AS Idioma, 
       @Version AS Version, 
       @Testamento AS Testamento, 
       @Libro AS Libro, 
       @Fecha AS Fecha;
*/
--SELECT * FROM historyView;
--SELECT * FROM DB_Bible.dbo.Libros;
--SELECT * FROM DB_Bible.dbo.Idiomas;
--SELECT * FROM DB_Bible.dbo.Versiones;

--INSERT INTO Historiales (palabra, filtro_testamento, filtro_libro, filtro_version, fecha, id_usuario)
--VALUES ('Génesis', 'ANTIGUO TESTAMENTO', 'Exodo', 'REINA VALERA 1960', GETDATE(), 1)

--SELECT * FROM Historiales;

