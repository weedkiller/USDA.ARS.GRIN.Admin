﻿CREATE TABLE [dbo].[taxonomy_cultivar_group] (
    [taxonomy_cultivar_group_id] INT            IDENTITY (1, 1) NOT NULL,
    [taxonomy_genus_id]          INT            NOT NULL,
    [taxonomy_species_id]        INT            NULL,
    [name]                       NVARCHAR (500) NOT NULL,
    [acceptance_type]            NVARCHAR (2)   NOT NULL,
    [current_name]               NVARCHAR (500) NULL,
    [common_name_1]              NVARCHAR (500) NULL,
    [common_name_2]              NVARCHAR (500) NULL,
    [common_name_3]              NVARCHAR (500) NULL,
    [originated_by]              NVARCHAR (150) NULL,
    [origination_year]           DATETIME2 (7)  NULL,
    [introduced_by]              NVARCHAR (150) NULL,
    [introduction_year]          DATETIME2 (7)  NULL,
    [group_parentage]            NVARCHAR (150) NULL,
    [note]                       NVARCHAR (MAX) NULL,
    [created_date]               DATETIME2 (7)  NOT NULL,
    [created_by]                 INT            NOT NULL,
    [modified_date]              DATETIME2 (7)  NULL,
    [modified_by]                INT            NULL,
    [owned_date]                 DATETIME2 (7)  NOT NULL,
    [owned_by]                   INT            NOT NULL,
    CONSTRAINT [PK_taxonomy_cultivar_group] PRIMARY KEY CLUSTERED ([taxonomy_cultivar_group_id] ASC),
    CONSTRAINT [FK_taxonomy_cultivar_group_taxonomy_genus] FOREIGN KEY ([taxonomy_genus_id]) REFERENCES [dbo].[taxonomy_genus] ([taxonomy_genus_id]),
    CONSTRAINT [FK_taxonomy_cultivar_group_taxonomy_species] FOREIGN KEY ([taxonomy_species_id]) REFERENCES [dbo].[taxonomy_species] ([taxonomy_species_id])
);

