using Microsoft.SqlServer.TransactSql.ScriptDom;
using SampleRules.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SampleRules.Visitors
{
    public class SelectLikeWildcardVisitor
        : TSqlConcreteFragmentVisitor, IStatementVisitor<SelectStatement>
    {
        public SelectLikeWildcardVisitor()
        {
            Statements = new List<SelectStatement>();
        }   

        public override void ExplicitVisit(SelectStatement node)
        {
            var querySpecification = (node.QueryExpression) as QuerySpecification;
            if (querySpecification.WhereClause != null)
            {
                var likePredicate = querySpecification.WhereClause.SearchCondition as LikePredicate;
                if (likePredicate != null)
                {
                    var stringLiteral = likePredicate.SecondExpression as StringLiteral;
                    if (stringLiteral != null)
                    {
                        var value = stringLiteral.Value;
                        if (value.StartsWith("%"))
                        {
                            Statements.Add(node);
                        }
                    }
                }
            }
        }

        public IList<SelectStatement> Statements { get; private set; }
    }
}
