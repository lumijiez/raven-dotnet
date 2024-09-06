CREATE TABLE Users (
                       Id UNIQUEIDENTIFIER PRIMARY KEY, 
                       Username NVARCHAR(50) NOT NULL, 
                       HashedPassword NVARCHAR(256) NOT NULL, 
                       Email NVARCHAR(100) NOT NULL, 
                       RegisterIp NVARCHAR(45), 
                       RegisterTimestamp DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE UNIQUE INDEX IX_Users_Username ON Users(Username);

CREATE UNIQUE INDEX IX_Users_Email ON Users(Email);
