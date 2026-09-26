CREATE DATABASE DB_SPA
GO
USE DB_SPA
GO

-- 1. TABLA CARGOS
CREATE TABLE [Cargos] (
    [Id_Cargo] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Cargo] NVARCHAR(150) NULL
);

-- 2. TABLA INVENTARIOS
CREATE TABLE [Inventarios] (
    [Id_Inventario] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Cantidad] INT NOT NULL,
    [Stock_min] INT NOT NULL
);

-- 3. TABLA PRODUCTOS
CREATE TABLE [Productos] (
    [Id_Producto] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Nombre] NVARCHAR(150) NOT NULL,
    [Descripcion] NVARCHAR(500) NULL,
    [Precio_Compra] DECIMAL(18, 2) NOT NULL,
    [Precio_Venta] DECIMAL(18, 2) NOT NULL,
    [Id_Inventario] INT NOT NULL REFERENCES [Inventarios]([Id_Inventario])
);

-- 4. TABLA PROVEEDORES
CREATE TABLE [Proveedores] (
    [Id_Proveedor] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Nombre] NVARCHAR(150) NOT NULL,
    [Telefono] NVARCHAR(20) NOT NULL,
    [Correo] NVARCHAR(100) NOT NULL,
    [Direccion] NVARCHAR(500) NOT NULL
);

-- 5. TABLA COMPRAS
CREATE TABLE [Compras] (
    [Id_Compra] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Fecha] DATETIME NOT NULL,
    [Compra] DECIMAL(18, 2) NOT NULL,
    [Total] DECIMAL(18, 2) NOT NULL,
    [Estado] INT NOT NULL,
    [Id_Proveedor] INT NOT NULL REFERENCES [Proveedores]([Id_Proveedor])
);

-- 6. TABLA DETALLE_COMPRAS
CREATE TABLE [Detalle_Compras] (
    [Id_Detalle_Compra] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Cantidad] INT NOT NULL,
    [Precio_Unidad] DECIMAL(18, 2) NOT NULL,
    [Subtotal] DECIMAL(18, 2) NOT NULL,
    [Id_Compra] INT NOT NULL REFERENCES [Compras]([Id_Compra]),
    [Id_Producto] INT NOT NULL REFERENCES [Productos]([Id_Producto])
);

-- 7. TABLA PERSONAS
CREATE TABLE [Personas] (
    [Id_Persona] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Nombre] NVARCHAR(150) NOT NULL,
    [Telefono] NVARCHAR(20) NULL,
    [Cedula] NVARCHAR(20) NOT NULL
);

-- 8. TABLA CLIENTES
CREATE TABLE [Clientes] (
    [Id_Cliente] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Id_Persona] INT NOT NULL REFERENCES [Personas]([Id_Persona])
);

-- 9. TABLA EMPLEADOS
CREATE TABLE [Empleados] (
    [Id_Empleado] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Id_Persona] INT NOT NULL REFERENCES [Personas]([Id_Persona]),
    [Id_Cargo] INT NOT NULL REFERENCES [Cargos]([Id_Cargo]),
    [Fecha_Contrato] DATETIME NOT NULL
);

-- 10. TABLA TURNOS
CREATE TABLE [Turnos] (
    [Id_Turno] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Dia] DATETIME NOT NULL,
    [Hora_Entrada] DATETIME NOT NULL,
    [Hora_Salida] DATETIME NOT NULL
);

-- 11. TABLA EMPLEADOS_TURNOS
CREATE TABLE [Empleados_Turnos] (
    [Id_Empleado_Turno] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Id_Turno] INT NOT NULL REFERENCES [Turnos]([Id_Turno]),
    [Id_Empleado] INT NOT NULL REFERENCES [Empleados]([Id_Empleado])
);

-- 12. TABLA SERVICIOS
CREATE TABLE [Servicios] (
    [Id_Servicio] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Nombre] NVARCHAR(150) NOT NULL,
    [Descripcion] NVARCHAR(500) NULL,
    [Duracion] DATETIME NOT NULL,
    [Precio] DECIMAL(18, 2) NOT NULL
);

-- 13. TABLA RESERVAS
CREATE TABLE [Reservas] (
    [Id_Reserva] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Fecha] DATETIME NOT NULL,
    [Hora] DATETIME NOT NULL,
    [Estado] INT NOT NULL,
    [Id_Empleado] INT NOT NULL REFERENCES [Empleados]([Id_Empleado]),
    [Id_Cliente] INT NOT NULL REFERENCES [Clientes]([Id_Cliente])
);

-- 14. TABLA DETALLE_RESERVAS
CREATE TABLE [Detalle_Reservas] (
    [Id_Detalle_Reserva] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Cantidad] INT NOT NULL,
    [Precio] DECIMAL(18, 2) NOT NULL,
    [Id_Reserva] INT NOT NULL REFERENCES [Reservas]([Id_Reserva]),
    [Id_Servicio] INT NOT NULL REFERENCES [Servicios]([Id_Servicio])
);

-- 15. TABLA FACTURAS
CREATE TABLE [Facturas] (
    [Id_Factura] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Fecha] DATETIME NOT NULL,
    [Subtotal] DECIMAL(18, 2) NOT NULL,
    [Descuento] DECIMAL(18, 2) NOT NULL,
    [Total] DECIMAL(18, 2) NOT NULL,
    [Id_Reserva] INT NOT NULL REFERENCES [Reservas]([Id_Reserva])
);

-- 16. TABLA DETALLE_FACTURAS
CREATE TABLE [Detalle_Facturas] (
    [Id_Detalle_Factura] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Id_Factura] INT NOT NULL REFERENCES [Facturas]([Id_Factura]),
    [Id_Servicio] INT NOT NULL REFERENCES [Servicios]([Id_Servicio]),
    [Cantidad] INT NOT NULL,
    [Precio_Unidad] DECIMAL(18, 2) NOT NULL,
    [Subtotal] DECIMAL(18, 2) NOT NULL
);

-- 17. TABLA PAGOS
CREATE TABLE [Pagos] (
    [Id_Pago] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Fecha_Pago] DATETIME NOT NULL,
    [Id_Factura] INT NOT NULL REFERENCES [Facturas]([Id_Factura])
);

-- 18. TABLA METODOS_PAGOS
CREATE TABLE [Metodos_Pagos] (
    [Id_Detalle_Pago] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Descripcion] NVARCHAR(250) NULL,
    [Metodo_Pago] NVARCHAR(100) NULL,
    [Id_Pago] INT NOT NULL REFERENCES [Pagos]([Id_Pago])
);