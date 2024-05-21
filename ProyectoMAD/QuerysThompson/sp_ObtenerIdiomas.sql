CREATE PROCEDURE ObtenerIdiomas
AS
BEGIN
    SELECT Id_Idioma, Nombre
    FROM DB_Bible.dbo.Idiomas;
END;
GO 



