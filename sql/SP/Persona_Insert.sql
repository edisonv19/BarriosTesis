IF EXISTS (SELECT 1 FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Persona_Insert]') AND [type] = N'P')
BEGIN
	DROP PROCEDURE [dbo].[Persona_Insert]
END
GO

-- =================================================
-- Author:		Edison Vidal
-- Create date: 22/07/2019
-- Description:	Inserta un [Persona]
-- =================================================

CREATE PROCEDURE [dbo].[Persona_Insert]
	@Nombre		NVARCHAR(50),
	@Edad		INT = NULL,
	@IdLugar	INT = NULL,
	@IdSocioEconomico 	INT = NULL,
	@IdSexo		INT = NULL,
	@IdNivelEducativo	INT = NULL,
	@IdOcupacion	INT = NULL,
	@IdEstacion	INT = NULL,
	@Identificacion VARCHAR(100) = NULL
AS
BEGIN
	INSERT INTO [dbo].[Persona](
		[Nombre]
		,[Edad]
		,[IdLugar]
		,[IdSocioEconomico]
		,[IdSexo]
		,[IdNivelEducativo]
		,[IdOcupacion]
		,[IdEstacion]
		,[Identificacion]
	)
    VALUES(
		@Nombre
		,@Edad
		,@IdLugar
		,@IdSocioEconomico
		,@IdSexo
		,@IdNivelEducativo
		,@IdOcupacion
		,@IdEstacion
		,@Identificacion
	);

	SELECT CONVERT(INT, IDENT_CURRENT('Persona'));
END
GO
