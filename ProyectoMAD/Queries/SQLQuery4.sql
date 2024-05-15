

CREATE VIEW VistaVersiculos AS
SELECT CONCAT(Libros.nombre, ' ', Versiculos.NumeroCap, ':', Versiculos.NumeroVers, ' ', Versiculos.texto) AS VersiculoFormateado
FROM DB_Bible.dbo.Versiculos
JOIN DB_Bible.dbo.Libros ON Versiculos.id_libro = Libros.id_libro;



