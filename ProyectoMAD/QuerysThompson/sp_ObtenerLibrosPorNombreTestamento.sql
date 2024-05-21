CREATE PROCEDURE sp_ObtenerLibrosPorNombreTestamento
    @nombre_testamento NVARCHAR(50)
AS
BEGIN
    SELECT L.id_libro, L.nombre 
    FROM DB_Bible.dbo.Libros L
    JOIN DB_Bible.dbo.Testamentos T ON L.id_testamento = T.id_testamento
    WHERE T.nombre = @nombre_testamento;
END;
