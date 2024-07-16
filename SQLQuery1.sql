USE master;
GO

CREATE DATABASE ToDoAppDatabase;
GO

USE ToDoAppDatabase;
GO

CREATE TABLE Tasks (
    TaskID INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100) NOT NULL,
    Content NVARCHAR(MAX),
    AddDate DATETIME NOT NULL,
    Deadline DATETIME,
    Priority NVARCHAR(20),
    UpdateDate DATETIME
);
GO
