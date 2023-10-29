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
