USE [dcq2]
GO
/****** Object:  Table [dbo].[Apparatus]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApparatusType]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AppCollection]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CalculateParam]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CalculateValue]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConstantParam]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MessureParam]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MessureValue]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectPart]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Remark]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskAppratus]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskType]    Script Date: 2013/10/7 10:55:52 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbcfj', N'mbcfj', N'08f5db50-43d6-4b4a-a6ce-13e79233a6fd', N'8bb09ea7-f32e-44cf-9ec3-8c82dbddbafb', N'0', N'0', N'0', CAST(0x000098C700000000 AS DateTime), NULL)
GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbcfjX', N'mbcfjX', N'a4a70aab-85c7-4e01-9d42-23eeba438409', N'8bb09ea7-f32e-44cf-9ec3-8c82dbddbafb', N'0', N'0', N'0', CAST(0x000098C800000000 AS DateTime), NULL)
GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbcyg', N'mbcyg', N'abc5d0fb-ca0e-497b-99df-96863b8f67b9', N'bfd76980-155c-4340-87bc-e40ddff5839a', N'0', N'0', N'0', CAST(0x000098C800000000 AS DateTime), NULL)
GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbddwyjX', N'mbddwyjX', N'ee4f7e07-52f0-40dd-95f7-94f8f3fd2829', N'076c7a39-a91c-46c0-8857-fb241d9613d5', N'0', N'0', N'0', CAST(0x000098C800000000 AS DateTime), NULL)
GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbgbj', N'mbgbj', N'c41f52c6-ec5f-4c7a-abc5-380882d29220', N'1677e3ec-b2dd-4776-847e-adde99300202', N'0', N'0', N'0', CAST(0x000098C800000000 AS DateTime), NULL)
GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbgbjX', N'mbgbjX', N'238e0b1f-e250-4fe2-8200-7336581b9bca', N'1677e3ec-b2dd-4776-847e-adde99300202', N'0', N'0', N'0', CAST(0x000098C800000000 AS DateTime), NULL)
GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbgjj', N'mbgjj', N'97c59c71-7db7-48bf-9255-742d1dd97a7b', N'2bdcad19-2414-49a8-959b-37174a36e161', N'0', N'0', N'0', CAST(0x000098C800000000 AS DateTime), NULL)
GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbgjjX', N'mbgjjX', N'adad0a5c-8171-4c13-ae19-b994ebb75004', N'2bdcad19-2414-49a8-959b-37174a36e161', N'0', N'0', N'0', CAST(0x000098C800000000 AS DateTime), NULL)
GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbsyj', N'mbsyj', N'4c79949d-a02b-4289-abb3-03ebfa2edeb2', N'2b15c024-b2db-4c66-982b-dc1fe35dcd5a', N'0', N'0', N'0', CAST(0x000098C700000000 AS DateTime), NULL)
GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbsyjX', N'mbsyjX', N'51db6811-e862-4234-bea1-551b9d6d307a', N'2b15c024-b2db-4c66-982b-dc1fe35dcd5a', N'0', N'0', N'0', CAST(0x000098C800000000 AS DateTime), NULL)
GO
INSERT [dbo].[Apparatus] ([AppName], [CalculateName], [ProjectPartID], [AppTypeID], [X], [Y], [Z], [BuriedTime], [OtherInfo]) VALUES (N'mbwdj', N'mbwdj', N'2b7ca1c4-1104-4996-b999-cbb464a6e338', N'f2fd16c1-85d7-4d60-a4d7-ee03008e372b', N'0', N'0', N'0', CAST(0x000098C700000000 AS DateTime), NULL)
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'ffa04fa7-c6fd-42fb-b0fd-306ca8a1b434', N'量水堰')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'2bdcad19-2414-49a8-959b-37174a36e161', N'钢筋计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'fc246410-23d8-4539-90fe-41a9fda61da7', N'无应力计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'53f0a4a2-7824-4e73-a023-5145ca5a9eb8', N'裂缝计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'30cd23f0-c631-41ca-a383-7a3dcfbf918d', N'压应力计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'8bb09ea7-f32e-44cf-9ec3-8c82dbddbafb', N'测缝计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'9f7b66ac-4cb5-4b44-967a-9ec8e6d71ff3', N'位错计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'32f5efe1-530d-4c74-9ca6-a801cc6af42e', N'应变计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'1677e3ec-b2dd-4776-847e-adde99300202', N'钢板计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'f851237a-a475-47d6-8dce-b8b3bd90232e', N'动力学仪器')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'2b15c024-b2db-4c66-982b-dc1fe35dcd5a', N'渗压计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'bfd76980-155c-4340-87bc-e40ddff5839a', N'测压管')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'8fa63bc4-4732-4a97-b4b9-e570eab9d054', N'基岩变形计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'f2fd16c1-85d7-4d60-a4d7-ee03008e372b', N'温度计')
GO
INSERT [dbo].[ApparatusType] ([ApparatusTypeID], [TypeName]) VALUES (N'076c7a39-a91c-46c0-8857-fb241d9613d5', N'多点位移计')
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'6c69d7ce-2604-438a-985b-0858c972a886', N'MBCFJ', N'温度', N'T', N'℃', 1, NULL, N'(Rz-Rx-R0)*a/100', 6, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'd872ef05-92e6-41f5-b7ad-16cf39058fb1', N'MBGJJX', N'应力', N'stress', N'MPa', 2, 2, N'(F1-Zj)*f/Area*10', 4, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'483b6a07-8a17-4ceb-9849-1ff0e917f91c', N'MBGJJ', N'温度', N'T', N'℃', 1, NULL, N'(Rz-Rx-R0)*a/100', 5, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'd62d1ddf-7381-4ff9-a42c-20ec6f3308bf', N'MBWDJ', N'温度', N'T', N'℃', 1, NULL, N'(Rz-Rx-R0)*a/100', 4, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'311081a5-d073-4bf7-987a-231f1f493ee2', N'MBCFJ', N'开合度', N'K', N'mm', 2, NULL, N'(Z1-Zj)*f+(Rz-Rx-Rj)*a/100*B', 10, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'197d008f-4bf9-4b64-9c6c-25a603f95627', N'MBSYJX', N'渗压水位', N'H', N'm', 2, 3, N'H0-P/0.00980', 8, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'a461b529-adb3-440b-9c2b-30ce02080edd', N'MBCFJX', N'温度', N'T', N'℃', 1, 1, N'T1', 6, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'8129a69a-757c-484f-8e64-3d60d224a96f', N'MBDDWYJX', N'温度', N'j', N'℃', 1, 0, N'e', 15, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'e991da22-d9ea-44c4-8cf5-425797b08c31', N'MBGJJX', N'温度', N'T', N'℃', 1, 1, N'T1', 6, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'ef72ded7-dc4e-4fc3-bae2-48a8298f2082', N'MBGJJ', N'应力', N'N', N'MPa', 2, NULL, N'(Z1-Zj)*f+B*(Rz-Rx-Rj)*a/100', 10, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'89c6c353-dabd-4047-aea7-57fab7cb2b01', N'MBDDWYJX', N'4号点', N'i', N'mm', 2, 4, N'(d-s)*n+(j-t)*o', 16, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'614d059f-27eb-4410-a9ee-5f6716efcf90', N'MBGBJX', N'应力', N'N', N'MPa', 2, NULL, N'(F1-Zj)*m*f', 4, N'2')
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'3059b179-f0ce-4919-8f33-61e58914c35b', N'MBGBJ', N'温度', N'T', N'℃', 1, NULL, N'(Rz-R0)*a/100', 11, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'0f5e9da9-46f5-4538-83a0-689f1e78bb77', N'MBDDWYJX', N'3号点', N'h', N'mm', 2, 3, N'(c-r)*m+(j-t)*o', 17, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'e4eaae73-e71c-4825-b5c0-80057f958673', N'MBSYJX', N'温度', N'T', N'℃', 1, 1, N'T1', 9, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'62256bef-ccfa-4941-bc1c-80cefbe57921', N'MBSYJ', N'温度', N'T', N'℃', 1, 3, N'(Rz-R0)*a', 6, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'98260dad-b901-4417-9735-96897b2c3927', N'MBDDWYJX', N'2号点', N'g', N'mm', 2, 2, N'(b-q)*l+(j-t)*o', 18, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'1e5a853f-eb21-4206-90a9-a96ecc00f675', N'MBDDWYJX', N'1号点', N'f', N'mm', 2, 1, N'(a-p)*k+(j-t)*o', 19, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'2782e072-a7d3-42e5-8142-b6968a4373ee', N'MBCYG', N'渗压水位', N'H', N'm', 2, 1, N'H1-big(H2,0)*H2*COS((3.14/180)*Angle)-smleq(H2,0)*H2', 3, N'这里使用了分段函数，详见说明书')
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'6a403b92-550b-47b9-9d91-cc1c2d0eac19', N'MBSYJ', N'渗压水位', N'H', N'm', 2, 2, N'H0-P/0.0098', 12, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'abef9456-96b4-4ad0-becb-dae7cf7c00dd', N'MBSYJ', N'渗透压力', N'P', N'MPa', 4, 1, N'(Z1-Zj)*f-(Rz-Rj-Rx)*a*B', 10, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'912dc82d-621b-48e4-8e6d-e6396dc6a06b', N'MBGBJX', N'温度', N'T', N'℃', 1, NULL, N'T1', 6, N'1')
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'b2a8921b-1539-4711-92e2-ebf408bd2108', N'MBSYJX', N'渗透压力', N'P', N'MPa', 4, 2, N'(F1-Zj)*f-B*(T1-Tj)', 7, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'a22a49e5-38d3-4d00-862b-f7f86da21f59', N'MBGBJ', N'应力', N'stree', N'MPa', 2, NULL, N'((Z1-Zj)*f+(Rz-Rj)*(B-C)*a/100)*m', 10, NULL)
GO
INSERT [dbo].[CalculateParam] ([CalculateParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [CalculateExpress], [CalculateOrder], [Description]) VALUES (N'252d16ec-c32d-4a7c-ad7b-f984f503b013', N'MBCFJX', N'开合度', N'K', N'mm', 2, 2, N'(F1-Zj)*f+B*(T-Tj)', 7, NULL)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'6c69d7ce-2604-438a-985b-0858c972a886', CAST(0x000098C701365D20 AS DateTime), 20.2)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'd872ef05-92e6-41f5-b7ad-16cf39058fb1', CAST(0x000098C80110BC50 AS DateTime), 9.41)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'483b6a07-8a17-4ceb-9849-1ff0e917f91c', CAST(0x000098C800A8EA30 AS DateTime), 21.4)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'd62d1ddf-7381-4ff9-a42c-20ec6f3308bf', CAST(0x000098C701499700 AS DateTime), 19.1)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'311081a5-d073-4bf7-987a-231f1f493ee2', CAST(0x000098C701365D20 AS DateTime), 2.9)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'197d008f-4bf9-4b64-9c6c-25a603f95627', CAST(0x000098C80104A690 AS DateTime), 65.61)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'a461b529-adb3-440b-9c2b-30ce02080edd', CAST(0x000098C801071F60 AS DateTime), 19.2)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'8129a69a-757c-484f-8e64-3d60d224a96f', CAST(0x000098C800FB9640 AS DateTime), 16.6)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'e991da22-d9ea-44c4-8cf5-425797b08c31', CAST(0x000098C80110BC50 AS DateTime), 19)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'ef72ded7-dc4e-4fc3-bae2-48a8298f2082', CAST(0x000098C800A8EA30 AS DateTime), -10.77)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'89c6c353-dabd-4047-aea7-57fab7cb2b01', CAST(0x000098C800FB9640 AS DateTime), -0.01)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'614d059f-27eb-4410-a9ee-5f6716efcf90', CAST(0x000098C800AC35F0 AS DateTime), -19.04)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'3059b179-f0ce-4919-8f33-61e58914c35b', CAST(0x000098C800AF81B0 AS DateTime), 28.9)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'0f5e9da9-46f5-4538-83a0-689f1e78bb77', CAST(0x000098C800FB9640 AS DateTime), -0.2)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'e4eaae73-e71c-4825-b5c0-80057f958673', CAST(0x000098C80104A690 AS DateTime), 19.3)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'62256bef-ccfa-4941-bc1c-80cefbe57921', CAST(0x000098C7014C0FD0 AS DateTime), 18.8)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'98260dad-b901-4417-9735-96897b2c3927', CAST(0x000098C800FB9640 AS DateTime), 0.28)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'1e5a853f-eb21-4206-90a9-a96ecc00f675', CAST(0x000098C800FB9640 AS DateTime), -1.43)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'2782e072-a7d3-42e5-8142-b6968a4373ee', CAST(0x000098C80111D590 AS DateTime), 70.42)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'6a403b92-550b-47b9-9d91-cc1c2d0eac19', CAST(0x000098C7014C0FD0 AS DateTime), 151.76)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'abef9456-96b4-4ad0-becb-dae7cf7c00dd', CAST(0x000098C7014C0FD0 AS DateTime), -0.6542)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'912dc82d-621b-48e4-8e6d-e6396dc6a06b', CAST(0x000098C800AC35F0 AS DateTime), 24.9)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'b2a8921b-1539-4711-92e2-ebf408bd2108', CAST(0x000098C80104A690 AS DateTime), -0.3637)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'a22a49e5-38d3-4d00-862b-f7f86da21f59', CAST(0x000098C800AF81B0 AS DateTime), -30.92)
GO
INSERT [dbo].[CalculateValue] ([calculateParamID], [Date], [Val]) VALUES (N'252d16ec-c32d-4a7c-ad7b-f984f503b013', CAST(0x000098C801071F60 AS DateTime), -0.17)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'2dac0158-0f41-4e25-bf38-0f6982f296f4', N'MBDDWYJX', N'温度修正系数', N'o', NULL, 4, 5, 0.00397374, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'c48fbb0e-a8e6-4bb7-a191-1066bdbd4797', N'MBGBJ', N'灵敏度', N'f', NULL, 6, 3, 5.68, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'74ab4536-81d4-4212-8809-140afbb38c1e', N'MBGJJX', N'灵敏度', N'f', NULL, 6, 1, 0.18926, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'd1e3d781-2a8c-4924-b3ed-1561fcf6d572', N'MBSYJ', N'温度系数', N'a', N'℃/Ω', 2, NULL, 4.76, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'41a9609b-8d23-4d78-a7a8-1898fbc9d892', N'MBGJJ', N'基准电阻', N'Rj', N'0.01Ω', 0, 1, 7400, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'b445a7f9-75c1-4165-a0b3-1c1b8aab8ad2', N'MBGBJ', N'温度系数', N'a', N'℃/Ω', 2, 1, 4.74, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'246c93a1-5fac-410a-8d54-2309abb20baf', N'MBGBJ', N'温度膨胀系数', N'C', NULL, NULL, 5, 12, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'58ec4fb7-89ab-49c3-9b3b-24293de36feb', N'MBSYJ', N'基准测值', N'Zj', NULL, 1, NULL, 10143, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'5864d443-a4c2-4f52-8d87-2701fe455580', N'MBSYJX', N'基准测值', N'Zj', NULL, 1, 2, 3522.27, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'4d36725b-a398-489f-8def-27408852e324', N'MBDDWYJX', N'2号点基值', N'q', NULL, NULL, 7, 6456, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'4361688c-a3c7-42ad-a4ba-2aa83250b955', N'MBDDWYJX', N'4号点系数', N'n', NULL, NULL, 4, 0.0040135, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'f2b91349-c752-4a79-9964-2f31f4429be7', N'MBSYJX', N'基准温度', N'Tj', N'℃', 1, 4, 22.8, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'c9bf9840-2631-48da-b695-3b76faefaae5', N'MBCFJ', N'基准电阻', N'Rj', N'0.01Ω', 0, 1, 5408, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'eba749b5-550d-40b5-90a4-407e2cf5784a', N'MBCFJX', N'基准温度', N'Tj', N'℃', 1, 4, 17.8, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'95770f3b-d9a5-4bbd-9845-4795b069b42a', N'MBSYJ', N'温度修正系数', N'B', NULL, 4, NULL, 0.00224, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'd0477676-abc2-4ac8-8c34-483ed694ebac', N'MBDDWYJX', N'1号点基值', N'p', NULL, NULL, 6, 5652, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'd8657080-e558-4b20-b30d-4bf4cc383f95', N'MBCFJ', N'基准测值', N'Zj', NULL, 1, 0, 10040, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'ef2adb34-065a-4aef-a162-509dbcebe58e', N'MBDDWYJX', N'4号点基值', N's', NULL, NULL, 9, 5413, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'00ac17db-099d-462b-9cdd-5259734b48b9', N'MBGBJ', N'温度修正系数', N'B', NULL, 4, 4, 13.4, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'b52c11c4-97e5-4dcc-8950-63583dce4afb', N'MBCFJ', N'温度系数', N'a', N'℃/Ω', 2, 5, 7.23, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'40679af6-c80c-44d2-9bc1-649351273ae5', N'MBWDJ', N'温度系数', N'a', N'℃/Ω', 2, NULL, 5, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'5a9d2519-3998-429b-8f13-66c60b99feb1', N'MBGJJ', N'温度修正系数', N'B', NULL, 4, 2, 0.9, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'5a65d046-2cff-4423-9300-66ff6f370d20', N'MBCFJX', N'灵敏度', N'f', NULL, 6, 1, 0.00448344, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'5d776f3b-100a-4eae-96de-700117592b92', N'MBWDJ', N'0°电阻', N'R0', N'0.01Ω', 0, NULL, 4660, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'9d84faae-2081-4b98-9e5a-71f4d7c76d94', N'MBGJJX', N'基准测值', N'Zj', NULL, 1, 2, 2045, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'bd767537-dfe3-4f9c-ad4d-805f98fffb6c', N'MBGBJ', N'0°电阻', N'R0', N'0.01Ω', 0, 2, 7061, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'd296d005-a77c-4706-b9be-844460b87f33', N'MBSYJX', N'温度修正系数', N'B', NULL, 4, 3, 2.43E-05, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'72fda708-a046-40b5-928e-84fe80080a5e', N'MBGBJ', N'基准测值', N'Zj', NULL, 1, 7, 9924, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'b5bd3ef5-734a-42fe-9e9b-90b183c6c4bd', N'MBGJJX', N'截面积', N'Area', N'cm2', NULL, 3, 9.57, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'28fa9d51-1164-4008-a616-94577b21321a', N'MBDDWYJX', N'2号点系数', N'l', NULL, NULL, 2, 0.00400755, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'0270f824-8129-417f-a685-957b9a8492b8', N'MBGBJX', N'弹模', N'm', N'kg/cm2', NULL, 3, 0.205, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'2cf2232b-46ae-4a7e-b976-9a596dbd1d15', N'MBCYG', N'钻孔角度', N'Angle', N'°', 1, NULL, 0, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'73094b5d-0688-46e1-9eb6-9ed9dd51dbcc', N'MBGJJ', N'灵敏度', N'f', NULL, 6, 3, 0.799, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'0b794da8-e707-4da1-8ea4-a0d2cb585430', N'MBDDWYJX', N'1号点系数', N'k', NULL, NULL, 1, 0.0045, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'e35cd32c-6670-4971-b18b-a274fb26beba', N'MBGBJX', N'灵敏度', N'f', NULL, 6, 1, 1, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'd8c84321-f9e5-4417-864d-a4a5a78b82f2', N'MBGBJX', N'基准测值', N'Zj', NULL, 1, 2, 1850.27, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'80b7c2a8-b101-4367-8959-a6107e734c21', N'MBDDWYJX', N'基准温度', N't', N'℃', 1, 10, 18.3, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'e689310f-8100-4829-b8c3-a760c321405b', N'MBGJJ', N'0°电阻', N'R0', N'0.01Ω', 0, 4, 7014, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'ef669e0c-d8eb-49ed-b054-a9c54cfa1175', N'MBSYJ', N'基准电阻', N'Rj', N'0.01Ω', 0, NULL, 75.6, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'9c60f025-f3b3-4fbb-a54a-ac2af1ee9211', N'MBDDWYJX', N'3号点基值', N'r', NULL, NULL, 8, 5896, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'8e1d7e49-9c4d-4d01-83e4-af00a7e14ccf', N'MBCFJX', N'温度修正系数', N'B', NULL, 4, 3, 0.0045, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'771290cb-71b7-4084-a306-b72448ec06c2', N'MBCFJ', N'温度修正系数', N'B', NULL, 4, 2, 0.005, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'c34b50bb-dc15-487f-891f-b7f1fbf5d630', N'MBGBJ', N'基准电阻', N'Rj', N'0.01Ω', 0, 6, 7632, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'580130a8-1aa9-4589-b84b-becdef466ee0', N'MBSYJ', N'0°电阻', N'R0', N'0.01Ω', 0, NULL, 71, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'da3ff684-aa39-4ae0-b0e5-c3a887baf4aa', N'MBGJJ', N'温度系数', N'a', N'℃/Ω', 2, 5, 4.73, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'3b9e87f2-c5d3-4895-9f00-c449a118b256', N'MBCFJ', N'0°电阻', N'R0', N'0.01Ω', 0, 4, 5051, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'b49c8f8b-1aa5-4f89-9087-cd5a753568bf', N'MBGBJ', N'弹模', N'm', NULL, NULL, 8, 0.205, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'9c10645b-46b0-49fa-bebc-df04094046f6', N'MBCFJX', N'基准测值', N'Zj', NULL, 1, 2, 5512.5, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'485bcbf1-d2ab-425c-831d-dfca40993fe0', N'MBSYJX', N'灵敏度', N'f', NULL, 6, 1, 0.0007267, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'69e8aa83-5b53-463a-b0a4-e04cda32676a', N'MBCFJ', N'灵敏度', N'f', NULL, 6, 3, 0.0214, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'fa85f70b-ed8d-4e40-95d2-e50dd607778d', N'MBSYJ', N'埋设高程', N'H0', N'm', NULL, NULL, 85, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'4e497620-ac86-4d96-a72c-e6ebb15d20cb', N'MBGJJ', N'基准测值', N'Zj', NULL, 1, 0, 10153, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'4dfcc1a3-2701-41f0-b1a7-e7c56e721ddc', N'MBCYG', N'孔口高程', N'H1', N'm', 4, NULL, 76.209, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'11bb925b-bd5c-4693-8bf7-e888ad804870', N'MBSYJX', N'埋设高程', N'H0', NULL, NULL, 5, 28.5, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'ca878686-f01f-4b1d-8970-ea354f186ff9', N'MBSYJ', N'灵敏度', N'f', NULL, 6, NULL, 0.00958, NULL)
GO
INSERT [dbo].[ConstantParam] ([ConstantParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Val], [Description]) VALUES (N'ea4ac607-9d64-472f-bcac-ebdeff00663b', N'MBDDWYJX', N'3号点系数', N'm', NULL, NULL, 3, 0.00400414, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'70690015-df27-4dc1-b5ae-041360782f03', N'MBDDWYJX', N'4号点线性', N'd', NULL, 2, 4, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'239ee4bd-e681-49e7-b3b9-150c0c6894b2', N'MBCYG', N'基于管口水位', N'H2', NULL, 0, 1, N'高于管口为负，反之为正，若将压力表，应为压力表计数除以-0.0098')
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'4e8c151e-03bc-4eb5-8727-57728b59a31e', N'MBGBJ', N'芯线电阻', N'Rx', N'0.01Ω', 0, 2, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'5f0f3fae-242b-4cc6-a281-587b935e0b3f', N'MBCFJ', N'电阻比', N'Z1', N'0.01%', 4, 3, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'3ba5f013-c634-4b16-ac87-5cbaa931ce88', N'MBGBJ', N'电阻比', N'Z1', N'0.01%', 4, 3, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'63661d86-08dc-419f-a2e2-60b80043cb7e', N'MBSYJ', N'芯线电阻', N'Rx', N'0.01Ω', 0, 2, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'e0cf5574-ac03-4057-85bd-61b805a4eb0e', N'MBDDWYJX', N'3号点线性', N'c', NULL, 2, 3, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'580d2c79-92da-47a5-a328-719b119fdc0a', N'MBSYJX', N'观测温度', N'T1', N'℃', 1, 2, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'52523bd1-bae4-49c1-b80e-732fa7ba83ed', N'MBWDJ', N'总电阻', N'Rz', N'0.01Ω', 0, 1, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'f2fe85b9-14e9-481a-83ab-7db39a86c9c0', N'MBCFJ', N'芯线电阻', N'Rx', N'0.01Ω', 0, 2, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'fcf20e38-8226-4f1b-92b4-8b9e8c001ffd', N'MBGJJX', N'线性测值', N'F1', N'digit(Hz)', 2, 1, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'8f89dccf-195d-4740-838a-9e7b2ea4d141', N'MBGBJX', N'观测温度', N'T1', N'℃', 1, 2, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'0b8e988d-49ce-453d-b53e-a6a6a16393a0', N'MBCFJX', N'观测温度', N'T1', N'℃', 1, 2, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'8f0db413-c8e4-42cc-8e0d-bbff7786e3c4', N'MBCFJX', N'线性测值', N'F1', N'digit(Hz)', 2, 1, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'de8c0524-8a2c-49fd-8979-c0e73e3d94ae', N'MBSYJ', N'电阻比', N'Z1', N'0.01%', 4, 3, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'6c7a2f70-429d-44a4-a946-c5cb0d249692', N'MBSYJ', N'总电阻', N'Rz', N'0.01Ω', 0, 1, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'68fee79a-e788-4a6c-84a5-cb253c8612ee', N'MBGJJ', N'电阻比', N'Z1', N'0.01%', 4, 3, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'ce3bfa7d-a960-48ff-8e31-cb95c325d3fd', N'MBGJJ', N'总电阻', N'Rz', N'0.01Ω', 0, 1, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'f6aea98e-524b-41a9-bf65-cca25a449069', N'MBGBJX', N'线性测值', N'F1', N'digit(Hz)', 2, 1, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'f5cee4e1-1279-4422-bb5c-d0257926fbb7', N'MBSYJX', N'线性测值', N'F1', N'digit(Hz)', 2, 1, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'77fb669d-2ae9-4eab-b032-d8b1128dea91', N'MBDDWYJX', N'1号点线性', N'a', NULL, 2, 1, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'bb266bde-fb35-46d2-a4d8-d8e29ee5e8f7', N'MBGJJ', N'芯线电阻', N'Rx', N'0.01Ω', 0, 2, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'bb241e1f-3697-40e1-8392-dc099c639135', N'MBCFJ', N'总电阻', N'Rz', N'0.01Ω', 0, 1, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'db771093-5eb3-449f-80af-dd65f63b5a65', N'MBGJJX', N'观测温度', N'T1', N'℃', 1, 2, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'e46943a9-0c73-411f-b900-e3ddf4785aba', N'MBWDJ', N'芯线电阻', N'Rx', N'0.01Ω', 0, 2, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'969dfa77-f42c-4b6e-aed8-f841f4aa4b27', N'MBDDWYJX', N'观测温度', N'e', N'℃', 1, 5, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'1e554fd8-50c1-4104-9682-fa17f08c1ff7', N'MBGBJ', N'总电阻', N'Rz', N'0.01Ω', 0, 1, NULL)
GO
INSERT [dbo].[MessureParam] ([MessureParamID], [appName], [ParamName], [ParamSymbol], [UnitSymbol], [PrecisionNum], [Order], [Description]) VALUES (N'963e2a09-bcfc-4f8d-8a0c-faf0c1604fda', N'MBDDWYJX', N'2号点线性', N'b', NULL, 2, 2, NULL)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'70690015-df27-4dc1-b5ae-041360782f03', CAST(0x000098C800FB9640 AS DateTime), 5411)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'239ee4bd-e681-49e7-b3b9-150c0c6894b2', CAST(0x000098C80111D590 AS DateTime), 5.788)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'4e8c151e-03bc-4eb5-8727-57728b59a31e', CAST(0x000098C800AF81B0 AS DateTime), 0)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'5f0f3fae-242b-4cc6-a281-587b935e0b3f', CAST(0x000098C701365D20 AS DateTime), 10177)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'3ba5f013-c634-4b16-ac87-5cbaa931ce88', CAST(0x000098C800AF81B0 AS DateTime), 9897)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'63661d86-08dc-419f-a2e2-60b80043cb7e', CAST(0x000098C7014C0FD0 AS DateTime), 0)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'e0cf5574-ac03-4057-85bd-61b805a4eb0e', CAST(0x000098C800FB9640 AS DateTime), 5847)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'580d2c79-92da-47a5-a328-719b119fdc0a', CAST(0x000098C80104A690 AS DateTime), 19.3)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'52523bd1-bae4-49c1-b80e-732fa7ba83ed', CAST(0x000098C701499700 AS DateTime), 5042)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'f2fe85b9-14e9-481a-83ab-7db39a86c9c0', CAST(0x000098C701365D20 AS DateTime), 0)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'fcf20e38-8226-4f1b-92b4-8b9e8c001ffd', CAST(0x000098C80110BC50 AS DateTime), 2092.56)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'8f89dccf-195d-4740-838a-9e7b2ea4d141', CAST(0x000098C800AC35F0 AS DateTime), 24.9)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'0b8e988d-49ce-453d-b53e-a6a6a16393a0', CAST(0x000098C801071F60 AS DateTime), 19.2)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'8f0db413-c8e4-42cc-8e0d-bbff7786e3c4', CAST(0x000098C801071F60 AS DateTime), 5472.3)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'de8c0524-8a2c-49fd-8979-c0e73e3d94ae', CAST(0x000098C7014C0FD0 AS DateTime), 10074)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'6c7a2f70-429d-44a4-a946-c5cb0d249692', CAST(0x000098C7014C0FD0 AS DateTime), 74.96)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'68fee79a-e788-4a6c-84a5-cb253c8612ee', CAST(0x000098C800A8EA30 AS DateTime), 10136)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'ce3bfa7d-a960-48ff-8e31-cb95c325d3fd', CAST(0x000098C800A8EA30 AS DateTime), 7466)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'f6aea98e-524b-41a9-bf65-cca25a449069', CAST(0x000098C800AC35F0 AS DateTime), 1757.37)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'f5cee4e1-1279-4422-bb5c-d0257926fbb7', CAST(0x000098C80104A690 AS DateTime), 3021.73)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'77fb669d-2ae9-4eab-b032-d8b1128dea91', CAST(0x000098C800FB9640 AS DateTime), 5336)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'bb266bde-fb35-46d2-a4d8-d8e29ee5e8f7', CAST(0x000098C800A8EA30 AS DateTime), 0)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'bb241e1f-3697-40e1-8392-dc099c639135', CAST(0x000098C701365D20 AS DateTime), 5331)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'db771093-5eb3-449f-80af-dd65f63b5a65', CAST(0x000098C80110BC50 AS DateTime), 19)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'e46943a9-0c73-411f-b900-e3ddf4785aba', CAST(0x000098C701499700 AS DateTime), 0)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'969dfa77-f42c-4b6e-aed8-f841f4aa4b27', CAST(0x000098C800FB9640 AS DateTime), 16.6)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'1e554fd8-50c1-4104-9682-fa17f08c1ff7', CAST(0x000098C800AF81B0 AS DateTime), 7670)
GO
INSERT [dbo].[MessureValue] ([messureParamID], [Date], [Val]) VALUES (N'963e2a09-bcfc-4f8d-8a0c-faf0c1604fda', CAST(0x000098C800FB9640 AS DateTime), 6528)
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'4c79949d-a02b-4289-abb3-03ebfa2edeb2', N'渗压计', N'f6a54d36-8fd6-46a0-8c65-fc67d4c3987e')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'08f5db50-43d6-4b4a-a6ce-13e79233a6fd', N'测缝计', N'f6a54d36-8fd6-46a0-8c65-fc67d4c3987e')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'a4a70aab-85c7-4e01-9d42-23eeba438409', N'测缝计', N'ac5f421f-1176-4af9-9e52-a69d3d718b85')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'c41f52c6-ec5f-4c7a-abc5-380882d29220', N'钢板计', N'f6a54d36-8fd6-46a0-8c65-fc67d4c3987e')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'51db6811-e862-4234-bea1-551b9d6d307a', N'渗压计', N'ac5f421f-1176-4af9-9e52-a69d3d718b85')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'238e0b1f-e250-4fe2-8200-7336581b9bca', N'钢板计', N'ac5f421f-1176-4af9-9e52-a69d3d718b85')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'97c59c71-7db7-48bf-9255-742d1dd97a7b', N'钢筋计', N'f6a54d36-8fd6-46a0-8c65-fc67d4c3987e')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'd796c4df-16ae-4689-b8ac-943644e75ed3', N'test', N'08c4a167-6ec9-43b0-8069-f8f3159975ca')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'ee4f7e07-52f0-40dd-95f7-94f8f3fd2829', N'多点位移计', N'ac5f421f-1176-4af9-9e52-a69d3d718b85')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'abc5d0fb-ca0e-497b-99df-96863b8f67b9', N'测压管', N'08c4a167-6ec9-43b0-8069-f8f3159975ca')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'b4248662-ae19-42b9-99de-a2e6a21edde4', N'模板', NULL)
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'ac5f421f-1176-4af9-9e52-a69d3d718b85', N'钢弦式', N'08c4a167-6ec9-43b0-8069-f8f3159975ca')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'adad0a5c-8171-4c13-ae19-b994ebb75004', N'钢筋计', N'ac5f421f-1176-4af9-9e52-a69d3d718b85')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'2b7ca1c4-1104-4996-b999-cbb464a6e338', N'温度计', N'f6a54d36-8fd6-46a0-8c65-fc67d4c3987e')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'08c4a167-6ec9-43b0-8069-f8f3159975ca', N'模板', N'b4248662-ae19-42b9-99de-a2e6a21edde4')
GO
INSERT [dbo].[ProjectPart] ([ProjectPartID], [PartName], [ParentPart]) VALUES (N'f6a54d36-8fd6-46a0-8c65-fc67d4c3987e', N'差动式', N'08c4a167-6ec9-43b0-8069-f8f3159975ca')
GO
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description], [Power]) VALUES (1, N'普通用户', N'普通注册用户                                      ', 1)
GO
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description], [Power]) VALUES (2, N'录入员', N'录入员', 2)
GO
INSERT [dbo].[Role] ([RoleID], [RoleName], [Description], [Power]) VALUES (10, N'管理员', N'系统管理员,拥有最高权限                           ', 255)
GO
INSERT [dbo].[SysUser] ([UserName], [PasswordHash], [Salt], [Question], [Answer], [roleID]) VALUES (N'admin', N'94C40DCC4D5DEF15510C4151A4C39A801B2895EB', N'38Eix6o=', N'0plTbuE=', N'eGeTtnI=', 10)
GO
INSERT [dbo].[SysUser] ([UserName], [PasswordHash], [Salt], [Question], [Answer], [roleID]) VALUES (N'user', N'D5DC6DD673F76CE5A650E28F8DFF9DDA7E532494', N'dV3555E=', N'question', N'ef乱sdde21d', 2)
GO
INSERT [dbo].[TaskType] ([TaskTypeID], [TypeName]) VALUES (0, N'输入任务')
GO
INSERT [dbo].[TaskType] ([TaskTypeID], [TypeName]) VALUES (1, N'检索任务')
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AppCollection]    Script Date: 2013/10/7 10:55:52 ******/
ALTER TABLE [dbo].[AppCollection] ADD  CONSTRAINT [IX_AppCollection] UNIQUE NONCLUSTERED
(
[CollectionName] ASC,
[taskTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CalculateParam]    Script Date: 2013/10/7 10:55:52 ******/
ALTER TABLE [dbo].[CalculateParam] ADD  CONSTRAINT [IX_CalculateParam] UNIQUE NONCLUSTERED
(
[appName] ASC,
[ParamName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ConstantParam]    Script Date: 2013/10/7 10:55:52 ******/
ALTER TABLE [dbo].[ConstantParam] ADD  CONSTRAINT [IX_ConstantParam] UNIQUE NONCLUSTERED
(
[appName] ASC,
[ParamName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ConstantParam_1]    Script Date: 2013/10/7 10:55:52 ******/
ALTER TABLE [dbo].[ConstantParam] ADD  CONSTRAINT [IX_ConstantParam_1] UNIQUE NONCLUSTERED
(
[appName] ASC,
[ParamSymbol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_MessureParam]    Script Date: 2013/10/7 10:55:52 ******/
ALTER TABLE [dbo].[MessureParam] ADD  CONSTRAINT [IX_MessureParam] UNIQUE NONCLUSTERED
(
[appName] ASC,
[ParamName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_MessureParam_1]    Script Date: 2013/10/7 10:55:52 ******/
ALTER TABLE [dbo].[MessureParam] ADD  CONSTRAINT [IX_MessureParam_1] UNIQUE NONCLUSTERED
(
[appName] ASC,
[ParamSymbol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Apparatus]  WITH CHECK ADD  CONSTRAINT [FK_Apparatus_ApparatusType] FOREIGN KEY([AppTypeID])
REFERENCES [dbo].[ApparatusType] ([ApparatusTypeID])
GO
ALTER TABLE [dbo].[Apparatus] CHECK CONSTRAINT [FK_Apparatus_ApparatusType]
GO
ALTER TABLE [dbo].[Apparatus]  WITH CHECK ADD  CONSTRAINT [FK_Apparatus_ProjectPart] FOREIGN KEY([ProjectPartID])
REFERENCES [dbo].[ProjectPart] ([ProjectPartID])
GO
ALTER TABLE [dbo].[Apparatus] CHECK CONSTRAINT [FK_Apparatus_ProjectPart]
GO
ALTER TABLE [dbo].[AppCollection]  WITH CHECK ADD  CONSTRAINT [FK_AppCollection_TaskType] FOREIGN KEY([taskTypeID])
REFERENCES [dbo].[TaskType] ([TaskTypeID])
GO
ALTER TABLE [dbo].[AppCollection] CHECK CONSTRAINT [FK_AppCollection_TaskType]
GO
ALTER TABLE [dbo].[CalculateParam]  WITH CHECK ADD  CONSTRAINT [FK_CalculateParam_Apparatus] FOREIGN KEY([appName])
REFERENCES [dbo].[Apparatus] ([AppName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CalculateParam] CHECK CONSTRAINT [FK_CalculateParam_Apparatus]
GO
ALTER TABLE [dbo].[CalculateValue]  WITH CHECK ADD  CONSTRAINT [FK_CalculateValue_CalculateParam] FOREIGN KEY([calculateParamID])
REFERENCES [dbo].[CalculateParam] ([CalculateParamID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CalculateValue] CHECK CONSTRAINT [FK_CalculateValue_CalculateParam]
GO
ALTER TABLE [dbo].[ConstantParam]  WITH CHECK ADD  CONSTRAINT [FK_ConstantParam_Apparatus] FOREIGN KEY([appName])
REFERENCES [dbo].[Apparatus] ([AppName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConstantParam] CHECK CONSTRAINT [FK_ConstantParam_Apparatus]
GO
ALTER TABLE [dbo].[MessureParam]  WITH CHECK ADD  CONSTRAINT [FK_MessureParam_Apparatus] FOREIGN KEY([appName])
REFERENCES [dbo].[Apparatus] ([AppName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessureParam] CHECK CONSTRAINT [FK_MessureParam_Apparatus]
GO
ALTER TABLE [dbo].[MessureValue]  WITH CHECK ADD  CONSTRAINT [FK_MessureValue_MessureParam] FOREIGN KEY([messureParamID])
REFERENCES [dbo].[MessureParam] ([MessureParamID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessureValue] CHECK CONSTRAINT [FK_MessureValue_MessureParam]
GO
ALTER TABLE [dbo].[Remark]  WITH CHECK ADD  CONSTRAINT [FK_Remark_Apparatus] FOREIGN KEY([appName])
REFERENCES [dbo].[Apparatus] ([AppName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Remark] CHECK CONSTRAINT [FK_Remark_Apparatus]
GO
ALTER TABLE [dbo].[SysUser]  WITH CHECK ADD  CONSTRAINT [FK_SysUser_Role] FOREIGN KEY([roleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[SysUser] CHECK CONSTRAINT [FK_SysUser_Role]
GO
ALTER TABLE [dbo].[TaskAppratus]  WITH CHECK ADD  CONSTRAINT [FK_TaskAppratus_Apparatus] FOREIGN KEY([appName])
REFERENCES [dbo].[Apparatus] ([AppName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskAppratus] CHECK CONSTRAINT [FK_TaskAppratus_Apparatus]
GO
ALTER TABLE [dbo].[TaskAppratus]  WITH CHECK ADD  CONSTRAINT [FK_TaskAppratus_AppCollection] FOREIGN KEY([appCollectionID])
REFERENCES [dbo].[AppCollection] ([AppCollectionID])
GO
ALTER TABLE [dbo].[TaskAppratus] CHECK CONSTRAINT [FK_TaskAppratus_AppCollection]
GO

