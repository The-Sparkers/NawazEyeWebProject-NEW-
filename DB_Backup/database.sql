
/****** Object:  Table [dbo].[ACCOUNT_USE_PROMOS]    Script Date: 7/25/2018 12:43:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ACCOUNT_USE_PROMOS](
	[PromoId] [int] NOT NULL,
	[BuyerId] [int] NOT NULL,
 CONSTRAINT [PK_ACCOUNT_USE_PROMOS_1] PRIMARY KEY CLUSTERED 
(
	[PromoId] ASC,
	[BuyerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[IsBlocked] [bit] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[BuyerHasAccount](
	[BuyerId] [int] NOT NULL,
	[AccountId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_BuyerHasAccount] PRIMARY KEY CLUSTERED 
(
	[BuyerId] ASC,
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[BUYERS](
	[BuyerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[PhoneNumber] [nchar](15) NOT NULL,
	[Address] [varchar](max) NOT NULL,
	[Email] [varchar](max) NOT NULL,
	[CityId] [int] NOT NULL,
 CONSTRAINT [PK_BUYERS] PRIMARY KEY CLUSTERED 
(
	[BuyerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[CART_HAS_PRESCRPTION_GLASSES](
	[CartId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Prescription] [varchar](max) NOT NULL,
	[LensName] [varchar](50) NULL,
 CONSTRAINT [PK_CART_HAS_PRESCRPTION_GLASSES_1] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CART_HAS_SUNGLASSES](
	[CartId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_CART_HAS_SUNGLASSES] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CARTS](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[StatusFlag] [bit] NOT NULL,
	[BuyerId] [int] NOT NULL,
 CONSTRAINT [PK_CARTS] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CITIES](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](20) NOT NULL,
	[DeliveryCharges] [money] NOT NULL,
 CONSTRAINT [PK_CITIES] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FRAMES](
	[FrameId] [int] IDENTITY(1,1) NOT NULL,
	[FrameName] [nchar](20) NOT NULL,
 CONSTRAINT [PK_FRAMES] PRIMARY KEY CLUSTERED 
(
	[FrameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LENS](
	[LensId] [int] IDENTITY(1,1) NOT NULL,
	[LensName] [nchar](20) NOT NULL,
 CONSTRAINT [PK_LENS] PRIMARY KEY CLUSTERED 
(
	[LensId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO](
	[OrderId] [int] NOT NULL,
	[CartId] [int] NOT NULL,
	[PromoId] [int] NOT NULL,
 CONSTRAINT [PK_ORDER_HAS_CART_WITH_PROMO_1] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[ORDERS](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nchar](15) NOT NULL,
	[OrderDate] [date] NOT NULL,
	[DispatchDate] [date] NOT NULL,
 CONSTRAINT [PK_ORDERS] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PRESCRIPTION_GLASSES](
	[ProductId] [int] NOT NULL,
	[LensId] [int] NOT NULL,
	[FrameId] [int] NOT NULL,
 CONSTRAINT [PK_PRESCRIPTION_GLASSES] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PRODUCT_IMAGES](
	[ProductId] [int] NOT NULL,
	[Image] [varchar](50) NOT NULL,
	[PrimaryFlag] [bit] NOT NULL,
 CONSTRAINT [PK_PRODUCT_IMAGES] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[Image] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PRODUCTS](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Discount] [int] NOT NULL,
	[FrameColor] [nchar](10) NOT NULL,
	[ProductDescription] [varchar](max) NOT NULL,
	[StopOrder] [bit] NOT NULL,
	[ItemsSold] [int] NOT NULL,
 CONSTRAINT [PK_PRODUCTS] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[PROMO_CODES](
	[PromoId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](20) NOT NULL,
	[Discount] [int] NOT NULL,
 CONSTRAINT [PK_PROMO_CODES] PRIMARY KEY CLUSTERED 
(
	[PromoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SUNGLASSES](
	[ProductId] [int] NOT NULL,
	[LensColor] [nchar](10) NOT NULL,
 CONSTRAINT [PK_SUNGLASSES] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SUNGLASSES]  WITH CHECK ADD  CONSTRAINT [SUNGLASSES_ARE_PRODUCTS] FOREIGN KEY([ProductId])
REFERENCES [dbo].[PRODUCTS] ([ProductId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SUNGLASSES] CHECK CONSTRAINT [SUNGLASSES_ARE_PRODUCTS]
GO

ALTER TABLE [dbo].[PRODUCTS] ADD  CONSTRAINT [DF_PRODUCTS_ItemsSold]  DEFAULT ((0)) FOR [ItemsSold]
GO


ALTER TABLE [dbo].[PRODUCT_IMAGES]  WITH CHECK ADD  CONSTRAINT [PRODUCT_HAS_IMAGES] FOREIGN KEY([ProductId])
REFERENCES [dbo].[PRODUCTS] ([ProductId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PRODUCT_IMAGES] CHECK CONSTRAINT [PRODUCT_HAS_IMAGES]
GO

ALTER TABLE [dbo].[PRESCRIPTION_GLASSES]  WITH CHECK ADD  CONSTRAINT [PRESCRIPTION_GLASSES_ARE_PRODUCTS] FOREIGN KEY([ProductId])
REFERENCES [dbo].[PRODUCTS] ([ProductId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PRESCRIPTION_GLASSES] CHECK CONSTRAINT [PRESCRIPTION_GLASSES_ARE_PRODUCTS]
GO

ALTER TABLE [dbo].[PRESCRIPTION_GLASSES]  WITH CHECK ADD  CONSTRAINT [PRESCRIPTION_GLASSES_HAVE_LENS] FOREIGN KEY([LensId])
REFERENCES [dbo].[LENS] ([LensId])
GO

ALTER TABLE [dbo].[PRESCRIPTION_GLASSES] CHECK CONSTRAINT [PRESCRIPTION_GLASSES_HAVE_LENS]
GO

ALTER TABLE [dbo].[PRESCRIPTION_GLASSES]  WITH CHECK ADD  CONSTRAINT [PRESCRPTION_GLASSES_HAVE_FRAME] FOREIGN KEY([FrameId])
REFERENCES [dbo].[FRAMES] ([FrameId])
GO

ALTER TABLE [dbo].[PRESCRIPTION_GLASSES] CHECK CONSTRAINT [PRESCRPTION_GLASSES_HAVE_FRAME]
GO

ALTER TABLE [dbo].[ORDERS] ADD  CONSTRAINT [DF_ORDERS_Status]  DEFAULT (N'PENDING') FOR [Status]
GO

ALTER TABLE [dbo].[ORDERS] ADD  CONSTRAINT [DF_ORDERS_DispatchDate]  DEFAULT ('9999-01-01') FOR [DispatchDate]
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO] ADD  CONSTRAINT [DF_ORDER_HAS_CART_WITH_PROMO_PromoId]  DEFAULT ((6)) FOR [PromoId]
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_CARTS] FOREIGN KEY([CartId])
REFERENCES [dbo].[CARTS] ([CartId])
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO] CHECK CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_CARTS]
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_ORDERS] FOREIGN KEY([OrderId])
REFERENCES [dbo].[ORDERS] ([OrderId])
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO] CHECK CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_ORDERS]
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_PROMO_CODES] FOREIGN KEY([PromoId])
REFERENCES [dbo].[PROMO_CODES] ([PromoId])
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO] CHECK CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_PROMO_CODES]
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO] ADD  CONSTRAINT [DF_ORDER_HAS_CART_WITH_PROMO_PromoId]  DEFAULT ((6)) FOR [PromoId]
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_CARTS] FOREIGN KEY([CartId])
REFERENCES [dbo].[CARTS] ([CartId])
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO] CHECK CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_CARTS]
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_ORDERS] FOREIGN KEY([OrderId])
REFERENCES [dbo].[ORDERS] ([OrderId])
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO] CHECK CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_ORDERS]
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_PROMO_CODES] FOREIGN KEY([PromoId])
REFERENCES [dbo].[PROMO_CODES] ([PromoId])
GO

ALTER TABLE [dbo].[ORDER_HAS_CART_WITH_PROMO] CHECK CONSTRAINT [FK_ORDER_HAS_CART_WITH_PROMO_PROMO_CODES]
GO


ALTER TABLE [dbo].[CARTS] ADD  CONSTRAINT [DF_CARTS_StatusFlag]  DEFAULT ((1)) FOR [StatusFlag]
GO

ALTER TABLE [dbo].[CARTS]  WITH CHECK ADD  CONSTRAINT [BUYER_CREATES_CART] FOREIGN KEY([BuyerId])
REFERENCES [dbo].[BUYERS] ([BuyerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CARTS] CHECK CONSTRAINT [BUYER_CREATES_CART]
GO


ALTER TABLE [dbo].[CART_HAS_SUNGLASSES]  WITH CHECK ADD  CONSTRAINT [FK_CART_HAS_SUNGLASSES_CARTS] FOREIGN KEY([CartId])
REFERENCES [dbo].[CARTS] ([CartId])
GO

ALTER TABLE [dbo].[CART_HAS_SUNGLASSES] CHECK CONSTRAINT [FK_CART_HAS_SUNGLASSES_CARTS]
GO

ALTER TABLE [dbo].[CART_HAS_SUNGLASSES]  WITH CHECK ADD  CONSTRAINT [FK_CART_HAS_SUNGLASSES_SUNGLASSES] FOREIGN KEY([ProductId])
REFERENCES [dbo].[SUNGLASSES] ([ProductId])
GO

ALTER TABLE [dbo].[CART_HAS_SUNGLASSES] CHECK CONSTRAINT [FK_CART_HAS_SUNGLASSES_SUNGLASSES]
GO

ALTER TABLE [dbo].[CART_HAS_PRESCRPTION_GLASSES]  WITH CHECK ADD  CONSTRAINT [FK_CART_HAS_PRESCRPTION_GLASSES_CARTS] FOREIGN KEY([CartId])
REFERENCES [dbo].[CARTS] ([CartId])
GO

ALTER TABLE [dbo].[CART_HAS_PRESCRPTION_GLASSES] CHECK CONSTRAINT [FK_CART_HAS_PRESCRPTION_GLASSES_CARTS]
GO

ALTER TABLE [dbo].[CART_HAS_PRESCRPTION_GLASSES]  WITH CHECK ADD  CONSTRAINT [FK_CART_HAS_PRESCRPTION_GLASSES_PRESCRIPTION_GLASSES] FOREIGN KEY([ProductId])
REFERENCES [dbo].[PRESCRIPTION_GLASSES] ([ProductId])
GO

ALTER TABLE [dbo].[CART_HAS_PRESCRPTION_GLASSES] CHECK CONSTRAINT [FK_CART_HAS_PRESCRPTION_GLASSES_PRESCRIPTION_GLASSES]
GO


ALTER TABLE [dbo].[BuyerHasAccount]  WITH CHECK ADD  CONSTRAINT [FK_BuyerHasAccount_AspNetUsers] FOREIGN KEY([AccountId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BuyerHasAccount] CHECK CONSTRAINT [FK_BuyerHasAccount_AspNetUsers]
GO

ALTER TABLE [dbo].[BuyerHasAccount]  WITH CHECK ADD  CONSTRAINT [FK_BuyerHasAccount_BUYERS] FOREIGN KEY([BuyerId])
REFERENCES [dbo].[BUYERS] ([BuyerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BuyerHasAccount] CHECK CONSTRAINT [FK_BuyerHasAccount_BUYERS]
GO

ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF_AspNetUsers_IsBlocked]  DEFAULT ((0)) FOR [IsBlocked]
GO


ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[ACCOUNT_USE_PROMOS]  WITH CHECK ADD  CONSTRAINT [FK_ACCOUNT_USE_PROMOS_PROMO_CODES] FOREIGN KEY([PromoId])
REFERENCES [dbo].[PROMO_CODES] ([PromoId])
GO

ALTER TABLE [dbo].[ACCOUNT_USE_PROMOS] CHECK CONSTRAINT [FK_ACCOUNT_USE_PROMOS_PROMO_CODES]
GO

ALTER TABLE [dbo].[BUYERS]  WITH CHECK ADD  CONSTRAINT [BUYER_BELONGS-TO_CITY] FOREIGN KEY([CityId])
REFERENCES [dbo].[CITIES] ([CityId])
GO
/*Inserting a default Promo Code*/
INSERT INTO [dbo].[PROMO_CODES]
           ([Code]
           ,[Discount])
     VALUES
           ('default',
           0)
GO
/*Set Default Promo Code*/

ALTER TABLE ORDER_HAS_CART_WITH_PROMO ADD CONSTRAINT Promo_default DEFAULT 1 FOR PromoId;
GO
