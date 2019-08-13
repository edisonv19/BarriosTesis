IF COL_LENGTH('dbo.Persona', 'Identificacion') IS NULL
BEGIN
    ALTER TABLE Persona
    ADD Identificacion VARCHAR(100) NOT NULL
END;