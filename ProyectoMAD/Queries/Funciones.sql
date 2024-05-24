USE DB_Proyecto;
GO

CREATE OR ALTER FUNCTION GetUser (@email VARCHAR(50))
RETURNS SMALLINT
AS
BEGIN
	DECLARE @id SMALLINT;

	SELECT @id = id_usuario
		FROM Usuarios
		WHERE email = @email;

	RETURN @id;
END
GO

CREATE OR ALTER FUNCTION GetQuestion (@id SMALLINT)
RETURNS VARCHAR(100)
AS
BEGIN
	DECLARE @question VARCHAR(100);
	SELECT @question = pregunta_seguridad
		FROM Usuarios
		WHERE id_usuario = @id;

	RETURN @question;
END
GO

CREATE OR ALTER FUNCTION GetSize(@id SMALLINT)
RETURNS SMALLINT
AS
BEGIN
	DECLARE @size SMALLINT
	SELECT @size = tamaño_texto
	FROM Usuarios
	WHERE id_usuario = @id

	RETURN @size
END
GO

CREATE OR ALTER FUNCTION GetVersiculoID(@text VARCHAR(MAX))
RETURNS SMALLINT
AS
BEGIN
	DECLARE @id SMALLINT
	SELECT @id = Id_Vers
	FROM DB_Bible.dbo.Versiculos
	WHERE CAST(Texto AS VARCHAR(MAX)) = @text

	RETURN @id
END
GO