
USE [TestDB]
GO

CREATE SCHEMA [web]


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [web].[Web_MenuItemType](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](150) NULL,
 CONSTRAINT [PK_Web_MenuItemType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO



CREATE TABLE [web].[Web_ApplicationType](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NULL,
 CONSTRAINT [PK_Web_ApplicationType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




CREATE TABLE [web].[Web_Application](
	[Id] [int] NOT NULL,
	[ApplicationName] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[ApplicationTypeId] [int] NULL,
	[Version] [nvarchar](50) NULL,
 CONSTRAINT [PK_Web_Application] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [web].[Web_Application] ADD  CONSTRAINT [DF_Web_Application_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [web].[Web_Application]  WITH CHECK ADD  CONSTRAINT [FK_Web_Application_Web_ApplicationType] FOREIGN KEY([ApplicationTypeId])
REFERENCES [web].[Web_ApplicationType] ([Id])
GO

ALTER TABLE [web].[Web_Application] CHECK CONSTRAINT [FK_Web_Application_Web_ApplicationType]
GO




CREATE TABLE [web].[Web_OperationCategory](
	[ApplicationId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Title] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
	[IsActive] [bit] NOT NULL,
	[DisplayOrder] [int] NULL,
 CONSTRAINT [PK_Web_OperationCategory] PRIMARY KEY CLUSTERED 
(
	[ApplicationId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [web].[Web_OperationCategory] ADD  CONSTRAINT [DF_Web_OperationCategory_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [web].[Web_OperationCategory]  WITH CHECK ADD  CONSTRAINT [FK_Web_OperationCategory_Web_Application] FOREIGN KEY([ApplicationId])
REFERENCES [web].[Web_Application] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [web].[Web_OperationCategory] CHECK CONSTRAINT [FK_Web_OperationCategory_Web_Application]
GO




CREATE TABLE [web].[Web_Operation](
	[OperationId] [int] NOT NULL,
	[Title] [nvarchar](150) NULL,
	[ApplicationId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Area] [nvarchar](150) NULL,
	[Controller] [nvarchar](150) NULL,
	[ActionName] [nvarchar](150) NULL,
	[OperationKey] [nvarchar](150) NULL,
	[HasUI] [bit] NOT NULL,
	[IsTechniker] [bit] NOT NULL,
	[HasParameter] [bit] NOT NULL,
 CONSTRAINT [PK_Web_Operation] PRIMARY KEY CLUSTERED 
(
	[OperationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [web].[Web_Operation] ADD  CONSTRAINT [DF_Web_Operation_HasUI]  DEFAULT ((1)) FOR [HasUI]
GO

ALTER TABLE [web].[Web_Operation] ADD  CONSTRAINT [DF_Web_Operation_IsTechniker]  DEFAULT ((0)) FOR [IsTechniker]
GO

ALTER TABLE [web].[Web_Operation] ADD  CONSTRAINT [DF_Web_Operation_HasParameter]  DEFAULT ((0)) FOR [HasParameter]
GO

ALTER TABLE [web].[Web_Operation]  WITH CHECK ADD  CONSTRAINT [FK_Web_Operation_Web_OperationCategory] FOREIGN KEY([ApplicationId], [CategoryId])
REFERENCES [web].[Web_OperationCategory] ([ApplicationId], [CategoryId])
ON UPDATE CASCADE
GO

ALTER TABLE [web].[Web_Operation] CHECK CONSTRAINT [FK_Web_Operation_Web_OperationCategory]
GO





CREATE TABLE [web].[Web_MenuItem](
	[MenuItemId] [int] NOT NULL,
	[Title] [nvarchar](150) NULL,
	[OperationId] [int] NULL,
	[ImageId] [uniqueidentifier] NULL,
	[ParentMenuItemId] [int] NULL,
	[IsLeaf] [bit] NULL,
	[OrderNo] [int] NULL,
	[MenuItemTypeId] [int] NULL,
	[Url] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Web_MenuItem] PRIMARY KEY CLUSTERED 
(
	[MenuItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [web].[Web_MenuItem] ADD  CONSTRAINT [DF_Web_MenuItem_MenuItemTypeId]  DEFAULT ((1)) FOR [MenuItemTypeId]
GO

ALTER TABLE [web].[Web_MenuItem] ADD  CONSTRAINT [DF_Web_MenuItem_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [web].[Web_MenuItem]  WITH CHECK ADD  CONSTRAINT [FK_Web_MenuItem_Web_MenuItem] FOREIGN KEY([ParentMenuItemId])
REFERENCES [web].[Web_MenuItem] ([MenuItemId])
GO

ALTER TABLE [web].[Web_MenuItem] CHECK CONSTRAINT [FK_Web_MenuItem_Web_MenuItem]
GO

ALTER TABLE [web].[Web_MenuItem]  WITH CHECK ADD  CONSTRAINT [FK_Web_MenuItem_Web_MenuItemType] FOREIGN KEY([MenuItemTypeId])
REFERENCES [web].[Web_MenuItemType] ([Id])
GO

ALTER TABLE [web].[Web_MenuItem] CHECK CONSTRAINT [FK_Web_MenuItem_Web_MenuItemType]
GO

ALTER TABLE [web].[Web_MenuItem]  WITH CHECK ADD  CONSTRAINT [FK_Web_MenuItem_Web_Operation] FOREIGN KEY([OperationId])
REFERENCES [web].[Web_Operation] ([OperationId])
ON UPDATE CASCADE
GO

ALTER TABLE [web].[Web_MenuItem] CHECK CONSTRAINT [FK_Web_MenuItem_Web_Operation]



GO





CREATE TABLE [web].[Web_WorkgroupDefinition](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[ParentId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[IsSystemWorkGroup] [bit] NOT NULL,
	[DefaultUrl] [nvarchar](250) NULL,
	[IsTel] [bit] NULL,
 CONSTRAINT [PK_Web_WorkgroupDefinition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [web].[Web_WorkgroupDefinition] ADD  CONSTRAINT [DF_Web_WorkgroupDefinition_IsSystemWorkGroup]  DEFAULT ((0)) FOR [IsSystemWorkGroup]
GO

ALTER TABLE [web].[Web_WorkgroupDefinition]  WITH CHECK ADD  CONSTRAINT [FK_Web_WorkgroupDefinition_Web_WorkgroupDefinition] FOREIGN KEY([ParentId])
REFERENCES [web].[Web_WorkgroupDefinition] ([Id])
GO

ALTER TABLE [web].[Web_WorkgroupDefinition] CHECK CONSTRAINT [FK_Web_WorkgroupDefinition_Web_WorkgroupDefinition]
GO




CREATE TABLE [web].[Web_WorkgroupOperation](
	[WorkGroupId] [int] NOT NULL,
	[OperationId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Web_WorkgroupOperation] PRIMARY KEY CLUSTERED 
(
	[WorkGroupId] ASC,
	[OperationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [web].[Web_WorkgroupOperation] ADD  CONSTRAINT [DF_Web_WorkgroupOperation_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [web].[Web_WorkgroupOperation]  WITH CHECK ADD  CONSTRAINT [FK_Web_WorkgroupOperation_Web_Operation] FOREIGN KEY([OperationId])
REFERENCES [web].[Web_Operation] ([OperationId])
ON UPDATE CASCADE
GO

ALTER TABLE [web].[Web_WorkgroupOperation] CHECK CONSTRAINT [FK_Web_WorkgroupOperation_Web_Operation]
GO

ALTER TABLE [web].[Web_WorkgroupOperation]  WITH CHECK ADD  CONSTRAINT [FK_Web_WorkgroupOperation_Web_WorkgroupDefinition] FOREIGN KEY([WorkGroupId])
REFERENCES [web].[Web_WorkgroupDefinition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [web].[Web_WorkgroupOperation] CHECK CONSTRAINT [FK_Web_WorkgroupOperation_Web_WorkgroupDefinition]
GO


CREATE TABLE [web].[Web_User](
	[Id] [uniqueidentifier] NOT NULL,
	[AccountName] [nvarchar](150) NOT NULL,
	[DomainName] [nvarchar](150) NULL,
	[DisplayName] [nvarchar](150) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateDate] [datetime] NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Email] [nvarchar](150) NULL,
	[Password] [nvarchar](150) NULL,
 CONSTRAINT [PK_Web_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Web_User_Uniq] UNIQUE NONCLUSTERED 
(
	[AccountName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [web].[Web_User] ADD  CONSTRAINT [DF_Web_User_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [web].[Web_User] ADD  CONSTRAINT [DF_Web_User_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

