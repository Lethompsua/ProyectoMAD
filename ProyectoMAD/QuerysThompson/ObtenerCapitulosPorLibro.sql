
CREATE OR ALTER   PROCEDURE [dbo].[ObtenerCapitulosPorLibro]
    @idLibro INT
AS
BEGIN
    SELECT NumeroCap
    FROM [DB_Bible].[dbo].[Versiculos]
    WHERE Id_Libro = @idLibro
    ORDER BY NumeroCap;
END;
