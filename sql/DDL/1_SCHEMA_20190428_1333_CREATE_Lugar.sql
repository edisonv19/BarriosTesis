--=========================================================
-- 	Author: Edison Vidal
-- 	Date: 29/03/2019
--	Description: Creación de tabla Lugar
--=========================================================

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Lugar')
BEGIN
    CREATE TABLE dbo.[Lugar] (
		[IdLugar] 	INT IDENTITY(1,1),
		
		[Calle]			VARCHAR(100) NOT NULL,
		[Numero]		VARCHAR(100) NOT NULL,
		[Latitud]		DECIMAL(9,7) NULL,
		[Longitud]		DECIMAL(9,7) NULL,
		[IdRadioCensal]	INT NULL,
		[IdZona]		INT NOT NULL,
		[IdCategoria]	INT NULL,
		[Descripcion]	NVARCHAR(200) NULL,
		[Radio]			INT NULL,
		[IdTipoZonaResidencial]		INT NULL
	);

	ALTER TABLE Lugar
	ADD CONSTRAINT PK_Lugar PRIMARY KEY (IdLugar);
	
	ALTER TABLE Lugar
	ADD CONSTRAINT FK_Lugar_Espacio_IdRadioCensal
	FOREIGN KEY (IdRadioCensal) REFERENCES [Espacio](IdEspacio);
	
	ALTER TABLE Lugar
	ADD CONSTRAINT FK_Lugar_Espacio_IdZona
	FOREIGN KEY (IdZona) REFERENCES [Espacio](IdEspacio);
	
	ALTER TABLE Lugar
	ADD CONSTRAINT FK_Lugar_Codigo_IdCategoria
	FOREIGN KEY (IdCategoria) REFERENCES [Codigo](IdCodigo);
	
	ALTER TABLE Lugar
	ADD CONSTRAINT FK_Lugar_Codigo_IdTipoZonaResidencial
	FOREIGN KEY (IdTipoZonaResidencial) REFERENCES [Codigo](IdCodigo);
END;