USE [DB_Proyecto]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerVersiculosPorNombreLibro]    Script Date: 19/05/2024 06:23:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ObtenerVersiculosPorNombreLibro]
    @nombre_libro NVARCHAR(50)
AS
BEGIN
    SELECT 
        L.nombre AS NombreLibro,
        V.NumeroCap,
        V.NumeroVers,
        CONCAT(L.nombre, ' ', V.NumeroCap, ':', V.NumeroVers, ' ', CAST(V.texto AS NVARCHAR(MAX))) AS Versiculo
    FROM DB_Bible.dbo.Versiculos V
    JOIN DB_Bible.dbo.Libros L ON V.id_libro = L.id_libro
    WHERE L.nombre = @nombre_libro;
END;
