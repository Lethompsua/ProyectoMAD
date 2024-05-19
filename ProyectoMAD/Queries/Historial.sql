USE DB_Proyecto
GO

CREATE OR ALTER VIEW historyView
AS
	SELECT h.id_usuario,
		h.id_historial AS #,
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
        SELECT #,
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

CREATE OR ALTER PROCEDURE spDeleteRecord
	@id SMALLINT
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		DELETE FROM Historiales
			WHERE id_historial = @id;
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE spDeleteAllHistory
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		TRUNCATE TABLE Historiales;
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE spFilterHistory
	@id_user SMALLINT,
	@month SMALLINT,
	@year SMALLINT
AS
BEGIN
	SET NOCOUNT ON;

    BEGIN TRY
		IF @month = 0
		BEGIN
			SELECT #,
				Palabra,
				Idioma,
				Version,
				Testamento,
				Libro,
				Fecha
			FROM historyView
			WHERE id_usuario = @id_user
				--AND MONTH(Fecha) = @month
				AND YEAR(Fecha) = @year;
		END
		ELSE IF @year = 0
		BEGIN
			SELECT #,
				Palabra,
				Idioma,
				Version,
				Testamento,
				Libro,
				Fecha
			FROM historyView
			WHERE id_usuario = @id_user
				AND MONTH(Fecha) = @month
				--AND YEAR(Fecha) = @year;
		END
		ELSE IF @month <> 0 AND @year <> 0
		BEGIN
			SELECT #,
				Palabra,
				Idioma,
				Version,
				Testamento,
				Libro,
				Fecha
			FROM historyView
			WHERE id_usuario = @id_user
				AND MONTH(Fecha) = @month
				AND YEAR(Fecha) = @year;
		END
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END


/* PRUEBAS

EXEC spGetHistory 1;
EXEC spDeleteRecord 1;
EXEC spDeleteAllHistory;
EXEC spFilterHistory 1, 0, 0;

SELECT * FROM historyView;
SELECT * FROM DB_Bible.dbo.Libros;
SELECT * FROM DB_Bible.dbo.Idiomas;
SELECT * FROM DB_Bible.dbo.Versiones;
SELECT * FROM DB_Bible.dbo.Testamentos;

INSERT INTO Historiales (palabra, filtro_testamento, filtro_libro, filtro_version, fecha, id_usuario)
VALUES ('Génesis', 'ANTIGUO TESTAMENTO', 'Exodo', 'REINA VALERA 1960', GETDATE(), 1)
INSERT INTO Historiales (palabra, filtro_testamento, filtro_libro, filtro_version, fecha, id_usuario)
VALUES ('Génesis', 'NUEVO TESTAMENTO', 'Números', 'REINA VALERA 1960', '2024-12-17 18:14:21.897', 1)
INSERT INTO Historiales (palabra, filtro_testamento, filtro_libro, filtro_version, fecha, id_usuario)
VALUES ('Génesis', 'OLD TESTAMENT', 'Jueces', 'REINA VALERA 1960', GETDATE(), 1)

TRUNCATE TABLE Historiales;

SELECT * FROM Historiales;
SELECT * FROM Usuarios;
*/

