﻿CREATE TABLE [genetic_observation_data] (
    [genetic_observation_data_id] INT             IDENTITY (1, 1) NOT NULL,
    [genetic_observation_id]      INT             NULL,
    [genetic_annotation_id]       INT             NOT NULL,
    [inventory_id]                INT             NOT NULL,
    [individual]                  INT             NULL,
    [individual_allele_number]    INT             NULL,
    [value]                       NVARCHAR (1000) NOT NULL,
    [created_date]                DATETIME2 (7)   NOT NULL,
    [created_by]                  INT             NOT NULL,
    [modified_date]               DATETIME2 (7)   NULL,
    [modified_by]                 INT             NULL,
    [owned_date]                  DATETIME2 (7)   NOT NULL,
    [owned_by]                    INT             NOT NULL,
    CONSTRAINT [PK_genetic_observation_data] PRIMARY KEY CLUSTERED ([genetic_observation_data_id] ASC),
    CONSTRAINT [fk_god_created] FOREIGN KEY ([created_by]) REFERENCES [cooperator] ([cooperator_id]),
    CONSTRAINT [fk_god_ga] FOREIGN KEY ([genetic_annotation_id]) REFERENCES [genetic_annotation] ([genetic_annotation_id]),
    CONSTRAINT [fk_god_i] FOREIGN KEY ([inventory_id]) REFERENCES [inventory] ([inventory_id]),
    CONSTRAINT [fk_god_modified] FOREIGN KEY ([modified_by]) REFERENCES [cooperator] ([cooperator_id]),
    CONSTRAINT [fk_god_ob] FOREIGN KEY ([genetic_observation_id]) REFERENCES [genetic_observation] ([genetic_observation_id]),
    CONSTRAINT [fk_god_owned] FOREIGN KEY ([owned_by]) REFERENCES [cooperator] ([cooperator_id])
);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_god_created]
    ON [genetic_observation_data]([created_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_god_ga]
    ON [genetic_observation_data]([genetic_annotation_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_god_i]
    ON [genetic_observation_data]([inventory_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_god_modified]
    ON [genetic_observation_data]([modified_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_god_ob]
    ON [genetic_observation_data]([genetic_observation_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_god_owned]
    ON [genetic_observation_data]([owned_by] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ndx_uniq_god]
    ON [genetic_observation_data]([genetic_annotation_id] ASC, [inventory_id] ASC, [individual] ASC, [individual_allele_number] ASC);


GO
GRANT DELETE
    ON OBJECT::[genetic_observation_data] TO [gg_user]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[genetic_observation_data] TO [gg_user]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[genetic_observation_data] TO [gg_user]
    AS [dbo];


GO
GRANT UPDATE
    ON OBJECT::[genetic_observation_data] TO [gg_user]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[genetic_observation_data] TO [gg_search]
    AS [dbo];

