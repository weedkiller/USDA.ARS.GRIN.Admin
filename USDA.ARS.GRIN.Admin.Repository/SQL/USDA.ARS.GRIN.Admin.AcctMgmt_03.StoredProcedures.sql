USE [gringlobal]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtWebUser_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtWebUser_Insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtWebCooperator_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtWebCooperator_Insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtWebCooperator_Copy]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtWebCooperator_Copy]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtUsers_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtUsers_Search]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtUserApplications_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtUserApplications_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtUserAggregate_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtUserAggregate_Search]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtUser_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtUser_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtUser_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtUser_Search]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtTitles_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtTitles_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtTableFieldName_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtTableFieldName_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUsers_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysUsers_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUsers_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysUsers_Search]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUserGroups_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysUserGroups_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUser_Update]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysUser_Update]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUser_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysUser_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUser_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysUser_Search]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUser_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysUser_Insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUser_Copy]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysUser_Copy]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysGroup_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysGroup_Insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysApplicationCooperators_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysApplicationCooperators_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysApplicationByURL_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSysApplicationByURL_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSearchOperators_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtSearchOperators_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtOwner_Update]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtOwner_Update]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtOrderRequestOwner_Update]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtOrderRequestOwner_Update]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtInventoryOwner_Update]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtInventoryOwner_Update]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCooperatorWebCooperator_Synch]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCooperatorWebCooperator_Synch]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCooperatorAggregate_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCooperatorAggregate_Search]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCooperatorAddress_Copy]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCooperatorAddress_Copy]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCooperator_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCooperator_Search]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCooperator_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCooperator_Insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCodeValuesByGroupName_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCodeValuesByGroupName_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCodeValues_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCodeValues_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCodesByGroup_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCodesByGroup_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCodeDetail_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCodeDetail_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCode_Update]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCode_Update]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCode_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtCode_Insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtApplications_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtApplications_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtActiveUsers_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
DROP PROCEDURE [dbo].[usp_AcctMgmtActiveUsers_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtActiveUsers_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtActiveUsers_Select]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	sys_user_id,
	user_name,
	c.first_name,
	c.last_name,
	c.email,
	c.primary_phone,
	is_enabled,
	c.created_date,
	c.modified_date
FROM
	sys_user su
JOIN
	cooperator c
ON
	su.cooperator_id = c.cooperator_id
WHERE 
	c.status_code = 'ACTIVE'
AND
	su.is_enabled = 'Y'
