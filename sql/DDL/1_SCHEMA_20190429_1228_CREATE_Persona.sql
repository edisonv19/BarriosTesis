--=========================================================
-- 	Author: Edison Vidal
-- 	Date: 29/03/2019
--	Description: Creación de tabla Persona
--=========================================================

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Persona')
BEGIN
    CREATE TABLE dbo.[Persona] (
		[IdPersona] 		INT IDENTITY(1,1),
		[Nombre]		NVARCHAR(50) NOT NULL,
		[Edad]			INT NULL,
		
		[IdLugar]		INT NULL,
		[IdSocioEconomico]	INT NULL,
		[IdSexo]			INT NULL,
		[IdNivelEducativo]	INT NULL,
		[IdOcupacion]		INT NULL,
		
		[IdEstacion]		INT NULL
	);

	ALTER TABLE Persona
	ADD CONSTRAINT PK_Persona PRIMARY KEY (IdPersona);
	
	
	ALTER TABLE Persona
	ADD CONSTRAINT FK_Persona_Lugar_IdLugar
	FOREIGN KEY (IdLugar) REFERENCES [Lugar](IdLugar);
	
	ALTER TABLE Persona
	ADD CONSTRAINT FK_Persona_Codigo_IdSocioEconomico
	FOREIGN KEY (IdSocioEconomico) REFERENCES [Codigo](IdCodigo);
	
	ALTER TABLE Persona
	ADD CONSTRAINT FK_Persona_Codigo_IdSexo
	FOREIGN KEY (IdSexo) REFERENCES [Codigo](IdCodigo);
	
	ALTER TABLE Persona
	ADD CONSTRAINT FK_Persona_Codigo_IdNivelEducativo
	FOREIGN KEY (IdNivelEducativo) REFERENCES [Codigo](IdCodigo);
	
	ALTER TABLE Persona
	ADD CONSTRAINT FK_Persona_Codigo_IdOcupacion
	FOREIGN KEY (IdOcupacion) REFERENCES [Codigo](IdCodigo);

	ALTER TABLE Persona
	ADD CONSTRAINT FK_Persona_Codigo_IdEstacion
	FOREIGN KEY (IdEstacion) REFERENCES [Codigo](IdCodigo);
END;