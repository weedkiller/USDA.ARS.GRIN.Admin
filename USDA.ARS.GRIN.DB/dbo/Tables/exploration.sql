﻿CREATE TABLE [dbo].[exploration] (
    [exploration_id]     INT             IDENTITY (1, 1) NOT NULL,
    [exploration_number] NVARCHAR (40)   NOT NULL,
    [title]              NVARCHAR (240)  NULL,
    [began_date]         DATETIME2 (7)   NULL,
    [finished_date]      DATETIME2 (7)   NULL,
    [funding_source]     NVARCHAR (100)  NULL,
    [funding_amount]     DECIMAL (10, 2) NULL,
    [target_species]     NVARCHAR (200)  NULL,
    [permits]            NVARCHAR (240)  NULL,
    [restrictions]       NVARCHAR (60)   NULL,
    [fiscal_year]        INT             NULL,
    [host_cooperator_id] INT             NULL,
    [note]               NVARCHAR (MAX)  NULL,
    [created_date]       DATETIME2 (7)   NOT NULL,
    [created_by]         INT             NOT NULL,
    [modified_date]      DATETIME2 (7)   NULL,
    [modified_by]        INT             NULL,
    [owned_date]         DATETIME2 (7)   NOT NULL,
    [owned_by]           INT             NOT NULL,
    CONSTRAINT [PK_exploration] PRIMARY KEY CLUSTERED ([exploration_id] ASC),
    CONSTRAINT [fk_ex_c] FOREIGN KEY ([host_cooperator_id]) REFERENCES [dbo].[cooperator] ([cooperator_id]),
    CONSTRAINT [fk_ex_created] FOREIGN KEY ([created_by]) REFERENCES [dbo].[cooperator] ([cooperator_id]),
    CONSTRAINT [fk_ex_modified] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[cooperator] ([cooperator_id]),
    CONSTRAINT [fk_ex_owned] FOREIGN KEY ([owned_by]) REFERENCES [dbo].[cooperator] ([cooperator_id])
);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_ex_c]
    ON [dbo].[exploration]([host_cooperator_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_ex_created]
    ON [dbo].[exploration]([created_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_ex_modified]
    ON [dbo].[exploration]([modified_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_ex_owned]
    ON [dbo].[exploration]([owned_by] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ndx_uniq_ex]
    ON [dbo].[exploration]([exploration_number] ASC);