ORDER BY
	first_name,
	last_name
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtApplications_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtApplications_Select]
	@sys_user_id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		sys_application_id,
		sg.sys_group_id,
		sg.group_tag,
		code,
		title,
		description,
		url,
		color_code,
		image_file_name
	FROM
		sys_application sa
	JOIN
		sys_group sg
	ON
		sa.sys_group_id = sg.sys_group_id
	JOIN	
		sys_group_user_map sgum
	ON
		sg.sys_group_id = sgum.sys_group_id
	WHERE
		sgum.sys_user_id = @sys_user_id
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCode_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCode_Insert]
	@group_name NVARCHAR(100),
	@code_value NVARCHAR(20),
	@title NVARCHAR(500),
	@description NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CONST_COOPERATOR_ID INT = 48
	DECLARE @CONST_SYS_LANG_ID INT = 1
	DECLARE @new_code_value_id INT

	BEGIN TRY
	INSERT INTO
		code_value
		(
			group_name,
			value,
			created_date,
			created_by,
			owned_date,
			owned_by
		)
		VALUES
		(
			@group_name,
			@code_value,
			GETDATE(),
			@CONST_COOPERATOR_ID,
			GETDATE(),
			@CONST_COOPERATOR_ID
		)
		SET @new_code_value_id = CAST(SCOPE_IDENTITY() AS INT)

		INSERT INTO 
			code_value_lang
			(
				code_value_id,
				sys_lang_id,
				title,
				description,
				created_date,
				created_by,
				owned_date,
				owned_by
			)
			VALUES
			(
				@new_code_value_id,
				@CONST_SYS_LANG_ID,
				@title,
				@description,
				GETDATE(),
				@CONST_COOPERATOR_ID,
				GETDATE(),
				@CONST_COOPERATOR_ID
			)
			END TRY
			BEGIN CATCH
				IF @@TRANCOUNT > 0
				ROLLBACK TRANSACTION
			END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCode_Update]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCode_Update]
	@code_value_id INT,
	@value NVARCHAR(20),
	@title NVARCHAR(500),
	@description NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CONST_COOPERATOR_ID INT = 48
	DECLARE @CONST_SYS_LANG_ID INT = 1
	DECLARE @new_code_value_id INT

	BEGIN TRY

		UPDATE 
			code_value
		SET
			value = @value,
			modified_by = 48,
			modified_date = GETDATE()
		WHERE code_value_id = @code_value_id

		UPDATE 
			code_value_lang
		SET
			title = @title,
			description = @description,
			modified_by = 48,
			modified_date = GETDATE()
		WHERE 
			code_value_id = @code_value_id
		AND 
			sys_lang_id = 1
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCodeDetail_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCodeDetail_Select]
	@value NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CONST_SYS_LANG_ID INT = 1
	DECLARE @CONST_GROUP_NAME NVARCHAR(20) = 'TAXONOMY'

	SELECT  
		cv.code_value_id,
		group_name,
		value,
		cvl.title,
		cvl.description
	FROM 
		code_value cv
	JOIN
		code_value_Lang cvl
	ON
		cv.code_value_id = cvl.code_value_id
	WHERE 
		value = @value
	AND
		cvl.sys_lang_id = 1
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCodesByGroup_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCodesByGroup_Select]
	@group_name nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		cv.code_value_id,
		cvl.code_value_lang_id,
		cv.group_name,
		cv.value,
		cvl.title,
		cvl.description,
		cv.created_date,
		cv.modified_date
	FROM
		code_value cv
	JOIN
		code_value_lang cvl
	ON
		cv.code_value_id = cvl.code_value_id
	WHERE
		cv.group_name = @group_name
	AND
		cvl.sys_lang_id = 1
	ORDER BY cv.created_date DESC
	
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCodeValues_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCodeValues_Select]
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CONST_SYS_LANG_ID INT = 1
	DECLARE @CONST_GROUP_NAME NVARCHAR(20) = 'TAXONOMY'

	SELECT  
		cv.code_value_id,
		group_name,
		value,
		cvl.title,
		cvl.description
	FROM 
		code_value cv
	JOIN
		code_value_Lang cvl
	ON
		cv.code_value_id = cvl.code_value_id
	WHERE 
		(group_name LIKE '%' + @CONST_GROUP_NAME + '%'
	OR
		group_name LIKE '%CWR%')
	AND
		cvl.sys_lang_id = @CONST_SYS_LANG_ID
	ORDER BY 
		group_name, value
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCodeValuesByGroupName_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCodeValuesByGroupName_Select]
	@group_name NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CONST_SYS_LANG_ID INT = 1

	SELECT  
		cv.code_value_id,
		group_name,
		value,
		cvl.title,
		cvl.description
	FROM 
		code_value cv
	JOIN
		code_value_Lang cvl
	ON
		cv.code_value_id = cvl.code_value_id
	WHERE 
		group_name LIKE '%' + @group_name + '%'
	AND
		cvl.sys_lang_id = @CONST_SYS_LANG_ID
	ORDER BY 
		group_name ASC, value ASC
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCooperator_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCooperator_Insert] 
	@out_error_number INT = 0 OUTPUT,
	@out_cooperator_id INT = 0 OUTPUT,
	@title nvarchar(10),
	@last_name nvarchar(100),
	@first_name nvarchar(100),
	@password nvarchar(20),
	@job nvarchar(100),
	@site_id int,
	@organization nvarchar(100),
	@organization_abbrev nvarchar(10),
	@address_line1 nvarchar(100) = NULL,
	@address_line2 nvarchar(100) = NULL,
	@address_line3 nvarchar(100) = NULL,
	@city nvarchar(100),
	@postal_index nvarchar(100),
	@geography_id int,
	@primary_phone nvarchar(30),
	@email nvarchar(100),
	@status_code nvarchar(20),
	@category_code nvarchar(20) = NULL,
	@organization_region_code nvarchar(20) = NULL,
	@discipline_code nvarchar(20) = NULL,
	@note nvarchar(max) = NULL,
	@web_cooperator_id INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ADMIN_COOPERATOR_ID INT = 48
    DECLARE @ADMIN_WEB_COOPERATOR_ID INT = 1
	
	BEGIN TRY
		INSERT INTO cooperator
		(
		 site_id
		 ,last_name
		 ,title
		,first_name
		,job
		,organization
		,organization_abbrev
		,address_line1
		,address_line2
		,address_line3
		,city
		,postal_index
		,geography_id
		,primary_phone
		,email
		,status_code
		,category_code
		,organization_region_code
		,discipline_code
		,note
		,sys_lang_id
		,web_cooperator_id
		,created_date
		,created_by
		,owned_date
		,owned_by
		  )
		  VALUES
		  (
			@site_id,
			@last_name,
			@title,
			@first_name,
			@job,
			@organization,
			@organization_abbrev,
			@address_line1,
			@address_line2,
			@address_line3,
			@city,
			@postal_index,
			@geography_id,
			@primary_phone,
			@email,
			'ACTIVE',
			@category_code,
			@organization_region_code,
			@discipline_code,
			@note,
			1,
			@web_cooperator_id,
			GETDATE(),
			@ADMIN_COOPERATOR_ID,
			GETDATE(),
			@ADMIN_COOPERATOR_ID
		)
		SET @out_cooperator_id = CAST(SCOPE_IDENTITY() AS INT)
	END TRY
	BEGIN CATCH
		SELECT @out_error_number=ERROR_NUMBER()
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCooperator_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCooperator_Search] 
	@search_text NVARCHAR(120)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	SELECT
		 site_id
		 ,last_name
		 ,title
		,first_name
		,job
		,organization
		,organization_abbrev
		,address_line1
		,address_line2
		,address_line3
		,city
		,postal_index
		,geography_id
		,primary_phone
		,email
		,status_code
		,category_code
		,organization_region_code
		,discipline_code
		,note
		,sys_lang_id
		,web_cooperator_id
		,created_date
		,created_by
		,owned_date
		,owned_by
	FROM
		cooperator c
	WHERE
		c.first_name LIKE '%' + @search_text + '%'
	OR
		c.last_name LIKE '%' + @search_text + '%'
	END TRY
	BEGIN CATCH
		--TODO
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCooperatorAddress_Copy]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCooperatorAddress_Copy]
 @source_cooperator_id INT,
 @target_cooperator_id INT
