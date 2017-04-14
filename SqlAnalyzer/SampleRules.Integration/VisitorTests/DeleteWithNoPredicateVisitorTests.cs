using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleRules.Visitors;
using System.Linq;

namespace SampleRules.Integration.VisitorTests
{
    public class DeleteWithNoPredicateVisitorTests
    {
        [TestClass]
        public class SelectIntoVisitorTests
            : SqlResultTest
        {
            [TestMethod]
            public void TestDeleteWithNoPredicateVisitor_NormalCase()
            {
                var query = @"DELETE FROM foobar WHERE id = 1;";

                var sqlResult = BuildSqlResult(query);
                var visitor = new DeleteWithNoPredicateVisitor();
                sqlResult.SqlStatementList.Accept(visitor);

                var statements = visitor.Statements.OfType<DeleteStatement>();
                Assert.AreEqual(0, statements.Count());
            }

            [TestMethod]
            public void TestDeleteWithNoPredicateVisitor_NoPredicate()
            {
                var query = @"DELETE FROM foobar;";

                var sqlResult = BuildSqlResult(query);
                var visitor = new DeleteWithNoPredicateVisitor();
                sqlResult.SqlStatementList.Accept(visitor);

                var statements = visitor.Statements.OfType<DeleteStatement>();
                Assert.AreEqual(1, statements.Count());
            }

            [TestMethod]
            public void TestDeleteWithNoPredicateVisitor_MultipleStatementsNoPredicate()
            {
                var query = @"
BEGIN
DELETE FROM foobar;
DELETE FROM foobar WHERE id = 1;
DELETE FROM foobar WHERE id in (1, 2, 3, 4);
END";

                var sqlResult = BuildSqlResult(query);
                var visitor = new DeleteWithNoPredicateVisitor();
                sqlResult.SqlStatementList.Accept(visitor);

                var statements = visitor.Statements.OfType<DeleteStatement>();
                Assert.AreEqual(1, statements.Count());
            }
        }
    }
}
