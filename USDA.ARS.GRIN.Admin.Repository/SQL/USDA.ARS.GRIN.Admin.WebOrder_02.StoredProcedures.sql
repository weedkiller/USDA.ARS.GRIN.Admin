﻿USE [training]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestStatuses_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequestStatuses_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestsByStatus_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequestsByStatus_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequests_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequests_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequests_Search]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequests_Search]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestLock_Update]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequestLock_Update]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestItems_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequestItems_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestItem_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequestItem_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestAddresses_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequestAddresses_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestActions_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequestActions_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestAction_Insert]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequestAction_Insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequest_Update]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequest_Update]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequest_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_WebOrderRequest_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequest_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequest_Select] 
	@web_order_request_id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		wor.web_order_request_id
		-- WEB COOPERATOR
		,wor.web_cooperator_id
		,wc.title AS web_cooperator_title
		,wc.last_name AS web_cooperator_last_name
		,wc.first_name AS web_cooperator_first_name
		,(wc.first_name + ' ' + wc.last_name) AS web_cooperator_full_name
		,wc.organization AS web_cooperator_organization
		,wc.job AS web_cooperator_job
		,wc.address_line1 AS web_cooperator_address_line_1
		,wc.address_line2 AS web_cooperator_address_line_2
		,wc.address_line3 AS web_cooperator_address_line_3
		,wc.city AS web_cooperator_address_city
		,wc.postal_index AS web_cooperator_address_postal_index
		,wc.geography_id
		,(SELECT adm1 FROM geography WHERE geography_id = wc.geography_id) AS web_cooperator_address_state
		,wc.primary_phone AS web_cooperator_primary_phone
		,wc.email AS web_cooperator_email
		,wc.created_date AS web_cooperator_created_date
		,wc.modified_date AS web_cooperator_modified_date
		,wc.status_code AS web_cooperator_status_code
		,wc.note As web_cooperator_note
		,wora.address_line1 AS web_order_request_address_line_1
		,wora.address_line2 AS web_order_request_address_line_2
		,wora.address_line3 AS web_order_request_address_line_3
		,wora.city AS web_order_request_city
		,wora.postal_index AS web_order_request_postal_index
		,wora.geography_id AS web_order_request_geography_id
		,(SELECT adm1 FROM geography WHERE geography_id = wora.geography_id) AS web_order_request_state
		,wor.ordered_date
		,wor.intended_use_code
		,wor.intended_use_note
		,wor.status_code
		,wor.note
		,wor.special_instruction
		,wor.created_date
		,wor.created_by
		,wor.modified_date
		,wor.modified_by
		,wor.owned_date
		,wor.owned_by
		,wor.is_locked
	FROM
		web_order_request wor
	LEFT JOIN 
		web_cooperator wc 
	ON 
		wc.web_cooperator_id = wor.web_cooperator_id
	LEFT JOIN 
		web_order_request_address wora 
	ON 
		wora.web_order_request_id = wor.web_order_request_id
	WHERE
		wor.web_order_request_id = @web_order_request_id
