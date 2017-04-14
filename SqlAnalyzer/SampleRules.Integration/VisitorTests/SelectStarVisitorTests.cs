using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleRules.Visitors;
using System.Linq;

namespace SampleRules.Integration.VisitorTests
{
    [TestClass]
    public class SelectStarVisitorTests
        : SqlResultTest
    {
        [TestMethod]
        public void TestSelectStarVisitor_NormalCase()
        {
            var query = @"SELECT * FROM FOOBAR;";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectStarVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(1, statements.Count());
        }

        [TestMethod]
        public void TestSelectStarVisitor_EnsureColumnsWork()
        {
            var query = @"SELECT col1, col2, col3 FROM FOOBAR;";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectStarVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(0, statements.Count());
        }

        [TestMethod]
        public void TestSelectStarVisitor_MultipleSelectStatements()
        {
            var query = @"
BEGIN
SELECT * from foobar;
SELECT col1, col2, col3 from foobar
SELECT * from foobar;
END
";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectStarVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(2, statements.Count());
        }
    }
}