AS
BEGIN
	SET NOCOUNT ON;

DROP TABLE IF EXISTS ##temp_cooperator 
CREATE TABLE ##temp_cooperator 
(
	temp_cooperator_id [int],
	site_id [int] NULL,
	organization [nvarchar](100) NULL,
	organization_abbrev [nvarchar](10) NULL,
	address_line1 [nvarchar](100) NULL,
	address_line2 [nvarchar](100) NULL,
	address_line3 [nvarchar](100) NULL,
	city [nvarchar](100) NULL,
	postal_index [nvarchar](100) NULL,
	geography_id [int] NULL,
	category_code NVARCHAR(20) NULL,
	organization_region_code NVARCHAR(20) NULL,
	discipline_code NVARCHAR(20) NULL
);

SELECT * FROM ##temp_cooperator

INSERT INTO ##temp_cooperator
	(temp_cooperator_id,
	site_id ,
	organization ,
	organization_abbrev ,
	address_line1,
	address_line2,
	address_line3 ,
	city ,
	postal_index ,
	geography_id,
	category_code,
	organization_region_code,
	discipline_code)
SELECT
	cooperator_id ,
	site_id ,
	organization ,
	organization_abbrev ,
	address_line1,
	address_line2,
	address_line3 ,
	city ,
	postal_index ,
	geography_id,
	category_code,
	organization_region_code,
	discipline_code
FROM
	cooperator
WHERE
	cooperator_id = @source_cooperator_id

UPDATE 
	cooperator 
SET 
	address_line1 = tc.address_line1,
	address_line2 = tc.address_line2,
	address_line3 = tc.address_line3,
	site_id = tc.site_id,
	city = tc.city,
	postal_index = tc.postal_index,
	geography_id = tc.geography_id,
	category_code = tc.category_code,
	organization_region_code = tc.organization_region_code,
	discipline_code = tc.discipline_code,
	modified_by = 1,
	modified_date = GETDATE()
FROM 
	##temp_cooperator tc
WHERE
	cooperator.cooperator_id = @target_cooperator_id 


END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCooperatorAggregate_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCooperatorAggregate_Search]
	@search_text NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		'PROD',
		c.status_code,
		c.cooperator_id,
		c.web_cooperator_id,
		c.last_name,
		c.first_name,
		c.email,
		c.site_id,
		c.address_line1,
		c.address_line2,
		c.address_line3,
		c.city,
		c.geography_id,
		(SELECT adm1 FROM geography WHERE geography_id = c.geography_id) AS address_state,
		c.postal_index,
		c.primary_phone,
		c.created_date,
		c.modified_date
	FROM
		cooperator c
	WHERE
		first_name LIKE '%' + @search_text + '%'
	OR
	   last_name LIKE '%' + @search_text + '%'
	OR
		email LIKE '%' + @search_text + '%'
	UNION
	SELECT
		'TRAINING',
		c.status_code,
		c.cooperator_id,
		c.web_cooperator_id,
		c.last_name,
		c.first_name,
		c.email,
		c.site_id,
		c.address_line1,
		c.address_line2,
		c.address_line3,
		c.city,
		c.geography_id,
		(SELECT adm1 FROM geography WHERE geography_id = c.geography_id) AS address_state,
		c.postal_index,
		c.primary_phone,
		c.created_date,
		c.modified_date
	FROM
		training.dbo.cooperator c
	WHERE
		first_name LIKE '%' + @search_text + '%'
	OR
	   last_name LIKE '%' + @search_text + '%'
	OR
		email LIKE '%' + @search_text + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtCooperatorWebCooperator_Synch]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtCooperatorWebCooperator_Synch]
	@out_error_number INT = 0 OUTPUT,
	@out_web_cooperator_id INT = 0 OUTPUT,
	@cooperator_id INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ADMIN_COOPERATOR_ID INT = 1

	BEGIN TRY
		INSERT INTO 
			web_cooperator
			(last_name,
			title,
			first_name,
			job,
			organization,
			organization_code,
			address_line1,
			address_line2,
			address_line3,
			city,
			postal_index,
			geography_id,
			primary_phone,
			secondary_phone,
			fax,
			email,
			is_active,
			category_code,
			organization_region,
			discipline,
			note,
			created_date,
			created_by,
			modified_date,
			modified_by,
			owned_date,
			owned_by)
		SELECT
			last_name,
			title,
			first_name,
			job,
			organization,
			organization_abbrev,
			address_line1,
			address_line2,
			address_line3,
			city,
			postal_index,
			geography_id,
			primary_phone,
			secondary_phone,
			fax,
			email,
			'ACTIVE',
			category_code,
			organization_region_code,
			discipline_code,
			note,
			GETDATE(),
			@ADMIN_COOPERATOR_ID,
			GETDATE(),
			@ADMIN_COOPERATOR_ID,
			GETDATE(),
			@ADMIN_COOPERATOR_ID
		FROM 
			cooperator
		WHERE
			cooperator_id = @cooperator_id
		SET @out_web_cooperator_id = CAST(SCOPE_IDENTITY() AS INT)
	END TRY
	BEGIN CATCH
		SELECT @out_error_number=ERROR_NUMBER()
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtInventoryOwner_Update]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtInventoryOwner_Update]
	@inventory_id INT,
	@cooperator_id INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE 
		inventory 
	SET 
		owned_by = @cooperator_id,
		owned_date = GETDATE()
	WHERE
		inventory_id = @inventory_id

	UPDATE 
		inventory_action
	SET 
		owned_by = @cooperator_id,
		owned_date = GETDATE()
	WHERE
		inventory_id = @inventory_id

	UPDATE 
		inventory_viability
	SET 
		owned_by = @cooperator_id,
		owned_date = GETDATE()
	WHERE
		inventory_id = @inventory_id

	UPDATE 
		inventory_quality_status
	SET 
		owned_by = @cooperator_id,
		owned_date = GETDATE()
	WHERE
		inventory_id = @inventory_id

	UPDATE 
		accession_inv_annotation
	SET 
		owned_by = @cooperator_id,
		owned_date = GETDATE()
	WHERE
		inventory_id = @inventory_id

	UPDATE 
		accession_inv_attach
	SET 
		owned_by = @cooperator_id,
		owned_date = GETDATE()
	WHERE
		inventory_id = @inventory_id

	UPDATE 
		accession_inv_name
	SET 
		owned_by = @cooperator_id,
		owned_date = GETDATE()
	WHERE
		inventory_id = @inventory_id

