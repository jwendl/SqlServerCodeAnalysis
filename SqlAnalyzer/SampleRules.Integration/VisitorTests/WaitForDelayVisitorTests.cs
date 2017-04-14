using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleRules.Visitors;
using System.Linq;

namespace SampleRules.Integration.VisitorTests
{
    [TestClass]
    public class WaitForDelayVisitorTests
        : SqlResultTest
    {
        [TestMethod]
        public void TestWaitForVisitor_NormalCase()
        {
            var query = @"
BEGIN
WAITFOR DELAY '00:00:10.000';
SELECT 'hello world';
END;";

            var sqlResult = BuildSqlResult(query);
            var visitor = new WaitForDelayVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<WaitForStatement>();
            Assert.AreEqual(1, statements.Count());
        }

        [TestMethod]
        public void TestWaitForVisitor_CaseInvariant()
        {
            var query = @"
BEGIN
WAITFOR delay '00:00:10.000';
SELECT 'hello world';
END;";

            var sqlResult = BuildSqlResult(query);
            var visitor = new WaitForDelayVisitor();
            sqlResult.SqlStatementList.Accept(visitor);

            var statements = visitor.Statements.OfType<WaitForStatement>();
            Assert.AreEqual(1, statements.Count());
        }

        [TestMethod]
        public void TestWaitForVisitor_DelayInWrongSpot()
        {
            var query = @"
BEGIN
WAITFOR '00:00:10.000' delay;
SELECT 'hello world';
END;";

            var sqlResult = BuildSqlResult(query);
            var parseError = sqlResult.ParseErrors.First();
            Assert.AreEqual("Incorrect syntax near '00:00:10.000'.", parseError.Message);
        }
    }
}
