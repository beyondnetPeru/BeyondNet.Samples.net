using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Jal.Monads;
using Jal.Settings.Interface;
using BeyondNet.Demo.Jal.Library.Interface;
using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Impl.DataAccess
{
    public class SqlLogRepository : IRepository
    {
        private readonly ISettingsExtractor _settingsExtractor;

        public SqlLogRepository(ISettingsExtractor settingsExtractor)
        {
            _settingsExtractor = settingsExtractor;
        }
 
        public Result Log(LogLevel level, string message, DateTime dateTime)
        {
            try
            {
                const string insert = "insert into logs (id, level, message, datetime) values (@id, @level, @message, @datetime)";
                var connectionString = _settingsExtractor.Get<string>("database");

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    connection.Execute(insert, new { id = Guid.NewGuid(), level, message, datetime = dateTime }, commandType: CommandType.Text);
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new[] { $"An erro was ocurred. Error: {ex.Message}" });
            }
        }
    }
}
