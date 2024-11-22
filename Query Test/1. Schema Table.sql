CREATE DATABASE MCFTest

Create Table Cabang (
	KodeCabang VARCHAR(3) PRIMARY KEY NOT NULL,
	NamaCabang VARCHAR(50)
)

Create Table Motor (
	KodeMotor VARCHAR(3) PRIMARY KEY NOT NULL,
	NamaMotor VARCHAR(50)
)

Create Table Pembayaran (
	NoKontrak BIGINT PRIMARY KEY NOT NULL,
	TglBayar DATETIME,
	JumlahBayar MONEY,
	KodeCabang VARCHAR(3) NOT NULL,
	NoKwitansi BIGINT,
	KodeMotor VARCHAR(3)  NOT NULL,
	FOREIGN KEY (KodeCabang) References Cabang(KodeCabang),
	FOREIGN KEY (KodeMotor) References Motor(KodeMotor)
)
