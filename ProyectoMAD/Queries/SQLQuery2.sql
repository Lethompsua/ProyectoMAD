CREATE OR ALTER PROCEDURE MostrarNombresTestamento
AS
BEGIN
    -- Seleccionar solo el nombre de la tabla testamento
    SELECT Nombre
    FROM DB_Bible.dbo.Testamentos;
END;



 SELECT *
    FROM DB_Bible.dbo.Libros;