END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequest_Update]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequest_Update] 
	@out_error_number INT = 0 OUTPUT,
	@web_order_request_id INT,
	@web_cooperator_id INT,
	@status_code NVARCHAR(20),
	@note NVARCHAR(2000)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @web_order_request_status_code NVARCHAR(20)
	DECLARE @web_order_request_item_status_code NVARCHAR(20)
	DECLARE @web_order_request_action_id INT
	DECLARE @error_code INT

	BEGIN TRY

		-- If order has been approved following an NRR review, reset the statuses
		-- of the order and its items to their original values, and undo any existing lock.
		IF (@status_code = 'NRR_APPROVE')
		BEGIN
			SET @web_order_request_status_code = 'SUBMITTED'
			SET @web_order_request_item_status_code = 'NEW'
		END
		ELSE
		BEGIN
			IF (@status_code = 'NRR_REJECT')
				BEGIN
					SET @web_order_request_status_code = 'CANCELED'
					SET @web_order_request_item_status_code = 'CANCEL'
				END
		END

		UPDATE
			web_order_request 
		SET
			status_code = @web_order_request_status_code,
			note = @note,
			modified_by = 1,
			modified_date = GETDATE(),
			is_locked = 0,
			owned_by = 1,
			owned_date = GETDATE()
		WHERE
			web_order_request_id = @web_order_request_id

		UPDATE
			web_order_request_item 
		SET
			status_code = @web_order_request_item_status_code,
			modified_by = 1,
			modified_date = GETDATE()
		WHERE
			web_order_request_id = @web_order_request_id
	
		--EXEC usp_WebOrderRequestAction_Insert @error_code OUTPUT, @web_order_request_action_id, @web_order_request_id, @status_code, @note, 1

	END TRY
	BEGIN CATCH
		ROLLBACK;
		INSERT INTO 
			dbo.sys_db_error
		VALUES
		  (SUSER_SNAME(),
		   ERROR_NUMBER(),
		   ERROR_STATE(),
		   ERROR_SEVERITY(),
		   ERROR_LINE(),
		   ERROR_PROCEDURE(),
		   ERROR_MESSAGE(),
		   GETDATE());
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestAction_Insert]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequestAction_Insert]
	@out_error_number INT = 0 OUTPUT,
	@out_web_order_request_action_id INT = 0 OUTPUT,
	@web_order_request_id INT,
	@action_code NVARCHAR(20),
	@note NVARCHAR(MAX) = '',
	@created_by INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		IF NOT EXISTS (SELECT 
							web_order_request_action_id 
					   FROM 
							web_order_request_action 
					   WHERE 
							web_order_request_id = @web_order_request_id
					   AND
							action_code != 'NRR_NOTE'
					   AND 
							action_code = @action_code)
		BEGIN
			INSERT INTO 
				web_order_request_action
				(web_order_request_id,
				action_code,
				acted_date,
				note,
				web_cooperator_id,
				created_date,
				created_by,
				owned_date,
				owned_by)
			VALUES
				(@web_order_request_id,
				@action_code,
				GETDATE(),
				@note,
				@created_by,
				GETDATE(),
				@created_by,
				GETDATE(),
				@created_by)
		END
	END TRY
	BEGIN CATCH
		ROLLBACK;
		INSERT INTO 
			dbo.sys_db_error
		VALUES
		  (SUSER_SNAME(),
		   ERROR_NUMBER(),
		   ERROR_STATE(),
		   ERROR_SEVERITY(),
		   ERROR_LINE(),
		   ERROR_PROCEDURE(),
		   ERROR_MESSAGE(),
		   GETDATE());
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestActions_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequestActions_Select]
	@web_order_request_id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		web_order_request_action_id,
		web_order_request_id,
		action_code,
		cvl.title AS action_title,
		cvl.description AS action_description,
		COALESCE(acted_date, wora.created_date, wora.modified_date) AS action_date,
		CONVERT(date, acted_date) AS action_date_converted,
		note,
		COALESCE(web_cooperator_id, wora.created_by, wora.modified_by) AS cooperator_id,
		wora.created_by,
		(SELECT first_name + '  ' + last_name FROM web_cooperator WHERE web_cooperator_id = wora.created_by) AS created_by_name,
		wora.created_date
FROM 
	web_order_request_action wora
JOIN
	code_value cv
ON
	wora.action_code = cv.value
JOIN
	code_value_lang cvl
ON
	cv.code_value_id = cvl.code_value_id
WHERE
	cv.group_name = 'WEB_ORDER_REQUEST_STATUS'
AND
	cvl.sys_lang_id = 1
AND
	web_order_request_id = @web_order_request_id
ORDER BY
	acted_date DESC
END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestAddresses_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequestAddresses_Select]
	@web_order_request_id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		wora.web_order_request_address_id,
		wora.address_line1,
		wora.address_line2,
		wora.address_line3,
		wora.city,
		wora.geography_id,
		g.adm1,
		g.adm2,
		g.adm3,
		wora.postal_index,
		wora.carrier,
		wora.carrier_account
FROM 
	web_order_request_address wora
JOIN
	geography g
ON
	wora.geography_id = g.geography_id
WHERE
	web_order_request_id = @web_order_request_id
END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestItem_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequestItem_Select] 
	@web_order_request_item_id INT
