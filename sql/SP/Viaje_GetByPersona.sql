IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Viaje_GetByPersona]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Viaje_GetByPersona]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 30/09/2019
-- Description:	Obtiene registros en base al filtro
-- =================================================

CREATE PROCEDURE [dbo].[Viaje_GetByPersona]
	@IdPersona	INT = NULL
AS
BEGIN
	DECLARE @Ratio INT = (SELECT Valor FROM Codigo WHERE Grupo = 'Ratios' AND Clave = 'MinTimeBase');
	
	SELECT
		v.IdViaje,
		CASE
			WHEN v.IdOrigen = p.IdLugar OR DATEDIFF(MINUTE, HoraInicio, HoraFin) > @Ratio THEN 1
			ELSE 0
		END AS EsOrigenBase,
		CASE
			WHEN v.IdDestino = p.IdLugar OR DATEDIFF(MINUTE, HoraInicio, HoraFin) > @Ratio THEN 1
			ELSE 0
		END AS EsDestinoBase,
		lo.Calle + ' ' + lo.Numero AS Origen,
		ld.Calle + ' ' + ld.Numero AS Destino,
		lo.IdLugar as IdOrigen,
		ld.IdLugar as IdDestino,
		v.HoraInicio,
		v.HoraFin,
		v.Fecha,
		v.FechaStr
	FROM Viaje v
		INNER JOIN Persona p ON v.IdPersona = p.IdPersona
		LEFT JOIN Lugar lo ON lo.IdLugar = v.IdOrigen
		LEFT JOIN lugar ld ON ld.IdLugar = v.IdDestino
	WHERE
		p.IdPersona = @IdPersona
	ORDER BY v.IdViaje;
END
GO
