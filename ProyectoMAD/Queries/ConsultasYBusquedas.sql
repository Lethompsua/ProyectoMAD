USE DB_Proyecto
GO

CREATE OR ALTER PROCEDURE MostrarNombresTestamento
AS
BEGIN
    SELECT Nombre
    FROM DB_Bible.dbo.Testamentos;
END;
GO

CREATE OR ALTER VIEW VistaVersiculos AS
	SELECT CONCAT(Libros.nombre, ' ', Versiculos.NumeroCap, ':', Versiculos.NumeroVers, ' ', Versiculos.texto) AS VersiculoFormateado
	FROM DB_Bible.dbo.Versiculos
	JOIN DB_Bible.dbo.Libros ON Versiculos.id_libro = Libros.id_libro;
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_ObtenerVersiculosPorNombreLibro]
    @nombre_libro NVARCHAR(50),
	@version NVARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		SELECT 
			CONCAT(L.nombre, ' ', V.NumeroCap, ':', V.NumeroVers) AS Cita,
			CONVERT (NVARCHAR(MAX), V.Texto) AS Texto
		FROM DB_Bible.dbo.Versiculos V
		JOIN DB_Bible.dbo.Libros L ON V.id_libro = L.id_libro
		JOIN DB_Bible.dbo.Versiones b ON V.Id_Version = b.Id_Version
		WHERE L.nombre = @nombre_libro
			AND b.NombreVersion = @version
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_ObtenerVersiculosPorNombreYNumeroCapitulo]
    @nombre_libro NVARCHAR(50),
    @numero_capitulo TINYINT,
	@version TINYINT
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		SELECT --NO USAR DISTINCT (desordena los resultados)
			CONCAT(L.nombre, ' ', V.NumeroCap, ':', V.NumeroVers) AS Cita,
			CONVERT(NVARCHAR(MAX), V.Texto) AS Texto
		FROM DB_Bible.dbo.Versiculos V
		JOIN DB_Bible.dbo.Libros L ON V.id_libro = L.id_libro
		WHERE L.nombre = @nombre_libro
			AND V.NumeroCap = @numero_capitulo
			AND V.Id_Version = @version
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE ObtenerVersionesPorNombreIdioma
    @NombreIdioma NVARCHAR(100)
AS
BEGIN
    SELECT v.Id_Version, v.NombreVersion
    FROM [DB_Bible].[dbo].[Versiones] v
    INNER JOIN [DB_Bible].[dbo].[Idiomas] i ON v.Id_Idioma = i.Id_Idioma
    WHERE i.Nombre = @NombreIdioma;
END;
GO

CREATE OR ALTER PROCEDURE ObtenerTestamentosPorNombreVersion
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

CREATE OR ALTER PROCEDURE ObtenerLibrosPorNombreTestamento
    @NombreTestamento NVARCHAR(100)
AS
BEGIN
    SELECT l.Id_Libro, l.Nombre
    FROM [DB_Bible].[dbo].[Libros] l
    INNER JOIN [DB_Bible].[dbo].[Testamentos] t ON l.Id_Testamento = t.Id_Testamento
    WHERE t.Nombre = @NombreTestamento;
END;
GO

CREATE OR ALTER PROCEDURE ObtenerIdiomas
AS
BEGIN
    SELECT Id_Idioma, Nombre
    FROM DB_Bible.dbo.Idiomas;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[ObtenerCapitulosPorLibro]
    @idLibro INT
AS
BEGIN
    SELECT DISTINCT NumeroCap
    FROM [DB_Bible].[dbo].[Versiculos]
    WHERE Id_Libro = @idLibro
    ORDER BY NumeroCap;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[BuscarVersiculosPorPalabraOFrase]
    @Busqueda NVARCHAR(100),
    @Version NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        CREATE TABLE #PalabrasBusqueda (Palabra NVARCHAR(100));

        INSERT INTO #PalabrasBusqueda (Palabra)
        SELECT VALUE
        FROM STRING_SPLIT(@Busqueda, ' ');

        SELECT DISTINCT 
            CONCAT(Libros.Nombre, ' ', Versiculos.NumeroCap, ':', Versiculos.NumeroVers) AS Cita,
            CONVERT(NVARCHAR(MAX), Versiculos.Texto) AS Texto
        FROM DB_Bible.dbo.Versiculos
        JOIN DB_Bible.dbo.Libros ON Versiculos.id_libro = Libros.id_libro
        JOIN DB_Bible.dbo.Versiones ON Versiculos.Id_Version = Versiones.Id_Version
        WHERE Versiones.NombreVersion = @Version
            AND NOT EXISTS (
                SELECT 1 
                FROM #PalabrasBusqueda 
                WHERE Versiculos.Texto NOT LIKE '%' + Palabra + '%');

        DROP TABLE #PalabrasBusqueda;
    END TRY
    BEGIN CATCH
        THROW
    END CATCH
