USE CommunityGestDB;

CREATE TABLE [Usuario] (
  [ID] int PRIMARY KEY NOT NULL,
  [Cedula] varchar(10) NOT NULL,
  [Nombre] varchar(25) NOT NULL,
  [Apellidos] varchar(25) NOT NULL,
  [Correo] varchar(25) NOT NULL,
  [Telefono] varchar(10) NOT NULL
)
GO

CREATE TABLE [Herramienta] (
  [ID] int PRIMARY KEY NOT NULL,
  [Nombre] varchar(25) NOT NULL,
  [Tipo] varchar(25) NOT NULL,
  [Cantidad] int NOT NULL,
  [Disponibilidad] bit NOT NULL
)
GO

CREATE TABLE [Instalacion] (
  [ID] int PRIMARY KEY NOT NULL,
  [Tipo] varchar(25) NOT NULL,
  [Dia] varchar(20) NOT NULL,
  [Hora_Inicio] time NOT NULL,
  [Hora_Fin] time NOT NULL,
  [Disponibilidad] bit NOT NULL
)
GO

CREATE TABLE [Reporte] (
  [ID] int PRIMARY KEY NOT NULL,
  [Titulo] varchar(50) NOT NULL,
  [Descripción] varchar(100),
  [Recurso_Afectado] varchar(50) NOT NULL,
  [Estado] bit NOT NULL
)
GO

CREATE TABLE [Reserva_Herramienta] (
  [ID] int PRIMARY KEY NOT NULL,
  [Usuario_ID] int NOT NULL,
  [Herramienta_ID] int NOT NULL,
  [Fecha] date NOT NULL,
  [Hora_Inicio] time NOT NULL,
  [Hora_Fin] time NOT NULL,
  [Disponibilidad] varchar(25) NOT NULL
)
GO

CREATE TABLE [Reserva_Instalacion] (
  [ID] int PRIMARY KEY NOT NULL,
  [Usuario_ID] int NOT NULL,
  [Instalacion_ID] int NOT NULL,
  [Fecha] date NOT NULL,
  [Disponibilidad] varchar(25) NOT NULL
)
GO

ALTER TABLE [Reserva_Instalacion] ADD FOREIGN KEY ([Instalacion_ID]) REFERENCES [Instalacion] ([ID])
GO

ALTER TABLE [Reserva_Herramienta] ADD FOREIGN KEY ([Herramienta_ID]) REFERENCES [Herramienta] ([ID])
GO

ALTER TABLE [Reserva_Instalacion] ADD FOREIGN KEY ([Usuario_ID]) REFERENCES [Usuario] ([ID])
GO

ALTER TABLE [Reserva_Herramienta] ADD FOREIGN KEY ([Usuario_ID]) REFERENCES [Usuario] ([ID])
GO