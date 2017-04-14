using Microsoft.SqlServer.TransactSql.ScriptDom;
using SampleRules.Interfaces;
using System.Collections.Generic;

namespace SampleRules.Visitors
{
    public class WaitForDelayVisitor
        : TSqlConcreteFragmentVisitor, IStatementVisitor<WaitForStatement>
    {
        public WaitForDelayVisitor()
        {
            Statements = new List<WaitForStatement>();
        }

        public override void ExplicitVisit(WaitForStatement node)
        {
            // We are only interested in WAITFOR DELAY occurrences  
            if (node.WaitForOption == WaitForOption.Delay)
            {
                Statements.Add(node);
            }
        }

        public IList<WaitForStatement> Statements { get; private set; }
    }
}