Question

You have up to a day to develop a small web application per the brief below using Visual Studio 2013 and written in ASP.NET MVC and C# in order to demonstrate your problem solving and coding skills.
Problem
You are designing a web application that processes account transaction data to calculate tax figures for a client tax return. We are interested in the data input/manipulation stage of the process which is achieved through an upload feature.
The transaction data has 4 columns: Account (text), Description (text), Currency Code (string) and Amount (decimal) and could be attached to a Database with 1m+ rows.  The format of the data will be in Excel (xlsx) or CSV and each individual file could contain up to 100k rows.
Requirement
To develop a small web application that does the following:
-	Has a clear interface with menu bar for navigation;
-	Allow the user to select a file and show the current file name (only one of XLSX or CSV needs to be supported at this stage);
-	Allow the user to upload the content of the current file to a table in a SQL database;
-	Each line of data should be validated before it is uploaded – all fields are required, Currency Code must be a valid ISO 4217 currency code and Amount must be a valid number;
-	On completion, the number of lines imported and the details of any lines ignored due to failed validation should be shown;
-	Allow user to see all the data transaction data in the system;
-	If time allows, allow user to remove or edit the transaction data;
Any solution should demonstrate best practices where appropriate such as SOLID principles.


Note


1.	This Web Application follows code first approach. Please set your database server connection string in web config - KPMGDbConnection
2.	Since the uploaded data has huge amount of records (100k rows), I have decided to use File Reading with chunk data tables so that while reading the data we can perform the write operations.
3.	To write the data, I have chosen SqlBulkCopy (Entity Framework Object relational mapping is not the good way to insert bulk data). 
4.	Entity Framework ORM approach is used for Data Management like CRUD operations
5.	This MVC Application use the Unit of Work and Repository Pattern design. Controller will get the access of Unit of work. Unit of work having the reference of all other repositories  
6.	Due to the limited time I did not cover all unit test coverage. I have only created Test Project to show that I have knowledge about Unit Testing and TDD.
7.	I used Kendo UI components for GUI 
8.      I did not commit the bin, obj, packages directory, please use the nuget to download appropriate packages

SetUp Instruction

This Web Application follows code first approach. Please set your database server connection string in web config - AccountDataProcessDbConnection
So Application will be create data base if not exixts. 

**** Only Set Database ServerName&Path and DatabaseName ***********

Example - 

<add name="AccountDataProcessDbConnection" connectionString="Data Source=ServerName&Path;Initial Catalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=true" providerName="System.Data.SqlClient" />
