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
    @id_user SMALLINT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT id_historial,
            Palabra,
            Idioma,
            Version,
            Testamento,
            Libro,
            Fecha
		FROM historyView
		WHERE id_usuario = @id_user;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO


/*
DECLARE @UserId SMALLINT;
SET @UserId = 1;  -- Reemplaza 1 con el ID de usuario que deseas consultar

EXEC spGetHistory @id_user = @UserId;
*/
--SELECT * FROM historyView;
--SELECT * FROM DB_Bible.dbo.Libros;
--SELECT * FROM DB_Bible.dbo.Idiomas;
--SELECT * FROM DB_Bible.dbo.Versiones;

--INSERT INTO Historiales (palabra, filtro_testamento, filtro_libro, filtro_version, fecha, id_usuario)
--VALUES ('Génesis', 'ANTIGUO TESTAMENTO', 'Exodo', 'REINA VALERA 1960', GETDATE(), 1)

--SELECT * FROM Historiales;

