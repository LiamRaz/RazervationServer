Use master

CREATE DATABASE RazervationDB

GO

Use RazervationDB

GO


CREATE TABLE Clients(
    ClientId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
    --UserId INT NOT NULL,
    FirstName nvarchar(255) NOT NULL,
    LastName nvarchar(255) NOT NULL,
    UserName nvarchar(255) NOT NULL,
    Gender nvarchar(255) NOT NULL,

);


CREATE TABLE Businesses(
    BusinessId INT IDENTITY(10000,1) PRIMARY KEY NOT NULL,
    --UserId INT NOT NULL,
    BusinessName nvarchar(255) NOT NULL,
    BusinessAddress nvarchar(255) NOT NULL,
    Bio nvarchar,
    CategoryId INT NOT NULL,
    InternetUrl nvarchar(255),
    UserName nvarchar(255) NOT NULL,
    InstagramUrl nvarchar(255),
    FacebookUrl nvarchar(255),
    --BusinessImage nvarchar(255)
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
    ServiceName nvarchar(255) NOT NULL,
    DurationMin INT NOT NULL,
    Price INT NOT NULL,
    BusinessId INT NOT NULL
);


--CREATE TABLE ServicesInBusiness(
--    Id INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
--    BusinessId INT NOT NULL,
--    ServiceId INT NOT NULL
--);



CREATE TABLE Favorites(
    FavoriteId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
    ClientId INT NOT NULL,
    BusinessId INT NOT NULL,
    IsActive BIT NOT NULL
);




CREATE TABLE Comments(
    BusinessId INT NOT NULL,
    CommentText nvarchar,
    ClientId INT NOT NULL,
    AutoCommentId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
	Rating INT NOT NULL,
    CDate DATETIME NOT NULL
);


CREATE TABLE Categories(
    CategoryId INT IDENTITY(10,1) PRIMARY KEY NOT NULL,
    CategoryName nvarchar(255) NOT NULL,
    CategoryImageUrl nvarchar(255) NOT NULL,
    CategoryIconUrl nvarchar(255) NOT NULL,
);




CREATE TABLE SpecialNumberOfWorkers(
    SpecialDate DATETIME PRIMARY KEY NOT NULL,
    NumWorkers INT NOT NULL,
    BusinessId INT NOT NULL
);



CREATE TABLE Users(
    --UserId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
    UserName nvarchar(255) PRIMARY KEY NOT NULL,
    UserPassword nvarchar(255) NOT NULL,
    UserType BIT NOT NULL,
	Email nvarchar(255) UNIQUE NOT NULL,
	PhoneNumber nvarchar(255) UNIQUE NOT NULL
);



CREATE TABLE BusinessDays(
    BusinessId INT NOT NULL,
    DayNum INT NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    NumberOfWorkers INT NOT NULL,
    DayId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL
);




CREATE TABLE Histories(
    HistoryId INT IDENTITY(100000,1) PRIMARY KEY NOT NULL,
    ClientId INT NOT NULL,
    BusinessId INT NOT NULL,
    HDate DATETIME NOT NULL,
    IsActive BIT NOT NULL
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
    BusinessDays ADD CONSTRAINT businessdays_businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);


ALTER TABLE
    Reservation ADD CONSTRAINT reservation_businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);
ALTER TABLE
    Reservation ADD CONSTRAINT reservation_clientid_foreign FOREIGN KEY(ClientId) REFERENCES Clients(ClientId);
ALTER TABLE
    Reservation ADD CONSTRAINT reservation_dayid_foreign FOREIGN KEY(DayId) REFERENCES BusinessDays(DayId);
ALTER TABLE
    Reservation ADD CONSTRAINT reservation_BServicesid_foreign FOREIGN KEY(ServiceId) REFERENCES BServices(ServiceId);
ALTER TABLE
    Reservation ADD CONSTRAINT reservation_StatusId_foreign FOREIGN KEY(StatusId) REFERENCES ReserveStatus(StatusId);



ALTER TABLE
    BServices ADD CONSTRAINT bservices_Businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);

--ALTER TABLE
--    ServicesInBusiness ADD CONSTRAINT servicesinbusiness_businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);
--ALTER TABLE
--    ServicesInBusiness ADD CONSTRAINT servicesinbusiness_serviceid_foreign FOREIGN KEY(ServiceId) REFERENCES BServices(ServiceId);


ALTER TABLE
    Businesses ADD CONSTRAINT businesses_categoryid_foreign FOREIGN KEY(CategoryId) REFERENCES Categories(CategoryId);


ALTER TABLE
    Clients ADD CONSTRAINT clients_userid_foreign FOREIGN KEY(UserName) REFERENCES Users(UserName);
