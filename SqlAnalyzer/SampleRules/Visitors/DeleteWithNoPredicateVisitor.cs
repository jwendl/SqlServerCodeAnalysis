using Microsoft.SqlServer.TransactSql.ScriptDom;
using SampleRules.Interfaces;
using System.Collections.Generic;

namespace SampleRules.Visitors
{
    public class DeleteWithNoPredicateVisitor
        : TSqlConcreteFragmentVisitor, IStatementVisitor<DeleteStatement>
    {
        public DeleteWithNoPredicateVisitor()
        {
            Statements = new List<DeleteStatement>();
        }

        public override void ExplicitVisit(DeleteStatement node)
        {
            if (node.DeleteSpecification.WhereClause == null)
            {
                Statements.Add(node);
            }
        }
            
        public IList<DeleteStatement> Statements { get; private set; }
    }
}
