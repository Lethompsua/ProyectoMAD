CREATE PROCEDURE sp_ObtenerVersionesPorNombreIdioma
    @nombre_idioma NVARCHAR(50)
AS
BEGIN
    SELECT V.id_version, V.nombreVersion 
    FROM DB_Bible.dbo.Versiones V
    JOIN DB_Bible.dbo.Idiomas I ON V.id_idioma = I.id_idioma
    WHERE I.nombre = @nombre_idioma;
END;
 
