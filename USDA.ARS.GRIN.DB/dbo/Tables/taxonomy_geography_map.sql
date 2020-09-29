﻿CREATE TABLE [dbo].[taxonomy_geography_map] (
    [taxonomy_geography_map_id] INT            IDENTITY (1, 1) NOT NULL,
    [taxonomy_species_id]       INT            NOT NULL,
    [geography_id]              INT            NULL,
    [geography_status_code]     NVARCHAR (20)  NULL,
    [citation_id]               INT            NULL,
    [note]                      NVARCHAR (MAX) NULL,
    [created_date]              DATETIME2 (7)  NOT NULL,
    [created_by]                INT            NOT NULL,
    [modified_date]             DATETIME2 (7)  NULL,
    [modified_by]               INT            NULL,
    [owned_date]                DATETIME2 (7)  NOT NULL,
    [owned_by]                  INT            NOT NULL,
    CONSTRAINT [PK_taxonomy_geography_map] PRIMARY KEY CLUSTERED ([taxonomy_geography_map_id] ASC),
    CONSTRAINT [fk_tgm_cit] FOREIGN KEY ([citation_id]) REFERENCES [dbo].[citation] ([citation_id]),
    CONSTRAINT [fk_tgm_created] FOREIGN KEY ([created_by]) REFERENCES [dbo].[cooperator] ([cooperator_id]),
    CONSTRAINT [fk_tgm_g] FOREIGN KEY ([geography_id]) REFERENCES [dbo].[geography] ([geography_id]),
    CONSTRAINT [fk_tgm_modified] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[cooperator] ([cooperator_id]),
    CONSTRAINT [fk_tgm_owned] FOREIGN KEY ([owned_by]) REFERENCES [dbo].[cooperator] ([cooperator_id]),
    CONSTRAINT [fk_tgm_ts] FOREIGN KEY ([taxonomy_species_id]) REFERENCES [dbo].[taxonomy_species] ([taxonomy_species_id])
);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_tgm_created]
    ON [dbo].[taxonomy_geography_map]([created_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_tgm_g]
    ON [dbo].[taxonomy_geography_map]([geography_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_tgm_modified]
    ON [dbo].[taxonomy_geography_map]([modified_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_tgm_owned]
    ON [dbo].[taxonomy_geography_map]([owned_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_tgm_ts]
    ON [dbo].[taxonomy_geography_map]([taxonomy_species_id] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ndx_uniq_tgm]
    ON [dbo].[taxonomy_geography_map]([taxonomy_species_id] ASC, [geography_id] ASC, [geography_status_code] ASC, [citation_id] ASC);

