using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleRules.Visitors;
using System.Linq;

namespace SampleRules.Integration.VisitorTests
{
    [TestClass]
    public class SelectWithoutNoLockVisitorTests
        : SqlResultTest
    {
        [TestMethod]
        public void TestSelectWithoutNoLockVisitor_NormalCase()
        {
            var query = @"SELECT * FROM FOOBAR WITH (NOLOCK);";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectWithoutNoLockVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(0, statements.Count());
        }

        [TestMethod]
        public void TestSelectWithoutNoLockVisitor_NoLockDoesNotExist()
        {
            var query = @"SELECT * FROM FOOBAR;";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectWithoutNoLockVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(1, statements.Count());
        }

        [TestMethod]
        public void TestSelectWithoutNoLockVisitor_NoLockDoesNotExistMultipleStatements()
        {
            var query = @"
BEGIN
SELECT * FROM FOOBAR;
SELECT * FROM FOOBAR WITH (NOLOCK);
SELECT col1, col2, col3 FROM FOOBAR;
END";

            var sqlResult = BuildSqlResult(query);
            var visitor = new SelectWithoutNoLockVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<SelectStatement>();
            Assert.AreEqual(2, statements.Count());
        }
    }
}
