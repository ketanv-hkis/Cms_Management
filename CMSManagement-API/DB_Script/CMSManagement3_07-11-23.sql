USE [CMSManagement]
GO
/****** Object:  StoredProcedure [dbo].[uspGetEmployee]    Script Date: 13-09-2023 02:37:57 PM ******/
DROP PROCEDURE [dbo].[uspGetEmployee]
GO
ALTER TABLE [dbo].[Employee_Details] DROP CONSTRAINT [DF_Employee_Details_Is_delete]
GO
ALTER TABLE [dbo].[Employee_Details] DROP CONSTRAINT [DF_Employee_Details_Is_status]
GO
/****** Object:  Table [dbo].[Employee_Details]    Script Date: 13-09-2023 02:37:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee_Details]') AND type in (N'U'))
DROP TABLE [dbo].[Employee_Details]
GO
/****** Object:  Table [dbo].[Employee_Details]    Script Date: 13-09-2023 02:37:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_Details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Gender] [tinyint] NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Birthdate] [date] NULL,
	[Role] [int] NULL,
	[Is_status] [tinyint] NULL,
	[Is_delete] [tinyint] NULL,
	[Created_by] [int] NULL,
	[Created_date] [datetime] NULL,
	[Modified_by] [int] NULL,
	[Modified_date] [datetime] NULL,
 CONSTRAINT [PK_Employee_Details] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee_Details] ON 

INSERT [dbo].[Employee_Details] ([Id], [Firstname], [Lastname], [Email], [Password], [Gender], [Mobile_No], [Birthdate], [Role], [Is_status], [Is_delete], [Created_by], [Created_date], [Modified_by], [Modified_date]) VALUES (1, N'Vivek', N'Solanki', N'viveksl.hkis@gmail.com', N'Vml2ZWtAMTIz', 1, N'9313964213', CAST(N'2003-04-05' AS Date), 1, 1, 0, 1, CAST(N'2023-09-12T02:55:35.123' AS DateTime), NULL, NULL)
INSERT [dbo].[Employee_Details] ([Id], [Firstname], [Lastname], [Email], [Password], [Gender], [Mobile_No], [Birthdate], [Role], [Is_status], [Is_delete], [Created_by], [Created_date], [Modified_by], [Modified_date]) VALUES (2, N'Admin', N'Admin', N'admin1234@gmail.com', N'YWRtaW5AMTIzNA==', 2, N'9328740170', CAST(N'2003-04-05' AS Date), 1, 1, 0, 1, CAST(N'1900-01-01T00:00:00.000' AS DateTime), 1, CAST(N'1900-01-01T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Employee_Details] OFF
GO
ALTER TABLE [dbo].[Employee_Details] ADD  CONSTRAINT [DF_Employee_Details_Is_status]  DEFAULT ((1)) FOR [Is_status]
GO
ALTER TABLE [dbo].[Employee_Details] ADD  CONSTRAINT [DF_Employee_Details_Is_delete]  DEFAULT ((0)) FOR [Is_delete]
GO
/****** Object:  StoredProcedure [dbo].[uspGetEmployee]    Script Date: 13-09-2023 02:37:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetEmployee]
(
	@Email		VARCHAR(50),
	@Password	VARCHAR(50)
)
AS		--Person Data Select by Email and Password
		--Crated by Jenil
BEGIN
	SELECT Id,Firstname,Lastname,Email,[Password],Gender,Mobile_No,Birthdate,[Role],Is_status,Is_delete,Created_by,Created_date,Modified_by,Modified_date FROM Employee_Details
	WHERE Email = @Email AND [Password] = @Password
END
GO
DROP PROCEDURE [dbo].[SP_AllEmployeeDetails]
GO
CREATE PROCEDURE [dbo].[SP_AllEmployeeDetails]ASBEGINSELECT [Id]      ,[Firstname]      ,[Lastname]      ,[Email]      ,[Password]      ,[Gender]      ,[Mobile_No]      ,[Role]      ,[Is_status]      ,[Is_delete]      ,[Created_by]      ,[Created_date]      ,[Modified_by]      ,[Modified_date]  FROM [dbo].[Employee_Details] WHERE Is_delete != 1END
GO
