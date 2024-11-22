CREATE DATABASE MCF_Davin
USE MCF_Davin

CREATE TABLE ms_storage_location(
	location_id varchar(10) primary key not null,
	location_name varchar(100)
)

CREATE TABLE ms_user(
	[user_id] bigint primary key not null,
	[user_name] varchar(20),
	[password] varchar(50),
	is_active bit
)

CREATE TABLE tr_bpkb(
	agreement_number varchar(100) primary key not null,
	bpkb_no varchar(100),
	branch_id varchar(10),
	bpkb_date datetime,
	faktur_no varchar(100),
	faktur_date datetime,
	location_id	varchar(10) foreign key references ms_storage_location(location_id),
	police_no varchar(20),
	bpkb_date_in datetime,
	created_by varchar(20),
	created_on datetime,
	last_updated_by varchar(20),
	last_updated_on datetime
)