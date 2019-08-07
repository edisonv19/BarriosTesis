IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Lugar_Insert]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Lugar_Insert]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 06/08/2019
-- Description:	Inserta un [Lugar]
-- =================================================

CREATE PROCEDURE [dbo].[Lugar_Insert]
	@Calle		VARCHAR(100),
	@Numero		VARCHAR(100),
	@Latitud	DECIMAL(9,7),
	@Longitud	DECIMAL(9,7),
	@IdRadioCensal	INT,
	@IdZona		INT,
	@IdCategoria	INT,
	@Descripcion	NVARCHAR(200),
	@Radio		INT
AS
BEGIN
	INSERT INTO [dbo].[Lugar]
           ([Calle]
           ,[Numero]
           ,[Latitud]
           ,[Longitud]
           ,[IdRadioCensal]
           ,[IdZona]
           ,[IdCategoria]
           ,[Descripcion]
           ,[Radio])
	VALUES(
		@Calle,
		@Numero,
		@Latitud,
		@Longitud,
		@IdRadioCensal,
		@IdZona,
		@IdCategoria,
		@Descripcion,
		@Radio
	)

	SELECT IDENT_CURRENT('Lugar')

END
GO
