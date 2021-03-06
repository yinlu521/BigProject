USE [master]
GO
/****** Object:  Database [iNethinkCMS]    Script Date: 2016/8/1 21:20:11 ******/
CREATE DATABASE [iNethinkCMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'iNethinkCMS', FILENAME = N'E:\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\iNethinkCMS.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'iNethinkCMS_log', FILENAME = N'E:\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\iNethinkCMS_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [iNethinkCMS] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [iNethinkCMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [iNethinkCMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [iNethinkCMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [iNethinkCMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [iNethinkCMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [iNethinkCMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [iNethinkCMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [iNethinkCMS] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [iNethinkCMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [iNethinkCMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [iNethinkCMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [iNethinkCMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [iNethinkCMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [iNethinkCMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [iNethinkCMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [iNethinkCMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [iNethinkCMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [iNethinkCMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [iNethinkCMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [iNethinkCMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [iNethinkCMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [iNethinkCMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [iNethinkCMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [iNethinkCMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [iNethinkCMS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [iNethinkCMS] SET  MULTI_USER 
GO
ALTER DATABASE [iNethinkCMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [iNethinkCMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [iNethinkCMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [iNethinkCMS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [iNethinkCMS]
GO
/****** Object:  Table [dbo].[iNethinkCMS_AdGroup]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_AdGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_AdList]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_AdList](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[IndexPic] [nvarchar](50) NULL,
	[LinkUrl] [nvarchar](100) NULL,
	[Desc] [nvarchar](50) NULL,
	[orderNum] [int] NULL,
	[addtime] [datetime] NULL,
	[ParentId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_Channel]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_Channel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Mold] [smallint] NULL,
	[Cid] [int] NULL,
	[FatherID] [int] NULL,
	[ChildID] [ntext] NULL,
	[ChildIDs] [ntext] NULL,
	[DeepPath] [int] NULL,
	[Name] [nvarchar](500) NULL,
	[OutSideLink] [int] NULL,
	[Domain] [nvarchar](200) NULL,
	[Templatechannel] [nvarchar](200) NULL,
	[Templateclass] [nvarchar](200) NULL,
	[Templateview] [nvarchar](200) NULL,
	[Picture] [nvarchar](200) NULL,
	[Contents] [ntext] NULL,
	[Keywords] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[Display] [int] NULL,
	[OrderNum] [int] NULL,
	[Ename] [nvarchar](50) NULL,
 CONSTRAINT [PK_iNethinkCMS_Channel] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_Channel_CustomFields]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_Channel_CustomFields](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CIDList] [ntext] NULL,
	[CustomFieldsName] [nvarchar](100) NULL,
	[CustomFieldsKey] [nvarchar](100) NULL,
	[CustomFieldsType] [nvarchar](100) NULL,
	[CustomFieldsValue] [nvarchar](4000) NULL,
	[CustomFieldsRequired] [smallint] NULL,
	[Display] [smallint] NULL,
	[OrderNum] [int] NULL,
 CONSTRAINT [PK_iNethinkCMS_Channel_CustomFields] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_Content]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_Content](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Cid] [int] NULL,
	[Sid] [int] NULL,
	[Title] [nvarchar](500) NULL,
	[SubTitle] [nvarchar](250) NULL,
	[Title_Color] [nvarchar](20) NULL,
	[Title_Style] [nvarchar](30) NULL,
	[Author] [nvarchar](200) NULL,
	[Source] [nvarchar](200) NULL,
	[Jumpurl] [nvarchar](400) NULL,
	[Keywords] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[Indexpic] [nvarchar](500) NULL,
	[Views] [int] NULL,
	[Commend] [int] NULL,
	[IsComment] [int] NULL,
	[Display] [int] NULL,
	[Createtime] [datetime] NULL,
	[Modifytime] [datetime] NULL,
	[OrderNum] [int] NULL,
	[Contents] [ntext] NULL,
	[FieldsInfo] [ntext] NULL,
 CONSTRAINT [PK_iNethinkCMS_Content] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_Custom_Pages]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_Custom_Pages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](400) NULL,
	[TemplatePath] [nvarchar](400) NULL,
	[Dir] [nvarchar](400) NULL,
	[Keywords] [nvarchar](400) NULL,
	[Description] [nvarchar](500) NULL,
	[Html] [text] NULL,
 CONSTRAINT [PK_iNethinkCMS_Custom_Pages] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_Custom_Tags]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_Custom_Tags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Remark] [nvarchar](150) NULL,
	[Code] [text] NULL,
 CONSTRAINT [PK_iNethinkCMS_Custom_Tags] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_Dict]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_Dict](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DictType] [smallint] NULL,
	[DictName] [nvarchar](200) NULL,
	[Display] [smallint] NULL,
	[OrderNum] [int] NULL,
 CONSTRAINT [PK_iNethinkCMS_Dict] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_Extend_Blogroll]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_Extend_Blogroll](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BlogrollClass] [int] NULL,
	[BlogrollName] [nvarchar](100) NULL,
	[BlogrollImg] [nvarchar](500) NULL,
	[BlogrollUrl] [nvarchar](400) NULL,
	[Display] [smallint] NULL,
	[OrderNum] [int] NULL,
 CONSTRAINT [PK_iNethinkCMS_Extend_Blogroll] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_Plugs_Guestbook]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_Plugs_Guestbook](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GuestbookUserName] [nvarchar](20) NULL,
	[GuestbookUserIP] [nvarchar](15) NULL,
	[GuestbookCompany] [nvarchar](200) NULL,
	[GuestbookAddress] [nvarchar](200) NULL,
	[GuestbookTel] [nvarchar](200) NULL,
	[GuestbookEmail] [nvarchar](200) NULL,
	[GuestbookQQ] [nvarchar](200) NULL,
	[GuestbookContent] [ntext] NULL,
	[GuestbookTime] [datetime] NULL,
	[ReplyUserName] [nvarchar](20) NULL,
	[ReplyContent] [ntext] NULL,
	[ReplyTime] [datetime] NULL,
	[Display] [smallint] NULL,
	[OrderNum] [int] NULL,
 CONSTRAINT [PK_iNethinkCMS_Plugs_Guestbook] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_Special]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_Special](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SpecialName] [nvarchar](200) NULL,
	[SpecialTitle] [nvarchar](200) NULL,
	[SpecialKeyword] [nvarchar](200) NULL,
	[SpecialDescription] [nvarchar](500) NULL,
	[SpecialTemplate] [nvarchar](200) NULL,
	[SpecialUrl] [nvarchar](200) NULL,
	[SpecialPic] [nvarchar](200) NULL,
	[SpecialContent] [text] NULL,
	[Display] [smallint] NULL,
	[OrderNum] [int] NULL,
 CONSTRAINT [PK_iNethinkCMS_Special] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_Upload]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_Upload](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UpType] [smallint] NULL,
	[Aid] [int] NULL,
	[Cid] [int] NULL,
	[Dir] [nvarchar](500) NULL,
	[Ext] [nvarchar](50) NULL,
	[Time] [datetime] NULL,
 CONSTRAINT [PK_iNethinkCMS_Upload] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[iNethinkCMS_User]    Script Date: 2016/8/1 21:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iNethinkCMS_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [smallint] NULL,
	[UserName] [nvarchar](20) NULL,
	[UserPass] [nvarchar](32) NULL,
	[UserTrueName] [nvarchar](20) NULL,
	[UserEmail] [nvarchar](50) NULL,
	[UserPower] [nvarchar](500) NULL,
	[UserChannelPower] [nvarchar](4000) NULL,
	[UserRegTime] [datetime] NULL,
	[SecurityCode] [nvarchar](32) NULL,
 CONSTRAINT [PK_iNethinkCMS_User] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[iNethinkCMS_Channel] ADD  CONSTRAINT [DF_iNethinkCMS_Channel_Cid]  DEFAULT ((0)) FOR [Cid]
GO
ALTER TABLE [dbo].[iNethinkCMS_Channel] ADD  CONSTRAINT [DF_iNethinkCMS_Channel_FatherID]  DEFAULT ((0)) FOR [FatherID]
GO
ALTER TABLE [dbo].[iNethinkCMS_Channel] ADD  CONSTRAINT [DF_iNethinkCMS_Channel_DeepPath]  DEFAULT ((0)) FOR [DeepPath]
GO
ALTER TABLE [dbo].[iNethinkCMS_Channel] ADD  CONSTRAINT [DF_iNethinkCMS_Channel_OutSideLink]  DEFAULT ((0)) FOR [OutSideLink]
GO
ALTER TABLE [dbo].[iNethinkCMS_Channel] ADD  CONSTRAINT [DF_iNethinkCMS_Channel_Display]  DEFAULT ((0)) FOR [Display]
GO
ALTER TABLE [dbo].[iNethinkCMS_Content] ADD  CONSTRAINT [DF_iNethinkCMS_Content_Cid]  DEFAULT ((0)) FOR [Cid]
GO
ALTER TABLE [dbo].[iNethinkCMS_Content] ADD  CONSTRAINT [DF_iNethinkCMS_Content_Sid]  DEFAULT ((0)) FOR [Sid]
GO
ALTER TABLE [dbo].[iNethinkCMS_Content] ADD  CONSTRAINT [DF_iNethinkCMS_Content_Views]  DEFAULT ((0)) FOR [Views]
GO
ALTER TABLE [dbo].[iNethinkCMS_Content] ADD  CONSTRAINT [DF_iNethinkCMS_Content_IsComment]  DEFAULT ((0)) FOR [IsComment]
GO
ALTER TABLE [dbo].[iNethinkCMS_Content] ADD  CONSTRAINT [DF_iNethinkCMS_Content_Display]  DEFAULT ((0)) FOR [Display]
GO
ALTER TABLE [dbo].[iNethinkCMS_Content] ADD  CONSTRAINT [DF_iNethinkCMS_Content_Createtime]  DEFAULT (getdate()) FOR [Createtime]
GO
ALTER TABLE [dbo].[iNethinkCMS_Content] ADD  CONSTRAINT [DF_iNethinkCMS_Content_Modifytime]  DEFAULT (getdate()) FOR [Modifytime]
GO
ALTER TABLE [dbo].[iNethinkCMS_Upload] ADD  CONSTRAINT [DF_iNethinkCMS_Upload_Aid]  DEFAULT ((0)) FOR [Aid]
GO
ALTER TABLE [dbo].[iNethinkCMS_Upload] ADD  CONSTRAINT [DF_iNethinkCMS_Upload_Cid]  DEFAULT ((0)) FOR [Cid]
GO
ALTER TABLE [dbo].[iNethinkCMS_Upload] ADD  CONSTRAINT [DF_iNethinkCMS_Upload_Time]  DEFAULT (getdate()) FOR [Time]
GO
USE [master]
GO
ALTER DATABASE [iNethinkCMS] SET  READ_WRITE 
GO
