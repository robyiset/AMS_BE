CREATE TABLE tbl_users
(
	id_user INT IDENTITY(1,1) PRIMARY KEY,
	username varchar(100) unique not null,
	email varchar(100) unique not null,
	password varchar(100) not null,
	last_login datetime,
	activated bit default 1,
	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0
);



create table tbl_companies
(
	id_company INT IDENTITY(1,1) PRIMARY KEY,
	company_name varchar(100) not null,
	phone varchar(20),
	email varchar(100),
	contact varchar(100),
	url varchar(max),

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,
);

CREATE TABLE tbl_user_details
(
	id_user_detail INT IDENTITY(1,1) PRIMARY KEY,
	id_user int not null,
	first_name VARCHAR(100) not null,
	last_name VARCHAR(100),
	phone_number VARCHAR(20),
	about VARCHAR(max),

	id_company int,

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,

	CONSTRAINT fk_users_user_details FOREIGN KEY (id_user) REFERENCES tbl_users(id_user),
	CONSTRAINT fk_user_details_company FOREIGN KEY (id_company) REFERENCES tbl_companies(id_company)
);

create table tbl_asset_types
(
	id_type INT IDENTITY(1,1) PRIMARY KEY,
	name_type varchar(100) not null,
	desc_type varchar(max),

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0
);

create table tbl_assets
(
	id_asset INT IDENTITY(1,1) PRIMARY KEY,
	serial varchar(100) not null,
	asset_name varchar(100) not null,
	asset_desc varchar(max),
	id_type int,
	id_user int,

	purchase_date datetime,
	purchase_cost decimal,

	warranty_expiration datetime,

	depreciate bit not null default 0,

	requestable bit not null default 0,
	consumable bit not null default 0,

	id_company int,

	status varchar(100),

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,

	CONSTRAINT fk_assets_asset_types FOREIGN KEY (id_type) REFERENCES tbl_asset_types(id_type),
	CONSTRAINT fk_assets_companies FOREIGN KEY (id_company) REFERENCES tbl_companies(id_company),
	CONSTRAINT fk_assets_users FOREIGN KEY (id_user) REFERENCES tbl_users(id_user),
);

create table tbl_requested_assets
(
	id_request INT IDENTITY(1,1) PRIMARY KEY,
	id_asset int not null,
	id_user int,
	id_company int,
	requested_at datetime,
	denied_at datetime,
	notes varchar(max),

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,

	CONSTRAINT fk_requested_assets_asset FOREIGN KEY (id_asset) REFERENCES tbl_assets(id_asset),
	CONSTRAINT fk_requested_assets_users FOREIGN KEY (id_user) REFERENCES tbl_users(id_user),
	CONSTRAINT fk_requested_assets_companies FOREIGN KEY (id_company) REFERENCES tbl_companies(id_company),
);

create table tbl_consumable_assets
(
	id_usage INT IDENTITY(1,1) PRIMARY KEY,
	id_asset int not null,
	id_user int,
	id_company int,
	notes varchar(max),

	purchase_date datetime,
	purchase_cost decimal,

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,

	CONSTRAINT fk_consumable_assets_asset FOREIGN KEY (id_asset) REFERENCES tbl_assets(id_asset),
	CONSTRAINT fk_consumable_assets_users FOREIGN KEY (id_user) REFERENCES tbl_users(id_user),
	CONSTRAINT fk_consumable_assets_companies FOREIGN KEY (id_company) REFERENCES tbl_companies(id_company),
);

create table tbl_asset_maintenances
(
	id_maintenance INT IDENTITY(1,1) PRIMARY KEY,
	id_asset int not null,
	title varchar(100),
	maintenance_desc varchar(max),
	start_date datetime,
	completion_date datetime,
	maintenance_time int,
	cost decimal,
	notes varchar(max),

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,
	CONSTRAINT fk_asset_maintenances_assets FOREIGN KEY (id_asset) REFERENCES tbl_assets(id_asset),
);

create table tbl_asset_logs
(
	id_asset_log INT IDENTITY(1,1) PRIMARY KEY,
	id_asset int not null,
	action_desc varchar(max) not null,

	log_date datetime default GETDATE(),

	CONSTRAINT fk_asset_logs_assets FOREIGN KEY (id_asset) REFERENCES tbl_assets(id_asset),
);

create table tbl_suppliers
(
	id_supplier INT IDENTITY(1,1) PRIMARY KEY,
	supplier_name varchar(100) not null,
	phone varchar(20),
	email varchar(100),
	contact varchar(100),
	url varchar(max),

	id_company int,

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,

	CONSTRAINT fk_suppliers_companies FOREIGN KEY (id_company) REFERENCES tbl_companies(id_company)
);

create table tbl_licences
(
	id_license INT IDENTITY(1,1) PRIMARY KEY,
	license_name varchar(100) not null,
	license_desc varchar(max),
	license_account varchar(100),
	license_version varchar(50),
	id_user int,
	id_asset int,

	purchase_date datetime,
	purchase_cost decimal,
	expired_date datetime,
	termination_date datetime,

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,

	CONSTRAINT fk_licenses_assets FOREIGN KEY (id_asset) REFERENCES tbl_assets(id_asset),
	CONSTRAINT fk_licenses_users FOREIGN KEY (id_user) REFERENCES tbl_users(id_user),
);