END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtOrderRequestOwner_Update]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[usp_AcctMgmtOrderRequestOwner_Update]
	@out_error_number INT = 0 OUTPUT,
	@order_request_id INT,
	@modified_by INT,
	@owned_by INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE 
			order_request
		SET
			owned_by = @owned_by,
			owned_date = GETDATE(),
			modified_by = @modified_by,
			modified_date = GETDATE()
		WHERE
			order_request_id = @order_request_id

		UPDATE 
			order_request_item
		SET
			owned_by = @owned_by,
			owned_date = GETDATE(),
			modified_by = @modified_by,
			modified_date = GETDATE()
		WHERE
			order_request_id = @order_request_id

		UPDATE 
			order_request_action
		SET
			owned_by = @owned_by,
			owned_date = GETDATE(),
			modified_by = @modified_by,
			modified_date = GETDATE()
		WHERE
			order_request_id = @order_request_id

		UPDATE 
			order_request_attach
		SET
			owned_by = @owned_by,
			owned_date = GETDATE(),
			modified_by = @modified_by,
			modified_date = GETDATE()
		WHERE
			order_request_id = @order_request_id
	END TRY
	BEGIN CATCH
		SELECT @out_error_number=ERROR_NUMBER()
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtOwner_Update]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtOwner_Update] 
	@out_error_number INT = 0 OUTPUT,
	@table_name NVARCHAR(50),
	@record_id INT,
	@owned_by INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sql_statement NVARCHAR(500)
	DECLARE @pk_column NVARCHAR(50)
	BEGIN TRY
		SELECT @pk_column = 
			stf.field_name
		FROM 
			sys_table st
		JOIN
			sys_table_field stf
		ON
			st.sys_table_id = stf.sys_table_id
		WHERE
			table_name = @table_name
		AND	
			stf.field_purpose = 'PRIMARY_KEY'

		SET @sql_statement = 'UPDATE ' + @table_name + ' SET owned_by=' + CONVERT(NVARCHAR(10), @owned_by) + ', owned_date = GETDATE(), modified_by = 48, modified_date = GETDATE() WHERE ' + @pk_column + '=' + CONVERT(NVARCHAR(10), @record_id)
		EXECUTE sp_executesql @sql_statement
		
	END TRY
	BEGIN CATCH
		SELECT @out_error_number=ERROR_NUMBER()
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSearchOperators_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_AcctMgmtSearchOperators_Select] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		sys_search_operator_id,
		title,
		syntax
	FROM 
		sys_search_operator
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysApplicationByURL_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysApplicationByURL_Select] 
	@url NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		[sys_application_id]
		,[sys_group_id]
		,[group_tag]
		,[code]
		,[title]
		,[description]
		,[url]
		,[color_code]
		,[image_file_name]
	FROM 
		[vw_AcctMgmtSysApplication]
	WHERE
		url = @url
		END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysApplicationCooperators_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysApplicationCooperators_Select]
	@sys_group_id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		su.sys_user_id,
		c.cooperator_id,
		c.last_name,
		c.first_name,
		c.last_name + ', ' + c.first_name AS full_name,
		c.email
	FROM
		sys_group_user_map sgum
	JOIN
		sys_user su
	ON
		sgum.sys_user_id = su.sys_user_id
	JOIN
		cooperator c
	ON
		su.cooperator_id = c.cooperator_id
	WHERE
		sys_group_id = @sys_group_id
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysGroup_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysGroup_Insert]
	@out_error_number INT = 0 OUTPUT,
	@out_group_id INT = 0 OUTPUT,
	@group_tag NVARCHAR(20)
