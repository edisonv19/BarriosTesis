IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Espacio_GetByCodigo]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Espacio_GetByCodigo]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 02/06/2019
-- Description:	Obtiene un registro en base al codigo
-- =================================================

CREATE PROCEDURE [dbo].[Espacio_GetByCodigo]
	@Codigo 	VARCHAR(50)
AS
BEGIN
	SELECT
		IdEspacio,
		IdCategoria,
		Codigo,
		Descripcion,
		Coordenadas as CoordenadasStr,
		IdPadre
	FROM
		[Espacio]
	WHERE
		[Codigo] = @Codigo
END
GO
