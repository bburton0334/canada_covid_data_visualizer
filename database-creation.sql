CREATE DATABASE [CovidData];
GO

CREATE TABLE [dbo].[Cases](
	[pruid] [int] NULL,
	[prname] [nvarchar](50) NULL,
	[prnameFR] [nvarchar](50) NULL,
	[date] [date] NULL,
	[reporting_week] [int] NULL,
	[reporting_year] [int] NULL,
	[update] [float] NULL,
	[totalcases] [float] NULL,
	[numtotal_last7] [float] NULL,
	[ratecases_total] [float] NULL,
	[numdeaths] [float] NULL,
	[numdeaths_last7] [float] NULL,
	[ratedeaths] [float] NULL,
	[ratecases_last7] [float] NULL,
	[ratedeaths_last7] [float] NULL,
	[numtotal_last14] [float] NULL,
	[numdeaths_last14] [float] NULL,
	[ratetotal_last14] [float] NULL,
	[ratedeaths_last14] [float] NULL,
	[avgcases_last7] [float] NULL,
	[avgincidence_last7] [float] NULL,
	[avgdeaths_last7] [float] NULL,
	[avgratedeaths_last7] [float] NULL
);
GO

CREATE TABLE [dbo].[Testings](
	[date] [date] NULL,
	[prname] [nvarchar](50) NULL,
	[pruid] [int] NULL,
	[numtests_total] [float] NULL,
	[numtests_weekly] [float] NULL,
	[avgnumtests_last7] [float] NULL,
	[avgratetests_last7] [float] NULL,
	[avgpositivity_last7] [float] NULL,
	[ratetests_total] [float] NULL,
	[update] [float] NULL
);
GO