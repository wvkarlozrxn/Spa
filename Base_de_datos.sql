
-- TABLA INVENTARIOS
CREATE TABLE Inventarios (
[Id] int PRIMARY KEY identity (1,1) NOT NULL,
[Cantidad] int NOT NULL,
[Stock_min] int NOT NULL,
);

INSERT INTO [Inventarios] ([Cantidad], [Stock_min])
VALUES (100, 10);
INSERT INTO [Inventarios] ([Cantidad], [Stock_min])
VALUES (50, 5);
INSERT INTO [Inventarios] ([Cantidad], [Stock_min])
VALUES (200, 20);

--  TABLA PRODUCTOS
CREATE TABLE Productos (
[Id] int PRIMARY KEY identity (1,1) NOT NULL,
[Nombre] nvarchar(150) not null,
[Descripcion] nvarchar (500) null,
[Precio_compra] decimal not null, --() preguntar la escala eje (10,2)
[Precio_venta] decimal not null, --() preguntar la escala eje (10,2)
[Inventario] int not null REFERENCES [inventarios](id),
);
/*
INSERT INTO [Productos] ([Nombre], [Descripcion], [Precio_compra], [Precio_venta], [Id])
VALUES ('Shampoo hidratante', 'Shampoo para cabello seco', 15000, 25000, 1);
INSERT INTO [Productos] ([Nombre], [Descripcion], [Precio_compra], [Precio_venta], [Id])
VALUES ('Crema facial', 'Crema hidratante facial', 20000, 35000, 2);
INSERT INTO [Productos] ([Nombre], [Descripcion], [Precio_compra], [Precio_venta], [Id])
VALUES ('Aceite esencial', 'Aceite para masajes', 10000, 18000, 3);
*/
-- TABLA PROVEEDORES
CREATE TABLE [Proveedores]
(
	[Id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	[Nombre] NVARCHAR(150) NOT NULL,
	[Telefono] NVARCHAR(20) NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL ,
	[Direccion] NVARCHAR(500) NOT NULL,
);

/*INSERT INTO [proveedores] ([nombre_empresa], [telefono], [correo], [direccion])
VALUES ('Distribuidora braka-Belleza SAS', '3001234567', 'ventas@belleza.com', 'Cra 45 #12-30');
INSERT INTO [proveedores] ([nombre_empresa], [telefono], [correo], [direccion])
VALUES ('Cosméticos juan del Valle', '3109876543', 'cartera@cosmeticosvalle.com', 'Calle 10 #5-20');
INSERT INTO [proveedores] ([nombre_empresa], [telefono], [correo], [direccion])
VALUES ('Insumos Spa Ltda', '3204567890', 'info@insumospa.com', 'Av. Siempre Vive 100');
*/
-- 4. COMPRAS --
CREATE TABLE [compras]
(
	[Id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	[Fecha] SMALLDATETIME NOT NULL, --VERIFICAR SI NO ES DATATIME 
	[Compra] DECIMAL NOT NULL,  --() preguntar la escala eje (10,2)
	[Total] DECIMAL NOT NULL, --() preguntar la escala eje (10,2)
	[Estado] CHAR(1) NOT NULL,--(P) pendiente, (A) aprobada--
	[Proveedor] INT NOT NULL REFERENCES [Proveedores]([Id]),
);
/*
INSERT INTO [compras] ([id_proveedor], [fecha], [compra], [total], [estado])
VALUES (1, GETDATE(), 150000, 150000, 'A');
INSERT INTO [compras] ([id_proveedor], [fecha], [compra], [total], [estado])
VALUES (2, GETDATE(), 200000, 200000, 'A');
INSERT INTO [compras] ([id_proveedor], [fecha], [compra], [total], [estado])
VALUES (3, GETDATE(), 90000, 90000, 'P');
*/
-- 5. DETALLES_COMPRAS--
CREATE TABLE [detalles_compras]
(
	[Id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	[Precio_unitario] decimal NOT NULL, --() preguntar la escala eje (10,2)
	[Subtotal] DECIMAL NOT NULL , --() preguntar la escala eje (10,2)
	[Cantidad] INT NOT NULL,
	[Compra] INT NOT NULL  REFERENCES [compras]([Id]),
	[producto] INT NOT NULL  REFERENCES [productos]([Id]),
);
/*
INSERT INTO [detalles_compras] ([id_compra], [id_producto], [cantidad], [precio_unitario], [subtotal])
VALUES (1, 1, 10, 15000, 150000);
INSERT INTO [detalles_compras] ([id_compra], [id_producto], [cantidad], [precio_unitario], [subtotal])
VALUES (2, 2, 10, 20000, 200000);
INSERT INTO [detalles_compras] ([id_compra], [id_producto], [cantidad], [precio_unitario], [subtotal])
VALUES (3, 3, 9, 10000, 90000);
*/

-- TABLA PERSONAS 

CREATE TABLE [Personas]
(
	[Id] int primary key identity (1, 1) not null,
	[Nombre] nvarchar (150) not null,
	[Telefono] nvarchar (150) null,
	[Cedula] nvarchar (150) not null,

);
--TABLA CLIENTES

CREATE TABLE [Clientes]
(
	[Id] int primary key identity(1, 1) not null,
	[Persona] int not null references [Personas](Id),

);
--TABLA EMPLEADOS
CREATE TABLE [Empleados]
(
	[Id] int primary key identity(1, 1) not null,
	[Fecha_Contrato] datetime not null, ---revisar si es smalldatetime o datetime
	[Persona] int not null references [Personas](Id),
	[Cargo] int not null references [Cargos](Id),

);

--TABLA EMPLEADOS TURNOS 
CREATE TABLE [Empleados_Turnos]
(
	[Id] int primary key identity(1, 1) not null,
	[Turno] int not null references [Turnos](Id),
	[Empleado] int not null references [Empleados](Id),

);

--TABLA TURNOS 
CREATE TABLE [Turnos]
(
	[Id] int primary key identity(1, 1) not null,
	[Dia] datetime not null, ---revisar si es smalldatetime o datetime
	[Hora_entrada] datetime not null, ---revisar si es smalldatetime o datetime
	[Hora_salida] datetime not null, ---revisar si es smalldatetime o datetime
);

--TABLA CARGOS
CREATE TABLE [Cargos]
(
	[Id] int primary key identity(1, 1) not null,
	[Descripcion] nvarchar (500) null,	
);

_______________________________________________________________________________

    create table [Servicios](
    [Id] int primary key identity (1,1),
    [Nombre] nvarchar (150)not null,
    [Descripcion] nvarchar(250)not null,
    [Duracion] smalldatetime not null,
    [Precio] double precision not null,
    )

    create table [Detalle_reservas](
    [Id] int primary key identity (1,1),
    [Cantidad] int not null,
    [Precio] double precision not null,
    [Servicio] int not null references [Servicios](Id),
    [Reserva] int not null references [Reservas](Id),
    )


    create table [Reservas] (
    [Id] int primary key identity (1,1),
    [Fecha] smalldatetime not null,
    [Hora] smalldatetime not null,
    [Estado] char not null,
    [Cliente] int not null references [Clientes](Id),
    [Empleado] int not null references [Empleados](Id),

    )
    
    create table [Facturas] (
    [Id] int primary key identity (1,1),
    [Descuento] double precision not null,
    [Total] double precision not null,
    [Subtotal]  double precision not null,
    [Fecha] smalldatetime not null,
    [Reserva] int not null references [Reservas](Id),
    ) 


    create table [Detalle_facturas](
    [Id] int primary key identity (1,1),
    [Cantidad] int not null,
    [Precio_unitario] double precision not null,
    [Subtotal]  double precision not null,
    [Servicio] int not null  references [Servicios](Id),
    [Factura] int not null  references [Facturas](Id),
    )


    create table [Metodos_pagos](
    [Id] int primary key identity (1,1),
    [Descripcion_metodo_pago] nvarchar(50),
    [Descripcion] nvarchar (50),
    [Pago] int not null references [Pagos](Id),
    )


    create table [Pagos](
    [Id] int primary key identity (1,1),
    [Fecha_pago] smalldatetime not null,
    [Factura] int not null references [Facturas](Id),
    )