create table tbl_locations
(
	id_location INT IDENTITY(1,1) PRIMARY KEY,

	address varchar(max),
	city varchar(100),
	state varchar(100),
	country varchar(100),
	zip varchar(10),
	details varchar(max),

	id_user int,
	id_company int,
	id_supplier int,
	id_asset int,
	id_usage INT,
	id_maintenance int,

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,

	CONSTRAINT fk_locations_users FOREIGN KEY (id_user) REFERENCES tbl_users(id_user),
	CONSTRAINT fk_locations_companies FOREIGN KEY (id_company) REFERENCES tbl_companies(id_company),
	CONSTRAINT fk_locations_suppliers FOREIGN KEY (id_supplier) REFERENCES tbl_suppliers(id_supplier),
	CONSTRAINT fk_locations_assets FOREIGN KEY (id_asset) REFERENCES tbl_assets(id_asset),
	CONSTRAINT fk_locations_consumables FOREIGN KEY (id_usage) REFERENCES tbl_consumable_assets(id_usage),
	CONSTRAINT fk_locations_asset_maintenances FOREIGN KEY (id_maintenance) REFERENCES tbl_asset_maintenances(id_maintenance),
);

GO

CREATE VIEW vw_users
AS
	(

	select
		a.id_user,
		a.username,
		a.email,
		b.first_name,
		b.last_name,
		b.phone_number,
		b.about
	from tbl_users a
		left join tbl_user_details b on a.id_user = b.id_user
	where a.deleted = 0
);
GO

create view vw_user_details
as
	(

	select
		a.id_user,
		a.username,
		a.email,
		b.first_name,
		b.last_name,
		b.phone_number,
		b.about,
		c.company_name,
		d.address,
		d.city,
		d.state,
		d.country,
		d.zip,
		d.details
	from tbl_users a
		left join tbl_user_details b on a.id_user = b.id_user
		left join tbl_companies c on b.id_company = c.id_company
		left join tbl_locations d on a.id_user = d.id_user

	where a.deleted = 0
);

GO


CREATE view vw_companies
as
	(
	select
		a.id_company,
		a.company_name,
		a.phone,
		a.email,
		a.contact,
		a.url,
		b.address,
		b.city,
		b.state,
		b.country,
		b.zip
	from tbl_companies a
		left join tbl_locations b on a.id_company = b.id_company
	where a.deleted = 0
	);
GO

create view vw_assets
as
	(
	select
		a.id_asset,
		a.serial,
		a.asset_name,
		a.asset_desc,
		b.name_type,
		c.first_name + ' ' + c.last_name as assigned_user,
		a.purchase_date,
		a.purchase_cost,
		a.depreciate,
		a.requestable,
		a.consumable,
		d.company_name,
		a.warranty_expiration,
		a.status,
		f.address,
		f.city,
		f.state,
		f.country,
		f.zip,
		f.details
	from tbl_assets a
		left join tbl_asset_types b on a.id_type = b.id_type
		left join vw_users c on a.id_user = c.id_user
		left join tbl_companies d on a.id_company = d.id_company
		left join tbl_locations f on a.id_asset = f.id_asset
	where a.deleted = 0
);
go


create view vw_maintenance
as
	(
	select
		b.id_maintenance,
		a.id_asset,
		a.asset_name,
		b.title,
		b.maintenance_desc,
		b.start_date,
		b.completion_date,
		b.maintenance_time,
		b.cost,
		b.notes,
		c.address,
		c.city,
		c.state,
		c.country,
		c.zip,
		c.details
	from tbl_assets a
		left join tbl_asset_maintenances b on a.id_asset = b.id_asset
		left join tbl_locations c on b.id_maintenance = c.id_maintenance
	where b.deleted = 0
);

go


create view vw_request_assets
as
	(
	select
		b.id_request,
		a.id_asset,
		a.asset_name,
		c.first_name + ' ' + c.last_name as requested_from_user,
		d.company_name as requested_from_company,
		b.requested_at,
		b.denied_at,
		b.notes
	from tbl_assets a
		left join tbl_requested_assets b on a.id_asset = b.id_asset
		left join tbl_user_details c on b.id_user = c.id_user
		left join tbl_companies d on b.id_company = d.id_company
	where b.deleted = 0
);

go


create view vw_consumable_assets
as
	(
	select
		b.id_usage,
		a.id_asset,
		a.asset_name,
		c.first_name + ' ' + c.last_name as consumed_by_user,
		d.company_name as consumed_by_company,
		b.purchase_date,
		b.purchase_cost
	from tbl_assets a
		left join tbl_consumable_assets b on a.id_asset = b.id_asset
		left join tbl_user_details c on b.id_user = c.id_user
		left join tbl_companies d on b.id_company = d.id_company
	where b.deleted = 0
);

go


create view vw_licenses
as
	(
	select
		a.id_license,
		a.license_name,
		a.license_version,
		a.license_account,
		a.license_desc,
		a.purchase_date,
		a.purchase_cost,
		a.expired_date,
		a.termination_date,
		b.asset_name,
		c.first_name + ' ' + c.last_name as license_to_user
	from tbl_licences a
		left join tbl_assets b on a.id_asset = b.id_asset
		left join tbl_user_details c on a.id_user = c.id_user
	where a.deleted = 0
);