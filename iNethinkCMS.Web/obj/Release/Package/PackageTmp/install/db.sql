 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_Channel]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_Channel] 
 CREATE TABLE [iNethinkCMS_Channel] (  [ID] [int] IDENTITY (1, 1) NOT NULL , [Mold] [smallint] NULL ,  [Cid] [int] NULL ,  [FatherID] [int] NULL , [ChildID] [ntext] NULL , [ChildIDs] [ntext] NULL ,  [DeepPath] [int] NULL , [Name] [nvarchar] (500) NULL ,  [OutSideLink] [int] NULL , [Domain] [nvarchar] (200) NULL , [Templatechannel] [nvarchar] (200) NULL , [Templateclass] [nvarchar] (200) NULL , [Templateview] [nvarchar] (200) NULL , [Picture] [nvarchar] (200) NULL , [Contents] [ntext] NULL , [Keywords] [nvarchar] (200) NULL , [Description] [nvarchar] (500) NULL ,  [Display] [int] NULL ,  [OrderNum] [int] NULL )
 ALTER TABLE [iNethinkCMS_Channel] WITH NOCHECK ADD CONSTRAINT [DF_iNethinkCMS_Channel_Cid] DEFAULT ((0)) FOR [Cid],CONSTRAINT [DF_iNethinkCMS_Channel_FatherID] DEFAULT ((0)) FOR [FatherID],CONSTRAINT [DF_iNethinkCMS_Channel_DeepPath] DEFAULT ((0)) FOR [DeepPath],CONSTRAINT [DF_iNethinkCMS_Channel_OutSideLink] DEFAULT ((0)) FOR [OutSideLink],CONSTRAINT [DF_iNethinkCMS_Channel_Display] DEFAULT ((0)) FOR [Display],CONSTRAINT [PK_iNethinkCMS_Channel] PRIMARY KEY  NONCLUSTERED ( [ID] )

 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_Channel_CustomFields]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_Channel_CustomFields] 
 CREATE TABLE [iNethinkCMS_Channel_CustomFields] (  [ID] [int] IDENTITY (1, 1) NOT NULL , [CIDList] [ntext] NULL , [CustomFieldsName] [nvarchar] (100) NULL , [CustomFieldsKey] [nvarchar] (100) NULL , [CustomFieldsType] [nvarchar] (100) NULL , [CustomFieldsValue] [nvarchar] (4000) NULL , [CustomFieldsRequired] [smallint] NULL , [Display] [smallint] NULL ,  [OrderNum] [int] NULL )
 ALTER TABLE [iNethinkCMS_Channel_CustomFields] WITH NOCHECK ADD CONSTRAINT [PK_iNethinkCMS_Channel_CustomFields] PRIMARY KEY  NONCLUSTERED ( [ID] )

 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_Content]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_Content] 
 CREATE TABLE [iNethinkCMS_Content] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Cid] [int] NULL ,  [Sid] [int] NULL , [Title] [nvarchar] (500) NULL , [SubTitle] [nvarchar] (250) NULL , [Title_Color] [nvarchar] (20) NULL , [Title_Style] [nvarchar] (30) NULL , [Author] [nvarchar] (200) NULL , [Source] [nvarchar] (200) NULL , [Jumpurl] [nvarchar] (400) NULL , [Keywords] [nvarchar] (200) NULL , [Description] [nvarchar] (500) NULL , [Indexpic] [nvarchar] (500) NULL ,  [Views] [int] NULL ,  [Commend] [int] NULL ,  [IsComment] [int] NULL ,  [Display] [int] NULL , [Createtime] [datetime]  NULL , [Modifytime] [datetime]  NULL ,  [OrderNum] [int] NULL , [Contents] [ntext] NULL , [FieldsInfo] [ntext] NULL )
 ALTER TABLE [iNethinkCMS_Content] WITH NOCHECK ADD CONSTRAINT [DF_iNethinkCMS_Content_Cid] DEFAULT ((0)) FOR [Cid],CONSTRAINT [DF_iNethinkCMS_Content_Sid] DEFAULT ((0)) FOR [Sid],CONSTRAINT [DF_iNethinkCMS_Content_Views] DEFAULT ((0)) FOR [Views],CONSTRAINT [DF_iNethinkCMS_Content_IsComment] DEFAULT ((0)) FOR [IsComment],CONSTRAINT [DF_iNethinkCMS_Content_Display] DEFAULT ((0)) FOR [Display],CONSTRAINT [DF_iNethinkCMS_Content_Createtime] DEFAULT (getdate()) FOR [Createtime],CONSTRAINT [DF_iNethinkCMS_Content_Modifytime] DEFAULT (getdate()) FOR [Modifytime],CONSTRAINT [PK_iNethinkCMS_Content] PRIMARY KEY  NONCLUSTERED ( [ID] )

 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_Custom_Pages]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_Custom_Pages] 
 CREATE TABLE [iNethinkCMS_Custom_Pages] (  [ID] [int] IDENTITY (1, 1) NOT NULL , [Title] [nvarchar] (400) NULL , [TemplatePath] [nvarchar] (400) NULL , [Dir] [nvarchar] (400) NULL , [Keywords] [nvarchar] (400) NULL , [Description] [nvarchar] (500) NULL ,  [Html] [text] NULL )
 ALTER TABLE [iNethinkCMS_Custom_Pages] WITH NOCHECK ADD CONSTRAINT [PK_iNethinkCMS_Custom_Pages] PRIMARY KEY  NONCLUSTERED ( [ID] )

 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_Custom_Tags]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_Custom_Tags] 
 CREATE TABLE [iNethinkCMS_Custom_Tags] (  [ID] [int] IDENTITY (1, 1) NOT NULL , [Name] [nvarchar] (150) NULL , [Remark] [nvarchar] (150) NULL ,  [Code] [text] NULL )
 ALTER TABLE [iNethinkCMS_Custom_Tags] WITH NOCHECK ADD CONSTRAINT [PK_iNethinkCMS_Custom_Tags] PRIMARY KEY  NONCLUSTERED ( [ID] )

 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_Dict]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_Dict] 
 CREATE TABLE [iNethinkCMS_Dict] (  [ID] [int] IDENTITY (1, 1) NOT NULL , [DictType] [smallint] NULL , [DictName] [nvarchar] (200) NULL , [Display] [smallint] NULL ,  [OrderNum] [int] NULL )
 ALTER TABLE [iNethinkCMS_Dict] WITH NOCHECK ADD CONSTRAINT [PK_iNethinkCMS_Dict] PRIMARY KEY  NONCLUSTERED ( [ID] )

 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_Extend_Blogroll]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_Extend_Blogroll] 
 CREATE TABLE [iNethinkCMS_Extend_Blogroll] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [BlogrollClass] [int] NULL , [BlogrollName] [nvarchar] (100) NULL , [BlogrollImg] [nvarchar] (500) NULL , [BlogrollUrl] [nvarchar] (400) NULL , [Display] [smallint] NULL ,  [OrderNum] [int] NULL )
 ALTER TABLE [iNethinkCMS_Extend_Blogroll] WITH NOCHECK ADD CONSTRAINT [PK_iNethinkCMS_Extend_Blogroll] PRIMARY KEY  NONCLUSTERED ( [ID] )

 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_Plugs_Guestbook]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_Plugs_Guestbook] 
 CREATE TABLE [iNethinkCMS_Plugs_Guestbook] (  [ID] [int] IDENTITY (1, 1) NOT NULL , [GuestbookUserName] [nvarchar] (20) NULL , [GuestbookUserIP] [nvarchar] (15) NULL , [GuestbookCompany] [nvarchar] (200) NULL , [GuestbookAddress] [nvarchar] (200) NULL , [GuestbookTel] [nvarchar] (200) NULL , [GuestbookEmail] [nvarchar] (200) NULL , [GuestbookQQ] [nvarchar] (200) NULL , [GuestbookContent] [ntext] NULL , [GuestbookTime] [datetime]  NULL , [ReplyUserName] [nvarchar] (20) NULL , [ReplyContent] [ntext] NULL , [ReplyTime] [datetime]  NULL , [Display] [smallint] NULL ,  [OrderNum] [int] NULL )
 ALTER TABLE [iNethinkCMS_Plugs_Guestbook] WITH NOCHECK ADD CONSTRAINT [PK_iNethinkCMS_Plugs_Guestbook] PRIMARY KEY  NONCLUSTERED ( [ID] )

 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_Special]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_Special] 
 CREATE TABLE [iNethinkCMS_Special] (  [ID] [int] IDENTITY (1, 1) NOT NULL , [SpecialName] [nvarchar] (200) NULL , [SpecialTitle] [nvarchar] (200) NULL , [SpecialKeyword] [nvarchar] (200) NULL , [SpecialDescription] [nvarchar] (500) NULL , [SpecialTemplate] [nvarchar] (200) NULL , [SpecialUrl] [nvarchar] (200) NULL , [SpecialPic] [nvarchar] (200) NULL ,  [SpecialContent] [text] NULL , [Display] [smallint] NULL ,  [OrderNum] [int] NULL )
 ALTER TABLE [iNethinkCMS_Special] WITH NOCHECK ADD CONSTRAINT [PK_iNethinkCMS_Special] PRIMARY KEY  NONCLUSTERED ( [ID] )

 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_Upload]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_Upload] 
 CREATE TABLE [iNethinkCMS_Upload] (  [ID] [int] IDENTITY (1, 1) NOT NULL , [UpType] [smallint] NULL ,  [Aid] [int] NULL ,  [Cid] [int] NULL , [Dir] [nvarchar] (500) NULL , [Ext] [nvarchar] (50) NULL , [Time] [datetime]  NULL )
 ALTER TABLE [iNethinkCMS_Upload] WITH NOCHECK ADD CONSTRAINT [DF_iNethinkCMS_Upload_Aid] DEFAULT ((0)) FOR [Aid],CONSTRAINT [DF_iNethinkCMS_Upload_Cid] DEFAULT ((0)) FOR [Cid],CONSTRAINT [DF_iNethinkCMS_Upload_Time] DEFAULT (getdate()) FOR [Time],CONSTRAINT [PK_iNethinkCMS_Upload] PRIMARY KEY  NONCLUSTERED ( [ID] )

 if exists (select * from sysobjects where id = OBJECT_ID('[iNethinkCMS_User]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [iNethinkCMS_User] 
 CREATE TABLE [iNethinkCMS_User] (  [ID] [int] IDENTITY (1, 1) NOT NULL , [UserType] [smallint] NULL , [UserName] [nvarchar] (20) NULL , [UserPass] [nvarchar] (32) NULL , [UserTrueName] [nvarchar] (20) NULL , [UserEmail] [nvarchar] (50) NULL , [UserPower] [nvarchar] (500) NULL , [UserChannelPower] [nvarchar] (4000) NULL , [UserRegTime] [datetime]  NULL , [SecurityCode] [nvarchar] (32) NULL )
 ALTER TABLE [iNethinkCMS_User] WITH NOCHECK ADD CONSTRAINT [PK_iNethinkCMS_User] PRIMARY KEY  NONCLUSTERED ( [ID] )

 INSERT [iNethinkCMS_User] ( [UserType] , [UserName] , [UserPass] , [UserTrueName] , [UserEmail] , [UserPower] , [UserChannelPower] , [UserRegTime] , [SecurityCode] ) VALUES ( 1 , 'admin' , '96e79218965eb72c92a549dd5a330112' , 'iNethinkCMS' , '69991000@qq.com' , 'a,b,c,d,e' , '0' , '###UserRegTime###' , '' )

