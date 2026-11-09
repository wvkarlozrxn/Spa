-- ==========================================================
-- DATOS POR DEFECTO (el Id 1 de cada tabla queda para pruebas)
-- Pegar DESPUES de los inserts de prueba, en una base recien creada
-- ==========================================================
USE DB_SPA
GO

-- 1. CARGOS (Id 2, 3, 4)
INSERT INTO [Cargos] (Cargo) VALUES
('Masajista'),
('Esteticista'),
('Recepcionista');

-- 2. SERVICIOS (Id 2, 3, 4, 5) - Duracion en MINUTOS
INSERT INTO [Servicios] (Nombre, Descripcion, Duracion, Precio) VALUES
('Masaje relajante',  'Masaje de cuerpo completo con aceites esenciales', 120, 150000.00),
('Facial hidratante', 'Limpieza profunda e hidratacion del rostro',        60,  90000.00),
('Manicure',          'Manicure con esmalte tradicional',                  45,  40000.00),
('Pedicure',          'Pedicure con exfoliacion y esmalte',                60,  55000.00);

-- 3. TURNOS (Id 2 a 7): lunes a sábado, 10:00 a.m. a 8:00 p.m.
INSERT INTO [Turnos] (Dia, Hora_Entrada, Hora_Salida) VALUES
(1, 1000, 2000),
(2, 1000, 2000),
(3, 1000, 2000),
(4, 1000, 2000),
(5, 1000, 2000),
(6, 1000, 2000);

-- 4. PERSONAS de los empleados (Id 2, 3, 4, 5)
INSERT INTO [Personas] (Nombre, Telefono, Cedula) VALUES
('Laura Gomez',    '3101110001', '1030000001'),
('Camilo Rojas',   '3101110002', '1030000002'),
('Daniela Torres', '3101110003', '1030000003'),
('Sofia Ramirez',  '3101110004', '1030000004');

-- 5. EMPLEADOS (FK buscadas por cedula y nombre de cargo, no por Id fijo)
INSERT INTO [Empleados] (Id_Persona, Id_Cargo, Fecha_Contrato) VALUES
((SELECT Id_Persona FROM Personas WHERE Cedula = '1030000001'), (SELECT Id_Cargo FROM Cargos WHERE Cargo = 'Masajista'),     GETDATE()),
((SELECT Id_Persona FROM Personas WHERE Cedula = '1030000002'), (SELECT Id_Cargo FROM Cargos WHERE Cargo = 'Masajista'),     GETDATE()),
((SELECT Id_Persona FROM Personas WHERE Cedula = '1030000003'), (SELECT Id_Cargo FROM Cargos WHERE Cargo = 'Esteticista'),   GETDATE()),
((SELECT Id_Persona FROM Personas WHERE Cedula = '1030000004'), (SELECT Id_Cargo FROM Cargos WHERE Cargo = 'Recepcionista'), GETDATE());

-- 6. EMPLEADOS_TURNOS: todos los empleados nuevos trabajan todos los turnos
INSERT INTO [Empleados_Turnos] (Id_Turno, Id_Empleado)
SELECT t.Id_Turno, e.Id_Empleado
FROM Turnos t
CROSS JOIN Empleados e
WHERE t.Id_Turno > 1
  AND e.Id_Empleado > 1;

-- 7. PROVEEDORES (Id 2)
INSERT INTO [Proveedores] (Nombre, Telefono, Correo, Direccion) VALUES
('Aromas y Esencias SAS', '3205550000', 'pedidos@aromas.com', 'Carrera 15 # 45-20');

-- 8. INVENTARIOS (Id 2, 3) - uno por producto
INSERT INTO [Inventarios] (Cantidad, Stock_min) VALUES
(30, 5),
(20, 5);

-- 9. PRODUCTOS (usan los inventarios 2 y 3)
INSERT INTO [Productos] (Nombre, Descripcion, Precio_Compra, Precio_Venta, Id_Inventario) VALUES
('Aceite esencial de lavanda', 'Frasco 100ml',   18000.00, 30000.00, 2),
('Crema hidratante facial',    'Pote 250g',      22000.00, 38000.00, 3);
GO