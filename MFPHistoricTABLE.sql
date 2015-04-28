USE [EMOS_WEB]
GO

/****** Object:  Table [dbo].[DMS_MFPHistoric]    Script Date: 28.04.2015 14:49:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DMS_MFPHistoric](
	[Timestamp] [datetime2](7) NOT NULL,
	[LocalSkidId] [int] NOT NULL,
	[MfpPos] [int] NOT NULL,
	[fk_ErpHistoricId] [int] NOT NULL
) ON [PRIMARY]

GO

