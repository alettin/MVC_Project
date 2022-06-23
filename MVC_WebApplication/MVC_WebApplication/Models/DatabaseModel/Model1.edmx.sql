
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/23/2022 16:00:24
-- Generated from EDMX file: D:\projects\MVC_Project\MVC_WebApplication\MVC_WebApplication\Models\DatabaseModel\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MvcProject];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Article_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Articles] DROP CONSTRAINT [FK_Article_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Event_Category];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Articles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Articles];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[SeoUrls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SeoUrls];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Articles'
CREATE TABLE [dbo].[Articles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [Short] nvarchar(max)  NOT NULL,
    [Long] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [MetaKeywords] nvarchar(max)  NULL,
    [MetaDescription] nvarchar(max)  NULL,
    [MetaTitle] nvarchar(max)  NULL,
    [HitCount] int  NOT NULL,
    [CategoryId] int  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'SeoUrls'
CREATE TABLE [dbo].[SeoUrls] (
    [Id] int  NOT NULL,
    [SystemTable] int  NOT NULL,
    [ItemId] int  NOT NULL,
    [SeoUrl1] nvarchar(50)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int  NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [Short] nvarchar(max)  NOT NULL,
    [Long] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [MetaKeywords] nvarchar(max)  NOT NULL,
    [MetaDescription] nvarchar(max)  NOT NULL,
    [MetaTitle] nvarchar(max)  NOT NULL,
    [HitCount] int  NOT NULL,
    [CategoryId] int  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [PK_Articles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SeoUrls'
ALTER TABLE [dbo].[SeoUrls]
ADD CONSTRAINT [PK_SeoUrls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id], [Title], [Short], [Long], [MetaKeywords], [MetaDescription], [MetaTitle], [HitCount], [CategoryId], [CreateDate] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id], [Title], [Short], [Long], [MetaKeywords], [MetaDescription], [MetaTitle], [HitCount], [CategoryId], [CreateDate] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryId] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [FK_Article_Category]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Article_Category'
CREATE INDEX [IX_FK_Article_Category]
ON [dbo].[Articles]
    ([CategoryId]);
GO

-- Creating foreign key on [CategoryId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_Event_Category]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_Category'
CREATE INDEX [IX_FK_Event_Category]
ON [dbo].[Events]
    ([CategoryId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------