AS
BEGIN

	DECLARE @CONST_COOPERATOR_ID INT = 48
	DECLARE @CONST_IS_ENABLED NVARCHAR(1) = 'Y'

	BEGIN TRY
		SET NOCOUNT ON;
		INSERT INTO 
			sys_group
			(
				group_tag,
				is_enabled,
				created_date,
				created_by,
				owned_date,
				owned_by
			)
		VALUES
		(
			@group_tag,
			@CONST_IS_ENABLED,
			GETDATE(),
			@CONST_COOPERATOR_ID,
			GETDATE(),
			@CONST_COOPERATOR_ID
		)
		SET @out_group_id = CAST(SCOPE_IDENTITY() AS INT)
	END TRY
	BEGIN CATCH
		SELECT @out_error_number=ERROR_NUMBER()
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUser_Copy]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysUser_Copy]
	@out_error_number INT = 0 OUTPUT,
	@out_sys_user_id INT = 0 OUTPUT,
	@user_name nvarchar(50),
	@password nvarchar(2000),
	@cooperator_id INT
	
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ADMIN_COOPERATOR_ID INT = 48

	SET @cooperator_id = 180787

	BEGIN TRY
		-- ========================================================================
		-- USER
		-- ========================================================================
		INSERT INTO training.dbo.sys_user 
		(
		 user_name,
		 password,
		 is_enabled,
		 cooperator_id,
		 created_date,
		 created_by,
		 owned_date,
		 owned_by
		)
		SELECT
		
		 user_name,
		 password,
		 is_enabled,
		 @cooperator_id,
		 GETDATE(),
		 @ADMIN_COOPERATOR_ID,
		 GETDATE(),
		 @ADMIN_COOPERATOR_ID
		FROM
		sys_user
		WHERE 
			sys_user_id = 0




		-- ========================================================================
		-- DEFAULT GROUPS
		-- ========================================================================
		INSERT INTO 
			sys_group_user_map
			(
				sys_group_id,
				sys_user_id,
				created_date,
				created_by,
				owned_date,
				owned_by
  			)
			VALUES
			(
				2,
				@out_sys_user_id,
				GETDATE(),
				@ADMIN_COOPERATOR_ID,
				GETDATE(),
				@ADMIN_COOPERATOR_ID
			)

		INSERT INTO 
		sys_group_user_map
		(
			sys_group_id,
			sys_user_id,
			created_date,
			created_by,
			owned_date,
			owned_by
  		)
		VALUES
		(
			3,
			@out_sys_user_id,
			GETDATE(),
			@ADMIN_COOPERATOR_ID,
			GETDATE(),
			@ADMIN_COOPERATOR_ID
		)
	END TRY
	BEGIN CATCH
		SELECT @out_error_number=ERROR_NUMBER()
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUser_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysUser_Insert]
	@out_error_number INT = 0 OUTPUT,
	@out_sys_user_id INT = 0 OUTPUT,
	@user_name nvarchar(50),
	@password nvarchar(2000),
	@cooperator_id INT
	
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ADMIN_COOPERATOR_ID INT = 48

	BEGIN TRY
		-- ========================================================================
		-- USER
		-- ========================================================================
		INSERT INTO sys_user 
		(
		 user_name,
		 password,
		 is_enabled,
		 cooperator_id,
		 created_date,
		 created_by,
		 owned_date,
		 owned_by
		)
		VALUES
		(
		 @user_name,
		 @password,
		 'Y',
		 @cooperator_id,
		 GETDATE(),
		 @ADMIN_COOPERATOR_ID,
		 GETDATE(),
		 @ADMIN_COOPERATOR_ID
		)
		SET @out_sys_user_id = CAST(SCOPE_IDENTITY() AS INT)

		-- ========================================================================
		-- DEFAULT GROUPS
		-- ========================================================================
		INSERT INTO 
			sys_group_user_map
			(
				sys_group_id,
				sys_user_id,
				created_date,
				created_by,
				owned_date,
				owned_by
  			)
			VALUES
			(
				2,
				@out_sys_user_id,
				GETDATE(),
				@ADMIN_COOPERATOR_ID,
				GETDATE(),
				@ADMIN_COOPERATOR_ID
			)

		INSERT INTO 
		sys_group_user_map
		(
			sys_group_id,
			sys_user_id,
			created_date,
			created_by,
			owned_date,
			owned_by
  		)
		VALUES
		(
			3,
			@out_sys_user_id,
			GETDATE(),
			@ADMIN_COOPERATOR_ID,
			GETDATE(),
			@ADMIN_COOPERATOR_ID
		)
	END TRY
	BEGIN CATCH
		SELECT @out_error_number=ERROR_NUMBER()
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUser_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysUser_Search] 
	@search_text NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sql_select NVARCHAR(2000)
	DECLARE @sql_where NVARCHAR(500)

	SET @sql_select = 'SELECT  
		user_name,
		password,
		is_enabled,
		cooperator_id,
		created_date,
		created_by,
		owned_date,
		owned_by
	FROM
		sys_user'

	IF LEN(TRIM(@search_text)) > 0
		BEGIN
			SET @sql_where = ' WHERE user_name LIKE ''%' + @search_text + '%'''
			SET @sql_select = @sql_select + @sql_where
		END

	EXECUTE sp_executesql @sql_select
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUser_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysUser_Select] 
	@sys_user_id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  
		su.sys_user_id,
		su.user_name,
		su.password,
		su.is_enabled,
		su.created_date,
		su.created_by,
		su.modified_date,
		su.modified_by,
		wu.web_user_id,
		wu.user_name AS web_user_name,
		c.cooperator_id,
		wc.web_cooperator_id,
		c.first_name,
		c.last_name,
		c.email,
		c.organization,
		c.organization_abbrev,
		c.organization_region_code,
		c.job,
		c.address_line1,
		c.address_line2,
		c.address_line3,
		c.city,
		c.geography_id,
		(SELECT adm1 FROM geography WHERE geography_id = c.geography_id) AS state,
		c.postal_index
	FROM
		sys_user su
	JOIN
		cooperator c
	ON
		su.cooperator_id = c.cooperator_id
	LEFT JOIN
			web_cooperator wc
	ON
		c.web_cooperator_id = wc.web_cooperator_id
	LEFT JOIN
		web_user wu
	ON
		wc.web_cooperator_id = wu.web_cooperator_id
	WHERE
		sys_user_id = @sys_user_id
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUser_Update]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysUser_Update]
	@out_error_number INT = 0 OUTPUT,
	@sys_user_id INT,
	@user_name nvarchar(50),
	@password nvarchar(2000)
	
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ADMIN_COOPERATOR_ID INT = 48

	BEGIN TRY
		UPDATE
			sys_user
		SET
			password = @password,
			modified_by = @ADMIN_COOPERATOR_ID,
			modified_date = GETDATE()
		WHERE
			sys_user_id = @sys_user_id
	END TRY
	BEGIN CATCH
		SELECT @out_error_number=ERROR_NUMBER()
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUserGroups_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysUserGroups_Select]
	@sys_user_id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		sys_group_user_map_id,
		sg.sys_group_id,
		sg.group_tag
	FROM 
		sys_group_user_map sgum
	JOIN
		sys_group sg
	ON
		sgum.sys_group_id = sg.sys_group_id
	WHERE
		sys_user_id = @sys_user_id
	ORDER BY 
		group_tag
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUsers_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysUsers_Search] 
	@sql_where_clause NVARCHAR(MAX) NULL 
	AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @sql NVARCHAR(MAX)

	BEGIN TRY

		SET @sql = 'SELECT  
			su.sys_user_id,
			su.user_name,
			su.password,
			su.is_enabled,
			su.created_date,
			su.created_by,
			su.modified_date,
			su.modified_by,
			wu.web_user_id,
			wu.user_name AS web_user_name,
			c.cooperator_id,
			wc.web_cooperator_id,
			c.first_name,
			c.last_name,
			c.email,
			c.organization,
			c.organization_abbrev,
			c.organization_region_code,
			c.job,
			c.address_line1,
			c.address_line2,
			c.address_line3,
			c.city,
			c.geography_id,
			(SELECT adm1 FROM geography WHERE geography_id = c.geography_id) AS state,
			c.postal_index
		FROM
			sys_user su
		JOIN
			cooperator c
		ON
			su.cooperator_id = c.cooperator_id
		LEFT JOIN
				web_cooperator wc
		ON
			c.web_cooperator_id = wc.web_cooperator_id
		LEFT JOIN
			web_user wu
		ON
			wc.web_cooperator_id = wu.web_cooperator_id'

		IF (LEN(TRIM(@sql_where_clause)) > 0)
			BEGIN
				SET @sql = @sql + ' ' + @sql_where_clause
			END

		EXECUTE sp_executesql @sql

	END TRY
	BEGIN CATCH
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
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtSysUsers_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtSysUsers_Select] 
	AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
	SELECT  
		su.sys_user_id,
		c.cooperator_id,
		su.user_name,
		su.password,
		su.is_enabled,
		su.cooperator_id,
		su.created_date,
		su.created_by,
		su.owned_date,
		su.owned_by
	FROM
		sys_user su
	JOIN
		cooperator c
	ON
		su.cooperator_id = c.cooperator_id
	ORDER BY
		user_name 
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
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtTableFieldName_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtTableFieldName_Select]
	@title NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		field_name
	FROM 
		vw_AcctMgmtSysTableFields
	WHERE
	 	table_name = @title
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtTitles_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtTitles_Select] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		DISTINCT title
	FROM
		cooperator
	WHERE 
		title IS NOT NULL
	ORDER BY
		title
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtUser_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtUser_Search]
	@user_name NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		su.sys_user_id,
		su.user_name,
		su.password,
		co.web_cooperator_id,
		co.first_name,
		co.last_name,
		co.email,
		co.organization,
		co.organization_abbrev,
		co.address_line1,
		co.address_line2,
		co.address_line3,
		co.city,
		co.geography_id,
		(SELECT adm1 FROM geography WHERE geography_id = co.geography_id) AS address_state,
		co.primary_phone,
		co.title,
		co.job,
		g.adm1 AS state,
		co.postal_index,
		su.is_enabled,
		su.cooperator_id,
		su.created_date,
		su.created_by,
		COALESCE(su.modified_date,su.created_date) AS modified_date,
		su.modified_by,
		su.owned_date,
		su.owned_by,
		co.sys_lang_id,
		co.site_id,
		s.site_short_name,
		s.site_long_name
	FROM 
		sys_user su
	LEFT JOIN 
		cooperator co 
	ON 
		su.cooperator_id = co.cooperator_id
	LEFT JOIN 
		site s 
	ON 
		co.site_id = s.site_id
	LEFT JOIN
		geography g
    ON
		co.geography_id = g.geography_id
	LEFT JOIN code_value cv ON g.country_code = cv.value AND cv.group_name = 'GEOGRAPHY_COUNTRY_CODE' 
    LEFT JOIN code_value_lang cvl ON  cv.code_value_id = cvl.code_value_id AND  cvl.sys_lang_id = 1

	WHERE 
		su.user_name LIKE '%' + @user_name + '%' 
	AND 
		su.is_enabled = 'Y'
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtUser_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtUser_Select]
	@sys_user_id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		su.sys_user_id,
		su.user_name,
		su.password,
		co.web_cooperator_id,
		co.first_name,
		co.last_name,
		co.email,
		co.organization,
		co.organization_abbrev,
		co.address_line1,
		co.address_line2,
		co.address_line3,
		co.city,
		co.geography_id,
		g.adm1 AS state,
		co.primary_phone,
		co.title,
		co.job,
		co.postal_index,
		su.is_enabled,
		su.cooperator_id,
		su.created_date,
		su.created_by,
		COALESCE(su.modified_date,su.created_date) AS modified_date,
		su.modified_by,
		su.owned_date,
		su.owned_by,
		co.sys_lang_id,
		co.site_id,
		s.site_short_name,
		s.site_long_name
	FROM 
		sys_user su
	LEFT JOIN 
		cooperator co 
	ON 
		su.cooperator_id = co.cooperator_id
	LEFT JOIN 
		site s 
	ON 
		co.site_id = s.site_id
	LEFT JOIN
		geography g
    ON
		co.geography_id = g.geography_id
	LEFT JOIN code_value cv ON g.country_code = cv.value AND cv.group_name = 'GEOGRAPHY_COUNTRY_CODE' 
    LEFT JOIN code_value_lang cvl ON  cv.code_value_id = cvl.code_value_id AND  cvl.sys_lang_id = 1
	WHERE 
		su.sys_user_id = @sys_user_id 
	AND 
		su.is_enabled = 'Y'
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtUserAggregate_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtUserAggregate_Search] 
	@search_text NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

