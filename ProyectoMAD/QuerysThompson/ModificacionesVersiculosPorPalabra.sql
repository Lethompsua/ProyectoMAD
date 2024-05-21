ALTER PROCEDURE [dbo].[BuscarVersiculosPorPalabraOFrase]
    @Busqueda NVARCHAR(100)
AS
BEGIN
    SELECT DISTINCT 
        CONCAT(Libros.Nombre, ' ', Versiculos.NumeroCap, ':', Versiculos.NumeroVers, ' ', Versiculos.Texto) AS Versiculo  -- Cambio en el nombre de la columna
    FROM DB_Bible.dbo.Versiculos
    JOIN DB_Bible.dbo.Libros ON Versiculos.id_libro = Libros.id_libro
    WHERE Versiculos.Texto LIKE '%' + @Busqueda + '%';
END;

