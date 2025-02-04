USE [gringlobal]
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtTableFieldName_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_DataMgmtTableFieldName_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtSysTables_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_DataMgmtSysTables_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCodeValuesByGroupName_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_DataMgmtCodeValuesByGroupName_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCodeValues_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_DataMgmtCodeValues_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCodesByGroup_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_DataMgmtCodesByGroup_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCodeDetail_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_DataMgmtCodeDetail_Select]
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCode_Update]    Script Date: 5/24/2021 11:17:09 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_DataMgmtCode_Update]
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCode_Insert]    Script Date: 5/24/2021 11:17:09 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_DataMgmtCode_Insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCode_Insert]    Script Date: 5/24/2021 11:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DataMgmtCode_Insert]
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
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCode_Update]    Script Date: 5/24/2021 11:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DataMgmtCode_Update]
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
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCodeDetail_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DataMgmtCodeDetail_Select]
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
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCodesByGroup_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DataMgmtCodesByGroup_Select]
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
	ORDER BY cv.value 
	
END
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCodeValues_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DataMgmtCodeValues_Select]
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
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtCodeValuesByGroupName_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DataMgmtCodeValuesByGroupName_Select]
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
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtSysTables_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DataMgmtSysTables_Select]
AS
BEGIN
	SET NOCOUNT ON;
	/****** Script for SelectTopNRows command from SSMS  ******/
	SELECT 
		st.sys_table_id,
		stl.sys_table_lang_id,
		st.table_name,
		stl.title
	FROM 
		sys_table st
	JOIN
		sys_table_lang stl
	ON
		st.sys_table_id = stl.sys_table_id
	AND
		stl.sys_lang_id = 1
END
GO
/****** Object:  StoredProcedure [dbo].[usp_DataMgmtTableFieldName_Select]    Script Date: 5/24/2021 11:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DataMgmtTableFieldName_Select]
	@title NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		field_name
	FROM 
		vw_table_fields
	WHERE
		formatted_title = @title
END
GO
