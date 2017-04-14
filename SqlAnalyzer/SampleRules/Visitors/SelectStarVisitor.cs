using Microsoft.SqlServer.TransactSql.ScriptDom;
using SampleRules.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SampleRules.Visitors
{
    // Good explanation here https://crismorris.wordpress.com/category/scriptdom/page/3/
    public class SelectStarVisitor
        : TSqlConcreteFragmentVisitor, IStatementVisitor<SelectStatement>
    {
        public SelectStarVisitor()
        {
            Statements = new List<SelectStatement>();
        }

        public override void ExplicitVisit(SelectStatement node)
        {
            var querySpecification = (node.QueryExpression) as QuerySpecification;
            var selectStartElements = querySpecification.SelectElements.OfType<SelectStarExpression>();
            if (selectStartElements.Any())
            {
                Statements.Add(node);
            }
        }

        public IList<SelectStatement> Statements { get; private set; }
    }
}
