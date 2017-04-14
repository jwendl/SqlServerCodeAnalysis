using Microsoft.SqlServer.TransactSql.ScriptDom;
using SampleRules.Integration.Models;
using System.Collections.Generic;
using System.IO;

namespace SampleRules.Integration
{
    public abstract class SqlResultTest
    {
        protected virtual SqlResult BuildSqlResult(string query)
        {
            var parser = new TSql140Parser(false);
            var parseErrors = new List<ParseError>() as IList<ParseError>;
            var textReader = new StringReader(query) as TextReader;
            var sqlFragment = parser.ParseStatementList(textReader, out parseErrors);

            return new SqlResult()
            {
                SqlStatementList = sqlFragment,
                ParseErrors = parseErrors,
            };
        }
    }
}