AS
BEGIN
	SET NOCOUNT ON;
SELECT
	wori.web_order_request_item_id,
	web_cooperator_id,
	web_order_request_id,
	sequence_number,
	accession_id,
	status_code,
	(SELECT c.site_id FROM cooperator c JOIN accession a ON a.owned_by = c.cooperator_id AND a.accession_id = wori.accession_id) AS site_id,
	name,
	quantity_shipped,
	unit_of_shipped_code,
	distribution_form_code,
	curator_note,
	user_note,
	(SELECT taxonomy_species_id FROM accession WHERE accession_id = wori.accession_id) AS taxonomy_species_id,
	created_date,
	created_by,
	modified_date,
	modified_by,
	owned_date,
	owned_by
FROM
    web_order_request_item wori
WHERE
	web_order_request_item_id = @web_order_request_item_id
  

END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestItems_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequestItems_Select] 
	@web_order_request_id INT
AS
BEGIN
	SET NOCOUNT ON;
SELECT
	wori.web_order_request_item_id,
	wori.web_cooperator_id,
	web_order_request_id,
	sequence_number,
	a.accession_id,
	LTRIM(RTRIM(LTRIM(COALESCE(accession_number_part1, '') + ' ') + LTRIM(COALESCE(convert(varchar, accession_number_part2), '') + ' ') + COALESCE(accession_number_part3, ''))) AS accession_text,
	wori.status_code,
	c.site_id,
	s.site_long_name,
	wori.name,
	ISNULL(quantity_shipped,0) AS quantity_shipped,
	unit_of_shipped_code,
	(SELECT TOP 1
		title 
	 FROM 
		code_value_lang cvl
	 JOIN 
		code_value cv 
	 ON 
		cvl.code_value_id = cv.code_value_id
	 WHERE 
		value = unit_of_shipped_code 
	 AND 
		cvl.sys_lang_id = 1 
	 AND 
		group_name = 'UNIT_OF_QUANTITY') AS unit_of_shipped,
	distribution_form_code,
	(SELECT TOP 1
		description 
	 FROM 
		code_value_lang cvl
	 JOIN 
		code_value cv 
	 ON 
		cvl.code_value_id = cv.code_value_id
	 WHERE 
		value = distribution_form_code 
	 AND cvl.sys_lang_id = 1) AS distribution_form,
	curator_note,
	user_note,
	ts.taxonomy_species_id,
	ts.name AS species_name
FROM
    web_order_request_item wori
JOIN
	accession a
ON
	wori.accession_id = a.accession_id
JOIN
	cooperator c
ON
	a.owned_by = c.cooperator_id
JOIN
	site s
ON
	c.site_id = s.site_id
JOIN
	taxonomy_species ts
ON
	a.taxonomy_species_id = ts.taxonomy_species_id
WHERE
	web_order_request_id = @web_order_request_id
  

