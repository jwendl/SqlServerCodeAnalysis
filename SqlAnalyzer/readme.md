SQL Server Custom Code Analysis Rules for Common Scenarios

Finds scenarios of:
* WAITFOR DELAY
* SELECT *
* SELECT [whatever] INTO 
* DELETE FROM [table] [no predicate]
* SELECT [no usage of NOLOCK]
* SELECT FROM [table] WHERE [column] LIKE '%bad idea'

Need to install .dlls from \bin\Debug to [Visual Studio Install Dir]\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\130\Extensions\MainRules.

Where [Visual Studio Install Dir] for VS 2017 is at C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise.

Additionally need to configure code analysis for Visual Studio inside a database project by right clicking the database project and selecting properties.
