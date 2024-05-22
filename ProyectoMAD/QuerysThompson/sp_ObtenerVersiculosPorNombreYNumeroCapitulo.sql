
CREATE OR ALTER   PROCEDURE [dbo].[sp_ObtenerVersiculosPorNombreYNumeroCapitulo]
    @nombre_libro NVARCHAR(50),
    @numero_capitulo TINYINT,
	@version TINYINT
AS
BEGIN
    SELECT DISTINCT
        L.nombre AS NombreLibro,
        V.NumeroCap,
        V.NumeroVers,
        CONCAT(L.nombre, ' ', V.NumeroCap, ':', V.NumeroVers, ' ', CAST(V.texto AS NVARCHAR(MAX))) AS Versiculo
    FROM 
        DB_Bible.dbo.Versiculos V
    JOIN 
        DB_Bible.dbo.Libros L ON V.id_libro = L.id_libro
    WHERE 
        L.nombre = @nombre_libro
        AND V.NumeroCap = @numero_capitulo
		AND Id_Version = @version;
END;
GO

/*
EXEC sp_ObtenerVersiculosPorNombreYNumeroCapitulo 'Génesis', 1, 1
SELECT * FROM DB_Bible.dbo.Libros
SELECT * FROM DB_Bible.dbo.Versiculos WHERE Id_Libro = 1

SELECT * FROM DB_Bible.dbo.Versiculos V JOIN DB_Bible.dbo.Libros L ON V.id_libro = L.id_libro
WHERE NumeroVers = 2 AND V.Id_Libro = 1 AND NumeroCap = 1
*/