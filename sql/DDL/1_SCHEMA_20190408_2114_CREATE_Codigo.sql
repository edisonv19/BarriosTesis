--=========================================================
-- 	Author: Edison Vidal
-- 	Date: 08/04/2019
--	Description: Creación de tabla Codigo
--=========================================================

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Codigo')
BEGIN
    CREATE TABLE dbo.[Codigo] (
		[IdCodigo] 			INT IDENTITY(1,1),
		[Valor] 		VARCHAR(100) NOT NULL,
		[Clave]			VARCHAR(50) NOT NULL,
		[Grupo]			VARCHAR(100) NOT NULL,
		[Descripcion]	NVARCHAR(200) NULL
	);

	ALTER TABLE dbo.[Codigo]
	ADD CONSTRAINT PK_Codigo PRIMARY KEY (IdCodigo);
	
	CREATE NONCLUSTERED INDEX IX_Codigo_Clave_Grupo ON Codigo(Clave, Grupo);
END;