SELECT 
	'SYS_USER',
	sys_user_id, 
	user_name, 
	password,
	is_enabled,
	created_date,
	modified_date
FROM 
	sys_user su
WHERE
	user_name LIKE '%' + @search_text + '%'
UNION
SELECT 
	'WEB_USER',
	web_user_id, 
	user_name, 
	password,
	is_enabled,
	created_date,
	modified_date
FROM 
	web_user wu
WHERE
	user_name LIKE '%' + @search_text + '%'

END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtUserApplications_Select]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtUserApplications_Select]
	@sys_user_id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		sys_application_id,
		sys_group_id,
		group_tag,
		code,
		title,
		description,
		url,
		color_code,
		image_file_name,
		(SELECT 
			COUNT(*) 
		 FROM 
			sys_group_user_map 
		 WHERE 
			sys_group_id = vsa.sys_group_id 
		 AND 
			sys_user_id = @sys_user_id) AS is_authorized
	FROM
		vw_AcctMgmtSysApplication vsa
	ORDER BY
		vsa.title
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtUsers_Search]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtUsers_Search]
	@sql_where_clause NVARCHAR(MAX) NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET FMTONLY OFF;
	DECLARE @sql NVARCHAR(MAX)
	
	SET @sql = 'SELECT 
	su.sys_user_id,
	su.user_name,
	su.password,
	su.is_enabled,
	su.created_date AS sys_user_created_date,
	su.created_by AS sys_user_created_by,
	su.modified_date AS sys_user_modified_date,
	su.modified_by AS sys_user_modified_by,
	co.cooperator_id,
	co.web_cooperator_id,
	co.first_name,
	co.last_name,
	co.primary_phone,
	co.email,
	co.title,
	co.job,
	co.organization,
	co.organization_abbrev,
	co.address_line1,
	co.address_line2,
	co.address_line3,
	co.city,
	co.geography_id,
	(SELECT adm1 FROM geography WHERE geography_id = co.geography_id) AS address_state,
	co.postal_index,
	co.created_date AS sys_user_created_date,
	co.created_by AS sys_user_created_by,
	co.modified_date AS sys_user_modified_date,
	co.modified_by AS sys_user_modified_by,
	s.site_id,
	s.site_short_name,
	s.site_long_name