END
GO


--EXEC BuscarVersiculosPorPalabraOFrase 'Dios mujer', 'REINA VALERA 1960'

CREATE OR ALTER PROCEDURE [dbo].[BuscarVersiculosPorCapitulo]
    @Busqueda NVARCHAR(100),
    @Version NVARCHAR(30),
    @Libro NVARCHAR(25),
    @Capitulo TINYINT
AS
BEGIN
	SET NOCOUNT ON

    BEGIN TRY
        CREATE TABLE #PalabrasBusqueda (Palabra NVARCHAR(100));

        INSERT INTO #PalabrasBusqueda (Palabra)
        SELECT VALUE
        FROM STRING_SPLIT(@Busqueda, ' ');

        SELECT DISTINCT 
            Libros.Nombre + ' ' + CAST(Versiculos.NumeroCap AS NVARCHAR) + ':' + CAST(Versiculos.NumeroVers AS NVARCHAR) AS Cita,
            CONVERT(NVARCHAR(MAX), Versiculos.Texto) AS Texto --TEXT no puede usarse con DISTINCT, así que lo convertimos a NVARCHAR
        FROM 
            DB_Bible.dbo.Versiculos
        INNER JOIN 
            DB_Bible.dbo.Libros ON Versiculos.Id_Libro = Libros.Id_Libro
        INNER JOIN 
            DB_Bible.dbo.Versiones ON Versiculos.Id_Version = Versiones.Id_Version
        WHERE 
            Libros.Nombre = @Libro
            AND Versiones.NombreVersion = @Version
            AND Versiculos.NumeroCap = @Capitulo
            AND NOT EXISTS (
                SELECT 1
                FROM #PalabrasBusqueda
                WHERE Versiculos.Texto NOT LIKE '%' + Palabra + '%');

        DROP TABLE #PalabrasBusqueda
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[BuscarVersiculosPorPalabraOFraseSegunElTestamentoYVersion]
    @Busqueda NVARCHAR(100),
	@Testamento NVARCHAR(20),
	@Version NVARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON

    BEGIN TRY
        CREATE TABLE #PalabrasBusqueda (Palabra NVARCHAR(100));

        INSERT INTO #PalabrasBusqueda (Palabra)
        SELECT VALUE
        FROM STRING_SPLIT(@Busqueda, ' ');

		SELECT DISTINCT 
			CONCAT(Libros.Nombre, ' ', Versiculos.NumeroCap, ':', Versiculos.NumeroVers) AS Cita,
			CONVERT(NVARCHAR(MAX), Versiculos.Texto) AS Texto
		FROM 
			 DB_Bible.dbo.Versiculos
		INNER JOIN
			 DB_Bible.dbo.Libros ON Versiculos.Id_Libro = Libros.Id_Libro
		INNER JOIN 
			 DB_Bible.dbo.Testamentos ON Libros.Id_Testamento = Testamentos.Id_Testamento
		INNER JOIN 
			 DB_Bible.dbo.Versiones ON Versiculos.Id_Version = Versiones.Id_Version
		WHERE 
			NOT EXISTS (SELECT 1 FROM #PalabrasBusqueda
			WHERE Versiculos.Texto NOT LIKE '%' + Palabra + '%')
			AND Testamentos.Nombre = @Testamento
			AND Versiones.NombreVersion = @Version;

		DROP TABLE #PalabrasBusqueda
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO
 /*
EXEC BuscarVersiculosPorPalabraOFraseSegunElTestamentoYVersion 'Dios hijos también', 'ANTIGUO TESTAMENTO', 'REINA VALERA 1960';

SELECT * FROM DB_Bible.dbo.Versiculos
GO
*/
CREATE OR ALTER PROCEDURE [dbo].[BuscarVersiculosPorPalabraOFraseSegunElTestamentoYVersionYLibro]
    @Busqueda NVARCHAR(100),
    @Testamento NVARCHAR(20),
    @Version NVARCHAR(30),
    @Libro NVARCHAR(25)
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
        AND Versiones.NombreVersion = @Version
        AND Libros.Nombre = @Libro;
END
GO

CREATE OR ALTER PROCEDURE spGetSize
	@id_user SMALLINT,
	@size SMALLINT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		SELECT @size = dbo.GetSize(@id_user)
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END
GO