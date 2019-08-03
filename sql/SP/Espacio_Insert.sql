IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Espacio_Insert]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Espacio_Insert]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 14/05/2019
-- Description:	Inserta un [Espacio]
-- =================================================

CREATE PROCEDURE [dbo].[Espacio_Insert]
	@IdEspacio 		INT = NULL OUTPUT,
	@IdCategoria 	INT,
	@Descripcion 	NVARCHAR(200) = NULL,
	@Coordenadas	NVARCHAR(MAX) = NULL,
	@IdPadre		INT = NULL	,
	@Codigo			VARCHAR(50)
AS
BEGIN
	INSERT INTO [Espacio] (
		[Nombre],
		[Edad],
		[TieneHijos],
		
		[IdEstadoCivil],
		[IdSexo],
		[IdSocioEconomico],
		[IdNivelEducativo],
		[IdOcupacion],
		[IdZonaResidencial],
		[IdLugar]	,
	)
	VALUES (
			@IdCategoria,
			@Descripcion,
			@Coordenadas,
			@IdPadre,
			@Codigo
	)

	SELECT	@IdEspacio = [IdEspacio]
	FROM 	[Espacio]
	WHERE 	[IdEspacio] = IDENT_CURRENT('Espacio')

END
GO
