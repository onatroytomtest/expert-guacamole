using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Faircode.Platform.Data.Sql.Enum;

namespace Faircode.Platform.Data.Sql.Commands
{
    public abstract class DataCommand : IDataCommand
    {
        private string _connectionString;

        protected DataCommand(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("ConnectionString Can't be empty");
            }

            _connectionString = connectionString;

        }
        public async Task<int> ExecuteNonQueryAsyc(string sql, CommandTypes type, IEnumerable<CommandParameter> parameters)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));

            }
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(sql, connection)
            {

                CommandType = type == CommandTypes.Procedure ? CommandType.StoredProcedure : CommandType.Text
            };
            if (parameters != null && parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Name, parameter.Value));
                }
            }

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return await command.ExecuteNonQueryAsync();

        }

        public Task<DataSet> ExecuteReaderAsync(string sql, CommandTypes type, IEnumerable<CommandParameter> parameters)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));

            }
            DataSet ds = new DataSet();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(sql, connection)
            {

                CommandType = type == CommandTypes.Procedure ? CommandType.StoredProcedure : CommandType.Text
            };
            if (parameters != null && parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Name, parameter.Value));
                }
            }

            using SqlDataAdapter ada = new SqlDataAdapter(command);
            ada.Fill(ds);
            return Task.FromResult(ds);
        }
    }
}
