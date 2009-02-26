/*
//-------------------------------------------------------------------
/// File       : create.sql
/// Version    : 1.1
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
/// http://www.techonthenet.com/sql/tables/create_table.php
/// http://www.1keydata.com/sql/sqlcreate.html
/// http://www.1keydata.com/sql/sql-foreign-key.html
/// http://www.w3schools.com/SQL/sql_view.asp
/// http://www.techonthenet.com/sql/views.php
//-------------------------------------------------------------------
*/

/* Code Snippets
//-------------------------------------------------------------------*/
/*

Create View:
CREATE VIEW view_name AS
SELECT columns FROM table WHERE condition

Update View:
CREATE OR REPLACE VIEW view_name AS
SELECT columns FROM table WHERE condition

Query View:
SELECT * FROM view_name;

Drop View:
DROP VIEW view_name; 

Add Foreign Key:
ALTER TABLE [dbo].[user_profiles] ADD FOREIGN KEY(user_id) REFERENCES users(user_id);
ALTER TABLE [dbo].[user_profiles] ADD CONSTRAINT fk_user_profiles_users FOREIGN KEY(user_id) REFERENCES users(user_id);

Drop Foreign Key:
ALTER TABLE [dbo].[user_profiles] DROP FOREIGN KEY fk_user_profiles_users;

*/

/* CREATE DATABASE
//-------------------------------------------------------------------*/
CREATE DATABASE [ucf_ent];

/* CREATE TABLE (database assumes "null" as default)
//-------------------------------------------------------------------*/
USE [ucf_ent];
CREATE TABLE [dbo].[users](
	user_id int not null IDENTITY(1,1),
	user_alias nvarchar(64) not null,
	user_name_first nvarchar(64) not null,
	user_name_last nvarchar(64) not null,
	user_password nvarchar(64) not null,
	user_email nvarchar(128) not null,
	user_activation_key nvarchar(64) not null,
	user_activated bit DEFAULT(0) not null,
	user_session_key nvarchar(64) DEFAULT('') not null,
	user_date_login datetime null,
	user_date_activation_created datetime null,
	user_date_session_created datetime null,
	user_date_created datetime not null,
	user_deleted bit DEFAULT(0) not null
);
ALTER TABLE [dbo].[users] ADD PRIMARY KEY(user_id);
INSERT INTO users(user_alias, user_name_first, user_name_last, user_password, user_email, user_activation_key, user_activated, user_date_activation_created, user_date_created) VALUES ('diehard', 'William', 'Chang', 'rCO9+p2VwqEQNvDGHCyUIg==', 'william@babybluebox.com', '', 1, GETDATE(), GETDATE());

USE [ucf_ent];
CREATE TABLE [dbo].[user_profiles](
	user_id int not null,
	user_name_salutation nvarchar(32) not null,
	user_name_middle nvarchar(64) not null,
	user_occupation nvarchar(128) not null,
	user_phone nvarchar(64) not null,
	user_phone_extension nvarchar(16) not null,
	user_organization_location_id int not null
);
ALTER TABLE [dbo].[user_profiles] ADD PRIMARY KEY(user_id);

USE [ucf_ent];
CREATE TABLE [dbo].[user_settings](
	setting_id int not null IDENTITY(1,1),
	setting_user_id int not null,
	setting_key nvarchar(256) not null,
	setting_value nvarchar(512) DEFAULT('') not null
);
ALTER TABLE [dbo].[user_settings] ADD PRIMARY KEY(setting_id);
INSERT INTO user_settings(setting_user_id, setting_key, setting_value) VALUES (1, 'role', '1');

USE [ucf_ent];
CREATE TABLE [dbo].[user_rolelist](
	list_id int not null IDENTITY(1,1),
	list_name nvarchar(256) not null,
	list_deleted bit DEFAULT(0) not null
);
ALTER TABLE [dbo].[user_rolelist] ADD PRIMARY KEY(list_id);
INSERT INTO user_rolelist(list_name) VALUES ('Administrator');
INSERT INTO user_rolelist(list_name) VALUES ('Moderator');
INSERT INTO user_rolelist(list_name) VALUES ('Author');
INSERT INTO user_rolelist(list_name) VALUES ('Subscriber');

USE [ucf_ent];
CREATE TABLE [dbo].[locations](
	location_id int not null IDENTITY(1,1),
	location_name nvarchar(256) not null,
	location_address1 nvarchar(128) not null,
	location_address2 nvarchar(128) DEFAULT('') not null,
	location_city nvarchar(64) not null,
	location_state nvarchar(64) not null,
	location_zip nvarchar(32) not null,
	location_country nvarchar(64) not null,
	location_deleted bit DEFAULT(0) not null
);
ALTER TABLE [dbo].[locations] ADD PRIMARY KEY(location_id);

USE [ucf_ent];
CREATE TABLE [dbo].[application_settings](
	setting_id int not null IDENTITY(1,1),
	setting_application_id int DEFAULT(0) not null,
	setting_key nvarchar(256) not null,
	setting_value nvarchar(512) DEFAULT('') not null
);
ALTER TABLE [dbo].[application_settings] ADD PRIMARY KEY(setting_id);

USE [ucf_ent];
CREATE TABLE [dbo].[application_logs](
	log_id int not null IDENTITY(1,1),
	log_user_id int not null,
	log_code int not null,
	log_message nvarchar(2048) not null,
	log_date_created datetime not null,
	log_deleted bit DEFAULT(0) not null
);
ALTER TABLE [dbo].[application_logs] ADD PRIMARY KEY(log_id);