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
	@IdPersona 		INT = NULL OUTPUT,
	@IdCategoria 	INT,
	@Descripcion 	NVARCHAR(200) = NULL,
	@Coordenadas	NVARCHAR(MAX) = NULL,
	@IdPadre		INT = NULL	,
	@Codigo			VARCHAR(50)
AS
BEGIN
	INSERT INTO [Persona] (
			[IdCategoria],
			[Descripcion],
			[Coordenadas],
			[IdPadre],
			[Codigo]
			)
	VALUES (
			@IdCategoria,
			@Descripcion,
			@Coordenadas,
			@IdPadre,
			@Codigo
	)

	SELECT	@IdPersona = [IdPersona]
	FROM 	[Persona]
	WHERE 	[IdPersona] = IDENT_CURRENT('Persona')

END
GO
