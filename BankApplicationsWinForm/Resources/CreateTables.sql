CREATE TABLE [dbo].[tbUsers](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[Gender] [bit] NULL,
	[DateOfBirth] [datetime] NULL,
	[Password] [varchar](max) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[DemandData] [varchar](max) NULL,
	[DepositData] [varchar](max) NULL,
	[FName] [varchar](50) NOT NULL DEFAULT (''),
	[LName] [varchar](50) NOT NULL DEFAULT (''),
PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[tbFiles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Image] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[tbFiles]  WITH CHECK ADD  CONSTRAINT [FK_tbFiles_tbUsers] FOREIGN KEY([User_ID])
REFERENCES [dbo].[tbUsers] ([User_ID])