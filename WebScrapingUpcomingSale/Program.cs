using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Xml;
using System.Net.Http;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.IO;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace WebScrapingUpcomingSale
{

    class Program
    {
        public static IWebDriver driver;
        public static string fileName = "webPush.csv";
        public static string SourceFolder = @"C:\Users\" + Environment.UserName + @"\Downloads\" + fileName;
        public static string WorkingFolder = @"C:\Users\" + Environment.UserName + @"\Documents\WebScraping\JeffersonUpcomingSale\";

        static void Main(string[] args)
        {
            InitWebScrapingWithSelenium();
            //Wait for 1 minutes to continue
            Thread.Sleep(3600);
            driver.Close();
            driver.Quit();
            MoveFilesToWorkingFolder();
            ReadCSVtoDataTable(WorkingFolder + fileName);
        }



        private static void InitWebScrapingWithSelenium()
        {
            //Download csv that used to populate Jefferson Upcoming Sale site
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Navigate().GoToUrl("http://jeffcomm.org/docs/webPush.csv");
        }


        private static void MoveFilesToWorkingFolder()
        {

            
            try
            {
                //Create Directory. Overwrite it if already exists
                DirectoryInfo directoryInfo = Directory.CreateDirectory(WorkingFolder);

                if(File.Exists(WorkingFolder + fileName))
                {
                    File.Delete(WorkingFolder + fileName);
                }


                //Move file from Source Folder to Working Folder
                File.Move(SourceFolder, WorkingFolder + fileName);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private static void ReadCSVtoDataTable(string myWorkingFolder)
        {
            DataTable cvsData = new DataTable();
            try
            {
                using (TextFieldParser cvsReader = new TextFieldParser(myWorkingFolder))
                {
                    cvsReader.SetDelimiters(new string[] { "," });
                    cvsReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = cvsReader.ReadFields();
                    foreach(string column in colFields)
                    {
                        DataColumn dataColumn = new DataColumn(column);
                        dataColumn.AllowDBNull = true;
                        cvsData.Columns.Add(dataColumn);
                    }


                    while (!cvsReader.EndOfData)
                    {
                        string[] fieldData = cvsReader.ReadFields();
                        //Making empty field as null
                        for(int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        cvsData.Rows.Add(fieldData);
                    }
                    cvsData.Columns.Add("ParseData");
                    for (int i = 0; i < cvsData.Rows.Count; i++)
                    { 
                        
                        string FormatString = FormatText(cvsData.Rows[i]["expr5"].ToString());
                        cvsData.Rows[i]["ParseData"] = FormatString;
                    }
                    SQL PerformSQL = new SQL();
                    PerformSQL.CallStoreProcedure("sp_webScraping_RealEstateCreateTable");
                    PerformSQL.ExecuteSQLStatements("EXEC('DELETE FROM Config.WebPushJeffersonUpcomingSale')");
                    PerformSQL.InsertDataIntoSQLServer(cvsData);


                    //Export to Excel
                    ExportToCSV export = new ExportToCSV();
                    export.ExportDtToCsv(cvsData);

                    File.Delete(SourceFolder);
                }
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

        private static string FormatText(string parseValue)
        {
            const string Pattern = @"vs\s*([^\n\r]*)";
            return Regex.Replace(Regex.Match(parseValue, Pattern).Value,@"vs.","");
        }
    }
}
