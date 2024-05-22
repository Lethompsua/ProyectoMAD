USE [DB_Proyecto]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerVersiculosPorNombreLibro]    Script Date: 19/05/2024 06:23:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ObtenerVersiculosPorNombreLibro]
    @nombre_libro NVARCHAR(50),
	@version NVARCHAR(30)
AS
BEGIN
	DECLARE @id_version SMALLINT
	BEGIN TRY
		SELECT @id_version = Id_Version
		FROM DB_Bible.dbo.Versiones
		WHERE NombreVersion = @version;

		SELECT 
			L.nombre AS NombreLibro,
			V.NumeroCap,
			V.NumeroVers,
			CONCAT(L.nombre, ' ', V.NumeroCap, ':', V.NumeroVers, ' ', CAST(V.texto AS NVARCHAR(MAX))) AS Versiculo
		FROM DB_Bible.dbo.Versiculos V
		JOIN DB_Bible.dbo.Libros L ON V.id_libro = L.id_libro
		WHERE L.nombre = @nombre_libro
			AND V.Id_Version = @id_version;
	END TRY
	BEGIN CATCH
		THROW
	END CATCH
END;
/*
SELECT * FROM DB_Bible.dbo.Versiculos WHERE Id_Version = 3
SELECT * FROM DB_Bible.dbo.Versiones
SELECT * FROM DB_Bible.dbo.Libros WHERE Nombre = 'Genesis'
EXEC sp_ObtenerVersiculosPorNombreLibro 'Génesis', 'REINA VALERA 1960'
*/
