﻿CREATE TABLE [dbo].[taxonomy_cultivar] (
    [taxonomy_cultivar_id]       INT            IDENTITY (1, 1) NOT NULL,
    [taxonomy_genus_id]          INT            NULL,
    [taxonomy_species_id]        INT            NULL,
    [name]                       NVARCHAR (100) NOT NULL,
    [type]                       NVARCHAR (1)   NULL,
    [patent_number]              NVARCHAR (50)  NULL,
    [acceptance_type]            NVARCHAR (2)   NULL,
    [current_name]               NVARCHAR (150) NULL,
    [common_name_1]              NVARCHAR (150) NULL,
    [common_name_2]              NVARCHAR (150) NULL,
    [common_name_3]              NVARCHAR (150) NULL,
    [taxonomy_cultivar_group_id] INT            NULL,
    [descriptor]                 NVARCHAR (500) NULL,
    [hybridized_by]              NVARCHAR (150) NULL,
    [hybridization_year]         DATETIME2 (7)  NULL,
    [introduced_by]              NVARCHAR (150) NULL,
    [introduction_year]          DATETIME2 (7)  NULL,
    [origin_year]                DATETIME2 (7)  NULL,
    [pi_original_intro]          NVARCHAR (50)  NULL,
    [pi_original_note]           NVARCHAR (MAX) NULL,
    [parentage_note]             NVARCHAR (MAX) NULL,
    [trade_name]                 NVARCHAR (150) NULL,
    [trade_type_1]               NVARCHAR (1)   NULL,
    [trade_type_2]               NVARCHAR (1)   NULL,
    [life_cycle_type]            NVARCHAR (1)   NULL,
    [habit_type]                 NVARCHAR (3)   NULL,
    [usda_zone]                  NCHAR (4)      NULL,
    [growth_form]                NVARCHAR (2)   NULL,
    [leaf_retention]             NVARCHAR (2)   NULL,
    [note]                       NVARCHAR (MAX) NULL,
    [created_date]               DATETIME2 (7)  NOT NULL,
    [created_by]                 INT            NOT NULL,
    [modified_date]              DATETIME2 (7)  NULL,
    [modified_by]                INT            NULL,
    [owned_date]                 DATETIME2 (7)  NOT NULL,
    [owned_by]                   INT            NOT NULL,
    CONSTRAINT [PK_taxonomy_cultivar] PRIMARY KEY CLUSTERED ([taxonomy_cultivar_id] ASC)
);