FROM 
	sys_user su
JOIN 
	cooperator co 
ON 
	su.cooperator_id = co.cooperator_id
JOIN 
	site s 
ON 
	co.site_id = s.site_id
JOIN
	geography g
ON
	co.geography_id = g.geography_id'

	IF (LEN(LTRIM(RTRIM(@sql_where_clause))) > 0)
		BEGIN
			SET @sql = @sql + ' ' +  @sql_where_clause 
		END

	EXECUTE sp_executesql @sql
END

GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtWebCooperator_Copy]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtWebCooperator_Copy] 
	@cooperator_id INT
	AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @new_web_cooperator_id INT
	
	-- Web Cooperator
	INSERT INTO web_cooperator
	(
	[last_name]  ,
	[title] ,
	[first_name],
	[job],
	[organization] ,
	[organization_code] ,
	[address_line1] ,
	[address_line2] ,
	[address_line3] ,
	[city] ,
	[postal_index] ,
	[geography_id] ,
	[primary_phone],
	[email],
	[is_active],
	[category_code],
	[organization_region],
	[discipline],
	[note],
	[created_date] ,
	[created_by] ,
	[owned_date] ,
	[owned_by]
	)
	SELECT
	last_name,
	title,
	first_name,
	job,
	organization,
	[organization_abbrev] ,
	[address_line1] ,
	[address_line2] ,
	[address_line3] ,
	[city] ,
	[postal_index] ,
	[geography_id] ,
	[primary_phone],
	[email],
	CASE
		WHEN 
			status_code = 'ACTIVE'
		THEN
			'Y'
		ELSE 
			'N'
	END,
	[category_code],
	[organization_region_code],
	[discipline_code],
	[note],
	GETDATE(),
	1,
	GETDATE(),
	1
	FROM 
		cooperator
	WHERE
		cooperator_id = @cooperator_id

	SET @new_web_cooperator_id = CAST(SCOPE_IDENTITY() AS INT)

	
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtWebCooperator_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtWebCooperator_Insert] 
	@out_error_number INT = 0 OUTPUT,
	@out_web_cooperator_id INT = 0 OUTPUT,
	@title nvarchar(10),
	@last_name nvarchar(100),
	@first_name nvarchar(100),
	@password nvarchar(20),
	@job nvarchar(100),
	@organization nvarchar(100),
	@organization_code nvarchar(10),
	@address_line1 nvarchar(100) = NULL,
	@address_line2 nvarchar(100) = NULL,
	@address_line3 nvarchar(100) = NULL,
	@city nvarchar(100),
	@postal_index nvarchar(100),
	@geography_id int,
	@primary_phone nvarchar(30),
	@email nvarchar(100),
	@is_active nvarchar(1),
	@category_code nvarchar(20) = NULL,
	@organization_region nvarchar(20) = NULL,
	@discipline nvarchar(20) = NULL,
	@note nvarchar(max) = NULL
