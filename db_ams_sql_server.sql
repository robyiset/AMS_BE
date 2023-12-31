USE [master]
GO
/****** Object:  Database [db_ams]    Script Date: 02/12/2023 21.10.13 ******/
CREATE DATABASE [db_ams]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_ams', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQL2016\MSSQL\DATA\db_ams.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db_ams_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQL2016\MSSQL\DATA\db_ams_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [db_ams] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_ams].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_ams] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_ams] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_ams] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_ams] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_ams] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_ams] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db_ams] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_ams] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_ams] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_ams] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_ams] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_ams] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_ams] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_ams] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_ams] SET  DISABLE_BROKER 
GO
ALTER DATABASE [db_ams] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_ams] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_ams] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_ams] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_ams] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_ams] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_ams] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_ams] SET RECOVERY FULL 
GO
ALTER DATABASE [db_ams] SET  MULTI_USER 
GO
ALTER DATABASE [db_ams] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_ams] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_ams] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_ams] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_ams] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db_ams] SET QUERY_STORE = OFF
GO
USE [db_ams]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [db_ams]
GO
/****** Object:  Table [dbo].[tbl_asset_maintenances]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_asset_maintenances](
	[id_maintenance] [int] IDENTITY(1,1) NOT NULL,
	[id_asset] [int] NOT NULL,
	[title] [varchar](100) NULL,
	[maintenance_desc] [varchar](max) NULL,
	[start_date] [datetime] NULL,
	[completion_date] [datetime] NULL,
	[maintenance_time] [int] NULL,
	[cost] [decimal](18, 0) NULL,
	[notes] [varchar](max) NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_maintenance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_locations]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_locations](
	[id_location] [int] IDENTITY(1,1) NOT NULL,
	[address] [varchar](max) NULL,
	[city] [varchar](100) NULL,
	[state] [varchar](100) NULL,
	[country] [varchar](100) NULL,
	[zip] [varchar](10) NULL,
	[details] [varchar](max) NULL,
	[id_user] [int] NULL,
	[id_company] [int] NULL,
	[id_supplier] [int] NULL,
	[id_asset] [int] NULL,
	[id_usage] [int] NULL,
	[id_maintenance] [int] NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_location] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_assets]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_assets](
	[id_asset] [int] IDENTITY(1,1) NOT NULL,
	[serial] [varchar](100) NOT NULL,
	[asset_name] [varchar](100) NOT NULL,
	[asset_desc] [varchar](max) NULL,
	[id_type] [int] NULL,
	[id_user] [int] NULL,
	[purchase_date] [datetime] NULL,
	[purchase_cost] [decimal](18, 0) NULL,
	[warranty_expiration] [datetime] NULL,
	[depreciate] [bit] NOT NULL,
	[requestable] [bit] NOT NULL,
	[consumable] [bit] NOT NULL,
	[id_company] [int] NULL,
	[status] [varchar](100) NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_asset] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_maintenance]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[vw_maintenance]
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

GO
/****** Object:  Table [dbo].[tbl_requested_assets]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_requested_assets](
	[id_request] [int] IDENTITY(1,1) NOT NULL,
	[id_asset] [int] NOT NULL,
	[id_user] [int] NULL,
	[id_company] [int] NULL,
	[requested_at] [datetime] NULL,
	[denied_at] [datetime] NULL,
	[notes] [varchar](max) NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_request] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_companies]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_companies](
	[id_company] [int] IDENTITY(1,1) NOT NULL,
	[company_name] [varchar](100) NOT NULL,
	[phone] [varchar](20) NULL,
	[email] [varchar](100) NULL,
	[contact] [varchar](100) NULL,
	[url] [varchar](max) NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_company] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_user_details]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_user_details](
	[id_user_detail] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL,
	[first_name] [varchar](100) NOT NULL,
	[last_name] [varchar](100) NULL,
	[phone_number] [varchar](20) NULL,
	[about] [varchar](max) NULL,
	[id_company] [int] NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_user_detail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_request_assets]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[vw_request_assets]
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

GO
/****** Object:  Table [dbo].[tbl_consumable_assets]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_consumable_assets](
	[id_usage] [int] IDENTITY(1,1) NOT NULL,
	[id_asset] [int] NOT NULL,
	[id_user] [int] NULL,
	[id_company] [int] NULL,
	[notes] [varchar](max) NULL,
	[purchase_date] [datetime] NULL,
	[purchase_cost] [decimal](18, 0) NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_consumable_assets]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[vw_consumable_assets]
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
GO
/****** Object:  Table [dbo].[tbl_licences]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_licences](
	[id_license] [int] IDENTITY(1,1) NOT NULL,
	[license_name] [varchar](100) NOT NULL,
	[license_desc] [varchar](max) NULL,
	[license_account] [varchar](100) NULL,
	[license_version] [varchar](50) NULL,
	[id_user] [int] NULL,
	[id_asset] [int] NULL,
	[purchase_date] [datetime] NULL,
	[purchase_cost] [decimal](18, 0) NULL,
	[expired_date] [datetime] NULL,
	[termination_date] [datetime] NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_license] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_licenses]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[vw_licenses] as(
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
GO
/****** Object:  Table [dbo].[tbl_users]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_users](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](100) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[last_login] [datetime] NULL,
	[activated] [bit] NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_users]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vw_users]
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
/****** Object:  View [dbo].[vw_user_details]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[vw_user_details]
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
/****** Object:  View [dbo].[vw_companies]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[vw_companies]
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
/****** Object:  Table [dbo].[tbl_asset_types]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_asset_types](
	[id_type] [int] IDENTITY(1,1) NOT NULL,
	[name_type] [varchar](100) NOT NULL,
	[desc_type] [varchar](max) NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_assets]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[vw_assets]
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
GO
/****** Object:  Table [dbo].[tbl_asset_logs]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_asset_logs](
	[id_asset_log] [int] IDENTITY(1,1) NOT NULL,
	[id_asset] [int] NOT NULL,
	[action_desc] [varchar](max) NOT NULL,
	[log_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_asset_log] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_suppliers]    Script Date: 02/12/2023 21.10.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_suppliers](
	[id_supplier] [int] IDENTITY(1,1) NOT NULL,
	[supplier_name] [varchar](100) NOT NULL,
	[phone] [varchar](20) NULL,
	[email] [varchar](100) NULL,
	[contact] [varchar](100) NULL,
	[url] [varchar](max) NULL,
	[id_company] [int] NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[deleted_at] [datetime] NULL,
	[deleted_by] [int] NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_supplier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_asset_logs] ADD  DEFAULT (getdate()) FOR [log_date]
GO
ALTER TABLE [dbo].[tbl_asset_maintenances] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_asset_maintenances] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_asset_types] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_asset_types] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_assets] ADD  DEFAULT ((0)) FOR [depreciate]
GO
ALTER TABLE [dbo].[tbl_assets] ADD  DEFAULT ((0)) FOR [requestable]
GO
ALTER TABLE [dbo].[tbl_assets] ADD  DEFAULT ((0)) FOR [consumable]
GO
ALTER TABLE [dbo].[tbl_assets] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_assets] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_companies] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_companies] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_consumable_assets] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_consumable_assets] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_licences] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_licences] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_locations] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_locations] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_requested_assets] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_requested_assets] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_suppliers] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_suppliers] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_user_details] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_user_details] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_users] ADD  DEFAULT ((1)) FOR [activated]
GO
ALTER TABLE [dbo].[tbl_users] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_users] ADD  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[tbl_asset_logs]  WITH CHECK ADD  CONSTRAINT [fk_asset_logs_assets] FOREIGN KEY([id_asset])
REFERENCES [dbo].[tbl_assets] ([id_asset])
GO
ALTER TABLE [dbo].[tbl_asset_logs] CHECK CONSTRAINT [fk_asset_logs_assets]
GO
ALTER TABLE [dbo].[tbl_asset_maintenances]  WITH CHECK ADD  CONSTRAINT [fk_asset_maintenances_assets] FOREIGN KEY([id_asset])
REFERENCES [dbo].[tbl_assets] ([id_asset])
GO
ALTER TABLE [dbo].[tbl_asset_maintenances] CHECK CONSTRAINT [fk_asset_maintenances_assets]
GO
ALTER TABLE [dbo].[tbl_assets]  WITH CHECK ADD  CONSTRAINT [fk_assets_asset_types] FOREIGN KEY([id_type])
REFERENCES [dbo].[tbl_asset_types] ([id_type])
GO
ALTER TABLE [dbo].[tbl_assets] CHECK CONSTRAINT [fk_assets_asset_types]
GO
ALTER TABLE [dbo].[tbl_assets]  WITH CHECK ADD  CONSTRAINT [fk_assets_companies] FOREIGN KEY([id_company])
REFERENCES [dbo].[tbl_companies] ([id_company])
GO
ALTER TABLE [dbo].[tbl_assets] CHECK CONSTRAINT [fk_assets_companies]
GO
ALTER TABLE [dbo].[tbl_assets]  WITH CHECK ADD  CONSTRAINT [fk_assets_users] FOREIGN KEY([id_user])
REFERENCES [dbo].[tbl_users] ([id_user])
GO
ALTER TABLE [dbo].[tbl_assets] CHECK CONSTRAINT [fk_assets_users]
GO
ALTER TABLE [dbo].[tbl_consumable_assets]  WITH CHECK ADD  CONSTRAINT [fk_consumable_assets_asset] FOREIGN KEY([id_asset])
REFERENCES [dbo].[tbl_assets] ([id_asset])
GO
ALTER TABLE [dbo].[tbl_consumable_assets] CHECK CONSTRAINT [fk_consumable_assets_asset]
GO
ALTER TABLE [dbo].[tbl_consumable_assets]  WITH CHECK ADD  CONSTRAINT [fk_consumable_assets_companies] FOREIGN KEY([id_company])
REFERENCES [dbo].[tbl_companies] ([id_company])
GO
ALTER TABLE [dbo].[tbl_consumable_assets] CHECK CONSTRAINT [fk_consumable_assets_companies]
GO
ALTER TABLE [dbo].[tbl_consumable_assets]  WITH CHECK ADD  CONSTRAINT [fk_consumable_assets_users] FOREIGN KEY([id_user])
REFERENCES [dbo].[tbl_users] ([id_user])
GO
ALTER TABLE [dbo].[tbl_consumable_assets] CHECK CONSTRAINT [fk_consumable_assets_users]
GO
ALTER TABLE [dbo].[tbl_licences]  WITH CHECK ADD  CONSTRAINT [fk_licenses_assets] FOREIGN KEY([id_asset])
REFERENCES [dbo].[tbl_assets] ([id_asset])
GO
ALTER TABLE [dbo].[tbl_licences] CHECK CONSTRAINT [fk_licenses_assets]
GO
ALTER TABLE [dbo].[tbl_licences]  WITH CHECK ADD  CONSTRAINT [fk_licenses_users] FOREIGN KEY([id_user])
REFERENCES [dbo].[tbl_users] ([id_user])
GO
ALTER TABLE [dbo].[tbl_licences] CHECK CONSTRAINT [fk_licenses_users]
GO
ALTER TABLE [dbo].[tbl_locations]  WITH CHECK ADD  CONSTRAINT [fk_locations_asset_maintenances] FOREIGN KEY([id_maintenance])
REFERENCES [dbo].[tbl_asset_maintenances] ([id_maintenance])
GO
ALTER TABLE [dbo].[tbl_locations] CHECK CONSTRAINT [fk_locations_asset_maintenances]
GO
ALTER TABLE [dbo].[tbl_locations]  WITH CHECK ADD  CONSTRAINT [fk_locations_assets] FOREIGN KEY([id_asset])
REFERENCES [dbo].[tbl_assets] ([id_asset])
GO
ALTER TABLE [dbo].[tbl_locations] CHECK CONSTRAINT [fk_locations_assets]
GO
ALTER TABLE [dbo].[tbl_locations]  WITH CHECK ADD  CONSTRAINT [fk_locations_companies] FOREIGN KEY([id_company])
REFERENCES [dbo].[tbl_companies] ([id_company])
GO
ALTER TABLE [dbo].[tbl_locations] CHECK CONSTRAINT [fk_locations_companies]
GO
ALTER TABLE [dbo].[tbl_locations]  WITH CHECK ADD  CONSTRAINT [fk_locations_consumables] FOREIGN KEY([id_usage])
REFERENCES [dbo].[tbl_consumable_assets] ([id_usage])
GO
ALTER TABLE [dbo].[tbl_locations] CHECK CONSTRAINT [fk_locations_consumables]
GO
ALTER TABLE [dbo].[tbl_locations]  WITH CHECK ADD  CONSTRAINT [fk_locations_suppliers] FOREIGN KEY([id_supplier])
REFERENCES [dbo].[tbl_suppliers] ([id_supplier])
GO
ALTER TABLE [dbo].[tbl_locations] CHECK CONSTRAINT [fk_locations_suppliers]
GO
ALTER TABLE [dbo].[tbl_locations]  WITH CHECK ADD  CONSTRAINT [fk_locations_users] FOREIGN KEY([id_user])
REFERENCES [dbo].[tbl_users] ([id_user])
GO
ALTER TABLE [dbo].[tbl_locations] CHECK CONSTRAINT [fk_locations_users]
GO
ALTER TABLE [dbo].[tbl_requested_assets]  WITH CHECK ADD  CONSTRAINT [fk_requested_assets_asset] FOREIGN KEY([id_asset])
REFERENCES [dbo].[tbl_assets] ([id_asset])
GO
ALTER TABLE [dbo].[tbl_requested_assets] CHECK CONSTRAINT [fk_requested_assets_asset]
GO
ALTER TABLE [dbo].[tbl_requested_assets]  WITH CHECK ADD  CONSTRAINT [fk_requested_assets_companies] FOREIGN KEY([id_company])
REFERENCES [dbo].[tbl_companies] ([id_company])
GO
ALTER TABLE [dbo].[tbl_requested_assets] CHECK CONSTRAINT [fk_requested_assets_companies]
GO
ALTER TABLE [dbo].[tbl_requested_assets]  WITH CHECK ADD  CONSTRAINT [fk_requested_assets_users] FOREIGN KEY([id_user])
REFERENCES [dbo].[tbl_users] ([id_user])
GO
ALTER TABLE [dbo].[tbl_requested_assets] CHECK CONSTRAINT [fk_requested_assets_users]
GO
ALTER TABLE [dbo].[tbl_suppliers]  WITH CHECK ADD  CONSTRAINT [fk_suppliers_companies] FOREIGN KEY([id_company])
REFERENCES [dbo].[tbl_companies] ([id_company])
GO
ALTER TABLE [dbo].[tbl_suppliers] CHECK CONSTRAINT [fk_suppliers_companies]
GO
ALTER TABLE [dbo].[tbl_user_details]  WITH CHECK ADD  CONSTRAINT [fk_user_details_company] FOREIGN KEY([id_company])
REFERENCES [dbo].[tbl_companies] ([id_company])
GO
ALTER TABLE [dbo].[tbl_user_details] CHECK CONSTRAINT [fk_user_details_company]
GO
ALTER TABLE [dbo].[tbl_user_details]  WITH CHECK ADD  CONSTRAINT [fk_users_user_details] FOREIGN KEY([id_user])
REFERENCES [dbo].[tbl_users] ([id_user])
GO
ALTER TABLE [dbo].[tbl_user_details] CHECK CONSTRAINT [fk_users_user_details]
GO
USE [master]
GO
ALTER DATABASE [db_ams] SET  READ_WRITE 
GO
