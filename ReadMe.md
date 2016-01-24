#Repository and Unit of Work Patterns with Entity Framework

The folowing web Application implemented with Unit of Work, Repoitory patters and Entity Framework Code First approach using Visual Studio 2013 and written in ASP.NET MVC and C#

This application processes account transaction data to calculate tax figures for a client tax return. The data input/manipulation stage of the process which is achieved through an upload feature.
The transaction data has 4 columns: Account (text), Description (text), Currency Code (string) and Amount (decimal) and could be attached to a Database with 1m+ rows.  The format of the data will be in Excel (xlsx) or CSV and each individual file could contain up to 100k rows.


The output of this web application does the following:
 1. Allow the user to select a file and show the current file name (only one of XLSX or CSV needs to be supported at this stage);
 2. Allow the user to upload the content of the current file to a table in a SQL database;
 3. Each line of data should be validated before it is uploaded – all fields are required, Currency Code must be a valid ISO 4217 currency code and Amount must be a valid number;
 4. On completion, the number of lines imported and the details of any lines ignored due to failed validation should be shown;
 5. Allow user to see all the data transaction data in the system;



SetUp Instruction

This Web Application follows code first approach. Please set your database server connection string in web config - BulkDataProcessDbConnection
So Application will be create database if not exixts. 

Example - 

`<add name="BulkDataProcessDbConnection" connectionString="Data Source=ServerName&Path;Initial Catalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=true" providerName="System.Data.SqlClient" />`
