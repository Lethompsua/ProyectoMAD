CREATE OR ALTER PROCEDURE [dbo].[BuscarVersiculosPorCapitulo]
    @Busqueda NVARCHAR(100),
    @Version NVARCHAR(30),
    @Libro NVARCHAR(25),
	@capitulo TINYINT
AS
BEGIN
	BEGIN TRY
		PRINT 'Busqueda: ' + @Busqueda
        PRINT 'Version: ' + @Version
        PRINT 'Libro: ' + @Libro
        PRINT 'Capitulo: ' + CAST(@capitulo AS NVARCHAR(3))

		SELECT DISTINCT 
			CONCAT(Libros.Nombre, ' ', Versiculos.NumeroCap, ':', Versiculos.NumeroVers, ' ', Versiculos.Texto) AS Versiculo
		FROM 
			DB_Bible.dbo.Versiculos
		INNER JOIN 
			DB_Bible.dbo.Libros ON Versiculos.Id_Libro = Libros.Id_Libro
		INNER JOIN 
			DB_Bible.dbo.Versiones ON Versiculos.Id_Version = Versiones.Id_Version
		WHERE 
			Versiculos.Texto LIKE '%' + @Busqueda + '%'
			AND Libros.Nombre = @Libro
			AND Versiones.NombreVersion = @Version
			AND Versiculos.NumeroCap = @capitulo;
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END;
GO
/*
EXEC BuscarVersiculosPorCapitulo 'fa', 'REINA VALERA 1960', 'Génesis', 1
*/