--=========================================================
-- 	Author: Edison Vidal
-- 	Date: 29/03/2019
--	Description: Creación de tabla Viaje
--=========================================================

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Viaje')
BEGIN
    CREATE TABLE dbo.[Viaje] (
		[IdViaje] 		INT IDENTITY(1,1),
		
		[IdPersona]		INT NOT NULL,
		[Fecha]			DATETIME NULL,
		[IdOrigen]		INT NULL,
		[IdTipoLugarOrigen]	INT NULL,
		[IdDestino]		INT NULL,
		[IdTipoLugarDestino]	INT NULL,
		
		[IdMotivo]		INT NULL,
		[HoraInicio]	TIME NULL,
		[HoraFin]		TIME NULL,
		
		[IdTransporte]	INT NULL,
		[Observaciones]	NVARCHAR(200) NULL
	);

	ALTER TABLE Viaje
	ADD CONSTRAINT PK_Viaje PRIMARY KEY (IdViaje);
	
	ALTER TABLE Viaje
	ADD CONSTRAINT FK_Viaje_Persona_IdPersona
	FOREIGN KEY (IdPersona) REFERENCES [Persona](IdPersona);
	
	ALTER TABLE Viaje
	ADD CONSTRAINT FK_Viaje_Lugar_IdOrigen
	FOREIGN KEY (IdOrigen) REFERENCES [Lugar](IdLugar);
	
	ALTER TABLE Viaje
	ADD CONSTRAINT FK_Viaje_Codigo_IdTipoLugarOrigen
	FOREIGN KEY (IdTipoLugarOrigen) REFERENCES [Codigo](IdCodigo);
	
	ALTER TABLE Viaje
	ADD CONSTRAINT FK_Viaje_Lugar_IdDestino
	FOREIGN KEY (IdDestino) REFERENCES [Lugar](IdLugar);
	
	ALTER TABLE Viaje
	ADD CONSTRAINT FK_Viaje_Codigo_IdTipoLugarDestino
	FOREIGN KEY (IdTipoLugarDestino) REFERENCES [Codigo](IdCodigo);
	
	ALTER TABLE Viaje
	ADD CONSTRAINT FK_Viaje_Codigo_IdMotivo
	FOREIGN KEY (IdMotivo) REFERENCES [Codigo](IdCodigo);
	
	ALTER TABLE Viaje
	ADD CONSTRAINT FK_Viaje_Codigo_IdTransporte
	FOREIGN KEY (IdTransporte) REFERENCES [Codigo](IdCodigo);
END;	