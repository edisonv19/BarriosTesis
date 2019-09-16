IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Espacio_GetByFilter]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Espacio_GetByFilter]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 04/06/2019
-- Description:	Obtiene registros en base al filtro
-- =================================================

CREATE PROCEDURE [dbo].[Espacio_GetByFilter]
	@IdCategoria	INT = NULL,
	@Descripcion	VARCHAR(200) = NULL,
	@IdPadre		INT = NULL,
	@Codigo 		VARCHAR(50) = NULL
AS
BEGIN
	SELECT
		e.IdEspacio,
		e.IdCategoria,
		e.Codigo,
		e.Descripcion,
		e.Coordenadas as CoordenadasStr,
		e.IdPadre
	FROM
		[Espacio] e
	WHERE
		(@IdCategoria IS NULL OR e.IdCategoria = @IdCategoria) AND
		(@Descripcion IS NULL OR e.Descripcion LIKE '%'+@Descripcion+'%') AND
		(@IdPadre IS NULL OR e.IdPadre = @IdPadre) AND
		(@Codigo IS NULL OR e.[Codigo] = @Codigo)
END
GO
