CREATE PROCEDURE ObtenerLibrosPorTestamento
    @Testamentoo NVARCHAR(50)
AS
BEGIN
    IF @Testamentoo = 'Antiguo Testamento'
    BEGIN
        -- Seleccionar los libros del Antiguo Testamento
        SELECT nombre
        FROM DB_Bible.dbo.Libros
        WHERE Id_Testamento = 1;
    END
    ELSE IF @Testamentoo = 'Nuevo Testamento'
    BEGIN
        -- Seleccionar los libros del Nuevo Testamento
        SELECT nombre
        FROM DB_Bible.dbo.Libros
        WHERE Id_Testamento = 2;
    END
    ELSE
    BEGIN
        -- Manejar el caso en que el nombre del testamento no sea válido
        PRINT 'Nombre de testamento no válido.';
    END
END;

