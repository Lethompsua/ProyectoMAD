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
	ALTER TABLE Usuarios DROP CONSTRAINT fk_generos_usuarios;
	ALTER TABLE Historiales DROP CONSTRAINT fk_usuarios_historiales;
	ALTER TABLE Favoritos DROP CONSTRAINT fk_usuarios_favoritos;
	ALTER TABLE ContraseñasAntiguas DROP CONSTRAINT fk_usuarios_contraseñas;
	DROP TABLE Usuarios;
END
IF OBJECT_ID('Generos') IS NOT NULL
	DROP TABLE Generos;
IF OBJECT_ID('Favoritos') IS NOT NULL
	DROP TABLE Favoritos;
IF OBJECT_ID('Historiales') IS NOT NULL
	DROP TABLE Historiales;
IF OBJECT_ID('ContraseñasAntiguas') IS NOT NULL
	DROP TABLE ContraseñasAntiguas;

--Para no usar ALTER, solo cambiar los CREATE y volver a ejecutar
CREATE TABLE Generos (
	id_genero SMALLINT IDENTITY(1,1),
	nombre VARCHAR(15),

	CONSTRAINT pk_generos PRIMARY KEY (id_genero)
);

CREATE TABLE Usuarios (
	id_usuario SMALLINT IDENTITY(1,1) NOT NULL,
	email VARCHAR(50) NOT NULL,
	password VARCHAR(50) NOT NULL,
	nombre_completo VARCHAR(50) NOT NULL,
	fecha_nacimiento DATE NOT NULL,
	id_genero SMALLINT NOT NULL,
	fecha_registro DATETIME NOT NULL,

	fecha_login DATETIME,
	intentos SMALLINT DEFAULT 0,
	habilitado BIT NOT NULL,
	contraseña_temporal VARCHAR(8),

	estatus BIT NOT NULL,
	fecha_baja DATE,

	tamaño_texto SMALLINT,
	idioma VARCHAR(10),

	pregunta_seguridad VARCHAR(50) NOT NULL,
	respuesta_seguridad TEXT NOT NULL

	CONSTRAINT pk_usuarios PRIMARY KEY (id_usuario),
	CONSTRAINT fk_generos_usuarios FOREIGN KEY (id_genero) REFERENCES Generos(id_genero)
);

CREATE TABLE Favoritos (
	id_favorito SMALLINT IDENTITY(1,1) NOT NULL,
	nombre VARCHAR(20) NOT NULL,
	fecha_registro DATETIME NOT NULL,
	libro VARCHAR(20) NOT NULL,
	capitulo VARCHAR(20) NOT NULL,
	texto_versiculo TEXT NOT NULL,
	id_versiculo SMALLINT,
	id_usuario SMALLINT NOT NULL,

	CONSTRAINT pk_favoritos PRIMARY KEY (id_favorito),
	CONSTRAINT fk_usuarios_favoritos FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario)
);

CREATE TABLE Historiales (
	id_historial SMALLINT IDENTITY(1,1) NOT NULL,
	búsqueda TEXT NOT NULL,
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

-- Habilitar IDENTITY_INSERT
SET IDENTITY_INSERT Generos ON;

-- Insertar valores explícitos en la columna de identidad
INSERT INTO Generos (id_genero, nombre)
VALUES (1, 'Hombre'),
       (0, 'Mujer');

-- Deshabilitar IDENTITY_INSERT
SET IDENTITY_INSERT Generos OFF;