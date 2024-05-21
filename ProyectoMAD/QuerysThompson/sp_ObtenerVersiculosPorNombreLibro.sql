CREATE PROCEDURE sp_ObtenerVersiculosPorNombreLibro
    @nombre_libro NVARCHAR(50)
AS
BEGIN
    SELECT 
        L.nombre AS NombreLibro,
        V.NumeroCap,
        V.NumeroVers,
        CONCAT(L.nombre, ' ', V.NumeroCap, ':', V.NumeroVers, ' ', V.texto) AS VersiculoFormateado
    FROM DB_Bible.dbo.Versiculos V
    JOIN DB_Bible.dbo.Libros L ON V.id_libro = L.id_libro
    WHERE L.nombre = @nombre_libro;
END;