END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestLock_Update]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequestLock_Update] 
	@out_error_number INT = 0 OUTPUT,
	@web_order_request_id INT,
	@web_cooperator_id INT,
	@is_locked BIT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		
		DECLARE @owned_by INT
		DECLARE @owned_date DATETIME2(7)

		IF (@is_locked = 0)
		BEGIN
			SET @owned_by = 1
			SET @owned_date = GETDATE()
		END
		ELSE
			BEGIN
				SET @owned_by = @web_cooperator_id
				SET @owned_date = GETDATE()
			END

		UPDATE
			web_order_request 
		SET
			is_locked = @is_locked,
			owned_by = @owned_by,
			owned_date = @owned_date
		WHERE
			web_order_request_id = @web_order_request_id
	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		INSERT INTO 
			dbo.sys_db_error
		VALUES
		  (SUSER_SNAME(),
		   ERROR_NUMBER(),
		   ERROR_STATE(),
		   ERROR_SEVERITY(),
		   ERROR_LINE(),
		   ERROR_PROCEDURE(),
		   ERROR_MESSAGE(),
		   GETDATE());
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequests_Search]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequests_Search]
	@sql_where_clause NVARCHAR(2000)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sql_select NVARCHAR(MAX)

	SET @sql_select = 
	'SELECT 
		wor.web_order_request_id,
		wor.is_locked,
		wor.web_cooperator_id
		,wc.last_name
		,wc.title
		,wc.first_name
		,(wc.first_name + '' '' + wc.last_name) AS full_name
		,wc.title AS web_cooperator_title
		,wc.last_name AS web_cooperator_last_name
		,wc.first_name AS web_cooperator_first_name
		,(wc.first_name + '' '' + wc.last_name) AS web_cooperator_full_name
		,wc.organization AS web_cooperator_organization
		,wc.address_line1 AS web_cooperator_address_line_1
		,wc.address_line2 AS web_cooperator_address_line_2
		,wc.address_line3 AS web_cooperator_address_line_3
		,wc.city AS web_cooperator_address_city
		,wc.postal_index AS web_cooperator_address_postal_index
		,wc.geography_id
		,g.adm1 AS web_cooperator_address_state
		,wc.primary_phone AS web_cooperator_primary_phone
		,wc.email AS web_cooperator_email
		,wc.created_date AS web_cooperator_created_date
		,wc.modified_date AS web_cooperator_modified_date
		,wora.address_line1
		,wora.address_line2
		,wora.address_line3
		,wora.city
		,wora.postal_index
		,wora.geography_id
		,wc.primary_phone
		,wc.email
		,wor.ordered_date
		,wor.intended_use_code
		,wor.intended_use_note
		,wor.status_code
		,wor.note
		,wor.special_instruction
		,wor.created_date,
		wor.created_by,
		(SELECT first_name + '' '' + last_name FROM cooperator WHERE cooperator_id = wor.created_by) AS created_by_name,
		wor.modified_date,
		wor.modified_by,
		(SELECT first_name + '' '' + last_name FROM cooperator WHERE cooperator_id = wor.modified_by) AS modified_by_name,
		wor.owned_date,
		wor.owned_by,
		(SELECT first_name + '' '' + last_name FROM cooperator WHERE cooperator_id = wor.owned_by) AS owned_by_name
	FROM
		web_order_request wor
	JOIN 
		web_cooperator wc 
	ON 
		wc.web_cooperator_id = wor.web_cooperator_id
	JOIN 
		web_order_request_address wora 
	ON 
		wora.web_order_request_id = wor.web_order_request_id
	JOIN
		geography g
	ON
		wc.geography_id = g.geography_id'
	SET @sql_select = @sql_select + @sql_where_clause + ' ORDER BY wor.ordered_date DESC'

	--SELECT @sql_select
	EXECUTE sp_executesql @sql_select
END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequests_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequests_Select]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT wor.web_order_request_id
      ,wor.web_cooperator_id
      ,wc.last_name
      ,wc.title
      ,wc.first_name
	  ,(wc.first_name + ' ' + wc.last_name) AS full_name
      ,wc.organization
      ,wora.address_line1
      ,wora.address_line2
      ,wora.address_line3
      ,wora.city
      ,wora.postal_index
      ,wora.geography_id
      ,wc.primary_phone
      ,wc.email
      ,wor.ordered_date
      ,wor.intended_use_code
      ,wor.intended_use_note
      ,wor.status_code
      ,wor.note
      ,wor.special_instruction
      ,wor.created_date
      ,wor.created_by
      ,wor.modified_date
      ,wor.modified_by
      ,wor.owned_date
      ,wor.owned_by
FROM
    web_order_request wor
LEFT JOIN 
	web_cooperator wc 
ON 
	wc.web_cooperator_id = wor.web_cooperator_id
LEFT JOIN 
	web_order_request_address wora 
ON 
	wora.web_order_request_id = wor.web_order_request_id
WHERE
	wor.status_code = 'CANCELED'
AND 
	wor.ordered_date > '8/1/2020'
