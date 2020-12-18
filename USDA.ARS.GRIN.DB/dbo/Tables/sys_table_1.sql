﻿CREATE TABLE [sys_table] (
    [sys_table_id]       INT            IDENTITY (1, 1) NOT NULL,
    [table_name]         NVARCHAR (50)  NOT NULL,
    [is_enabled]         NVARCHAR (1)   NOT NULL,
    [is_readonly]        NVARCHAR (1)   NOT NULL,
    [audits_created]     NVARCHAR (1)   NOT NULL,
    [audits_modified]    NVARCHAR (1)   NOT NULL,
    [audits_owned]       NVARCHAR (1)   NOT NULL,
    [database_area_code] NVARCHAR (100) NULL,
    [created_date]       DATETIME2 (7)  NOT NULL,
    [created_by]         INT            NOT NULL,
    [modified_date]      DATETIME2 (7)  NULL,
    [modified_by]        INT            NULL,
    [owned_date]         DATETIME2 (7)  NOT NULL,
    [owned_by]           INT            NOT NULL,
    CONSTRAINT [PK_sys_table] PRIMARY KEY CLUSTERED ([sys_table_id] ASC),
    CONSTRAINT [fk_st_created] FOREIGN KEY ([created_by]) REFERENCES [cooperator] ([cooperator_id]),
    CONSTRAINT [fk_st_modified] FOREIGN KEY ([modified_by]) REFERENCES [cooperator] ([cooperator_id]),
    CONSTRAINT [fk_st_owned] FOREIGN KEY ([owned_by]) REFERENCES [cooperator] ([cooperator_id])
);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_st_created]
    ON [sys_table]([created_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_st_modified]
    ON [sys_table]([modified_by] ASC);


GO
CREATE NONCLUSTERED INDEX [ndx_fk_st_owned]
    ON [sys_table]([owned_by] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ndx_uniq_st]
    ON [sys_table]([table_name] ASC);


GO
GRANT DELETE
    ON OBJECT::[sys_table] TO [gg_user]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[sys_table] TO [gg_user]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[sys_table] TO [gg_user]
    AS [dbo];


GO
GRANT UPDATE
    ON OBJECT::[sys_table] TO [gg_user]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[sys_table] TO [gg_search]
    AS [dbo];

