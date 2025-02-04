﻿CREATE TABLE [dbo].[taxonomy_common_name] (
    [taxonomy_common_name_id] INT            IDENTITY (1, 1) NOT NULL,
    [taxonomy_genus_id]       INT            NULL,
    [taxonomy_species_id]     INT            NULL,
    [language_description]    NVARCHAR (100) NULL,
    [name]                    NVARCHAR (100) NOT NULL,
    [simplified_name]         NVARCHAR (100) NULL,
    [alternate_transcription] NVARCHAR (100) NULL,
    [citation_id]             INT            NULL,
    [note]                    NVARCHAR (MAX) NULL,
    [created_date]            DATETIME2 (7)  NOT NULL,
    [created_by]              INT            NOT NULL,
    [modified_date]           DATETIME2 (7)  NULL,
    [modified_by]             INT            NULL,
    [owned_date]              DATETIME2 (7)  NOT NULL,
    [owned_by]                INT            NOT NULL,
    CONSTRAINT [PK_taxonomy_common_name] PRIMARY KEY CLUSTERED ([taxonomy_common_name_id] ASC),
    CONSTRAINT [fk_tcn_cit] FOREIGN KEY ([citation_id]) REFERENCES [dbo].[citation] ([citation_id]),
    CONSTRAINT [fk_tcn_created] FOREIGN KEY ([created_by]) REFERENCES [dbo].[cooperator] ([cooperator_id]),
    CONSTRAINT [fk_tcn_modified] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[cooperator] ([cooperator_id]),
    CONSTRAINT [fk_tcn_owned] FOREIGN KEY ([owned_by]) REFERENCES [dbo].[cooperator] ([cooperator_id]),
    CONSTRAINT [fk_tcn_tg] FOREIGN KEY ([taxonomy_genus_id]) REFERENCES [dbo].[taxonomy_genus] ([taxonomy_genus_id]),
    CONSTRAINT [fk_tcn_ts] FOREIGN KEY ([taxonomy_species_id]) REFERENCES [dbo].[taxonomy_species] ([taxonomy_species_id])
);


GO
CREATE NONCLUSTERED INDEX [ndx_cn_name]
    ON [dbo].[taxonomy_common_name]([name] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_cn_simplified_name]
    ON [dbo].[taxonomy_common_name]([simplified_name] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_tcn_created]
    ON [dbo].[taxonomy_common_name]([created_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_tcn_modified]
    ON [dbo].[taxonomy_common_name]([modified_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_tcn_owned]
    ON [dbo].[taxonomy_common_name]([owned_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_tcn_tg]
    ON [dbo].[taxonomy_common_name]([taxonomy_genus_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_tcn_ts]
    ON [dbo].[taxonomy_common_name]([taxonomy_species_id] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ndx_uniq_tcn]
    ON [dbo].[taxonomy_common_name]([taxonomy_genus_id] ASC, [taxonomy_species_id] ASC, [language_description] ASC, [name] ASC, [citation_id] ASC);

