using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Collections.Generic;

namespace SampleRules.Interfaces
{
    public interface IStatementVisitor<TStatement>
    {
        IList<TStatement> Statements { get; }
    }
}