ALTER TABLE
    Businesses ADD CONSTRAINT businesses_userid_foreign FOREIGN KEY(UserName) REFERENCES Users(UserName);




ALTER TABLE
    Histories ADD CONSTRAINT history_clientid_foreign FOREIGN KEY(ClientId) REFERENCES Clients(ClientId);
ALTER TABLE
    Histories ADD CONSTRAINT history_businessid_foreign FOREIGN KEY(BusinessId) REFERENCES Businesses(BusinessId);





   USE [RazervationDB]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName]
           ,[CategoryImageUrl]
           ,[CategoryIconUrl])
     VALUES
           ('BarberShop'
           ,'BarbershopCategoryImage.png'
           ,'BarbershopCategoryIcon.png')
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName]
           ,[CategoryImageUrl]
           ,[CategoryIconUrl])
     VALUES
           ('Teacher'
           ,'TeacherCategoryImage.png'
           ,'TeacherCategoryIcon.png')
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName]
           ,[CategoryImageUrl]
           ,[CategoryIconUrl])
     VALUES
           ('Doctor'
           ,'DoctorCategoryImage.png'
           ,'DoctorCategoryIcon.png')
GO



INSERT INTO [Users]
           ([UserName]
           ,[UserPassword]
           ,[UserType]
           ,[Email]
           ,[PhoneNumber])
     VALUES
           ('Razchik'
           ,'123'
           ,1
           ,'liamraz@gmail.com'
           ,'0527794488')
GO

INSERT INTO [Clients]
           ([FirstName]
           ,[LastName]
           ,[UserName]
           ,[Gender])
     VALUES
           ('Liam'
           ,'Raz'
           ,'Razchik'
           ,'Male')
GO



INSERT INTO [Users]
           ([UserName]
           ,[UserPassword]
           ,[UserType]
           ,[Email]
           ,[PhoneNumber])
     VALUES
           ('DcBarber'
           ,'123'
           ,0
           ,'dcbarber@gmail.com'
           ,'0544359010')
GO

INSERT INTO [Users]
           ([UserName]
           ,[UserPassword]
           ,[UserType]
           ,[Email]
           ,[PhoneNumber])
     VALUES
           ('Mazka18'
           ,'123'
           ,0
           ,'Mazka18@gmail.com'
           ,'0501234567')
GO

INSERT INTO [Users]
           ([UserName]
           ,[UserPassword]
           ,[UserType]
           ,[Email]
           ,[PhoneNumber])
     VALUES
           ('Capit123'
           ,'123'
           ,0
           ,'Capit123@gmail.com'
           ,'0521234567')
GO

INSERT INTO [Businesses]
           ([BusinessName]
           ,[BusinessAddress]
           ,[Bio]
           ,[CategoryId]
           ,[InternetUrl]
           ,[UserName]
           ,[InstagramUrl]
           ,[FacebookUrl])
     VALUES
           ('dc Barbershop'
           ,'Ezer Weizman 8'
           ,''
           ,10
           ,'' 
           ,'DcBarber'
           ,''
           ,'')
GO

INSERT INTO [Businesses]
           ([BusinessName]
           ,[BusinessAddress]
           ,[Bio]
           ,[CategoryId]
           ,[InternetUrl]
           ,[UserName]
           ,[InstagramUrl]
           ,[FacebookUrl])
     VALUES
           ('Ofek Mazori'
           ,'Ana Frank 1'
           ,''
           ,11
           ,'' 
           ,'Mazka18'
           ,''
           ,'')
GO

INSERT INTO [Businesses]
           ([BusinessName]
           ,[BusinessAddress]
           ,[Bio]
           ,[CategoryId]
           ,[InternetUrl]
           ,[UserName]
           ,[InstagramUrl]
           ,[FacebookUrl])
     VALUES
           ('Amit Yogev'
           ,'Hetzel 32'
           ,''
           ,12
           ,'' 
           ,'Capit123'
           ,''
           ,'')
GO



INSERT INTO [Histories]
           ([ClientId]
           ,[BusinessId]
           ,[HDate]
           ,[IsActive])
     VALUES
           (100000
           ,10000
           ,GETDATE()
           ,1)
GO

INSERT INTO [Histories]
           ([ClientId]
           ,[BusinessId]
           ,[HDate]
           ,[IsActive])
     VALUES
           (100000
           ,10001
           ,GETDATE()
           ,1)
GO

INSERT INTO [Histories]
           ([ClientId]
           ,[BusinessId]
           ,[HDate]
           ,[IsActive])
     VALUES
           (100000
           ,10002
           ,GETDATE()
           ,1)
GO


INSERT INTO [Favorites]
           ([ClientId]
           ,[BusinessId]
           ,[IsActive])
     VALUES
           (100000
           ,10000
           ,1)
