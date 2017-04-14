using Microsoft.SqlServer.TransactSql.ScriptDom;
using SampleRules.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SampleRules.Visitors
{
    public class SelectWithoutNoLockVisitor
        : TSqlConcreteFragmentVisitor, IStatementVisitor<SelectStatement>
    {
        public SelectWithoutNoLockVisitor()
        {
            Statements = new List<SelectStatement>();
        }

        public override void ExplicitVisit(SelectStatement node)
        {
            var querySpecification = (node.QueryExpression) as QuerySpecification;
            var fromClause = querySpecification.FromClause;
            if (fromClause != null)
            {
                var tableReferences = fromClause.TableReferences.OfType<NamedTableReference>();
                foreach (var tableReference in tableReferences)
                {
                    var noLockHintExists = tableReference.TableHints.Where(th => th.HintKind == TableHintKind.NoLock).Any();
                    if (!noLockHintExists)
                    {
                        Statements.Add(node);
                    }
                }
            }
        }

        public IList<SelectStatement> Statements { get; private set; }
    }
}
