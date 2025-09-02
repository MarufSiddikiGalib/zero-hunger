
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/01/2025 19:32:28
-- Generated from EDMX file: E:\All study meterials\11th semister summer\Adv .net\Zero Hunger\ZeroHunger\ZeroHunger\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ZeroHunger];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_CollectRequests_dbo_Employees_AssignedEmployeeId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CollectRequests] DROP CONSTRAINT [FK_dbo_CollectRequests_dbo_Employees_AssignedEmployeeId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_CollectRequests_dbo_Restaurants_RestaurantId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CollectRequests] DROP CONSTRAINT [FK_dbo_CollectRequests_dbo_Restaurants_RestaurantId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Distributions_dbo_CollectRequests_CollectRequestId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Distributions] DROP CONSTRAINT [FK_dbo_Distributions_dbo_CollectRequests_CollectRequestId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Distributions_dbo_Employees_EmployeeId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Distributions] DROP CONSTRAINT [FK_dbo_Distributions_dbo_Employees_EmployeeId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CollectRequests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CollectRequests];
GO
IF OBJECT_ID(N'[dbo].[Distributions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Distributions];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Restaurants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Restaurants];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CollectRequests'
CREATE TABLE [dbo].[CollectRequests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RestaurantId] int  NOT NULL,
    [FoodDescription] nvarchar(max)  NULL,
    [MaxPreserveTime] datetime  NOT NULL,
    [Status] nvarchar(max)  NULL,
    [AssignedEmployeeId] int  NULL
);
GO

-- Creating table 'Distributions'
CREATE TABLE [dbo].[Distributions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CollectRequestId] int  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [DistributionDetails] nvarchar(max)  NULL,
    [DistributedAt] datetime  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ContactInfo] nvarchar(max)  NULL
);
GO

-- Creating table 'Restaurants'
CREATE TABLE [dbo].[Restaurants] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ContactInfo] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL
);
GO

-- Creating table 'UserCredentials'
CREATE TABLE [dbo].[UserCredentials] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [PasswordHash] nvarchar(max)  NOT NULL,
    [UserType] nvarchar(max)  NOT NULL,
    [RestaurantId] nvarchar(max)  NOT NULL,
    [EmployeeId] nvarchar(max)  NOT NULL,
    [Employee_Id] int  NOT NULL,
    [Restaurant_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CollectRequests'
ALTER TABLE [dbo].[CollectRequests]
ADD CONSTRAINT [PK_CollectRequests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Distributions'
ALTER TABLE [dbo].[Distributions]
ADD CONSTRAINT [PK_Distributions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Restaurants'
ALTER TABLE [dbo].[Restaurants]
ADD CONSTRAINT [PK_Restaurants]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserCredentials'
ALTER TABLE [dbo].[UserCredentials]
ADD CONSTRAINT [PK_UserCredentials]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AssignedEmployeeId] in table 'CollectRequests'
ALTER TABLE [dbo].[CollectRequests]
ADD CONSTRAINT [FK_dbo_CollectRequests_dbo_Employees_AssignedEmployeeId]
    FOREIGN KEY ([AssignedEmployeeId])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_CollectRequests_dbo_Employees_AssignedEmployeeId'
CREATE INDEX [IX_FK_dbo_CollectRequests_dbo_Employees_AssignedEmployeeId]
ON [dbo].[CollectRequests]
    ([AssignedEmployeeId]);
GO

-- Creating foreign key on [RestaurantId] in table 'CollectRequests'
ALTER TABLE [dbo].[CollectRequests]
ADD CONSTRAINT [FK_dbo_CollectRequests_dbo_Restaurants_RestaurantId]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_CollectRequests_dbo_Restaurants_RestaurantId'
CREATE INDEX [IX_FK_dbo_CollectRequests_dbo_Restaurants_RestaurantId]
ON [dbo].[CollectRequests]
    ([RestaurantId]);
GO

-- Creating foreign key on [CollectRequestId] in table 'Distributions'
ALTER TABLE [dbo].[Distributions]
ADD CONSTRAINT [FK_dbo_Distributions_dbo_CollectRequests_CollectRequestId]
    FOREIGN KEY ([CollectRequestId])
    REFERENCES [dbo].[CollectRequests]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Distributions_dbo_CollectRequests_CollectRequestId'
CREATE INDEX [IX_FK_dbo_Distributions_dbo_CollectRequests_CollectRequestId]
ON [dbo].[Distributions]
    ([CollectRequestId]);
GO

-- Creating foreign key on [EmployeeId] in table 'Distributions'
ALTER TABLE [dbo].[Distributions]
ADD CONSTRAINT [FK_dbo_Distributions_dbo_Employees_EmployeeId]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Distributions_dbo_Employees_EmployeeId'
CREATE INDEX [IX_FK_dbo_Distributions_dbo_Employees_EmployeeId]
ON [dbo].[Distributions]
    ([EmployeeId]);
GO

-- Creating foreign key on [Employee_Id] in table 'UserCredentials'
ALTER TABLE [dbo].[UserCredentials]
ADD CONSTRAINT [FK_UserCredentialsEmployee]
    FOREIGN KEY ([Employee_Id])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCredentialsEmployee'
CREATE INDEX [IX_FK_UserCredentialsEmployee]
ON [dbo].[UserCredentials]
    ([Employee_Id]);
GO

-- Creating foreign key on [Restaurant_Id] in table 'UserCredentials'
ALTER TABLE [dbo].[UserCredentials]
ADD CONSTRAINT [FK_UserCredentialsRestaurant]
    FOREIGN KEY ([Restaurant_Id])
    REFERENCES [dbo].[Restaurants]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCredentialsRestaurant'
CREATE INDEX [IX_FK_UserCredentialsRestaurant]
ON [dbo].[UserCredentials]
    ([Restaurant_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------