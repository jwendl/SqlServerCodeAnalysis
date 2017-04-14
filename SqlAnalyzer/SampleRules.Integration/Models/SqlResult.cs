using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Collections.Generic;

namespace SampleRules.Integration.Models
{
    public class SqlResult
    {
        public StatementList SqlStatementList { get; set; }

        public IEnumerable<ParseError> ParseErrors { get; set; }
    }
}
