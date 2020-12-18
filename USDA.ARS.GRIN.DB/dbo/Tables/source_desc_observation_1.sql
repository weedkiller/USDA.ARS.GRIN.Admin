﻿CREATE TABLE [source_desc_observation] (
    [source_desc_observation_id] INT             IDENTITY (1, 1) NOT NULL,
    [accession_source_id]        INT             NOT NULL,
    [source_descriptor_id]       INT             NOT NULL,
    [source_descriptor_code_id]  INT             NULL,
    [numeric_value]              DECIMAL (18, 5) NULL,
    [string_value]               NVARCHAR (255)  NULL,
    [original_value]             NVARCHAR (30)   NULL,
    [note]                       NVARCHAR (MAX)  NULL,
    [created_date]               DATETIME2 (7)   NOT NULL,
    [created_by]                 INT             NOT NULL,
    [modified_date]              DATETIME2 (7)   NULL,
    [modified_by]                INT             NULL,
    [owned_date]                 DATETIME2 (7)   NOT NULL,
    [owned_by]                   INT             NOT NULL,
    CONSTRAINT [PK_source_desc_observation] PRIMARY KEY CLUSTERED ([source_desc_observation_id] ASC),
    CONSTRAINT [fk_sodo_as] FOREIGN KEY ([accession_source_id]) REFERENCES [accession_source] ([accession_source_id]),
    CONSTRAINT [fk_sodo_created] FOREIGN KEY ([created_by]) REFERENCES [cooperator] ([cooperator_id]),
    CONSTRAINT [fk_sodo_modified] FOREIGN KEY ([modified_by]) REFERENCES [cooperator] ([cooperator_id]),
    CONSTRAINT [fk_sodo_owned] FOREIGN KEY ([owned_by]) REFERENCES [cooperator] ([cooperator_id]),
    CONSTRAINT [fk_sodo_sd] FOREIGN KEY ([source_descriptor_id]) REFERENCES [source_descriptor] ([source_descriptor_id]),
    CONSTRAINT [fk_sodo_sodc] FOREIGN KEY ([source_descriptor_code_id]) REFERENCES [source_descriptor_code] ([source_descriptor_code_id])
);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_sodo_as]
    ON [source_desc_observation]([accession_source_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_sodo_created]
    ON [source_desc_observation]([created_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_sodo_modified]
    ON [source_desc_observation]([modified_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_sodo_owned]
    ON [source_desc_observation]([owned_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_sodo_sd]
    ON [source_desc_observation]([source_descriptor_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_sodo_sodc]
    ON [source_desc_observation]([source_descriptor_code_id] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ndx_uniq_sodo]
    ON [source_desc_observation]([accession_source_id] ASC, [source_descriptor_id] ASC, [source_descriptor_code_id] ASC, [numeric_value] ASC, [string_value] ASC);


GO
GRANT DELETE
    ON OBJECT::[source_desc_observation] TO [gg_user]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[source_desc_observation] TO [gg_user]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[source_desc_observation] TO [gg_user]
    AS [dbo];


GO
GRANT UPDATE
    ON OBJECT::[source_desc_observation] TO [gg_user]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[source_desc_observation] TO [gg_search]
    AS [dbo];

