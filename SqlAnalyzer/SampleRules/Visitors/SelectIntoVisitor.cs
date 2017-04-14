using Microsoft.SqlServer.TransactSql.ScriptDom;
using SampleRules.Interfaces;
using System.Collections.Generic;

namespace SampleRules.Visitors
{
    // Good explanation here https://crismorris.wordpress.com/category/scriptdom/page/3/
    public class SelectIntoVisitor
        : TSqlConcreteFragmentVisitor, IStatementVisitor<SelectStatement>
    {
        public SelectIntoVisitor()
        {
            Statements = new List<SelectStatement>();
        }

        public override void ExplicitVisit(SelectStatement node)
        {
            if (node.Into != null)
            {
                Statements.Add(node);
            }
        }

        public IList<SelectStatement> Statements { get; private set; }
    }
}
