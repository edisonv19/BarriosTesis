
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name = 'IX_Lat_lgn' AND object_id = OBJECT_ID('tablename'))
BEGIN
	CREATE NONCLUSTERED INDEX IX_Lat_lgn
	ON [dbo].[Lugar](Latitud, Longitud);
END