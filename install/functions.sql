/*
//-------------------------------------------------------------------
/// File       : functions.sql
/// Version    : 1.0
/// Created    : 2008-09-13
/// Modified   : 2008-09-13
///
/// Author     : William Chang
/// Email      : william@babybluebox.com
/// Website    : http://www.babybluebox.com
///
/// Compatible : Microsoft SQL Server 8+
//-------------------------------------------------------------------
/// References:
/// http://www.sqlteam.com/article/intro-to-user-defined-functions-updated
/// http://www.sqlteam.com/article/user-defined-functions
/// http://www.sqlbook.com/SQL/Create-comma-delimited-list-27.aspx
//-------------------------------------------------------------------
*/

USE [ucf_ent];
GO
IF OBJECT_ID('[dbo].[fnSelectListName]') IS NOT NULL 
	DROP FUNCTION [dbo].[fnSelectListName]
GO
CREATE FUNCTION [dbo].[fnSelectListName](
	@list_id int,
	@list_deleted bit = 0
)
RETURNS nvarchar(256)
AS
BEGIN
	DECLARE @list_name nvarchar(256)

	SET @list_name = (
		SELECT list_name FROM tablelist WHERE list_id = @list_id AND list_deleted = @list_deleted
	}

	RETURN ISNULL(@list_name, '')
END
GO