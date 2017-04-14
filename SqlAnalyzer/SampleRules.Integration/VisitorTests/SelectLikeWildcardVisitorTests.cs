using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleRules.Visitors;
using System.Linq;

namespace SampleRules.Integration.VisitorTests
{
    [TestClass]
    public class SelectLikeWildcardVisitorTests
        : SqlResultTest
    {
        [TestMethod]
        public void TestSelectLikeWildcardVisitor_NormalCase()
        {
            var query = @"SELECT * FROM FOOBAR WHERE item LIKE '%Test';";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectLikeWildcardVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(1, statements.Count());
        }

        [TestMethod]
        public void TestSelectLikeWildcardVisitor_MiddleCase()
        {
            var query = @"SELECT * FROM FOOBAR WHERE item LIKE 'T%est';";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectLikeWildcardVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(0, statements.Count());
        }

        [TestMethod]
        public void TestSelectLikeWildcardVisitor_NoLikeClause()
        {
            var query = @"SELECT * FROM FOOBAR WHERE item = '%Test';";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectLikeWildcardVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(0, statements.Count());
        }

        [TestMethod]
        public void TestSelectLikeWildcardVisitor_MultipleStatements()
        {
            var query = @"
BEGIN
SELECT * FROM FOOBAR WHERE item = '%Test';
SELECT * FROM FOOBAR WHERE item LIKE '%Test';
END
";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectLikeWildcardVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(1, statements.Count());
        }

        [TestMethod]
        public void TestSelectLikeWildcardVisitor_NoWhereClause()
        {
            var query = @"SELECT * FROM FOOBAR;";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectLikeWildcardVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(0, statements.Count());
        }
    }
}
