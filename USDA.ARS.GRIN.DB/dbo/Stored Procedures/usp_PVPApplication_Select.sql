﻿CREATE PROCEDURE [dbo].[usp_PVPApplication_Select]
	@pvp_application_number INT
AS
BEGIN
	SELECT 
		pa.pvp_application_number
		,[cultivar_name]
		,[experimental_name]
		,pa.pvp_common_name_id
		,pa.scientific_name
		,pc.name AS common_name
		,pa.pvp_applicant_id
		,papp.name AS applicant_name
		,[application_date]
		,[is_certified_seed]
		,pas.pvp_application_status_id
		,pas.description AS application_status
		,[status_date]
		,[certificate_issued_date]
		,[years_protected]
		 ,CONVERT(nvarchar(12),dateadd(year,years_protected,cast(certificate_issued_date as date)),101) AS expiration_date
		,vi.accession_id
	FROM 
		[gringlobal].[dbo].[pvp_application] pa
	JOIN
		pvp_common_name pc
	ON
		pa.pvp_common_name_id = pc.pvp_common_name_id
	JOIN
		pvp_application_status pas
	ON
		pa.pvp_application_status_id = pas.pvp_application_status_id
	JOIN
		pvp_applicant papp
	ON
		pa.pvp_applicant_id = papp.pvp_applicant_id
	LEFT JOIN
		vw_accession_ipr vi
	ON
		pa.pvp_application_number = vi.pv_number
WHERE
	 pa.pvp_application_number = @pvp_application_number
END
