������� ��������� ��:
> (localdb)\MSSQLLocalDB
> BankDB

�������:

CREATE TABLE [dbo].[Clients] (
    [idClient]   NVARCHAR (4)  NOT NULL,
    [clientName] NVARCHAR (50) NOT NULL,
    [isPerson]   BIT           NOT NULL,
    [adress]     NVARCHAR (50) NOT NULL,
    [account]    FLOAT (53)    NOT NULL,
    [invoice]    FLOAT (53)    NOT NULL,
    PRIMARY KEY CLUSTERED ([idClient] ASC)
);

CREATE TABLE [dbo].[Persons] (
    [idClient] NVARCHAR (4)  NOT NULL,
    [cName]    NVARCHAR (50) NOT NULL,
    [isVip]    BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([idClient] ASC)
);

CREATE TABLE [dbo].[Entity] (
    [idClient]   NVARCHAR (4)  NOT NULL,
    [dirName]    NVARCHAR (50) NOT NULL,
    [dirSurname] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([idClient] ASC)
);

CREATE TABLE [dbo].[Deposits] (
    [idClient]      NVARCHAR (4) NOT NULL,
    [amount]        FLOAT (53)   NOT NULL,
    [percent]       FLOAT (53)   NOT NULL,
    [isCapitalised] BIT          NOT NULL,
    [isOpened]      BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([idClient] ASC)
);

CREATE TABLE [dbo].[Logs] (
    [idLog]      INT           IDENTITY (1, 1) NOT NULL,
    [idClient]   NVARCHAR (4)  NOT NULL,
    [time]       DATETIME      NOT NULL,
    [idReciever] NVARCHAR (4)  NULL,
    [amount]     FLOAT (53)	   NULL,
    [direction]  BIT           NULL,
    [message]    NVARCHAR (80) NULL,
    PRIMARY KEY CLUSTERED ([idLog] ASC)
);

������:

INSERT INTO [dbo].[Clients] ([idClient], [clientName], [isPerson], [adress], [account], [invoice]) VALUES (N'1367', N'��������', 1, N'������, ��.������, �.12, ��.2', 5000, 56000)
INSERT INTO [dbo].[Clients] ([idClient], [clientName], [isPerson], [adress], [account], [invoice]) VALUES (N'53d7', N'��� ����� � ����', 0, N'������, ��.�������, �.2', 2281780, 170000)
INSERT INTO [dbo].[Clients] ([idClient], [clientName], [isPerson], [adress], [account], [invoice]) VALUES (N'58b8', N'������', 1, N'������, ��.������, �.23, ��.7', 90990, 42000)
INSERT INTO [dbo].[Clients] ([idClient], [clientName], [isPerson], [adress], [account], [invoice]) VALUES (N'5acd', N'�����', 1, N'������, ��.������, �.2, ��.23', 65000, 35000)
INSERT INTO [dbo].[Clients] ([idClient], [clientName], [isPerson], [adress], [account], [invoice]) VALUES (N'c93a', N'��� ���� � ������', 0, N'�����������, ��.�������, �.12�4', 6578000, 700000)

INSERT INTO [dbo].[Persons] ([idClient], [cName], [isVip]) VALUES (N'1367', N'����', 0)
INSERT INTO [dbo].[Persons] ([idClient], [cName], [isVip]) VALUES (N'58b8', N'�������', 1)
INSERT INTO [dbo].[Persons] ([idClient], [cName], [isVip]) VALUES (N'5acd', N'������', 0)

INSERT INTO [dbo].[Entity] ([idClient], [dirName], [dirSurname]) VALUES (N'53d7', N'�������', N'������')
INSERT INTO [dbo].[Entity] ([idClient], [dirName], [dirSurname]) VALUES (N'c93a', N'�������', N'�������')

INSERT INTO [dbo].[Deposits] ([idClient], [amount], [percent], [isCapitalised], [isOpened]) VALUES (N'1367', 30000, 3.8, 0, 1)
INSERT INTO [dbo].[Deposits] ([idClient], [amount], [percent], [isCapitalised], [isOpened]) VALUES (N'c93a', 0, 0, 0, 1)
INSERT INTO [dbo].[Deposits] ([idClient], [amount], [percent], [isCapitalised], [isOpened]) VALUES (N'53d7', 0, 0, 0, 0)
INSERT INTO [dbo].[Deposits] ([idClient], [amount], [percent], [isCapitalised], [isOpened]) VALUES (N'58b8', 0, 0, 0, 0)
INSERT INTO [dbo].[Deposits] ([idClient], [amount], [percent], [isCapitalised], [isOpened]) VALUES (N'5acd', 0, 0, 0, 0)

SET IDENTITY_INSERT [dbo].[Logs] ON
INSERT INTO [dbo].[Logs] ([idLog], [idClient], [time], [idReciever], [amount], [direction], [message]) VALUES (1, N'1367', N'2020-05-07 18:45:29', N'58b8', 1000, NULL, NULL)
INSERT INTO [dbo].[Logs] ([idLog], [idClient], [time], [idReciever], [amount], [direction], [message]) VALUES (2, N'1367', N'2020-05-07 18:45:38', NULL, 20000, 0, NULL)
INSERT INTO [dbo].[Logs] ([idLog], [idClient], [time], [idReciever], [amount], [direction], [message]) VALUES (3, N'53d7', N'2020-05-07 18:44:17', N'c93a', 22000, NULL, NULL)
INSERT INTO [dbo].[Logs] ([idLog], [idClient], [time], [idReciever], [amount], [direction], [message]) VALUES (4, N'53d7', N'2020-05-07 18:44:28', NULL, NULL, NULL, N'���������� ��������: ����������� ����������')
INSERT INTO [dbo].[Logs] ([idLog], [idClient], [time], [idReciever], [amount], [direction], [message]) VALUES (5, N'53d7', N'2020-05-07 18:44:44', NULL, 1000000, 0, NULL)
INSERT INTO [dbo].[Logs] ([idLog], [idClient], [time], [idReciever], [amount], [direction], [message]) VALUES (6, N'1367', N'2020-05-07 18:46:36', NULL, NULL, NULL, N'���������� ��������: ����������� ����������')
INSERT INTO [dbo].[Logs] ([idLog], [idClient], [time], [idReciever], [amount], [direction], [message]) VALUES (7, N'5acd', N'2020-05-07 18:46:00', N'1367', 35000, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Logs] OFF
