IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name = 'IX_Identificacion' AND object_id = OBJECT_ID('Persona'))
BEGIN
	CREATE NONCLUSTERED INDEX IX_Identificacion
	ON [dbo].[Persona](Identificacion);
END