USE [DamDB]
GO
/****** Object:  Table [dbo].[ApparatusType]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApparatusType](
	[ApparatusTypeID] [uniqueidentifier] NOT NULL,
	[TypeName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_ApparatusType] PRIMARY KEY CLUSTERED 
(
	[ApparatusTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectPart]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPart](
	[ProjectPartID] [uniqueidentifier] NOT NULL,
	[PartName] [nvarchar](50) NOT NULL,
	[ParentPart] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProjectPart] PRIMARY KEY CLUSTERED 
(
	[ProjectPartID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[RoleName] [nvarchar](20) NULL,
	[Description] [nvarchar](50) NULL,
	[Power] [tinyint] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description], [Power]) VALUES (1, N'普通用户', N'普通注册用户                                      ', 1)
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description], [Power]) VALUES (2, N'录入员', N'录入员', 2)
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description], [Power]) VALUES (10, N'管理员', N'系统管理员,拥有最高权限                           ', 255)
/****** Object:  Table [dbo].[TaskType]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskType](
	[TaskTypeID] [int] NOT NULL,
	[TypeName] [nvarchar](30) NULL,
 CONSTRAINT [PK_TaskType] PRIMARY KEY CLUSTERED 
(
	[TaskTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUser](
	[UserName] [nvarchar](20) NOT NULL,
	[PasswordHash] [nvarchar](200) NULL,
	[Salt] [nvarchar](20) NULL,
	[Question] [nvarchar](50) NULL,
	[Answer] [nvarchar](50) NULL,
	[roleID] [int] NULL,
 CONSTRAINT [PK_SysUser] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[SysUser] ([UserName], [PasswordHash], [Salt], [Question], [Answer], [roleID]) VALUES (N'admin', N'94C40DCC4D5DEF15510C4151A4C39A801B2895EB', N'38Eix6o=', N'0plTbuE=', N'eGeTtnI=', 10)
INSERT [dbo].[SysUser] ([UserName], [PasswordHash], [Salt], [Question], [Answer], [roleID]) VALUES (N'user', N'D5DC6DD673F76CE5A650E28F8DFF9DDA7E532494', N'dV3555E=', N'question', N'ef乱sdde21d', 2)
/****** Object:  Table [dbo].[Apparatus]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apparatus](
	[AppName] [nvarchar](20) NOT NULL,
	[CalculateName] [nvarchar](20) NOT NULL,
	[ProjectPartID] [uniqueidentifier] NULL,
	[AppTypeID] [uniqueidentifier] NULL,
	[X] [nvarchar](50) NULL,
	[Y] [nvarchar](50) NULL,
	[Z] [nvarchar](50) NULL,
	[BuriedTime] [datetime] NULL,
	[OtherInfo] [nvarchar](200) NULL,
 CONSTRAINT [PK_Apparatus] PRIMARY KEY CLUSTERED 
(
	[AppName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppCollection]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppCollection](
	[AppCollectionID] [uniqueidentifier] NOT NULL,
	[taskTypeID] [int] NOT NULL,
	[CollectionName] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[Order] [int] NULL,
 CONSTRAINT [PK_AppCollection] PRIMARY KEY CLUSTERED 
(
	[AppCollectionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_AppCollection] UNIQUE NONCLUSTERED 
(
	[CollectionName] ASC,
	[taskTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalculateParam]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalculateParam](
	[CalculateParamID] [uniqueidentifier] NOT NULL,
	[appName] [nvarchar](20) NULL,
	[ParamName] [nvarchar](20) NULL,
	[ParamSymbol] [nvarchar](10) NULL,
	[UnitSymbol] [nvarchar](10) NULL,
	[PrecisionNum] [tinyint] NULL,
	[Order] [tinyint] NULL,
	[CalculateExpress] [nvarchar](100) NULL,
	[CalculateOrder] [tinyint] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_CalculateParam] PRIMARY KEY CLUSTERED 
(
	[CalculateParamID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_CalculateParam] UNIQUE NONCLUSTERED 
(
	[appName] ASC,
	[ParamName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessureParam]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessureParam](
	[MessureParamID] [uniqueidentifier] NOT NULL,
	[appName] [nvarchar](20) NULL,
	[ParamName] [nvarchar](20) NULL,
	[ParamSymbol] [nvarchar](10) NULL,
	[UnitSymbol] [nvarchar](10) NULL,
	[PrecisionNum] [tinyint] NULL,
	[Order] [tinyint] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_MessureParam] PRIMARY KEY CLUSTERED 
(
	[MessureParamID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_MessureParam] UNIQUE NONCLUSTERED 
(
	[appName] ASC,
	[ParamName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_MessureParam_1] UNIQUE NONCLUSTERED 
(
	[appName] ASC,
	[ParamSymbol] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConstantParam]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConstantParam](
	[ConstantParamID] [uniqueidentifier] NOT NULL,
	[appName] [nvarchar](20) NULL,
	[ParamName] [nvarchar](20) NULL,
	[ParamSymbol] [nvarchar](10) NULL,
	[UnitSymbol] [nvarchar](10) NULL,
	[PrecisionNum] [tinyint] NULL,
	[Order] [tinyint] NULL,
	[Val] [float] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ConstantParam] PRIMARY KEY CLUSTERED 
(
	[ConstantParamID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_ConstantParam] UNIQUE NONCLUSTERED 
(
	[appName] ASC,
	[ParamName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_ConstantParam_1] UNIQUE NONCLUSTERED 
(
	[appName] ASC,
	[ParamSymbol] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskAppratus]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskAppratus](
	[appCollectionID] [uniqueidentifier] NOT NULL,
	[appName] [nvarchar](20) NOT NULL,
	[Order] [int] NULL,
 CONSTRAINT [PK_TaskAppratus] PRIMARY KEY CLUSTERED 
(
	[appCollectionID] ASC,
	[appName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Remark]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Remark](
	[appName] [nvarchar](20) NOT NULL,
	[Date] [datetime] NOT NULL,
	[RemarkText] [nvarchar](80) NULL,
 CONSTRAINT [PK_Remark] PRIMARY KEY CLUSTERED 
(
	[appName] ASC,
	[Date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessureValue]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessureValue](
	[messureParamID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Val] [float] NULL,
 CONSTRAINT [PK_MessureValue] PRIMARY KEY CLUSTERED 
(
	[messureParamID] ASC,
	[Date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalculateValue]    Script Date: 03/09/2013 13:18:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalculateValue](
	[calculateParamID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Val] [float] NULL,
 CONSTRAINT [PK_CalculateValue] PRIMARY KEY CLUSTERED 
(
	[calculateParamID] ASC,
	[Date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Apparatus_ApparatusType]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[Apparatus]  WITH CHECK ADD  CONSTRAINT [FK_Apparatus_ApparatusType] FOREIGN KEY([AppTypeID])
REFERENCES [dbo].[ApparatusType] ([ApparatusTypeID])
GO
ALTER TABLE [dbo].[Apparatus] CHECK CONSTRAINT [FK_Apparatus_ApparatusType]
GO
/****** Object:  ForeignKey [FK_Apparatus_ProjectPart]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[Apparatus]  WITH CHECK ADD  CONSTRAINT [FK_Apparatus_ProjectPart] FOREIGN KEY([ProjectPartID])
REFERENCES [dbo].[ProjectPart] ([ProjectPartID])
GO
ALTER TABLE [dbo].[Apparatus] CHECK CONSTRAINT [FK_Apparatus_ProjectPart]
GO
/****** Object:  ForeignKey [FK_AppCollection_TaskType]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[AppCollection]  WITH CHECK ADD  CONSTRAINT [FK_AppCollection_TaskType] FOREIGN KEY([taskTypeID])
REFERENCES [dbo].[TaskType] ([TaskTypeID])
GO
ALTER TABLE [dbo].[AppCollection] CHECK CONSTRAINT [FK_AppCollection_TaskType]
GO
/****** Object:  ForeignKey [FK_CalculateParam_Apparatus]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[CalculateParam]  WITH CHECK ADD  CONSTRAINT [FK_CalculateParam_Apparatus] FOREIGN KEY([appName])
REFERENCES [dbo].[Apparatus] ([AppName])
GO
ALTER TABLE [dbo].[CalculateParam] CHECK CONSTRAINT [FK_CalculateParam_Apparatus]
GO
/****** Object:  ForeignKey [FK_CalculateValue_CalculateParam]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[CalculateValue]  WITH CHECK ADD  CONSTRAINT [FK_CalculateValue_CalculateParam] FOREIGN KEY([calculateParamID])
REFERENCES [dbo].[CalculateParam] ([CalculateParamID])
GO
ALTER TABLE [dbo].[CalculateValue] CHECK CONSTRAINT [FK_CalculateValue_CalculateParam]
GO
/****** Object:  ForeignKey [FK_ConstantParam_Apparatus]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[ConstantParam]  WITH CHECK ADD  CONSTRAINT [FK_ConstantParam_Apparatus] FOREIGN KEY([appName])
REFERENCES [dbo].[Apparatus] ([AppName])
GO
ALTER TABLE [dbo].[ConstantParam] CHECK CONSTRAINT [FK_ConstantParam_Apparatus]
GO
/****** Object:  ForeignKey [FK_MessureParam_Apparatus]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[MessureParam]  WITH CHECK ADD  CONSTRAINT [FK_MessureParam_Apparatus] FOREIGN KEY([appName])
REFERENCES [dbo].[Apparatus] ([AppName])
GO
ALTER TABLE [dbo].[MessureParam] CHECK CONSTRAINT [FK_MessureParam_Apparatus]
GO
/****** Object:  ForeignKey [FK_MessureValue_MessureParam]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[MessureValue]  WITH CHECK ADD  CONSTRAINT [FK_MessureValue_MessureParam] FOREIGN KEY([messureParamID])
REFERENCES [dbo].[MessureParam] ([MessureParamID])
GO
ALTER TABLE [dbo].[MessureValue] CHECK CONSTRAINT [FK_MessureValue_MessureParam]
GO
/****** Object:  ForeignKey [FK_Remark_Apparatus]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[Remark]  WITH CHECK ADD  CONSTRAINT [FK_Remark_Apparatus] FOREIGN KEY([appName])
REFERENCES [dbo].[Apparatus] ([AppName])
GO
ALTER TABLE [dbo].[Remark] CHECK CONSTRAINT [FK_Remark_Apparatus]
GO
/****** Object:  ForeignKey [FK_SysUser_Role]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[SysUser]  WITH CHECK ADD  CONSTRAINT [FK_SysUser_Role] FOREIGN KEY([roleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[SysUser] CHECK CONSTRAINT [FK_SysUser_Role]
GO
/****** Object:  ForeignKey [FK_TaskAppratus_Apparatus]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[TaskAppratus]  WITH CHECK ADD  CONSTRAINT [FK_TaskAppratus_Apparatus] FOREIGN KEY([appName])
REFERENCES [dbo].[Apparatus] ([AppName])
GO
ALTER TABLE [dbo].[TaskAppratus] CHECK CONSTRAINT [FK_TaskAppratus_Apparatus]
GO
/****** Object:  ForeignKey [FK_TaskAppratus_AppCollection]    Script Date: 03/09/2013 13:18:43 ******/
ALTER TABLE [dbo].[TaskAppratus]  WITH CHECK ADD  CONSTRAINT [FK_TaskAppratus_AppCollection] FOREIGN KEY([appCollectionID])
REFERENCES [dbo].[AppCollection] ([AppCollectionID])
GO
ALTER TABLE [dbo].[TaskAppratus] CHECK CONSTRAINT [FK_TaskAppratus_AppCollection]
GO
