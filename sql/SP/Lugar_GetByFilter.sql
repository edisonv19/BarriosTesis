IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Lugar_GetByFilter]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Lugar_GetByFilter]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 04/06/2019
-- Description:	Obtiene registros en base al filtro
-- =================================================

CREATE PROCEDURE [dbo].[Lugar_GetByFilter]
	@IdRadioCensal	INT = NULL
AS
BEGIN
	SELECT
		l.IdLugar,
		l.Latitud,
		l.Longitud
	FROM
		[Lugar] l
	WHERE
		(@IdRadioCensal IS NULL OR l.IdRadioCensal = @IdRadioCensal);
END
GO
