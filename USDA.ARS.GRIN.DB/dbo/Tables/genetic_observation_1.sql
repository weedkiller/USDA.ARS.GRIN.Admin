﻿CREATE TABLE [genetic_observation] (
    [genetic_observation_id] INT             IDENTITY (1, 1) NOT NULL,
    [inventory_id]           INT             NOT NULL,
    [genetic_annotation_id]  INT             NULL,
    [is_archived]            NVARCHAR (1)    NOT NULL,
    [data_quality_code]      NVARCHAR (20)   NULL,
    [frequency]              DECIMAL (18, 5) NULL,
    [value]                  NVARCHAR (1000) NULL,
    [rank]                   INT             NULL,
    [mean_value]             DECIMAL (18, 5) NULL,
    [maximum_value]          DECIMAL (18, 5) NULL,
    [minimum_value]          DECIMAL (18, 5) NULL,
    [standard_deviation]     DECIMAL (18, 5) NULL,
    [sample_size]            INT             NULL,
    [note]                   NVARCHAR (MAX)  NULL,
    [created_date]           DATETIME2 (7)   NOT NULL,
    [created_by]             INT             NOT NULL,
    [modified_date]          DATETIME2 (7)   NULL,
    [modified_by]            INT             NULL,
    [owned_date]             DATETIME2 (7)   NOT NULL,
    [owned_by]               INT             NOT NULL,
    CONSTRAINT [PK_genetic_observation] PRIMARY KEY CLUSTERED ([genetic_observation_id] ASC),
    CONSTRAINT [fk_go_created] FOREIGN KEY ([created_by]) REFERENCES [cooperator] ([cooperator_id]),
    CONSTRAINT [fk_go_ga] FOREIGN KEY ([genetic_annotation_id]) REFERENCES [genetic_annotation] ([genetic_annotation_id]),
    CONSTRAINT [fk_go_i] FOREIGN KEY ([inventory_id]) REFERENCES [inventory] ([inventory_id]),
    CONSTRAINT [fk_go_modified] FOREIGN KEY ([modified_by]) REFERENCES [cooperator] ([cooperator_id]),
    CONSTRAINT [fk_go_owned] FOREIGN KEY ([owned_by]) REFERENCES [cooperator] ([cooperator_id])
);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_go_created]
    ON [genetic_observation]([created_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_go_ga]
    ON [genetic_observation]([genetic_annotation_id] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_go_modified]
    ON [genetic_observation]([modified_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_go_owned]
    ON [genetic_observation]([owned_by] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ndx_uniq_go]
    ON [genetic_observation]([inventory_id] ASC, [genetic_annotation_id] ASC);


GO
GRANT DELETE
    ON OBJECT::[genetic_observation] TO [gg_user]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[genetic_observation] TO [gg_user]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[genetic_observation] TO [gg_user]
    AS [dbo];


GO
GRANT UPDATE
    ON OBJECT::[genetic_observation] TO [gg_user]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[genetic_observation] TO [gg_search]
    AS [dbo];

