USE CommunityGestDB;

-- CREATE TABLE [Usuario] (
--   [ID] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
--   [Cedula] varchar(10) NOT NULL,
--   [Nombre] varchar(25) NOT NULL,
--   [Apellidos] varchar(25) NOT NULL,
--   [Correo] varchar(25) NOT NULL,
--   [Telefono] varchar(10) NOT NULL
-- )
-- GO

-- CREATE TABLE [Herramienta] (
--   [ID] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
--   [Nombre] varchar(25) NOT NULL,
--   [Ubicacion] varchar(25) NOT NULL,
--   [Descripcion] nvarchar(MAX) NOT NULL,
--   [Cantidad] int NOT NULL,
--   [Disponibilidad] varchar(50) NOT NULL
-- )
-- GO

-- CREATE TABLE [Instalacion] (
--   [ID] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
--   [Nombre] varchar(50),
--   [Tipo] varchar(25) NOT NULL,
--   [Capacidad] int not null,
--   [Descripcion] NVARCHAR(MAX) not null,
--   [Dia] varchar(20) NOT NULL,
--   [Hora_Inicio] time NOT NULL,
--   [Hora_Fin] time NOT NULL,
--   [Disponibilidad] varchar(50) NOT NULL
-- )
-- GO

-- CREATE TABLE [Reporte] (
--   [ID] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
--   [Titulo] varchar(50) NOT NULL,
--   [Descripci�n] varchar(100),
--   [Recurso_Afectado] varchar(50) NOT NULL,
--   [Estado] bit NOT NULL
-- )
-- GO

-- CREATE TABLE [Reserva_Herramienta] (
--   [ID] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
--   [Usuario_ID] int NOT NULL,
--   [Herramienta_ID] int NOT NULL,
--   [Dia] NVARCHAR(50),
--   [Fecha] date NOT NULL,
--   [Hora_Inicio] time NOT NULL,
--   [Hora_Fin] time NOT NULL,
--   [Disponibilidad] varchar(25) NOT NULL
-- )
-- GO

-- CREATE TABLE [Reserva_Instalacion] (
--   [ID] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
--   [Usuario_ID] int NOT NULL,
--   [Instalacion_ID] int NOT NULL,
--   [Fecha] date NOT NULL,
--   [Disponibilidad] varchar(25) NOT NULL
-- )
-- GO

-- ALTER TABLE [Reserva_Instalacion] ADD FOREIGN KEY ([Instalacion_ID]) REFERENCES [Instalacion] ([ID])
-- GO

-- ALTER TABLE [Reserva_Herramienta] ADD FOREIGN KEY ([Herramienta_ID]) REFERENCES [Herramienta] ([ID])
-- GO

-- ALTER TABLE [Reserva_Instalacion] ADD FOREIGN KEY ([Usuario_ID]) REFERENCES [Usuario] ([ID])
-- GO

-- ALTER TABLE [Reserva_Herramienta] ADD FOREIGN KEY ([Usuario_ID]) REFERENCES [Usuario] ([ID])
-- GO

SELECT * FROM Usuario;
SELECT * FROM Herramienta;
SELECT * FROM Instalacion;
SELECT * FROM Reserva_Herramienta;
SELECT * FROM Reserva_Instalacion;
SELECT * FROM Reporte;

INSERT INTO Usuario (Cedula, Nombre, Apellido, Correo, Telefono)
VALUES ('0102030405', 'Juan', 'Perez', 'juanperez@mail.com', '0987654321'),
       ('0102030406', 'Maria', 'Lopez', 'marialopez@mail.com', '0987654322');

INSERT INTO Herramienta (Nombre, Ubicacion, Descripcion, Cantidad, Disponibilidad)
VALUES ('Martillo', 'Bodega 1', 'Martillo de construccion', 10, 'Disponible'),
       ('Destornillador', 'Bodega 2', 'Destornillador Phillips', 5, 'Disponible');

INSERT INTO Instalacion (Nombre, Tipo, Capacidad, Descripcion, Dia, Hora_Inicio, Hora_Fin, Disponibilidad)
VALUES ('Auditorio', 'Conferencia', 50, 'Auditorio para eventos y conferencias', 'Lunes', '08:00', '12:00', 'Disponible'),
       ('Sala de reuniones', 'Reunion', 10, 'Sala de reuniones para equipos pequenos', 'Martes', '10:00', '14:00', 'Disponible');

INSERT INTO Reporte (Titulo, Descripcion, Recurso_Afectado, Estado)
VALUES ('Problema con martillo', 'El martillo esta danado', 'Martillo', 0),
       ('Falta de sillas en auditorio', 'El auditorio no tiene suficientes sillas', 'Auditorio', 0);

INSERT INTO Reserva_Herramienta(Usuario_ID, Herramienta_ID,Dia, Fecha, Hora_Inicio, Hora_Fin, Disponibilidad)
VALUES 
    (1, 1, 'Lunes', '2025-01-21','08:00', '10:00', 'Reservada'),
    (2, 2, 'Martes', '2025-01-22','10:00', '12:00', 'Reservada');

-- UPDATE Reserva_Herramienta SET Fecha = '2025-01-21' WHERE ID = 1;
-- UPDATE Reserva_Herramienta SET Fecha = '2025-01-22' WHERE ID = 2;

INSERT INTO Reserva_Instalacion(Usuario_ID, Instalacion_ID, Fecha, Disponibilidad)
VALUES (1, 1, '2025-01-21', 'Reservada'),
       (2, 2, '2025-01-22', 'Reservada');

-- SELECT
--     r.ID AS ReservacionHerramientaId,
--     u.Nombre AS Usuario,
--     h.Nombre AS Herramienta,
--     r.Dia,
--     r.Fecha,
--     CONVERT(VARCHAR(5), r.Hora_Inicio, 108) AS Hora_Inicio,
--     CONVERT(VARCHAR(5), r.Hora_Fin, 108) AS Hora_Fin,
--     r.Disponibilidad
-- FROM
--     Reserva_Herramienta r
-- JOIN
--     Usuario u ON r.Usuario_ID= u.Id
-- JOIN
--     Herramienta h ON r.Herramienta_ID= h.Id;

-- SELECT
--     r.ID AS ReservacionInstalacionId,
--     u.Nombre AS Usuario,
--     i.Nombre AS Instalacion,
-- 	i.Dia AS Dia, 
-- 	CONVERT(VARCHAR(5), i.Hora_Inicio, 108) AS Hora_Inicio,
--     CONVERT(VARCHAR(5), i.Hora_Fin, 108) AS Hora_Fin,
--     r.Fecha,
--     r.Disponibilidad
-- FROM
--     Reserva_Instalacion r
-- JOIN
--     Usuario u ON r.Usuario_ID = u.Id
-- JOIN
--     Instalacion i ON r.Instalacion_ID = i.Id;