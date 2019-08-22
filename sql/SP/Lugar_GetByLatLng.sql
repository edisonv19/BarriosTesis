IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Lugar_GetByLatLng]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Lugar_GetByLatLng]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 14/05/2019
-- Description:	Inserta un [Espacio]
-- =================================================

CREATE PROCEDURE [dbo].[Lugar_GetByLatLng]
	@Latitud [float] = NULL,
	@Longitud [float] = NULL
AS
BEGIN
	SELECT
		IdLugar,
		Latitud,
		Longitud
	FROM
		[dbo].[Lugar] lugar
	WHERE
		lugar.Latitud = @Latitud AND
		lugar.Longitud = @Longitud;
END
GO