END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestsByStatus_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequestsByStatus_Select]
	@status_code NVARCHAR(20),
	@time_frame_code INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sql_select NVARCHAR(MAX)
	DECLARE @sql_where NVARCHAR(2000) = ''

	SET @sql_select = 
	'SELECT 
		wor.web_order_request_id,
		wor.is_locked,
		wor.web_cooperator_id
		,wc.last_name
		,wc.title
		,wc.first_name
		,(wc.first_name + '' '' + wc.last_name) AS full_name
		,wc.title AS web_cooperator_title
		,wc.last_name AS web_cooperator_last_name
		,wc.first_name AS web_cooperator_first_name
		,(wc.first_name + '' '' + wc.last_name) AS web_cooperator_full_name
		,wc.organization AS web_cooperator_organization
		,wc.address_line1 AS web_cooperator_address_line_1
		,wc.address_line2 AS web_cooperator_address_line_2
		,wc.address_line3 AS web_cooperator_address_line_3
		,wc.city AS web_cooperator_address_city
		,wc.postal_index AS web_cooperator_address_postal_index
		,wc.geography_id
		,g.adm1 AS web_cooperator_address_state
		,wc.primary_phone AS web_cooperator_primary_phone
		,wc.email AS web_cooperator_email
		,wc.created_date AS web_cooperator_created_date
		,wc.modified_date AS web_cooperator_modified_date
		,wora.address_line1
		,wora.address_line2
		,wora.address_line3
		,wora.city
		,wora.postal_index
		,wora.geography_id
		,wc.primary_phone
		,wc.email
		,wor.ordered_date
		,wor.intended_use_code
		,wor.intended_use_note
		,wor.status_code
		,wor.note
		,wor.special_instruction
		,wor.created_date,
		wor.created_by,
		(SELECT first_name + '' '' + last_name FROM cooperator WHERE cooperator_id = wor.created_by) AS created_by_name,
		wor.modified_date,
		wor.modified_by,
		(SELECT first_name + '' '' + last_name FROM cooperator WHERE cooperator_id = wor.modified_by) AS modified_by_name,
		wor.owned_date,
		wor.owned_by,
		(SELECT first_name + '' '' + last_name FROM cooperator WHERE cooperator_id = wor.owned_by) AS owned_by_name
	FROM
		web_order_request wor
	JOIN 
		web_cooperator wc 
	ON 
		wc.web_cooperator_id = wor.web_cooperator_id
	JOIN 
		web_order_request_address wora 
	ON 
		wora.web_order_request_id = wor.web_order_request_id
	JOIN
		geography g
	ON
		wc.geography_id = g.geography_id'

	IF (@status_code != 'ANY') OR (@time_frame_code > 0)
		BEGIN
			IF (@status_code != 'ANY')
				BEGIN
					SET @sql_where = @sql_where + ' WHERE wor.status_code = ''' + @status_code + ''''
				END

			IF (@time_frame_code > 0)
				BEGIN
					IF LEN(@sql_where) > 0
						BEGIN
							SET @sql_where = @sql_where + ' AND '
						END
					ELSE
						BEGIN
							SET @sql_where = @sql_where + ' WHERE '
						END
				END

			IF (@time_frame_code = 1)
				BEGIN
					SET @sql_where = @sql_where + ' wor.created_date >= DATEADD(day,-1, GETDATE())'
				END
		ELSE
			BEGIN
				IF (@time_frame_code = 2)
					BEGIN
						SET @sql_where = @sql_where +  ' wor.created_date >= DATEADD(day,-7, GETDATE())'
					END
				ELSE
					BEGIN
						IF (@time_frame_code = 3)
							BEGIN
								SET @sql_where = @sql_where +  ' wor.created_date >= DATEADD(day,-30, GETDATE())'
							END
						ELSE
							BEGIN
								IF (@time_frame_code = 4)
									BEGIN
										SET @sql_where = @sql_where +  ' wor.created_date >= DATEADD(day,-180, GETDATE())'
									END
							END
					END
			END
			SET @sql_select = @sql_select + @sql_where + ' ORDER BY wor.ordered_date DESC'
		END
	--SELECT @sql_select
	EXECUTE sp_executesql @sql_select
END
GO
/****** Object:  StoredProcedure [dbo].[usp_WebOrderRequestStatuses_Select]    Script Date: 5/18/2021 10:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_WebOrderRequestStatuses_Select] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		status_code,
		COUNT(web_order_request_id) AS web_order_request_count
	FROM 
		web_order_request
	GROUP BY 
		status_code
END
GO
