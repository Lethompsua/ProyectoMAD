-- Procedimiento para obtener versiones por nombre de idioma
CREATE PROCEDURE ObtenerVersionesPorNombreIdioma
    @NombreIdioma NVARCHAR(100)
AS
BEGIN
    SELECT v.Id_Version, v.NombreVersion
    FROM [DB_Bible].[dbo].[Versiones] v
    INNER JOIN [DB_Bible].[dbo].[Idiomas] i ON v.Id_Idioma = i.Id_Idioma
    WHERE i.Nombre = @NombreIdioma;
END;
GO

-- Procedimiento para obtener testamentos por nombre de versión
CREATE PROCEDURE ObtenerTestamentosPorNombreVersion
    @NombreIdioma NVARCHAR(100),
    @NombreVersion NVARCHAR(100)
AS
BEGIN
    SELECT t.Id_Testamento, t.Nombre
    FROM [DB_Bible].[dbo].[Testamentos] t
    INNER JOIN [DB_Bible].[dbo].[Versiones] v ON t.Id_Idioma = v.Id_Idioma
    INNER JOIN [DB_Bible].[dbo].[Idiomas] i ON v.Id_Idioma = i.Id_Idioma
    WHERE i.Nombre = @NombreIdioma AND v.NombreVersion = @NombreVersion;
END;
GO

-- Procedimiento para obtener libros por nombre de testamento
CREATE PROCEDURE ObtenerLibrosPorNombreTestamento
    @NombreTestamento NVARCHAR(100)
AS
BEGIN
    SELECT l.Id_Libro, l.Nombre
    FROM [DB_Bible].[dbo].[Libros] l
    INNER JOIN [DB_Bible].[dbo].[Testamentos] t ON l.Id_Testamento = t.Id_Testamento
    WHERE t.Nombre = @NombreTestamento;
END;
GO
