CREATE DATABASE TaskManagementDB
GO

USE TaskManagementDB
GO

/* =========================================================
   ROLES
========================================================= */
CREATE TABLE Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE
)
GO

INSERT INTO Roles (RoleName)
VALUES 
('Admin'),
('Manager'),
('Employee')
GO

/* =========================================================
   USERS
========================================================= */
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    RoleId INT NOT NULL,
    IsDeleted BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Users_Roles
        FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
)
GO

/* =========================================================
   PROJECTS
========================================================= */
CREATE TABLE Projects (
    ProjectId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500),
    StartDate DATE,
    EndDate DATE,
    CreatedBy INT NOT NULL,
    IsDeleted BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Projects_Users
        FOREIGN KEY (CreatedBy) REFERENCES Users(UserId),

    CONSTRAINT CK_Project_Dates
        CHECK (EndDate IS NULL OR EndDate >= StartDate)
)
GO

/* =========================================================
   PROJECT MEMBERS
========================================================= */
CREATE TABLE ProjectMembers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProjectId INT NOT NULL,
    UserId INT NOT NULL,
    JoinedAt DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_ProjectMembers_Project
        FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId),

    CONSTRAINT FK_ProjectMembers_User
        FOREIGN KEY (UserId) REFERENCES Users(UserId),

    CONSTRAINT UQ_Project_User
        UNIQUE(ProjectId, UserId)
)
GO

/* =========================================================
   TASK STATUSES
========================================================= */
CREATE TABLE TaskStatuses (
    StatusId INT IDENTITY(1,1) PRIMARY KEY,
    StatusName NVARCHAR(50) NOT NULL UNIQUE
)
GO

INSERT INTO TaskStatuses (StatusName)
VALUES
('To Do'),
('In Progress'),
('Completed'),
('Overdue')
GO

/* =========================================================
   TASKS
========================================================= */
CREATE TABLE Tasks (
    TaskId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500),

    ProjectId INT NOT NULL,
    AssignedTo INT NULL,
    StatusId INT NOT NULL,

    Priority TINYINT DEFAULT 2,
    Deadline DATE,

    IsDeleted BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Tasks_Project
        FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId),

    CONSTRAINT FK_Tasks_User
        FOREIGN KEY (AssignedTo) REFERENCES Users(UserId),

    CONSTRAINT FK_Tasks_Status
        FOREIGN KEY (StatusId) REFERENCES TaskStatuses(StatusId),

    CONSTRAINT CK_Task_Priority
        CHECK (Priority BETWEEN 1 AND 3)
)
GO

/* =========================================================
   NOTIFICATIONS
========================================================= */
CREATE TABLE Notifications (
    NotificationId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Message NVARCHAR(255) NOT NULL,
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Notifications_User
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
)
GO

/* =========================================================
   INDEXES
========================================================= */

CREATE INDEX IX_Users_Email
ON Users(Email)
GO

CREATE INDEX IX_Tasks_ProjectId
ON Tasks(ProjectId)
GO

CREATE INDEX IX_Tasks_StatusId
ON Tasks(StatusId)
GO

CREATE INDEX IX_Tasks_AssignedTo
ON Tasks(AssignedTo)
GO

CREATE INDEX IX_ProjectMembers_UserId
ON ProjectMembers(UserId)
GO

CREATE INDEX IX_Notifications_UserId
ON Notifications(UserId)
GO