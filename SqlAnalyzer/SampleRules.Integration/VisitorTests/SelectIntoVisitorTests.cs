using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleRules.Visitors;
using System.Linq;

namespace SampleRules.Integration.VisitorTests
{
    [TestClass]
    public class SelectIntoVisitorTests
        : SqlResultTest
    {
        [TestMethod]
        public void TestSelectIntoVisitor_NormalCase()
        {
            var query = @"SELECT col1 INTO foo FROM bar;";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectIntoVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(1, statements.Count());
        }

        [TestMethod]
        public void TestSelectIntoVisitor_MultipleSelectStatements()
        {
            var query = @"
BEGIN
SELECT col1 INTO foo FROM bar;
SELECT col2 INTO foo FROM bar;
SELECT col3 INTO foo FROM bar;
END
";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectIntoVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(3, statements.Count());
        }

        [TestMethod]
        public void TestSelectIntoVisitor_MultipleSelectStatementsWithoutMatch()
        {
            var query = @"
BEGIN
SELECT col1 INTO foo FROM bar;
SELECT col2 FROM foobar;
SELECT col3 INTO foo FROM bar;
END
";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectIntoVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(2, statements.Count());
        }
    }
}
