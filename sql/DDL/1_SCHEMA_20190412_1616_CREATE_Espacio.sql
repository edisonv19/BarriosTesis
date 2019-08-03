--=========================================================
-- 	Author: Edison Vidal
-- 	Date: 12/05/2019
--	Description: Creación de tabla Espacio
--=========================================================

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Espacio')
BEGIN
    CREATE TABLE dbo.[Espacio] (
		[IdEspacio] 	INT IDENTITY(1,1),
		
		[IdCategoria]	INT NOT NULL,
		[Codigo]		VARCHAR(50) NOT NULL,
		[Descripcion]	NVARCHAR(200) NULL,
		[Coordenadas]	VARCHAR(MAX) NULL,
		[IdPadre]		INT NULL
	);

	ALTER TABLE dbo.[Espacio]
	ADD CONSTRAINT PK_Espacio PRIMARY KEY (IdEspacio);
	
	ALTER TABLE dbo.[Espacio]
	ADD CONSTRAINT FK_Espacio_Codigo_IdCategoria FOREIGN KEY (IdCategoria) REFERENCES dbo.[Codigo](IdCodigo);
	
	ALTER TABLE dbo.[Espacio]
	ADD CONSTRAINT FK_Espacio_Codigo_IdPadre FOREIGN KEY (IdPadre) REFERENCES dbo.[Espacio](IdEspacio);
	
	ALTER TABLE dbo.[Espacio]
	ADD CONSTRAINT UC_Espacio_Codigo UNIQUE (Codigo);
	
	CREATE NONCLUSTERED INDEX IX_Espacio_Codigo  
    ON dbo.[Espacio](Codigo);  
END;