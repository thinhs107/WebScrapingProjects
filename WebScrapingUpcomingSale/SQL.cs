using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace WebScrapingUpcomingSale
{
    class SQL
    {


       string ConnectionString = GetConnectionString();

        public void CallStoreProcedure(string StoreProcedure)
        {
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(StoreProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadLine();
                }

            }
        }

        public void ExecuteSQLStatements(string SQLStatement)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(SQLStatement, connection);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadLine();
                }

            }
        }

        public void InsertDataIntoSQLServer(DataTable cvsFileData)
        {
                using(SqlBulkCopy bulkCopy = new SqlBulkCopy(ConnectionString))
                {
                    bulkCopy.DestinationTableName = "config.WebPushJeffersonUpcomingSale";

                    try
                    {
                        bulkCopy.WriteToServer(cvsFileData);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }
        }

         private static string GetConnectionString()
        {
                 return "Server = DESKTOP-G2AP4L0; Database = WebScraping; Trusted_Connection=True";
        }

    }
}
