USE [CSTest]
GO

/****** Object:  StoredProcedure [dbo].[CATEGORIZE_TRADES]    Script Date: 12/05/2022 18:43:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Thiago Maia
-- Create date: 2022-05-12
-- Description:	Procedural Version for CSTest
-- =============================================
CREATE OR ALTER   PROCEDURE [dbo].[CATEGORIZE_TRADES] 	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Create Sectors Table
	CREATE TABLE [dbo].[Sector](
	[id_sector] [int] IDENTITY(1,1) NOT NULL,
	[sec_description] [varchar](50) NOT NULL,
	CONSTRAINT [PK_Sector] PRIMARY KEY CLUSTERED 
	(
		[id_sector] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY];

	--Create Values Comparison Rule Table
	CREATE TABLE [dbo].[ComparisonRule](
	[id_comparison_rule] [int] IDENTITY(1,1) NOT NULL,
	[cr_description] [varchar](50) NOT NULL,
	CONSTRAINT [PK_ComparisonRule] PRIMARY KEY CLUSTERED 
	(
	[id_comparison_rule] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY];

	--Create Category Table
	CREATE TABLE [dbo].[Category](
	[id_category] [int] IDENTITY(1,1) NOT NULL,
	[parameter_value] [float] NOT NULL,
	[id_sector] [int] NOT NULL,
	[id_comparison_rule] [int] NOT NULL,
	[cat_description] [varchar](50) NOT NULL,
	CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
	(
		[id_category] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	--Create Trade Table
	CREATE TABLE [dbo].[Trade](
	[id_trade] [int] IDENTITY(1,1) NOT NULL,
	[tr_value] [float] NOT NULL,
	[id_sector] [int] NOT NULL,
	[id_category] [int] NULL,
	CONSTRAINT [PK_Trade] PRIMARY KEY CLUSTERED 
	(
		[id_trade] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

   
	--Initialize Sector Table
	INSERT INTO [dbo].[Sector] ([sec_description]) VALUES ('Public');
	INSERT INTO [dbo].[Sector] ([sec_description]) VALUES ('Private');
	
	--Initialize Values Comparison Rule Table
	INSERT INTO [dbo].[ComparisonRule] ([cr_description]) VALUES ('GreaterThan');
	INSERT INTO [dbo].[ComparisonRule] ([cr_description]) VALUES ('LessThan');
	INSERT INTO [dbo].[ComparisonRule] ([cr_description]) VALUES ('Equals');

	--Initialize Category Table
	INSERT INTO [dbo].[Category] ([parameter_value],[id_sector],[id_comparison_rule],[cat_description]) VALUES (1000000,1,2,'LOWRISK');
	INSERT INTO [dbo].[Category] ([parameter_value],[id_sector],[id_comparison_rule],[cat_description]) VALUES (1000000,1,1,'MEDIUMRISK');
	INSERT INTO [dbo].[Category] ([parameter_value],[id_sector],[id_comparison_rule],[cat_description]) VALUES (1000000,2,1,'HIGHRISK');

	--Initialize Trade Table
	INSERT INTO [dbo].[Trade] ([tr_value],[id_sector]) VALUES (2000000,2);
	INSERT INTO [dbo].[Trade] ([tr_value],[id_sector]) VALUES (400000,1);
	INSERT INTO [dbo].[Trade] ([tr_value],[id_sector]) VALUES (500000,1);
	INSERT INTO [dbo].[Trade] ([tr_value],[id_sector]) VALUES (3000000,1);
				
	--Categorize Trades
	UPDATE t
	SET t.id_category = c.id_category
	FROM dbo.Trade t
	INNER JOIN dbo.Category c on t.id_sector = c.id_sector 
	INNER JOIN dbo.ComparisonRule r on c.id_comparison_rule = r.id_comparison_rule
	WHERE r.cr_description = 
	CASE 
		WHEN r.cr_description = 'GreaterThan' AND t.tr_value > c.parameter_value THEN 'GreaterThan'
		WHEN r.cr_description = 'LessThan' AND t.tr_value < c.parameter_value THEN 'LessThan'
		ELSE 'Equals' 
	END;

	--Retrieve Categories from Trades
	SELECT c.cat_description
	FROM dbo.Category c
	INNER JOIN dbo.Trade t on c.id_category = t.id_category;
 
	
END
GO


