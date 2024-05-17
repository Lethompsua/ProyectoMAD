USE master; --Comprueba si existe la DB

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DB_Proyecto')
BEGIN
    CREATE DATABASE DB_Proyecto;
END
GO

USE DB_Proyecto;

--Borra todas las tablas y las vuelve a crear
IF OBJECT_ID('Usuarios') IS NOT NULL
BEGIN
	ALTER TABLE Historiales DROP CONSTRAINT fk_usuarios_historiales;
	ALTER TABLE Favoritos DROP CONSTRAINT fk_usuarios_favoritos;
	ALTER TABLE ContraseñasAntiguas DROP CONSTRAINT fk_usuarios_contraseñas;
	DROP TABLE Usuarios;
END
IF OBJECT_ID('Favoritos') IS NOT NULL
	DROP TABLE Favoritos;
IF OBJECT_ID('Historiales') IS NOT NULL
	DROP TABLE Historiales;
IF OBJECT_ID('ContraseñasAntiguas') IS NOT NULL
	DROP TABLE ContraseñasAntiguas;

--Para no usar ALTER, solo cambiar los CREATE y volver a ejecutar
CREATE TABLE Usuarios (
	id_usuario SMALLINT IDENTITY(1,1) NOT NULL,
	email VARCHAR(50) NOT NULL UNIQUE,
	password VARCHAR(50) NOT NULL,
	nombre_completo VARCHAR(50) NOT NULL,
	fecha_nacimiento DATE NOT NULL,
	genero VARCHAR(15) NOT NULL,
	fecha_registro DATETIME NOT NULL,

	fecha_login DATETIME,
	intentos SMALLINT DEFAULT 0,
	habilitado BIT DEFAULT 1,
	contraseña_temporal VARCHAR(50),

	estatus BIT DEFAULT 1, --1 = Activado, 0 = Desactivado
	fecha_baja DATE,

	tamaño_texto SMALLINT DEFAULT 12,
	idioma VARCHAR(10) DEFAULT 'Español',

	pregunta_seguridad VARCHAR(100) NOT NULL,
	respuesta_seguridad VARCHAR(100) NOT NULL

	CONSTRAINT pk_usuarios PRIMARY KEY (id_usuario),
);

CREATE TABLE Favoritos (
	id_favorito SMALLINT IDENTITY(1,1) NOT NULL,
	nombre VARCHAR(20) NOT NULL,
	fecha_registro DATETIME NOT NULL,
	libro VARCHAR(20) NOT NULL,
	capitulo VARCHAR(20) NOT NULL,
	version VARCHAR(20) NOT NULL,
	id_versiculo SMALLINT,
	id_usuario SMALLINT NOT NULL,

	CONSTRAINT pk_favoritos PRIMARY KEY (id_favorito),
	CONSTRAINT fk_usuarios_favoritos FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario)
);

CREATE TABLE Historiales (
	id_historial SMALLINT IDENTITY(1,1) NOT NULL,
	palabra TEXT NOT NULL,
	filtro_testamento VARCHAR(15),
	filtro_libro VARCHAR(20),
	filtro_version VARCHAR(15),
	fecha DATETIME NOT NULL,
	id_usuario SMALLINT NOT NULL,

	CONSTRAINT pk_historiales PRIMARY KEY (id_historial),
	CONSTRAINT fk_usuarios_historiales FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario)
);

CREATE TABLE ContraseñasAntiguas (
	id_contraseña SMALLINT IDENTITY(1,1) NOT NULL,
	id_usuario SMALLINT NOT NULL,
	password VARCHAR(50) NOT NULL,

	CONSTRAINT pk_contraseñas PRIMARY KEY (id_contraseña),
	CONSTRAINT fk_usuarios_contraseñas FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario)
);

SELECT * FROM Usuarios;
SELECT * FROM Historiales;