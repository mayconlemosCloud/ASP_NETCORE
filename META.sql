USE [META]
GO
/****** Object:  Table [dbo].[Audiencia]    Script Date: 17/05/2021 12:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audiencia](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Pontos_audiencia] [int] NULL,
	[Data_hora_audiencia] [datetime] NULL,
	[Emissora_audiencia] [nvarchar](50) NULL,
 CONSTRAINT [PK_Audiencia] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Emissoras]    Script Date: 17/05/2021 12:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emissoras](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Emissoras] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Emissoras] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Audiencia] ADD  CONSTRAINT [DF_Audiencia_Data_hora_audiencia]  DEFAULT (getdate()) FOR [Data_hora_audiencia]
GO
ALTER TABLE [dbo].[Audiencia]  WITH NOCHECK ADD  CONSTRAINT [FK_Audiencia_Emissoras] FOREIGN KEY([Emissora_audiencia])
REFERENCES [dbo].[Emissoras] ([Emissoras])
GO
ALTER TABLE [dbo].[Audiencia] CHECK CONSTRAINT [FK_Audiencia_Emissoras]
GO
