Use master

CREATE DATABASE RazervationDB

GO

Use RazervationDB

GO


CREATE TABLE Clients(
    ClientId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
    FirstName nvarchar(255) NOT NULL,
    LastName nvarchar(255) NOT NULL,
    UserName nvarchar(255) NOT NULL,
    Gender nvarchar(255) NOT NULL,

);


CREATE TABLE Businesses(
    BusinessId INT IDENTITY(10000,1) PRIMARY KEY NOT NULL,
    BusinessName nvarchar(255) NOT NULL,
    BusinessAddress nvarchar(255) NOT NULL,
    Bio nvarchar,
    CategoryId INT NOT NULL,
    InternetUrl nvarchar(255),
    UserName nvarchar(255) NOT NULL,
    InstagramUrl nvarchar(255),
    FacebookUrl nvarchar(255),
    BusinessImage nvarchar(255)
);



CREATE TABLE Reservation(
    StartDateTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    BusinessId INT NOT NULL,
    ClientId INT NOT NULL,
    ServiceId INT NOT NULL,
    ReservationId INT IDENTITY(1000000,1) PRIMARY KEY NOT NULL,
    DayId INT NOT NULL,
	StatusId INT NOT NULL
);


CREATE TABLE ReserveStatus(
	StatusId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	StatusName nvarchar(255) NOT NULL
);



CREATE TABLE BServices(
    ServiceId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
    ServiceName INT NOT NULL,
    DurationMin INT NOT NULL,
    Price INT NOT NULL
);


CREATE TABLE ServicesInBusiness(
    Id INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
    BusinessId INT NOT NULL,
    ServiceId INT NOT NULL
);



CREATE TABLE Favorites(
    FavoriteId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
    ClientId INT NOT NULL,
    BusinessId INT NOT NULL
);




CREATE TABLE Comments(
    BusinessId INT NOT NULL,
    CommentText nvarchar,
    ClientId INT NOT NULL,
    AutoCommentId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
	Rating INT NOT NULL
);


CREATE TABLE Categories(
    CategoryId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
    CategoryName nvarchar(255) NOT NULL
);




CREATE TABLE SpecialNumberOfWorkers(
    SpecialDate DATE PRIMARY KEY NOT NULL,
    NumWorkers INT NOT NULL,
    BusinessId INT NOT NULL
);



CREATE TABLE Users(
    UserName nvarchar(255) PRIMARY KEY NOT NULL,
    UserPassword nvarchar(255) NOT NULL,
    UserType BIT NOT NULL,
	Email nvarchar(255) UNIQUE NOT NULL,
	PhoneNumber nvarchar(255) UNIQUE NOT NULL
);



CREATE TABLE BusinessDay(
    BusinessId INT NOT NULL,
    DayNum INT NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    NumberOfWorkers INT NOT NULL,
    DayId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL
);




CREATE TABLE History(
    HistoryId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
    ClientId INT NOT NULL,
    BusinessId INT NOT NULL
);



ALTER TABLE
    Favorites ADD CONSTRAINT favorites_clientid_foreign FOREIGN KEY(ClientId) REFERENCES Clients(ClientId);
ALTER TABLE
    Favorites ADD CONSTRAINT favorites_Businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);



ALTER TABLE
    Comments ADD CONSTRAINT comments_clientid_foreign FOREIGN KEY(ClientId) REFERENCES Clients(ClientId);
ALTER TABLE
    Comments ADD CONSTRAINT comments_businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);


ALTER TABLE
    SpecialNumberOfWorkers ADD CONSTRAINT specialnumberofworkers_businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);


ALTER TABLE
    BusinessDay ADD CONSTRAINT businessday_businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);


ALTER TABLE
    Reservation ADD CONSTRAINT reservation_businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);
ALTER TABLE
    Reservation ADD CONSTRAINT reservation_clientid_foreign FOREIGN KEY(ClientId) REFERENCES Clients(ClientId);
ALTER TABLE
    Reservation ADD CONSTRAINT reservation_dayid_foreign FOREIGN KEY(DayId) REFERENCES BusinessDay(DayId);
ALTER TABLE
    Reservation ADD CONSTRAINT reservation_BServicesid_foreign FOREIGN KEY(ServiceId) REFERENCES BServices(ServiceId);
ALTER TABLE
    Reservation ADD CONSTRAINT reservation_StatusId_foreign FOREIGN KEY(StatusId) REFERENCES ReserveStatus(StatusId);


ALTER TABLE
    ServicesInBusiness ADD CONSTRAINT servicesinbusiness_businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);
ALTER TABLE
    ServicesInBusiness ADD CONSTRAINT servicesinbusiness_serviceid_foreign FOREIGN KEY(ServiceId) REFERENCES BServices(ServiceId);


ALTER TABLE
    Businesses ADD CONSTRAINT businesses_categoryid_foreign FOREIGN KEY(CategoryId) REFERENCES Categories(CategoryId);


ALTER TABLE
    Clients ADD CONSTRAINT clients_username_foreign FOREIGN KEY(UserName) REFERENCES Users(UserName);
ALTER TABLE
    Businesses ADD CONSTRAINT businesses_username_foreign FOREIGN KEY(UserName) REFERENCES Users(UserName);




ALTER TABLE
    History ADD CONSTRAINT history_clientid_foreign FOREIGN KEY(ClientId) REFERENCES Clients(ClientId);
ALTER TABLE
    History ADD CONSTRAINT history_businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);