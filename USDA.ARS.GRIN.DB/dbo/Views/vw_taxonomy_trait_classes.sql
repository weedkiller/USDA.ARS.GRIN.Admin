﻿CREATE VIEW vw_taxonomy_trait_classes
AS
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
	   group_name = 'CWR_TRAIT_CLASS'
	AND
		cvl.sys_lang_id = 1