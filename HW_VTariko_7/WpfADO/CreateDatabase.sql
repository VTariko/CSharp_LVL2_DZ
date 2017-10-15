USE master;
--------------------------
IF DB_ID('Company') IS NOT NULL DROP DATABASE Company;

CREATE DATABASE Company;
GO

USE Company;
GO

--------------------------

CREATE SCHEMA HR AUTHORIZATION dbo;
GO

--------------------------

CREATE TABLE HR.Department
(
	DepartmentId	INT				NOT NULL IDENTITY(1, 1),
	Code			NVARCHAR(10)	NOT NULL,
	DepartmentName	NVARCHAR(50)	NOT NULL,
	
	CONSTRAINT PK_Department PRIMARY KEY(DepartmentId)
);

CREATE TABLE HR.Employees
(
	EmployeeId      INT				NOT NULL IDENTITY(1, 1),
	LastName        NVARCHAR(15)	NOT NULL,
	FirstName       NVARCHAR(10)	NOT NULL,
	DepartmentId	INT				NOT NULL,
	BirthDate		DATETIME		NOT NULL,
	
  CONSTRAINT PK_Employees PRIMARY KEY(EmployeeId),
  CONSTRAINT FK_Employee_DepartmentId FOREIGN KEY (DepartmentId)
	REFERENCES HR.Department (DepartmentId)
);

--------------------------
INSERT INTO [HR].[Department]
 VALUES ('Work', 'Трудяги')
INSERT INTO [HR].[Department]
 VALUES ('Intern', 'Стажеры')
INSERT INTO [HR].[Department]
 VALUES ('Report', 'Бухгалтерия')


INSERT INTO HR.Employees(LastName, FirstName, DepartmentId, BirthDate)
	SELECT N'Дэвис', N'Сара', d.DepartmentId, '19581208 00:00:00.000'
		FROM [HR].[Department] AS d
		WHERE d.Code = 'Work'
INSERT INTO HR.Employees(LastName, FirstName, DepartmentId, BirthDate)
	SELECT N'Функ', N'Дон', d.DepartmentId, '19620219 00:00:00.000'
		FROM [HR].[Department] AS d
		WHERE d.Code = 'Work'
INSERT INTO HR.Employees(LastName, FirstName, DepartmentId, BirthDate)
	SELECT N'Лью', N'Джуди', d.DepartmentId, '19730830 00:00:00.000'
		FROM [HR].[Department] AS d
		WHERE d.Code = 'Intern'
INSERT INTO HR.Employees(LastName, FirstName, DepartmentId, BirthDate)
	SELECT N'Пелед', N'Иаиль', d.DepartmentId, '19470919 00:00:00.000'
		FROM [HR].[Department] AS d
		WHERE d.Code = 'Intern'
INSERT INTO HR.Employees(LastName, FirstName, DepartmentId, BirthDate)
	SELECT N'Бак', N'Свен', d.DepartmentId, '19650304 00:00:00.000'
		FROM [HR].[Department] AS d
		WHERE d.Code = 'Work'
INSERT INTO HR.Employees(LastName, FirstName, DepartmentId, BirthDate)
	SELECT N'Суурс', N'Пол', d.DepartmentId, '19730702 00:00:00.000'
		FROM [HR].[Department] AS d
		WHERE d.Code = 'Work'
INSERT INTO HR.Employees(LastName, FirstName, DepartmentId, BirthDate)
	SELECT N'Кинг', N'Рассел', d.DepartmentId, '19700529 00:00:00.000'
		FROM [HR].[Department] AS d
		WHERE d.Code = 'Work'
INSERT INTO HR.Employees(LastName, FirstName, DepartmentId, BirthDate)
	SELECT N'Камерон', N'Мария', d.DepartmentId, '19680109 00:00:00.000'
		FROM [HR].[Department] AS d
		WHERE d.Code = 'Report'
INSERT INTO HR.Employees(LastName, FirstName, DepartmentId, BirthDate)
	SELECT N'Долгопятова', N'Зоя', d.DepartmentId, '19760127 00:00:00.000'
		FROM [HR].[Department] AS d
		WHERE d.Code = 'Report'
  