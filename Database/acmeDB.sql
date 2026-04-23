-- =============================================
-- CREAR BASE DE DATOS
-- =============================================
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DBACME')
BEGIN
    CREATE DATABASE DBACME;
END
GO

USE DBACME;
GO

-- =============================================
-- TABLA USUARIO
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'Usuario' AND type = 'U')
BEGIN
    CREATE TABLE Usuario (
        Codigo VARCHAR(20) PRIMARY KEY,
        Nombre VARCHAR(50) NOT NULL,
        Clave VARCHAR(200) NOT NULL,
        Fecha DATETIME DEFAULT GETDATE(),
        Token VARCHAR(200)
    );
END
GO

-- =============================================
-- TABLA ENCUESTA
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'Encuesta' AND type = 'U')
BEGIN
    CREATE TABLE Encuesta (
        Codigo INT IDENTITY(1,1) PRIMARY KEY,
        CodigoUsuario VARCHAR(20) NOT NULL,
        Nombre VARCHAR(100) NOT NULL,
        Descripcion VARCHAR(300),
        CodigoUnico VARCHAR(20) NOT NULL,
        FechaCreacion DATETIME2 DEFAULT GETDATE(),

        CONSTRAINT FK_Encuesta_Usuario 
            FOREIGN KEY (CodigoUsuario) REFERENCES Usuario(Codigo),

        CONSTRAINT UQ_Encuesta_CodigoUnico 
            UNIQUE (CodigoUnico)
    );
END
GO

-- =============================================
-- TABLA CAMPO (PREGUNTAS)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'Campo' AND type = 'U')
BEGIN
    CREATE TABLE Campo (
        Codigo INT IDENTITY(1,1) PRIMARY KEY,
        CodigoEncuesta INT NOT NULL,
        Nombre VARCHAR(100),
        Titulo VARCHAR(100),
        Requerido BIT DEFAULT 0,
        Tipo VARCHAR(20) NOT NULL,

        CONSTRAINT FK_Campo_Encuesta 
            FOREIGN KEY (CodigoEncuesta) REFERENCES Encuesta(Codigo)
    );
END
GO

-- =============================================
-- TABLA RESPUESTA
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'Respuesta' AND type = 'U')
BEGIN
    CREATE TABLE Respuesta (
        Codigo INT IDENTITY(1,1) PRIMARY KEY,
        CodigoEncuesta INT NOT NULL,
        CodigoCampos INT NOT NULL,
        FechaCreacion DATETIME2 DEFAULT GETDATE(),
        Resultado VARCHAR(500),

        CONSTRAINT FK_Respuesta_Encuesta 
            FOREIGN KEY (CodigoEncuesta) REFERENCES Encuesta(Codigo),

        CONSTRAINT FK_Respuesta_Campo 
            FOREIGN KEY (CodigoCampos) REFERENCES Campo(Codigo)
    );
END
GO