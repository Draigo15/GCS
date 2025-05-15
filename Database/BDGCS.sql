 -- Crear la base de datos si no existe
IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'BDGCS')
BEGIN
    CREATE DATABASE BDGCS;
END;
GO

-- Usar la base de datos
USE BDGCS;
GO

/****** Object:  Table [dbo].[Elemento_Configuracion]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Elemento_Configuracion](
	[Id_elementoconfiguracion] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Nomenclatura] [varchar](50) NULL,
	[Estado] [varchar](1)NULL,
	[Id_fase] [int] NOT NULL,
 CONSTRAINT [PK_Elemento_Configuracion] PRIMARY KEY CLUSTERED 
(
	[Id_elementoconfiguracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fase]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fase](
	[Id_fase] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](255) NULL,
	[Id_metodologia] [int] NOT NULL,
 CONSTRAINT [PK_Fase] PRIMARY KEY CLUSTERED 
(
	[Id_fase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metodologia]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metodologia](
	[Id_metodologia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Metodologia] PRIMARY KEY CLUSTERED 
(
	[Id_metodologia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembro_Elemento]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miembro_Elemento](
	[Id_miembroelemento] [int] IDENTITY(1,1) NOT NULL,
	[Id_miembro] [int] NOT NULL,
	[Id_elementoconfiguracion] [int] NOT NULL,
	[Url] [varchar](50) NULL,
	[FechaInicio] [varchar](50) NULL,
	[Fechafin] [varchar](50) NULL,
 CONSTRAINT [PK_Miembro_Elemento] PRIMARY KEY CLUSTERED 
(
	[Id_miembroelemento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembro_Proyecto]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miembro_Proyecto](
	[Id_miembro] [int] IDENTITY(1,1) NOT NULL,
	[Id_usuario] [int] NOT NULL,
	[Id_rol] [int] NOT NULL,
	[Id_proyecto] [int] NOT NULL,
 CONSTRAINT [PK_Miembro_Proyecto] PRIMARY KEY CLUSTERED 
(
	[Id_miembro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proyecto]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proyecto](
	[Id_proyecto] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[FechaInicio] [varchar](50) NULL,
	[FechaTermino] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[Id_metodologia] [int] NOT NULL,
	[Id_solicitud_cambios] [int] NOT NULL,
 CONSTRAINT [PK_Proyecto] PRIMARY KEY CLUSTERED 
(
	[Id_proyecto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Tabla intermedia: Elemento_Proyecto ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Elemento_Proyecto](
    [Id_elemento_proyecto] INT IDENTITY(1,1) NOT NULL,
    [Id_proyecto] INT NOT NULL,
    [Id_elementoconfiguracion] INT NOT NULL,
    [Estado] VARCHAR(1) NOT NULL DEFAULT 'A', -- 'A' = Activo, 'I' = Inactivo
    CONSTRAINT [PK_Elemento_Proyecto] PRIMARY KEY CLUSTERED ([Id_elemento_proyecto] ASC)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Elemento_Proyecto]  WITH CHECK 
ADD CONSTRAINT [FK_Elemento_Proyecto_Proyecto]
FOREIGN KEY([Id_proyecto])
REFERENCES [dbo].[Proyecto] ([Id_proyecto])
GO

ALTER TABLE [dbo].[Elemento_Proyecto] CHECK CONSTRAINT [FK_Elemento_Proyecto_Proyecto]
GO

ALTER TABLE [dbo].[Elemento_Proyecto]  WITH CHECK 
ADD CONSTRAINT [FK_Elemento_Proyecto_Elemento_Configuracion]
FOREIGN KEY([Id_elementoconfiguracion])
REFERENCES [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion])
GO

ALTER TABLE [dbo].[Elemento_Proyecto] CHECK CONSTRAINT [FK_Elemento_Proyecto_Elemento_Configuracion]
GO

/****** Object:  Table [dbo].[Rol]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[Id_rol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[Id_rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Solicitud]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solicitud](
	[Id_solicitud] [int] IDENTITY(1,1) NOT NULL,
	[Requerimiento] [varchar](50) NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Solicitud] PRIMARY KEY CLUSTERED 
(
	[Id_solicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Solicitud_Cambios]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solicitud_Cambios](
	[Id_solicitud_cambios] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [varchar](50) NULL,
	[Objetivo] [varchar](50) NULL,
	[Descripcion] [varchar](50) NULL,
	[Respuesta] [varchar](50) NULL,
	[Id_solicitud] [int] NOT NULL,
 CONSTRAINT [PK_Solicitud_Cambios] PRIMARY KEY CLUSTERED 
(
	[Id_solicitud_cambios] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Usuario]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Usuario](
	[Id_tipousuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Tipo_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id_tipousuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 03/12/2021 18:00:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[Correo] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[Id_tipousuario] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Elemento_Configuracion] ON 

INSERT [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion], [Codigo], [Nombre], [Nomenclatura], [Id_fase]) VALUES (1, N'SAD', N'SAD', N'SAD', 1)
INSERT [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion], [Codigo], [Nombre], [Nomenclatura], [Id_fase]) VALUES (4, N'SRSS', N'SRSS', N'SRSS', 2)
INSERT [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion], [Codigo], [Nombre], [Nomenclatura], [Id_fase]) VALUES (5, N'EDT', N'EDT', N'EDT', 3)
INSERT [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion], [Codigo], [Nombre], [Nomenclatura], [Id_fase]) VALUES (9, N'SAD', N'SAD', N'SAD', 26)
INSERT [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion], [Codigo], [Nombre], [Nomenclatura], [Id_fase]) VALUES (13, N'SAD', N'SAD', N'SAD', 21)
INSERT [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion], [Codigo], [Nombre], [Nomenclatura], [Id_fase]) VALUES (14, N'SAD', N'CASCADA', N'SAD', 11)
INSERT [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion], [Codigo], [Nombre], [Nomenclatura], [Id_fase]) VALUES (15, N'EDT', N'EDT', N'EDT', 11)
SET IDENTITY_INSERT [dbo].[Elemento_Configuracion] OFF
GO
SET IDENTITY_INSERT [dbo].[Fase] ON 

INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (1, N'INICIO', 1)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (2, N'ELABORACION', 1)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (3, N'CONSTRUCCION', 1)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (4, N'ELABORACION', 1)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (5, N'INICIO', 4)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (6, N'ELABORACION', 4)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (7, N'CONSTRUCCION', 4)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (8, N'TRANSICION', 4)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (9, N'ANALISIS', 4)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (10, N'DISEÑO', 4)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (11, N'REQUISITOS', 5)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (12, N'DISEÑO', 5)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (13, N'IMPLEMENTACION', 5)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (14, N'VERIFICACION', 5)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (15, N'MANTENIMIENTO', 5)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (16, N'FASES', 6)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (17, N'DOCUMENTACION', 6)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (18, N'TECNICA Y HERRAMIENTAS', 6)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (19, N'METODOS', 6)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (20, N'CONTROL Y EVALUACION', 6)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (21, N'REVISION', 2)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (22, N'DESPLEGAR', 2)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (23, N'TESTEO', 2)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (24, N'DESARROLLO', 2)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (25, N'DISEÑO', 2)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (26, N'FASES', 7)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (27, N'DOCUMENTACION', 7)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (28, N'TESTEO', 7)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (29, N'DESARROLLO', 7)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (30, N'DISEÑO', 7)
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Id_metodologia]) VALUES (31, N'COMPLEMENTO', 1)
SET IDENTITY_INSERT [dbo].[Fase] OFF
GO
SET IDENTITY_INSERT [dbo].[Metodologia] ON 

INSERT [dbo].[Metodologia] ([Id_metodologia], [Nombre]) VALUES (1, N'RUP')
INSERT [dbo].[Metodologia] ([Id_metodologia], [Nombre]) VALUES (2, N'AGIL')
INSERT [dbo].[Metodologia] ([Id_metodologia], [Nombre]) VALUES (3, N'XP')
INSERT [dbo].[Metodologia] ([Id_metodologia], [Nombre]) VALUES (4, N'AUP')
INSERT [dbo].[Metodologia] ([Id_metodologia], [Nombre]) VALUES (5, N'CASCADA')
INSERT [dbo].[Metodologia] ([Id_metodologia], [Nombre]) VALUES (6, N'SCRUM')
INSERT [dbo].[Metodologia] ([Id_metodologia], [Nombre]) VALUES (7, N'SCRUM/AGIL')
SET IDENTITY_INSERT [dbo].[Metodologia] OFF
GO
SET IDENTITY_INSERT [dbo].[Miembro_Elemento] ON 

INSERT [dbo].[Miembro_Elemento] ([Id_miembroelemento], [Id_miembro], [Id_elementoconfiguracion], [Url], [FechaInicio], [Fechafin]) VALUES (1, 1, 1, N'https://acortar.link/lMmXjU', N'15-05-2023', N'10-08-2023')
INSERT [dbo].[Miembro_Elemento] ([Id_miembroelemento], [Id_miembro], [Id_elementoconfiguracion], [Url], [FechaInicio], [Fechafin]) VALUES (4, 2, 4, N'https://acortar.link/lMmXjU', N'15-05-2023', N'10-08-2023')

SET IDENTITY_INSERT [dbo].[Miembro_Elemento] OFF
GO
SET IDENTITY_INSERT [dbo].[Miembro_Proyecto] ON 

INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (1, 4, 3, 1)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (2, 2, 4, 1)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (3, 3, 2, 1)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (4, 6, 1, 6)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (5, 5, 2, 6)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (6, 7, 3, 6)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (7, 1, 1, 7)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (14, 1, 5, 4)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (16, 6, 1, 1)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (17, 7, 3, 1)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (18, 1, 1, 5)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (19, 4, 2, 5)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (20, 2, 3, 5)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (21, 1, 1, 8)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (22, 2, 2, 8)
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto]) VALUES (23, 3, 3, 8)
SET IDENTITY_INSERT [dbo].[Miembro_Proyecto] OFF
GO
SET IDENTITY_INSERT [dbo].[Proyecto] ON 

INSERT [dbo].[Proyecto] ([Id_proyecto], [Codigo], [Nombre], [FechaInicio], [FechaTermino], [Estado], [Id_metodologia], [Id_solicitud_cambios]) VALUES (1, N'001', N'Sistema Veterinaria', N'02/12/2021', N'02/02/2022', N'A', 1, 1)
INSERT [dbo].[Proyecto] ([Id_proyecto], [Codigo], [Nombre], [FechaInicio], [FechaTermino], [Estado], [Id_metodologia], [Id_solicitud_cambios]) VALUES (4, N'002', N'Sistema Farmacia', N'02/12/2022', N'02/02/2022', N'A', 2, 6)
INSERT [dbo].[Proyecto] ([Id_proyecto], [Codigo], [Nombre], [FechaInicio], [FechaTermino], [Estado], [Id_metodologia], [Id_solicitud_cambios]) VALUES (5, N'005', N'Sistema Asistencia', N'2021-12-03', N'2021-12-03', N'A', 1, 1)
INSERT [dbo].[Proyecto] ([Id_proyecto], [Codigo], [Nombre], [FechaInicio], [FechaTermino], [Estado], [Id_metodologia], [Id_solicitud_cambios]) VALUES (6, N'0055', N'Sistema Reservas Medicas', N'10/03/2021', N'10/12/2021', N'A', 7, 13)

-- Insertar nuevos roles
SET IDENTITY_INSERT [dbo].[Rol] ON;

INSERT [dbo].[Rol] ([Id_rol], [Nombre]) VALUES (1, N'Jefe de proyecto');
INSERT [dbo].[Rol] ([Id_rol], [Nombre]) VALUES (2, N'Arquitecto de Software');
INSERT [dbo].[Rol] ([Id_rol], [Nombre]) VALUES (3, N'Agente del Comité de Cambio');
INSERT [dbo].[Rol] ([Id_rol], [Nombre]) VALUES (4, N'Solicitante');
INSERT [dbo].[Rol] ([Id_rol], [Nombre]) VALUES (5, N'Ingeniero de software');
INSERT [dbo].[Rol] ([Id_rol], [Nombre]) VALUES (6, N'Ingeniero de pruebas');
INSERT [dbo].[Rol] ([Id_rol], [Nombre]) VALUES (7, N'Bibliotecario');

SET IDENTITY_INSERT [dbo].[Rol] OFF;
GO

SET IDENTITY_INSERT [dbo].[Solicitud] ON 

INSERT [dbo].[Solicitud] ([Id_solicitud], [Requerimiento], [Descripcion]) VALUES (1, N'Veterinaria', N'Cambiar Requerimientos')
INSERT [dbo].[Solicitud] ([Id_solicitud], [Requerimiento], [Descripcion]) VALUES (2, N'Farmacia', N'Cambiar Requerimientos')
INSERT [dbo].[Solicitud] ([Id_solicitud], [Requerimiento], [Descripcion]) VALUES (3, N'Agro', N'Cambiar Requerimientos')

SET IDENTITY_INSERT [dbo].[Solicitud] OFF
GO
SET IDENTITY_INSERT [dbo].[Solicitud_Cambios] ON 

INSERT [dbo].[Solicitud_Cambios] ([Id_solicitud_cambios], [Fecha], [Objetivo], [Descripcion], [Respuesta], [Id_solicitud]) VALUES (1, N'02/12/2021', N'Cambiar req del sistema veterinaria', N'Cambiar req del sistema veterinaria', N'Positiva', 1)
INSERT [dbo].[Solicitud_Cambios] ([Id_solicitud_cambios], [Fecha], [Objetivo], [Descripcion], [Respuesta], [Id_solicitud]) VALUES (6, N'02/12/2021', N'Cambiar req del sistema Farmacia', N'Cambiar req del sistema Farmacia', N'Positiva', 2)
INSERT [dbo].[Solicitud_Cambios] ([Id_solicitud_cambios], [Fecha], [Objetivo], [Descripcion], [Respuesta], [Id_solicitud]) VALUES (8, N'10/03/2022', N'Cambiar req del sistema veterinaria', N'Cambiar req del sistema veterinaria', N'Positiva', 1)
INSERT [dbo].[Solicitud_Cambios] ([Id_solicitud_cambios], [Fecha], [Objetivo], [Descripcion], [Respuesta], [Id_solicitud]) VALUES (9, N'10/03/2022', N'Cambiar req del sistema veterinaria', N'Cambiar req del sistema veterinaria', N'Positiva', 1)
INSERT [dbo].[Solicitud_Cambios] ([Id_solicitud_cambios], [Fecha], [Objetivo], [Descripcion], [Respuesta], [Id_solicitud]) VALUES (10, N'10/03/2022', N'Cambiar req del sistema Farmacia', N'Cambiar req del sistema Farmacia', N'Positiva', 2)

SET IDENTITY_INSERT [dbo].[Solicitud_Cambios] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipo_Usuario] ON 

INSERT [dbo].[Tipo_Usuario] ([Id_tipousuario], [Nombre]) VALUES (1, N'Administrador')
INSERT [dbo].[Tipo_Usuario] ([Id_tipousuario], [Nombre]) VALUES (2, N'Desarrollador')
SET IDENTITY_INSERT [dbo].[Tipo_Usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT INTO [dbo].[Usuario] ([Id_usuario], [Nombre], [Apellido], [Correo], [Password], [Estado], [Id_tipousuario]) VALUES
(1, 'Sebastian', 'Arce Bracamonte', 'sarce@upt.edu.pe', '123456', 'A', 2),
(2, 'Cesar Fabian', 'Chavez Linares', 'cfchavez@upt.edu.pe', '123456', 'A', 1),
(3, 'Rodrigo Samael Adonai', 'Lira Alvarez', 'rsalira@upt.edu.pe', '123456', 'A', 1),
(4, 'Soledad Noemí', 'Maltrain Yáñez', 'snmaltrain@upt.edu.pe', '123456', 'A', 2),
(5, 'Cristian Aldair', 'Quispe Levano', 'caquispe@upt.edu.pe', '123456', 'A', 2);

SET IDENTITY_INSERT [dbo].[Usuario] OFF;
GO
ALTER TABLE [dbo].[Elemento_Configuracion]  WITH CHECK ADD  CONSTRAINT [FK_Elemento_Configuracion_Fase] FOREIGN KEY([Id_fase])
REFERENCES [dbo].[Fase] ([Id_fase])
GO
ALTER TABLE [dbo].[Elemento_Configuracion] CHECK CONSTRAINT [FK_Elemento_Configuracion_Fase]
GO
ALTER TABLE [dbo].[Fase]  WITH CHECK ADD  CONSTRAINT [FK_Fase_Metodologia] FOREIGN KEY([Id_metodologia])
REFERENCES [dbo].[Metodologia] ([Id_metodologia])
GO
ALTER TABLE [dbo].[Fase] CHECK CONSTRAINT [FK_Fase_Metodologia]
GO
ALTER TABLE [dbo].[Miembro_Elemento]  WITH CHECK ADD  CONSTRAINT [FK_Miembro_Elemento_Elemento_Configuracion] FOREIGN KEY([Id_elementoconfiguracion])
REFERENCES [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion])
GO
ALTER TABLE [dbo].[Miembro_Elemento] CHECK CONSTRAINT [FK_Miembro_Elemento_Elemento_Configuracion]
GO
ALTER TABLE [dbo].[Miembro_Elemento]  WITH CHECK ADD  CONSTRAINT [FK_Miembro_Elemento_Miembro_Proyecto] FOREIGN KEY([Id_miembro])
REFERENCES [dbo].[Miembro_Proyecto] ([Id_miembro])
GO
ALTER TABLE [dbo].[Miembro_Elemento] CHECK CONSTRAINT [FK_Miembro_Elemento_Miembro_Proyecto]
GO
ALTER TABLE [dbo].[Miembro_Proyecto]  WITH CHECK ADD  CONSTRAINT [FK_Miembro_Proyecto_Proyecto] FOREIGN KEY([Id_proyecto])
REFERENCES [dbo].[Proyecto] ([Id_proyecto])
GO
ALTER TABLE [dbo].[Miembro_Proyecto] CHECK CONSTRAINT [FK_Miembro_Proyecto_Proyecto]
GO
ALTER TABLE [dbo].[Miembro_Proyecto]  WITH CHECK ADD  CONSTRAINT [FK_Miembro_Proyecto_Rol] FOREIGN KEY([Id_rol])
REFERENCES [dbo].[Rol] ([Id_rol])
GO
ALTER TABLE [dbo].[Miembro_Proyecto] CHECK CONSTRAINT [FK_Miembro_Proyecto_Rol]
GO
ALTER TABLE [dbo].[Miembro_Proyecto]  WITH CHECK ADD  CONSTRAINT [FK_Miembro_Proyecto_Usuario] FOREIGN KEY([Id_usuario])
REFERENCES [dbo].[Usuario] ([Id_usuario])
GO
ALTER TABLE [dbo].[Miembro_Proyecto] CHECK CONSTRAINT [FK_Miembro_Proyecto_Usuario]
GO
ALTER TABLE [dbo].[Proyecto]  WITH CHECK ADD  CONSTRAINT [FK_Proyecto_Metodologia] FOREIGN KEY([Id_metodologia])
REFERENCES [dbo].[Metodologia] ([Id_metodologia])
GO
ALTER TABLE [dbo].[Proyecto] CHECK CONSTRAINT [FK_Proyecto_Metodologia]
GO
ALTER TABLE [dbo].[Proyecto]  WITH CHECK ADD  CONSTRAINT [FK_Proyecto_Solicitud_Cambios] FOREIGN KEY([Id_solicitud_cambios])
REFERENCES [dbo].[Solicitud_Cambios] ([Id_solicitud_cambios])
GO
ALTER TABLE [dbo].[Proyecto] CHECK CONSTRAINT [FK_Proyecto_Solicitud_Cambios]
GO
ALTER TABLE [dbo].[Solicitud_Cambios]  WITH CHECK ADD  CONSTRAINT [FK_Solicitud_Cambios_Solicitud] FOREIGN KEY([Id_solicitud])
REFERENCES [dbo].[Solicitud] ([Id_solicitud])
GO
ALTER TABLE [dbo].[Solicitud_Cambios] CHECK CONSTRAINT [FK_Solicitud_Cambios_Solicitud]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Tipo_Usuario] FOREIGN KEY([Id_tipousuario])
REFERENCES [dbo].[Tipo_Usuario] ([Id_tipousuario])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Tipo_Usuario]
GO