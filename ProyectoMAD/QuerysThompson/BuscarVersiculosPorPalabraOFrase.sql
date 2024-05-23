CREATE OR ALTER PROCEDURE BuscarVersiculosPorPalabraOFrase
    @Busqueda NVARCHAR(100),
	@version NVARCHAR(30)
AS
BEGIN
	DECLARE @id_version SMALLINT

	BEGIN TRY
		SELECT @id_version = Id_Version
		FROM DB_Bible.dbo.Versiones
		WHERE NombreVersion = @version;

		SELECT DISTINCT 
			CONCAT(Libros.Nombre, ' ', Versiculos.NumeroCap, ':', Versiculos.NumeroVers, ' ', Versiculos.Texto) AS Versiculo
		FROM DB_Bible.dbo.Versiculos
		JOIN DB_Bible.dbo.Libros ON Versiculos.id_libro = Libros.id_libro
		WHERE Versiculos.Texto LIKE '%' + @Busqueda + '%'
			AND Versiculos.Id_Version = @id_version;
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END;
GO

--EXEC BuscarVersiculosPorPalabraOFrase 'fa', 'REINA VALERA 1960'
