USE [L.NENU.ZZX]
GO
/****** Object:  Table [dbo].[Show_TheHost_Info]    Script Date: 04/22/2016 13:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Show_TheHost_Info](
	[ShowInfoID] [int] NOT NULL,
	[TheHostInfoID] [int] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Show_TheHost_Info] ([ShowInfoID], [TheHostInfoID]) VALUES (1, 1)
INSERT [dbo].[Show_TheHost_Info] ([ShowInfoID], [TheHostInfoID]) VALUES (2, 1)
INSERT [dbo].[Show_TheHost_Info] ([ShowInfoID], [TheHostInfoID]) VALUES (3, 1)
INSERT [dbo].[Show_TheHost_Info] ([ShowInfoID], [TheHostInfoID]) VALUES (4, 1)
INSERT [dbo].[Show_TheHost_Info] ([ShowInfoID], [TheHostInfoID]) VALUES (7, 1)
/****** Object:  Table [dbo].[TheHostInfo]    Script Date: 04/22/2016 13:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheHostInfo](
	[ID] [int] NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[JoinTime] [datetime] NULL,
	[BirthTime] [datetime] NULL,
	[PhptoUrl] [nvarchar](200) NULL,
	[Introduce] [nvarchar](1000) NULL,
 CONSTRAINT [PK_TheHostInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[TheHostInfo] ([ID], [UserName], [Name], [JoinTime], [BirthTime], [PhptoUrl], [Introduce]) VALUES (1, N'夕颜', N'段誉', CAST(0x0000A39900000000 AS DateTime), CAST(0x000089D400000000 AS DateTime), NULL, NULL)
/****** Object:  StoredProcedure [dbo].[sp_ShowInfo_TheHost_BYTop5]    Script Date: 04/22/2016 13:08:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

CREATE PROCEDURE [dbo].[sp_ShowInfo_TheHost_BYTop5]
AS
BEGIN
	--不输出受影响的行数
	SET NOCOUNT ON;

    --开始查询
	SELECT top 5 s.*,t.username from showInfo as s ,Show_TheHost_Info as st ,TheHostInfo as t
	where s.id = st.showinfoID and t.id = st.theHostinfoID
	order by s.createtime DESC
END
GO
