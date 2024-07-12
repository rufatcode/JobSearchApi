-- Create a new database called 'JobSearchDB'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'JobSearchDB'
)
CREATE DATABASE JobSearchDB
GO

USE JobSearchDB


-- Create a new table called '[Categories]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Categories]', 'U') IS NOT NULL
DROP TABLE [dbo].[Categories]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Categories]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Name] NVARCHAR(300) NOT NULL,
    [ImageUrl] NVARCHAR(max) NOT NULL,
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO


-- Create a new table called '[Companies]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Companies]', 'U') IS NOT NULL
DROP TABLE [dbo].[Companies]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Companies]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Name] NVARCHAR(300) NOT NULL,
    [LogoUrl] NVARCHAR(max) NOT NULL,
    [About] NVARCHAR(max) NOT NULL,
    [Location] NVARCHAR(max) NOT NULL,
    [LocationIframe] NVARCHAR(max) NOT NULL,
    [Contact] NVARCHAR(max) NOT NULL,
    [WebSiteUrl] NVARCHAR(max) NOT NULL,
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO




-- Create a new table called '[Advertaismets]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Advertaismets]', 'U') IS NOT NULL
DROP TABLE [dbo].[Advertaismets]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Advertaismets]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Name] NVARCHAR(300) NOT NULL,
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO


-- Create a new table called '[Positions]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Positions]', 'U') IS NOT NULL
DROP TABLE [dbo].[Positions]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Positions]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Name] NVARCHAR(300) NOT NULL,
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO


-- Create a new table called '[Cities]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Cities]', 'U') IS NOT NULL
DROP TABLE [dbo].[Cities]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Cities]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Name] NVARCHAR(300) NOT NULL,
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO


-- Create a new table called '[EmploymentTypes]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[EmploymentTypes]', 'U') IS NOT NULL
DROP TABLE [dbo].[EmploymentTypes]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[EmploymentTypes]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Name] NVARCHAR(300) NOT NULL,
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO

-- Create a new table called '[Jobs]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Jobs]', 'U') IS NOT NULL
DROP TABLE [dbo].[Jobs]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Jobs]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Name] NVARCHAR(30) NOT NULL,
    [Information] NVARCHAR(max) NOT NULL,
    [CreatedAt] DATETIME NOT NULL,
    [Deadline] DATETIME,
    [Salary] FLOAT ,
    [EmploymentTypeId] int REFERENCES EmploymentTypes(id),
    [CityId] INT NOT NULL REFERENCES Cities(Id),
    [PositionId] INT NOT NULL REFERENCES Positions(Id),
    [AdvertaismetId] INT NOT NULL REFERENCES Advertaismets(Id),
    [CategoryId] INT NOT NULL REFERENCES Categories(id),
    [CompanyId] INT NOT NULL REFERENCES Companies(id),
    [InformationForApply] NVARCHAR(max) ,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO

Alter TABLE Jobs alter COLUMN [Name] NVARCHAR(max)


-- Create a new table called '[JobDescribtions]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[JobDescribtions]', 'U') IS NOT NULL
DROP TABLE [dbo].[JobDescribtions]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[JobDescribtions]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Description] NVARCHAR(max) NOT NULL,
    [JobId] INT NOT NULL REFERENCES Jobs(Id),
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO



-- Create a new table called '[JobRequirements]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[JobRequirements]', 'U') IS NOT NULL
DROP TABLE [dbo].[JobRequirements]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[JobRequirements]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Requirement] NVARCHAR(max) NOT NULL,
    [JobId] INT NOT NULL REFERENCES Jobs(Id),
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO


-- Create a new table called '[JobOffers]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[JobOffers]', 'U') IS NOT NULL
DROP TABLE [dbo].[JobOffers]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[JobOffers]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Offer] NVARCHAR(max) NOT NULL,
    [JobId] INT NOT NULL REFERENCES Jobs(Id),
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO


-- Create a new table called '[JobValues]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[JobValues]', 'U') IS NOT NULL
DROP TABLE [dbo].[JobValues]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[JobValues]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [OurValue] NVARCHAR(max) NOT NULL,
    [JobId] INT NOT NULL REFERENCES Jobs(Id),
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO

-- Create a new table called '[JobResponsibilities]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[JobResponsibilities]', 'U') IS NOT NULL
DROP TABLE [dbo].[JobResponsibilities]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[JobResponsibilities]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Responsibility] NVARCHAR(max) NOT NULL,
    [JobId] INT NOT NULL REFERENCES Jobs(Id),
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO


-- Create a new table called '[JobQualifications]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[JobQualifications]', 'U') IS NOT NULL
DROP TABLE [dbo].[JobQualifications]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[JobQualifications]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Qualification] NVARCHAR(max) NOT NULL,
    [JobId] INT NOT NULL REFERENCES Jobs(Id),
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO

-- Create a new table called '[Users]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Users]', 'U') IS NOT NULL
DROP TABLE [dbo].[Users]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Users]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [Name] NVARCHAR(300) NOT NULL,
    [Email] NVARCHAR(300) NOT NULL,
    [Password] NVARCHAR(max) NOT NULL,
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO



-- Create a new table called '[WishLists]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[WishLists]', 'U') IS NOT NULL
DROP TABLE [dbo].[WishLists]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[WishLists]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, -- Primary Key column
    [UserId] INT NOT NULL REFERENCES Users(Id),
    [JobId] INT NOT NULL REFERENCES Jobs(Id),
    [CreatedDate] DATETIME,
    [IsActive] bit,
    [DeletedAt] DATETIME,
    [UpdatedAt] DateTime,
    [RegUser] NVARCHAR(300),
    -- Specify more columns here
);
GO


select
j.Name Job,
j.Information Information,
j.CreatedAt,
j.Deadline,
et.Name EmploymentType,
ci.Name City,
p.Name Position,
a.Name Advertaisment,
c.Name Category,
co.Name Company
from jobs j
JOIN Categories c
ON c.Id=j.CategoryId
JOIN Companies co
ON co.Id=j.CompanyId
JOIN Advertaismets a
ON a.Id=j.AdvertaismetId
JOIN Positions p
ON p.Id=j.PositionId
JOIN Cities ci
ON ci.Id=j.CityId
JOIN EmploymentTypes et
ON et.Id=j.EmploymentTypeId
