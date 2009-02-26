/*
//-------------------------------------------------------------------
/// File       : stored_procedures.sql
/// Version    : 1.0
/// Created    : 2008-08-29
/// Modified   : 2009-02-03
///
/// Author     : William Chang
/// Email      : william@babybluebox.com
/// Website    : http://www.babybluebox.com
///
/// Compatible : Microsoft SQL Server 8+
//-------------------------------------------------------------------
/// References:
/// http://www.sqlteam.com/article/temporary-tables
/// http://www.sqlteam.com/article/stored-procedures-returning-data
/// http://www.sqlbook.com/SQL/Create-comma-delimited-list-27.aspx
/// http://www.udel.edu/evelyn/SQL-Class3/SQL3_SQL.html
///
/// Dynamic SQL:
/// http://www.sommarskog.se/dynamic_sql.html
//-------------------------------------------------------------------
*/

/* Dynamic SQL
//-------------------------------------------------------------------*/
USE [ucf_ent];
IF OBJECT_ID('[dbo].[spGeneralSelect]') IS NOT NULL
	DROP PROCEDURE [dbo].[spGeneralSelect]
GO
CREATE PROCEDURE [dbo].[spGeneralSelect](
	@tablename nvarchar(256),
	@key nvarchar(256),
	@debug bit = 0
)
AS
SET NOCOUNT ON
BEGIN
	DECLARE @sql_statement nvarchar(max)
	DECLARE @sql_params nvarchar(max)

	SET @sql_params = 
	N'@key nvarchar(256)'
	
	SET @sql_statement = 
	N' SELECT *' + 
	N' FROM dbo.' + @tablename + 
	N' WHERE id = @key'
	
	IF @debug = 1 PRINT @sql_statement
	
	EXEC sp_executesql @sql_statement, @sql_params, @key = @key
END
SET NOCOUNT OFF
GO

/* INSERT
//-------------------------------------------------------------------*/
USE [ucf_ent];
IF OBJECT_ID('[dbo].[spInsertUser]') IS NOT NULL
	DROP PROCEDURE [dbo].[spInsertUser]
GO
CREATE PROCEDURE [dbo].[spInsertUser](
	@user_alias nvarchar(64),
	@user_name_first nvarchar(64),
	@user_name_last nvarchar(64),
	@user_password nvarchar(64) = '',
	@user_email nvarchar(128),
	@user_activation_key nvarchar(64) = '',
	@user_activated bit = 0,
	@user_session_key nvarchar(64) = '',
	@user_date_login datetime = null,
	@user_date_activation_created datetime,
	@user_date_session_created datetime = null,
	@user_date_created datetime,
	@user_deleted bit = 0,
	
	@setting_key nvarchar(256) = 'role',
	@setting_value int = 4
)
AS
SET NOCOUNT ON
BEGIN
	DECLARE @user_id int

	INSERT INTO users(
		user_alias,
		user_name_first,
		user_name_last,
		user_password,
		user_email,
		user_activation_key,
		user_activated,
		user_session_key,
		user_date_login,
		user_date_activation_created,
		user_date_session_created,
		user_date_created,
		user_deleted
	)
	VALUES(
		@user_alias,
		@user_name_first,
		@user_name_last,
		@user_password,
		@user_email,
		@user_activation_key,
		@user_activated,
		@user_session_key,
		@user_date_login,
		@user_date_activation_created,
		@user_date_session_created,
		@user_date_created,
		@user_deleted
	)
	SET @user_id = IDENT_CURRENT('users')
	
	INSERT INTO user_settings(
		setting_user_id,
		setting_key,
		setting_value
	)
	VALUES (
		@user_id,
		@setting_key,
		@setting_value
	);
	
	/*SELECT SCOPE_IDENTITY()*/
	SELECT @user_id
END
SET NOCOUNT OFF
GO

USE [ucf_ent];
IF OBJECT_ID('[dbo].[spInsertUserProfile]') IS NOT NULL 
	DROP PROCEDURE [dbo].[spInsertUserProfile]
GO
CREATE PROCEDURE [dbo].[spInsertUserProfile](
	@location_name nvarchar(256),
	@location_address1 nvarchar(128),
	@location_address2 nvarchar(128) = '',
	@location_city nvarchar(64),
	@location_state nvarchar(64),
	@location_zip nvarchar(32),
	@location_country nvarchar(64),
	@location_deleted bit = 0,

	@user_id int,
	@user_name_first nvarchar(64),
	@user_name_last nvarchar(64),

	@user_name_salutation nvarchar(32),
	@user_name_middle nvarchar(64) = '',
	@user_occupation nvarchar(128),
	@user_phone nvarchar(32),
	@user_phone_extension nvarchar(16) = ''
)
AS
SET NOCOUNT ON
BEGIN
	DECLARE @user_organization_location_id int

	IF EXISTS(SELECT * FROM locations WHERE location_name = @location_name AND location_address1 = @location_address1 AND location_zip = @location_zip)
	BEGIN
		UPDATE locations SET
			location_name = @location_name,
			location_address1 = @location_address1,
			location_address2 = @location_address2,
			location_city = @location_city,
			location_state = @location_state,
			location_zip = @location_zip,
			location_country = @location_country,
			location_deleted = @location_deleted
		WHERE location_name = @location_name AND location_address1 = @location_address1 AND location_zip = @location_zip
	END
	ELSE
	BEGIN
		INSERT INTO locations(
			location_name,
			location_address1,
			location_address2,
			location_city,
			location_state,
			location_zip,
			location_country,
			location_deleted
		)
		VALUES(
			@location_name,
			@location_address1,
			@location_address2,
			@location_city,
			@location_state,
			@location_zip,
			@location_country,
			@location_deleted
		)
	END
	SET @user_organization_location_id = IDENT_CURRENT('locations')
	
	UPDATE users SET
		user_name_first = @user_name_first,
		user_name_last = @user_name_last
	WHERE user_id = @user_id
	
	INSERT INTO user_profiles(
		user_id,
		user_name_salutation,
		user_name_middle,
		user_occupation,
		user_phone,
		user_phone_extension,
		user_organization_location_id
	)
	VALUES(
		@user_id,
		@user_name_salutation,
		@user_name_middle,
		@user_occupation,
		@user_phone,
		@user_phone_extension,
		@user_organization_location_id
	)
