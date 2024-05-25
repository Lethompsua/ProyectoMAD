USE DB_Proyecto
GO

CREATE OR ALTER VIEW historyView
AS
	SELECT id_usuario,
		id_historial AS #,
		palabra AS Palabra, 
		filtro_version AS Version,
		CASE WHEN filtro_testamento = '' OR filtro_testamento IS NULL
			THEN 'N/A'
			ELSE filtro_testamento
		END AS Testamento,
		CASE WHEN filtro_libro = '' OR filtro_libro IS NULL
			THEN 'N/A'
			ELSE filtro_libro
		END AS Libro,
		fecha AS Fecha
	FROM Historiales h
GO

CREATE OR ALTER PROCEDURE spGetHistory
    @id_user SMALLINT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT #,
            Palabra,
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

