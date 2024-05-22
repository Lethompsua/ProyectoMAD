CREATE OR ALTER PROCEDURE [dbo].[BuscarVersiculosPorPalabraOFraseSegunElTestamentoYVersion]
    @Busqueda NVARCHAR(100),
	@Testamento NVARCHAR(20),
	@Version NVARCHAR(30)
	--@Libro NVARCHAR(25),
	--@NumCap INT
AS
BEGIN
    SELECT DISTINCT 
        CONCAT(Libros.Nombre, ' ', Versiculos.NumeroCap, ':', Versiculos.NumeroVers, ' ', Versiculos.Texto) AS Versiculo
    FROM 
         DB_Bible.dbo.Versiculos
    INNER JOIN 
         DB_Bible.dbo.Libros ON Versiculos.Id_Libro = Libros.Id_Libro
    INNER JOIN 
         DB_Bible.dbo.Testamentos ON Libros.Id_Testamento = Testamentos.Id_Testamento
    INNER JOIN 
         DB_Bible.dbo.Versiones ON Versiculos.Id_Version = Versiones.Id_Version
    WHERE 
        Versiculos.Texto LIKE '%' + @Busqueda + '%'
        AND Testamentos.Nombre = @Testamento
        AND Versiones.NombreVersion = @Version;
END;

--EXEC BuscarVersiculosPorPalabraOFraseSegunElTestamentoYVersion 'Jehova', 'ANTIGUO TESTAMENTO', 'REINA VALERA 1960'