AS
BEGIN
	SET NOCOUNT ON;
    DECLARE @ADMIN_WEB_COOPERATOR_ID INT = 1
	
	BEGIN TRY
		INSERT INTO web_cooperator
			(
			[last_name]  ,
			[title] ,
			[first_name],
			[job],
			[organization] ,
			[organization_code] ,
			[address_line1] ,
			[address_line2] ,
			[address_line3] ,
			[city] ,
			[postal_index] ,
			[geography_id] ,
			[primary_phone],
			[email],
			[is_active],
			[category_code],
			[organization_region],
			[discipline],
			[note],
			[created_date] ,
			[created_by] ,
			[owned_date] ,
			[owned_by]
			)
		VALUES
			(
			@last_name,
			@title,
			@first_name,
			@job,
			@organization,
			@organization_code,
			@address_line1,
			@address_line2,
			@address_line3,
			@city,
			@postal_index,
			@geography_id,
			@primary_phone,
			@email,
			'Y',
			@category_code,
			@organization_region,
			@discipline,
			@note,
			GETDATE(),
			@ADMIN_WEB_COOPERATOR_ID,
			GETDATE(),
			@ADMIN_WEB_COOPERATOR_ID
			)
		SET @out_web_cooperator_id = CAST(SCOPE_IDENTITY() AS INT)
	END TRY
	BEGIN CATCH
		SELECT @out_error_number=ERROR_NUMBER()
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AcctMgmtWebUser_Insert]    Script Date: 7/2/2021 12:48:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcctMgmtWebUser_Insert]
	 @out_error_number INT = 0 OUTPUT,
	 @out_web_user_id INT = 0 OUTPUT,
	 @user_name nvarchar(50),
	 @password nvarchar(2000),
	 @is_enabled nvarchar(1),
	 @web_cooperator_id int NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		INSERT INTO web_user 
		(
		 user_name,
		 password,
		 is_enabled,
		 web_cooperator_id,
		 sys_lang_id,
		 created_date
		)
		VALUES
		(
		 @user_name,
		 @password,
		 'Y',
		 @web_cooperator_id,
		 1,
		 GETDATE()
		)
		SET @out_web_user_id = CAST(SCOPE_IDENTITY() AS INT)
	END TRY
	BEGIN CATCH
		SELECT @out_error_number=ERROR_NUMBER()
	END CATCH
END
GO
