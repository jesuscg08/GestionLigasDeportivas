
-- Script Liga Deportiva

CREATE DATABASE LigaDeportiva;
GO

USE LigaDeportiva;
GO

-- Tabla Usuario
CREATE TABLE Usuario (
    UsuarioId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Correo NVARCHAR(100) UNIQUE,
    TipoUsuario NVARCHAR(50), -- 'Administrador', 'Entrenador', 'Jugador'
    Contrasena NVARCHAR(200),
    TokenRecuperacion NVARCHAR(200) NULL
);
GO

-- Tabla Liga
CREATE TABLE Liga (
    LigaId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    FechaInicio DATE,
    FechaFin DATE,
    NumeroJornadas INT
);
GO

-- Tabla Equipo
CREATE TABLE Equipo (
    EquipoId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    LigaId INT,
    FOREIGN KEY (LigaId) REFERENCES Liga(LigaId)
);
GO

-- Tabla JugadorEquipo
CREATE TABLE JugadorEquipo (
    Id INT PRIMARY KEY IDENTITY,
    UsuarioId INT,
    EquipoId INT,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId),
    FOREIGN KEY (EquipoId) REFERENCES Equipo(EquipoId)
);
GO

-- Tabla EntrenadorEquipo
CREATE TABLE EntrenadorEquipo (
    Id INT PRIMARY KEY IDENTITY,
    UsuarioId INT,
    EquipoId INT,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId),
    FOREIGN KEY (EquipoId) REFERENCES Equipo(EquipoId)
);
GO

-- Tabla Evento
CREATE TABLE Evento (
    EventoId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Fecha DATE,
    Hora TIME,
    EquipoLocalId INT,
    EquipoVisitanteId INT,
    Notificacion BIT DEFAULT 0,
    FOREIGN KEY (EquipoLocalId) REFERENCES Equipo(EquipoId),
    FOREIGN KEY (EquipoVisitanteId) REFERENCES Equipo(EquipoId)
);
GO

-- Tabla Estadistica
CREATE TABLE Estadistica (
    EstadisticaId INT PRIMARY KEY IDENTITY,
    EventoId INT,
    JugadorId INT,
    EquipoId INT,
    Goles INT,
    Asistencias INT,
    TarjetasRojas INT,
    TarjetasAmarillas INT,
    FOREIGN KEY (EventoId) REFERENCES Evento(EventoId),
    FOREIGN KEY (JugadorId) REFERENCES Usuario(UsuarioId),
    FOREIGN KEY (EquipoId) REFERENCES Equipo(EquipoId)
);
GO

-- Tabla PerfilJugador
CREATE TABLE PerfilJugador (
    PerfilJugadorId INT PRIMARY KEY IDENTITY,
    UsuarioId INT,
    EquipoId INT,
    CantidadGoles INT,
    CantidadPartidos INT,
    TarjetasAmarillas INT,
    TarjetasRojas INT,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId),
    FOREIGN KEY (EquipoId) REFERENCES Equipo(EquipoId)
);
GO

-- Tabla PerfilEquipo
CREATE TABLE PerfilEquipo (
    PerfilEquipoId INT PRIMARY KEY IDENTITY,
    EquipoId INT,
    LigaId INT,
    CantidadGoles INT,
    TarjetasAmarillas INT,
    TarjetasRojas INT,
    FOREIGN KEY (EquipoId) REFERENCES Equipo(EquipoId),
    FOREIGN KEY (LigaId) REFERENCES Liga(LigaId)
);
GO

-- Tabla Entrenamiento
CREATE TABLE Entrenamiento (
    EntrenamientoId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Fecha DATE,
    Detalle NVARCHAR(500),
    Notificacion BIT DEFAULT 0
);
GO

-- Tabla AsistenciaEntrenamiento
CREATE TABLE AsistenciaEntrenamiento (
    AsistenciaId INT PRIMARY KEY IDENTITY,
    EntrenamientoId INT,
    UsuarioId INT,
    EstadoAsistencia NVARCHAR(50), -- 'Presente', 'Ausente', etc.
    FOREIGN KEY (EntrenamientoId) REFERENCES Entrenamiento(EntrenamientoId),
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId)
);
GO