END
SET NOCOUNT OFF
GO

/* SELECT
//-------------------------------------------------------------------*/
USE [ucf_ent];
GO
IF OBJECT_ID('[dbo].[spSelectUserProfile]') IS NOT NULL 
	DROP PROCEDURE [dbo].[spSelectUserProfile]
GO
CREATE PROCEDURE [dbo].[spSelectUserProfile](
	@user_id int
)
AS
SET NOCOUNT ON
BEGIN
	DECLARE @user_organization_location_id int

	SET @user_organization_location_id = (
		SELECT user_organization_location_id FROM user_profiles WHERE user_id = @user_id
	)

	SELECT
		user_name_alias,
		user_name_first,
		user_name_last,
		user_email,
		user_date_created,
		user_deleted,
		
		user_name_salutation,
		user_name_middle,
		user_occupation,
		user_phone,
		user_phone_extension,
		
		location_name,
		location_address1,
		location_address2,
		location_city,
		location_state,
		location_zip,
		location_country,
		location_deleted
	FROM users, user_profiles, locations
	WHERE user_profiles.user_id = @user_id
	AND users.user_id = user_profiles.user_id
	AND locations.location_id = @user_organization_location_id
END
SET NOCOUNT OFF
GO

/* UPDATE
//-------------------------------------------------------------------*/
USE [ucf_ent];
GO
IF OBJECT_ID('[dbo].[spUpdateUserProfile]') IS NOT NULL 
	DROP PROCEDURE [dbo].[spUpdateUserProfile]
GO
CREATE PROCEDURE [dbo].[spUpdateUserProfile](
	@user_id int,
	@user_name_first nvarchar(63),
	@user_name_last nvarchar(63),

	@user_name_salutation nvarchar(32),
	@user_name_middle nvarchar(63) = '',
	@user_occupation nvarchar(127),
	@user_phone nvarchar(31),

	@location_name nvarchar(255),
	@location_address1 nvarchar(127),
	@location_address2 nvarchar(127) = '',
	@location_city nvarchar(63),
	@location_state nvarchar(63),
	@location_zip nvarchar(31),
	@location_country nvarchar(63)
)
AS
SET NOCOUNT ON
BEGIN
	DECLARE @user_organization_location_id int

	UPDATE users SET
		user_name_first = @user_name_first,
		user_name_last = @user_name_last
	WHERE user_id = @user_id

	UPDATE user_profiles SET
		user_name_first = @user_name_first,
		user_name_middle = @user_name_middle,
		user_name_last = @user_name_last,
		user_occupation = @user_occupation,
		user_phone = @user_phone
	WHERE user_id = @user_id
	SET @user_organization_location_id = (
		SELECT user_organization_location_id FROM user_profiles WHERE user_id = @user_id
	)

	UPDATE locations SET
		location_name = @location_name,
		location_address1 = @location_address1,
		location_address2 = @location_address2,
		location_city = @location_city,
		location_state = @location_state,
		location_zip = @location_zip,
		location_country = @location_country
	WHERE location_id = @user_organization_location_id
END
SET NOCOUNT OFF
GO

/* DELETE
//-------------------------------------------------------------------*/
USE [ucf_ent]
GO
IF OBJECT_ID('[dbo].[spDeleteUsers]') IS NOT NULL 
	DROP PROCEDURE [dbo].[spDeleteUsers]
GO
CREATE PROCEDURE [dbo].[spDeleteUsers]
AS
SET NOCOUNT ON
BEGIN
	DELETE FROM locations
	WHERE location_id IN (
		SELECT user_organization_location_id FROM user_profiles
	)
	
	DELETE FROM user_profiles
	
	DELETE FROM user_settings
	
	DELETE FROM users
	
	SET IDENTITY_INSERT users ON

	INSERT INTO users(
		user_id,
		user_alias,
		user_name_first,
		user_name_last,
		user_password,
		user_email,
		user_activation_key,
		user_activated,
		user_date_activation_created,
		user_date_created
	)
	VALUES (
		1,
		'diehard',
		'William',
		'Chang',
		'rCO9+p2VwqEQNvDGHCyUIg==',
		'william@babybluebox.com',
		'',
		1,
		GETDATE(),
		GETDATE()
	)

	SET IDENTITY_INSERT users OFF

	SET IDENTITY_INSERT user_settings ON
	
	INSERT INTO user_settings (
		setting_id,
		setting_user_id,
		setting_key,
		setting_value
	)
	VALUES (
		1,
		1,
		'role',
		'1'
	)
	
	SET IDENTITY_INSERT user_settings OFF
END
SET NOCOUNT OFF
GO