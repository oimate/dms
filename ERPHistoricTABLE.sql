USE [EMOS_WEB]
GO

/****** Object:  Table [dbo].[DMS_ERPHistoric]    Script Date: 28.04.2015 14:48:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DMS_ERPHistoric](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SkidID] [int] NOT NULL,
	[DerivativeCode] [int] NOT NULL,
	[Colour] [int] NOT NULL,
	[BSN] [int] NOT NULL,
	[Track] [int] NOT NULL,
	[Roof] [int] NOT NULL,
	[Door] [int] NOT NULL,
	[Spare] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[fk_User] [bigint] NOT NULL,
 CONSTRAINT [PK_DMS_ERP_Historic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