GO

INSERT INTO [Favorites]
           ([ClientId]
           ,[BusinessId]
           ,[IsActive])
     VALUES
           (100000
           ,10001
           ,1)
GO

INSERT INTO [Favorites]
           ([ClientId]
           ,[BusinessId]
           ,[IsActive])
     VALUES
           (100000
           ,10002
           ,1)
GO


INSERT INTO [dbo].[ReserveStatus]
           ([StatusName])
     VALUES
           ('Active')
GO

INSERT INTO [dbo].[ReserveStatus]
           ([StatusName])
     VALUES
           ('Deleted by client')
GO

INSERT INTO [dbo].[ReserveStatus]
           ([StatusName])
     VALUES
           ('Deleted by business')
GO

INSERT INTO [dbo].[ReserveStatus]
           ([StatusName])
     VALUES
           ('Deleted by business to all')
GO

INSERT INTO [dbo].[ReserveStatus]
           ([StatusName])
     VALUES
           ('Completed')
GO


INSERT INTO [dbo].[BServices]
           ([ServiceName]
           ,[DurationMin]
           ,[Price]
           ,[BusinessId])
     VALUES
           ('Mans Cut'
           ,20
           ,10
           ,10000)
GO

INSERT INTO [dbo].[BServices]
           ([ServiceName]
           ,[DurationMin]
           ,[Price]
           ,[BusinessId])
     VALUES
           ('Private Lesson'
           ,60
           ,25
           ,10001)
GO

INSERT INTO [dbo].[BServices]
           ([ServiceName]
           ,[DurationMin]
           ,[Price]
           ,[BusinessId])
     VALUES
           ('Brain Check'
           ,30
           ,300
           ,10002)
GO


INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10000
           ,1
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10000
           ,2
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10000
           ,3
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10000
           ,4
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10000
           ,5
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10000
           ,6
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10000
           ,7
           ,'8:00'
           ,'17:00'
           ,0)
GO


INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10001
           ,1
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10001
           ,2
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10001
           ,3
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10001
           ,4
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10001
           ,5
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10001
           ,6
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10001
           ,7
           ,'8:00'
           ,'17:00'
           ,0)
GO


INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10002
           ,1
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10002
           ,2
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10002
           ,3
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10002
           ,4
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10002
           ,5
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10002
           ,6
           ,'8:00'
           ,'17:00'
           ,1)
GO

INSERT INTO [dbo].[BusinessDays]
           ([BusinessId]
           ,[DayNum]
           ,[StartTime]
           ,[EndTime]
           ,[NumberOfWorkers])
     VALUES
           (10002
           ,7
           ,'8:00'
           ,'17:00'
           ,0)
GO

INSERT INTO [dbo].[Reservation]
           ([StartDateTime]
           ,[EndTime]
           ,[BusinessId]
           ,[ClientId]
           ,[ServiceId]
           ,[DayId]
           ,[StatusId])
     VALUES
           ('20211205 10:35:00 AM'
           ,'20211205 10:55:00 AM'
           ,10000
           ,100000
           ,100000
           ,100000
           ,1)
GO

INSERT INTO [dbo].[Reservation]
           ([StartDateTime]
           ,[EndTime]
           ,[BusinessId]
           ,[ClientId]
           ,[ServiceId]
           ,[DayId]
           ,[StatusId])
     VALUES
           ('20211206 10:35:00 AM'
           ,'20211206 10:55:00 AM'
           ,10000
           ,100000
           ,100000
           ,100001
           ,1)
GO

INSERT INTO [dbo].[Reservation]
           ([StartDateTime]
           ,[EndTime]
           ,[BusinessId]
           ,[ClientId]
           ,[ServiceId]
           ,[DayId]
           ,[StatusId])
     VALUES
           ('20211207 10:35:00 AM'
           ,'20211207 10:55:00 AM'
           ,10000
           ,100000
           ,100000
           ,100002
           ,1)
GO

INSERT INTO [dbo].[Reservation]
           ([StartDateTime]
           ,[EndTime]
           ,[BusinessId]
           ,[ClientId]
           ,[ServiceId]
           ,[DayId]
           ,[StatusId])
     VALUES
           ('20211208 10:35:00 AM'
           ,'20211208 10:55:00 AM'
           ,10000
           ,100000
           ,100000
           ,100003
           ,1)
GO

INSERT INTO [dbo].[Reservation]
           ([StartDateTime]
           ,[EndTime]
           ,[BusinessId]
           ,[ClientId]
           ,[ServiceId]
           ,[DayId]
           ,[StatusId])
     VALUES
           ('20211209 10:35:00 AM'
           ,'20211209 10:55:00 AM'
           ,10000
           ,100000
           ,100000
           ,100004
           ,1)
GO