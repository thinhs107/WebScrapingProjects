Business Requirement: Application is design to automate download an csv file from jeffcomm.org and format it base on customer request.
Application was built on .Net Framework 4.8, also connecting to Microsoft SQL Server as the backend

Version 1.0
	* Process is able to automate download csv file.
	* Process is able to substring and parse out information that is request based on the customer needs
	* Process is able to bulk insert into Microsoft SQL Server.
		* There are Store Procedures and a  SQL that is being executed during the bulk insert. The following is high detail of these functions
			1) sp_webScraping_RealEstateCreateTable - Check whether the schema and table exists yet, if not created it
			2) EXEC('DELETE FROM Config.WebPushJeffersonUpcomingSale') - to delete any existing data,  eliminate duplicates values
	* Process is able to format and upload into excel

Version 1.1 (Upcoming path)
	* Need to fix and make SQL Server Connection more dynamically
	* Upload the excel file into Google Sheet.
