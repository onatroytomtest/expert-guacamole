using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Faircode.Platform.Data.Sql.Enum;

namespace Faircode.Platform.Data.Sql.Commands
{
    public interface IDataCommand
    {
        Task<int> ExecuteNonQueryAsyc(string sql, CommandTypes type, IEnumerable<CommandParameter> parameters);

        Task<DataSet> ExecuteReaderAsync(string sql, CommandTypes type, IEnumerable<CommandParameter> parameters);

    }
}
