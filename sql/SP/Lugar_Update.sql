IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Lugar_Update]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Lugar_Update]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 06/08/2019
-- Description:	Updatea un [Lugar]
-- =================================================

CREATE PROCEDURE [dbo].[Lugar_Update]
	@IdLugar INT,
	@IdRadioCensal	INT = NULL
AS
BEGIN
	UPDATE [dbo].[Lugar]
	SET
		[IdRadioCensal] = @IdRadioCensal
	WHERE
		[IdLugar] = @IdLugar;
END
GO
