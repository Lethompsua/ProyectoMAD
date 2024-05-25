USE DB_Proyecto
GO

CREATE OR ALTER TRIGGER InsertIdVersFavorito
	ON Favoritos
	AFTER INSERT
AS
BEGIN
	DECLARE @id_inserted SMALLINT
	DECLARE @id_fav SMALLINT

	SELECT @id_inserted = id_versiculo,
		@id_fav = id_favorito
	FROM inserted

	IF @id_inserted IS NULL
	BEGIN
		UPDATE Favoritos
		SET id_versiculo = 0
		WHERE id_favorito = @id_fav
	END
END
GO

CREATE OR ALTER TRIGGER InsertOldPassword
	ON Usuarios
	AFTER UPDATE
AS
BEGIN
	DECLARE @passwordCambiada VARCHAR(50)
	DECLARE @id_user SMALLINT

	SELECT @passwordCambiada = password,
		@id_user = id_usuario
	FROM deleted

	IF @passwordCambiada IS NOT NULL
	BEGIN
		INSERT INTO ContraseñasAntiguas (id_usuario, password)
		VALUES (@id_user, @passwordCambiada)
	END
END
GO

CREATE OR ALTER TRIGGER InsertTestamentoYLibroHistorial
	ON Historiales
	AFTER INSERT
AS
BEGIN
	DECLARE @testamento VARCHAR(50)
	DECLARE @libro VARCHAR(50)
	DECLARE @id_hist SMALLINT

	SELECT @testamento = filtro_testamento,
		@libro = filtro_libro,
		@id_hist = id_historial
	FROM inserted

	IF @testamento = '' OR @testamento IS NULL
	BEGIN
		UPDATE Historiales
		SET filtro_testamento = 'N/A'
		WHERE id_historial = @id_hist
	END
	IF @libro = '' OR @libro IS NULL
	BEGIN
		UPDATE Historiales
		SET filtro_libro = 'N/A'
		WHERE id_historial = @id_hist
	END
END
GO

/*
INSERT INTO Usuarios (email, password, nombre_completo, fecha_nacimiento, genero, fecha_registro, pregunta_seguridad, respuesta_seguridad)
VALUES ('usuario1@example.com', 'Password#1', 'Usuario Uno', '1990-01-01', 'Masculino', GETDATE(), 'Nombre de tu mascota', 'Rex')
INSERT INTO Usuarios (email, password, nombre_completo, fecha_nacimiento, genero, fecha_registro, pregunta_seguridad, respuesta_seguridad)
VALUES ('usuario2@example.com', 'Password#2', 'Usuario Dos', '1985-05-15', 'Femenino', GETDATE(), 'Nombre de tu mejor amigo', 'Carlos')
INSERT INTO Usuarios (email, password, nombre_completo, fecha_nacimiento, genero, fecha_registro, pregunta_seguridad, respuesta_seguridad)
VALUES ('usuario3@example.com', 'Password#3', 'Usuario Tres', '2000-10-20', 'Masculino', GETDATE(), 'Ciudad de nacimiento', 'Paris');

INSERT INTO Favoritos (nombre, fecha_registro, libro, capitulo, version, id_usuario)
VALUES ('Favorito 1', GETDATE(), 'Ruth', 3, 'REINA VALERA 1960', 1)
INSERT INTO Favoritos (nombre, fecha_registro, libro, capitulo, version, id_usuario)
VALUES ('Favorito 2', GETDATE(), 'Esther', 5, 'REINA VALERA 1960', 2)
INSERT INTO Favoritos (nombre, fecha_registro, libro, capitulo, version, id_usuario)
VALUES ('Favorito 3', GETDATE(), 'Génesis', 12, 'REINA VALERA 1960', 3);

INSERT INTO Historiales (palabra, filtro_testamento, filtro_libro, filtro_version, fecha, id_usuario)
VALUES ('amor', 'ANTIGUO TESTAMENTO', 'Hageo', 'REINA VALERA 1960', GETDATE(), 1)
INSERT INTO Historiales (palabra, filtro_testamento, filtro_libro, filtro_version, fecha, id_usuario)
VALUES('paz', NULL, NULL, 'REINA VALERA 1960', GETDATE(), 2)
INSERT INTO Historiales (palabra, filtro_testamento, filtro_libro, filtro_version, fecha, id_usuario)
VALUES ('esperanza', 'NUEVO TESTAMENTO', 'Romanos', 'REINA VALERA 1960', GETDATE(), 3);

INSERT INTO ContraseñasAntiguas (id_usuario, password)
VALUES (1, 'oldpassword1')
INSERT INTO ContraseñasAntiguas (id_usuario, password)
VALUES (2, 'oldpassword2')
INSERT INTO ContraseñasAntiguas (id_usuario, password)
VALUES (3, 'oldpassword3');
*/

/*
SELECT * FROM Usuarios
SELECT * FROM Favoritos
SELECT * FROM Historiales
SELECT * FROM ContraseñasAntiguas
*/