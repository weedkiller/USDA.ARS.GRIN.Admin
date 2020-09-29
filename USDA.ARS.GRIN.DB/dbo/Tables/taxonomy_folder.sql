﻿CREATE TABLE [dbo].[taxonomy_folder] (
    [taxonomy_folder_id] INT             IDENTITY (1, 1) NOT NULL,
    [title]              NVARCHAR (250)  NOT NULL,
    [description]        NVARCHAR (MAX)  NULL,
    [category]           NVARCHAR (50)   NOT NULL,
    [note]               NVARCHAR (2000) NULL,
    [created_by]         INT             NULL,
    [created_date]       DATETIME        CONSTRAINT [DF_taxonomy_folder_created_date] DEFAULT (getdate()) NULL,
    [is_shared]          BIT             NULL,
    [modified_by]        INT             NULL,
    [modified_date]      DATETIME        NULL,
    CONSTRAINT [PK_taxonomy_folder] PRIMARY KEY CLUSTERED ([taxonomy_folder_id] ASC),
    CONSTRAINT [FK_taxonomy_folder_cooperator] FOREIGN KEY ([created_by]) REFERENCES [dbo].[cooperator] ([cooperator_id])
);

