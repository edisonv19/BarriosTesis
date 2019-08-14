IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Viaje_Insert]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Viaje_Insert]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 13/08/2019
-- Description:	Inserta un [Viaje]
-- =================================================

CREATE PROCEDURE [dbo].[Viaje_Insert]
	@IdPersona INT,
	@Fecha DATETIME = NULL,
	@IdOrigen INT = NULL,
	@IdTipoLugarOrigen INT = NULL,
	@IdDestino INT = NULL,
	@IdTipoLugarDestino INT = NULL,
	@IdMotivo INT = NULL,
	@HoraInicio TIME = NULL,
	@HoraFin TIME = NULL,
	@IdTransporte INT = NULL,
	@Observaciones NVARCHAR(200) = NULL
AS
BEGIN
	INSERT INTO [dbo].[Viaje]
	(
		[IdPersona]
		,[Fecha]
		,[IdOrigen]
		,[IdTipoLugarOrigen]
		,[IdDestino]
		,[IdTipoLugarDestino]
		,[IdMotivo]
		,[HoraInicio]
		,[HoraFin]
		,[IdTransporte]
		,[Observaciones]
	)
	VALUES
	(
		@IdPersona
		,@Fecha
		,@IdOrigen
		,@IdTipoLugarOrigen
		,@IdDestino
		,@IdTipoLugarDestino
		,@IdMotivo
		,@HoraInicio
		,@HoraFin
		,@IdTransporte
		,@Observaciones
	)

	SELECT IDENT_CURRENT('Viaje');
END
GO
