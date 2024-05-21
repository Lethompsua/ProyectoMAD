CREATE PROCEDURE sp_ObtenerTestamentosPorNombreVersion
    @nombre_version NVARCHAR(100)
AS
BEGIN
    SELECT T.id_testamento, T.nombre 
    FROM DB_Bible.dbo.Testamentos T
    JOIN DB_Bible.dbo.Versiones V ON T.id_idioma = V.id_idioma
    WHERE V.nombreVersion = @nombre_version;
END;
