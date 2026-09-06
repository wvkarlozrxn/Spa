
-- 1. INVENTARIOS
CREATE TABLE inventarios (
[id_inventario] int PRIMARY KEY identity (1,1) NOT NULL,
[cantidad] int NOT NULL,
[stock_min] int NOT NULL,
);

INSERT INTO [inventarios] ([cantidad], [stock_min])
VALUES (100, 10);
INSERT INTO [inventarios] ([cantidad], [stock_min])
VALUES (50, 5);
INSERT INTO [inventarios] ([cantidad], [stock_min])
VALUES (200, 20);

-- 2. PRODUCTOS
CREATE TABLE productos (
[id_producto] int PRIMARY KEY identity (1,1) NOT NULL,
[nombre] nvarchar(150),
[descripcion] nvarchar (500),
[precio_compra] float,
[precio_venta] float,
[id_inventario] int not null FOREIGN KEY REFERENCES [inventarios](id_inventario),
);

INSERT INTO [productos] ([nombre], [descripcion], [precio_compra], [precio_venta], [id_inventario])
VALUES ('Shampoo hidratante', 'Shampoo para cabello seco', 15000, 25000, 1);
INSERT INTO [productos] ([nombre], [descripcion], [precio_compra], [precio_venta], [id_inventario])
VALUES ('Crema facial', 'Crema hidratante facial', 20000, 35000, 2);
INSERT INTO [productos] ([nombre], [descripcion], [precio_compra], [precio_venta], [id_inventario])
VALUES ('Aceite esencial', 'Aceite para masajes', 10000, 18000, 3);

-- 3. PROVEEDORES
CREATE TABLE [proveedores]
(
	[id_proveedor] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	[nombre_empresa] NVARCHAR(150) NOT NULL,
	[telefono] NVARCHAR(20) NOT NULL,
	[correo] NVARCHAR(100) NOT NULL UNIQUE,
	[direccion] NVARCHAR(500) NOT NULL,
);

INSERT INTO [proveedores] ([nombre_empresa], [telefono], [correo], [direccion])
VALUES ('Distribuidora braka-Belleza SAS', '3001234567', 'ventas@belleza.com', 'Cra 45 #12-30');
INSERT INTO [proveedores] ([nombre_empresa], [telefono], [correo], [direccion])
VALUES ('Cosméticos juan del Valle', '3109876543', 'cartera@cosmeticosvalle.com', 'Calle 10 #5-20');
INSERT INTO [proveedores] ([nombre_empresa], [telefono], [correo], [direccion])
VALUES ('Insumos Spa Ltda', '3204567890', 'info@insumospa.com', 'Av. Siempre Vive 100');

-- 4. COMPRAS --
CREATE TABLE [compras]
(
	[id_compra] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	[id_proveedor] INT NOT NULL FOREIGN KEY REFERENCES [proveedores]([id_proveedor]),
	[fecha] SMALLDATETIME NOT NULL,
	[compra] FLOAT NOT NULL,
	[total] FLOAT NOT NULL,
	[estado] CHAR(1) NOT NULL,--(P) pendiente, (A) aprobada--
);

INSERT INTO [compras] ([id_proveedor], [fecha], [compra], [total], [estado])
VALUES (1, GETDATE(), 150000, 150000, 'A');
INSERT INTO [compras] ([id_proveedor], [fecha], [compra], [total], [estado])
VALUES (2, GETDATE(), 200000, 200000, 'A');
INSERT INTO [compras] ([id_proveedor], [fecha], [compra], [total], [estado])
VALUES (3, GETDATE(), 90000, 90000, 'P');

-- 5. DETALLES_COMPRAS--
CREATE TABLE [detalles_compras]
(
	[id_detalle_compra] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	[id_compra] INT NOT NULL FOREIGN KEY REFERENCES [compras]([id_compra]),
	[id_producto] INT NOT NULL FOREIGN KEY REFERENCES [productos]([id_producto]),
	[cantidad] INT NOT NULL,
	[precio_unitario] FLOAT NOT NULL,
	[subtotal] FLOAT NOT NULL,
);

INSERT INTO [detalles_compras] ([id_compra], [id_producto], [cantidad], [precio_unitario], [subtotal])
VALUES (1, 1, 10, 15000, 150000);
INSERT INTO [detalles_compras] ([id_compra], [id_producto], [cantidad], [precio_unitario], [subtotal])
VALUES (2, 2, 10, 20000, 200000);
INSERT INTO [detalles_compras] ([id_compra], [id_producto], [cantidad], [precio_unitario], [subtotal])
VALUES (3, 3, 9, 10000, 90000);


