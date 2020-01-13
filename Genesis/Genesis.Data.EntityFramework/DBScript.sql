/****** Object:  Table [dbo].[Companies]    Script Date: 13-01-20 16:40:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[VatNumber] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyAddresses]    Script Date: 13-01-20 16:40:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyAddresses](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[IsEstablishement] [bit] NOT NULL,
	[AddressLine1] [nvarchar](255) NOT NULL,
	[AddressLine2] [nvarchar](255) NULL,
	[PostCode] [nvarchar](15) NOT NULL,
	[City] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_CompanyAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyContacts]    Script Date: 13-01-20 16:40:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyContacts](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[ContactId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 13-01-20 16:40:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Type] [int] NOT NULL,
	[VatNumber] [nvarchar](50) NULL,
	[AddressLine1] [nvarchar](255) NOT NULL,
	[AddressLine2] [nvarchar](255) NULL,
	[PostCode] [nvarchar](15) NOT NULL,
	[City] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Companies] ADD  CONSTRAINT [DF_Companies_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Companies] ADD  CONSTRAINT [DF_Companies_VatNumber]  DEFAULT (N'') FOR [VatNumber]
GO
ALTER TABLE [dbo].[CompanyAddresses] ADD  CONSTRAINT [DF_CompanyAddresses_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[CompanyAddresses] ADD  CONSTRAINT [DF_CompanyAddresses_IsEstablishement]  DEFAULT ((0)) FOR [IsEstablishement]
GO
ALTER TABLE [dbo].[CompanyContacts] ADD  CONSTRAINT [DF_CompanyContacts_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Contacts] ADD  CONSTRAINT [DF_Contacts_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Contacts] ADD  CONSTRAINT [DF_Contacts_Type]  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[CompanyContacts]  WITH CHECK ADD  CONSTRAINT [FK_CompanyContacts_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompanyContacts] CHECK CONSTRAINT [FK_CompanyContacts_Companies]
GO
ALTER TABLE [dbo].[CompanyContacts]  WITH CHECK ADD  CONSTRAINT [FK_CompanyContacts_Contacts] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompanyContacts] CHECK CONSTRAINT [FK_CompanyContacts_Contacts]
GO
