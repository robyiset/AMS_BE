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

create table tbl_asset_waranties
(
	id_warranty INT IDENTITY(1,1) PRIMARY KEY,
	warranty_name varchar(100),
	warranty_expiration datetime,

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,
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

	depreciate bit not null default 0,

	requestable bit not null default 0,
	consumable bit not null default 0,

	id_company int,
	id_warranty int,

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
	CONSTRAINT fk_assets_assets_waranties FOREIGN KEY (id_warranty) REFERENCES tbl_asset_waranties(id_warranty),
	CONSTRAINT fk_assets_users FOREIGN KEY (id_user) REFERENCES tbl_users(id_user),
);

create table tbl_requested_assets
(
	id_request INT IDENTITY(1,1) PRIMARY KEY,
	id_asset int not null,
	id_user int,
	id_company int,
	requested_at int,
	denied_at int,
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
	id_warranty int,
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
	CONSTRAINT fk_asset_maintenances_assets_waranties FOREIGN KEY (id_warranty) REFERENCES tbl_asset_waranties(id_warranty),
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

	id_warranty int,

	created_at datetime default GETDATE(),
	created_by int,
	updated_at datetime,
	updated_by int,
	deleted_at datetime,
	deleted_by int,
	deleted bit default 0,

	CONSTRAINT fk_licenses_assets FOREIGN KEY (id_asset) REFERENCES tbl_assets(id_asset),
	CONSTRAINT fk_licenses_users FOREIGN KEY (id_user) REFERENCES tbl_users(id_user),
	CONSTRAINT fk_licenses_waranties FOREIGN KEY (id_warranty) REFERENCES tbl_asset_waranties(id_warranty),
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