-- Create Centrica schema
IF SCHEMA_ID(N'Centrica') IS NULL
EXEC(N'CREATE SCHEMA [Centrica]');
GO

-- Creating Salespersons Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Salesperson' AND schema_id = SCHEMA_ID('Centrica'))
BEGIN
    CREATE TABLE Centrica.Salesperson (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(255) NOT NULL
    );
END
GO

-- Creating Districts Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'District' AND schema_id = SCHEMA_ID('Centrica'))
BEGIN
    CREATE TABLE Centrica.District (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(255) NOT NULL,
        PrimarySalespersonId INT NOT NULL,
        FOREIGN KEY (PrimarySalespersonId) REFERENCES Centrica.Salesperson(Id)
    );
END
GO

-- Creating Stores Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Store' AND schema_id = SCHEMA_ID('Centrica'))
BEGIN
    CREATE TABLE Centrica.Store (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(255) NOT NULL,
        DistrictId INT NOT NULL,
        FOREIGN KEY (DistrictId) REFERENCES Centrica.District(Id)
    );
END
GO

-- Creating SalespersonDistricts join Table for secondary salespersons
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SalespersonDistrict' AND schema_id = SCHEMA_ID('Centrica'))
BEGIN
    CREATE TABLE Centrica.SalespersonDistrict (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        SalespersonId INT NOT NULL ,
        DistrictId INT NOT NULL ,
        UNIQUE (SalespersonId, DistrictId),
        FOREIGN KEY (SalespersonId) REFERENCES Centrica.Salesperson(Id),
        FOREIGN KEY (DistrictId) REFERENCES Centrica.District(Id)
    );
END
GO

CREATE LOGIN Centrica WITH PASSWORD = '$(User_PASSWORD)', CHECK_POLICY = ON;
GO

CREATE USER Centrica FOR LOGIN Centrica;
GO

ALTER USER Centrica WITH DEFAULT_SCHEMA = Centrica
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::Centrica TO Centrica;
GO

INSERT INTO Centrica.Salesperson (Name)
VALUES('Adele Vance'),
      ('Diego Siciliani'),
      ('Grady Archie'),
      ('Henrietta Mueller')
GO

INSERT INTO Centrica.District(Name, PrimarySalespersonId)
VALUES('North Denmark', (SELECT Id From Centrica.Salesperson WHERE Name = 'Adele Vance')),
      ('South Denmark', (SELECT Id From Centrica.Salesperson WHERE NAME = 'Adele Vance')),
      ('East Denmark', (SELECT Id From Centrica.Salesperson WHERE NAME = 'Grady Archie')),
      ('West Denmark', (SELECT Id From Centrica.Salesperson WHERE NAME = 'Henrietta Mueller'))
GO

INSERT INTO Centrica.SalespersonDistrict(SalespersonId, DistrictId)
VALUES((SELECT Id From Centrica.Salesperson WHERE Name = 'Diego Siciliani'),  (select Id from Centrica.District where Name = 'North Denmark')),
    ((SELECT Id From Centrica.Salesperson WHERE Name = 'Diego Siciliani'),  (select Id from Centrica.District where Name = 'South Denmark')),
    ((SELECT Id From Centrica.Salesperson WHERE Name = 'Diego Siciliani'),  (select Id from Centrica.District where Name = 'East Denmark')),
    ((SELECT Id From Centrica.Salesperson WHERE Name = 'Henrietta Mueller'),  (select Id from Centrica.District where Name = 'East Denmark')),
    ((SELECT Id From Centrica.Salesperson WHERE Name = 'Grady Archie'),  (select Id from Centrica.District where Name = 'West Denmark')),
    ((SELECT Id From Centrica.Salesperson WHERE Name = 'Grady Archie'),  (select Id from Centrica.District where Name = 'North Denmark'))

INSERT INTO centrica.Store(Name, DistrictId)
VALUES('Store North 1', (SELECT Id FROM Centrica.District WHERE Name = 'North Denmark')),
    ('Store North 2', (SELECT Id FROM Centrica.District WHERE Name = 'North Denmark')),
    ('Store South 1', (SELECT Id FROM Centrica.District WHERE Name = 'South Denmark')),
    ('Store South 2', (SELECT Id FROM Centrica.District WHERE Name = 'South Denmark')),
    ('Store East 1', (SELECT Id FROM Centrica.District WHERE Name = 'East Denmark')),
    ('Store East 2', (SELECT Id FROM Centrica.District WHERE Name = 'East Denmark')),
    ('Store West 1', (SELECT Id FROM Centrica.District WHERE Name = 'West Denmark')),
    ('Store West 2', (SELECT Id FROM Centrica.District WHERE Name = 'West Denmark'))