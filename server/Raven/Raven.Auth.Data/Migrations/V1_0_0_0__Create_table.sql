CREATE TABLE Users (
                       Id UUID PRIMARY KEY,
                       Username VARCHAR(50) NOT NULL,
                       HashedPassword VARCHAR(256) NOT NULL,
                       Email VARCHAR(100) NOT NULL,
                       RegisterIp VARCHAR(45),
                       RegisterTimestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX IX_Users_Username ON Users (Username);

CREATE UNIQUE INDEX IX_Users_Email ON Users (Email);
