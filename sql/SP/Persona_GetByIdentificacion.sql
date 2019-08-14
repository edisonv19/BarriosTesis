IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Persona_GetByIdentificacion]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Persona_GetByIdentificacion]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 13/08/2019
-- Description:	Obtiene un registro en base al Identificacion
-- =================================================

CREATE PROCEDURE [dbo].[Persona_GetByIdentificacion]
	@Identificacion 	VARCHAR(100)
AS
BEGIN
	SELECT
		[IdPersona]
		,[Nombre]
		,[Edad]
		,[IdLugar]
		,[IdSocioEconomico]
		,[IdSexo]
		,[IdNivelEducativo]
		,[IdOcupacion]
		,[IdEstacion]
		,[Identificacion]
	FROM
		[Persona]
	WHERE
		[Identificacion] = @Identificacion
END
GO
