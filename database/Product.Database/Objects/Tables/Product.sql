CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[StockQuantity] [int] NOT NULL,
	[Manufacturer] [nvarchar](100) NULL,
	[Category] [nvarchar](50) NULL,
	[ReleaseDate] [date] NULL,
	[IsAvailable] [bit] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK__Product__3214EC07CBB2A165] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__IsAvail__24927208]  DEFAULT ((1)) FOR [IsAvailable]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__Created__25869641]  DEFAULT (getdate()) FOR [CreatedAt]
GO

