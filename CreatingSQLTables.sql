use WebScraping
--Go

--Drop Procedure sp_webScraping_RealEstate
--Go

Create or alter Procedure sp_webScraping_RealEstateCreateTable
	AS
	BEGIN
		--Check if Schema exists
		IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'config')
		BEGIN
			EXEC('CREATE SCHEMA Config')
		END
		
		--Check if Table exists
		IF NOT EXISTS (SELECT *
						FROM SYS.tables WHERE name = 'WebPushJeffersonUpcomingSale')
		BEGIN
			EXEC('CREATE TABLE config.WebPushJeffersonUpcomingSale(
					[CASE_NUMBER] NVARCHAR(250) NOT NULL PRIMARY KEY,
					[DIVISION] INT,
					[COURT NAME] VARCHAR(500),
					[ATTORNEY] VARCHAR(250),
					[DATE] VARCHAR(250),
					[DOCKET_NUMBER] VARCHAR(250),
					[ADDRESS] VARCHAR(500),
					[COUNT] NVARCHAR(250),
					[STATUS] VARCHAR(250),
					[PARSEDATA] varchar(500)
				  )')
		END
	END
Go

--EXEC sp_webScraping_RealEstateCreateTable
--Go

--drop table config.WebPushJeffersonUpcomingSale
--go

--select * from config.WebPushJeffersonUpcomingSale
--order by Docket_number
--go


--EXEC('DELETE FROM Config.WebPushJeffersonUpcomingSale')
--Go