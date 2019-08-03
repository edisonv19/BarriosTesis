IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Codigo_GetByClave]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Codigo_GetByClave]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 14/05/2019
-- Description:	Obtiene un registro en base a la tupla (Clave, Grupo)
-- =================================================

CREATE PROCEDURE [dbo].[Codigo_GetByClave]
	@Clave 	VARCHAR(50),
	@Grupo 	VARCHAR(100)
AS
BEGIN
	SELECT
		IdCodigo,
		Valor,
		Clave,
		Grupo,
		Descripcion
	FROM
		[Codigo]
	WHERE
		[Grupo] = @Grupo AND
		[Clave] = @Clave;
END
GO
