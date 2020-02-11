using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Dapper;

namespace CsvHelperSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting process file...");

            try
            {
                var fileProcessor = new FileProcessor();

                var records = fileProcessor.LoadRecords();

                var total = records.Count;

                using (var progress = new ProgressBar())
                {
                    for (int i = 0; i <= total; i++)
                    {
                        progress.Report((double)i / total);
                        fileProcessor.Process(records);
                    }
                }

                Console.WriteLine("Process is finished");

            }
            catch (Exception err)
            {

                Console.WriteLine("Error", err);
            }
           

            Console.WriteLine("The file was processed successfully");
        }
    }

    public class FileProcessor
    {
        public List<Record> LoadRecords()
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string file = Path.Combine(executableLocation, ConfigurationManager.AppSettings["FileName"].ToString());

            try
            {
                using var reader = new StreamReader(file);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var records = csv.GetRecords<Record>();
                return records.AsList();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Process(List<Record> records)
        {
            foreach (var record in records)
            {
                if (!ExistsAdditionalField(record))
                {
                    AddNewField(record);               
                }
                else
                {
                    UpdateField(record);                   
                };

            }        
        }

        private bool ExistsAdditionalField(Record record)
        {
            string sql = "";

            try
            {
                var builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["TZTLeads"].ToString());

                using var connection = new SqlConnection(builder.ConnectionString);
                
                connection.Open();

                var valueFound = connection.ExecuteScalar(sql, new { leadId = record.TztLeadId });

                //What happen if the universal id found in the database is not null or empty?
                return valueFound == null ? false : true;
            }
            catch (Exception err)
            {
                throw err;
            }           
        }

        private string getPersonId(string leadId)
        {
            string sql = "";

            var builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["Database"].ToString());

            using var connection = new SqlConnection(builder.ConnectionString);

            connection.Open();

            var personIdFound = connection.ExecuteScalar(sql, new { leadId });

            return personIdFound.ToString().ToUpper();
        }

        private void AddNewField(Record record)
        {           
            try
            {
                var personIdFound = getPersonId(record.TztLeadId);

                var builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["Database"].ToString());

                using var connection = new SqlConnection(builder.ConnectionString);

                connection.Open();

                string sql = "INSERT INTO ";

                connection.Execute(sql, new { 
                    PersonId=personIdFound,
                    DisplayText="",
                    IsEncrypted=0,
                    Value=record.UniversalLeadId,
                    WasAnswered=1,
                    FieldKey="UniversalLeadID"
                });

                Log(record, "Done", "Record added successfully.");

            }
            catch (Exception err)
            {
                Log(record, "Error", "Record cannot be added. Details: " + err.Message);
                throw err;
            }
        }

        private void UpdateField(Record record)
        {
            try
            {
                var personIdFound = getPersonId(record.TztLeadId);

                var builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["Database"].ToString());

                using var connection = new SqlConnection(builder.ConnectionString);

                connection.Open();

                string sql = "UPDATE PersonFields SET";
          
                connection.Execute(sql, new
                {
                    PersonId = personIdFound,
                    Value = record.UniversalLeadId,
                });

                Log(record, "Done", "Record updated successfully.");

            }
            catch (Exception err)
            {
                Log(record, "Error", "Record cannot be updated. Details: " + err.Message);
                throw err;
            }
        } 

        private void Log(Record record, string status, string message)
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string file = Path.Combine(executableLocation, ConfigurationManager.AppSettings["LogFileName"].ToString());

            StreamWriter log;

            if (!File.Exists(file)) {
                log = new StreamWriter(file);
            }
            else {
                log = File.AppendText(file);
            }

            log.WriteLine(string.Format("Data Time: {0} - TZTLeadId: {1} Universal Lead Id: {2} Status: {3} Message: {4} ", DateTime.Now, record.TztLeadId, record.UniversalLeadId, status, message));
            log.Close();
        }
    }

    public class Record
    {
        [Index(0)]
        public string LmktLeadId { get; set; }
        [Index(1)]
        public string TimeLineStarted { get; set; }
        [Index(2)]
        public string UniversalLeadId { get; set; }
        [Index(3)]
        public string TztLeadId { get; set; }
    }
}
