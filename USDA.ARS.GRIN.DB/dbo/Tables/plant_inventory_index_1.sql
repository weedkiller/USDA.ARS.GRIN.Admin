﻿CREATE TABLE [plant_inventory_index] (
    [plant_inventory_index_id] INT          IDENTITY (1, 1) NOT NULL,
    [plant_inventory_volumn]   INT          NOT NULL,
    [plant_inventory_page]     INT          NOT NULL,
    [first_number]             INT          NOT NULL,
    [last_number]              INT          NOT NULL,
    [page_offset]              INT          NOT NULL,
    [volumn_suffix]            NVARCHAR (6) NOT NULL,
    CONSTRAINT [PK_plant_inventory_index_id] PRIMARY KEY CLUSTERED ([plant_inventory_index_id] ASC)
);


GO
GRANT DELETE
    ON OBJECT::[plant_inventory_index] TO [gg_user]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[plant_inventory_index] TO [gg_user]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[plant_inventory_index] TO [gg_user]
    AS [dbo];


GO
GRANT UPDATE
    ON OBJECT::[plant_inventory_index] TO [gg_user]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[plant_inventory_index] TO [gg_search]
    AS [dbo];

