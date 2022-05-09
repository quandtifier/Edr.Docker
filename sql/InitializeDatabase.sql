﻿USE master
GO 
PRINT 'DROPPING Edr IF EXISTS';
GO
DROP DATABASE IF EXISTS Edr
GO

PRINT 'CREATING Edr';
GO
CREATE DATABASE Edr;
GO

USE master
GO
PRINT 'CREATING edr_dbuser ON master'
GO
CREATE LOGIN [edr_dbuser] WITH PASSWORD=N'EdrCuzTwitterSuck$', CHECK_EXPIRATION=OFF, CHECK_POLICY=ON;
GO
USE Edr;
GO

PRINT 'CREATING USER LOGIN FOR edr_dbuser ON Edr';
GO
CREATE USER [edr_dbuser] FOR LOGIN [edr_dbuser];
GO
EXEC sp_addrolemember N'db_owner', [edr_dbuser];
GO