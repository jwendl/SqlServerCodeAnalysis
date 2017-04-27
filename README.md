# T-SQL Code Analysis using the Microsoft.SqlServer.TransactSql.ScriptDom namespace

Requires at least Visual Studio 2015

To run, just clone the repository and open visual studio

Unit tests are built around the visitors

## Documentation
Rules are the integration point with Visual Studio and MSBuild
Visitors are the classes that are called during an explicit visit of a specific node

The query SELECT * FROM TABLE WHERE COLUMN LIKE '%TEST'; gets broken up into Nodes that are explicitly visited and translated into something similar to the following:

* SelectStarStatement
    * QuerySpecification
        * WhereClause
            * LikePredicate
                * FirstExpression
                * SecondExpression
                    * StringLiteral

