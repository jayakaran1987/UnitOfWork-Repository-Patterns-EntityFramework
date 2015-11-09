1.	This Web Application follows code first approach. Please set your database server connection string in web config - KPMGDbConnection
2.	Since the uploaded data has huge amount of records (100k rows), I have decided to use File Reading with chunk data tables so that while reading the data we can perform the write operations.
3.	To write the data, I have chosen SqlBulkCopy (Entity Framework Object relational mapping is not the good way to insert bulk data). 
4.	Entity Framework ORM approach is used for Data Management like CRUD operations
5.	This MVC Application use the Unit of Work and Repository Pattern design. Controller will get the access of Unit of work. Unit of work having the reference of all other repositories  
6.	Due to the limited time I did not cover all unit test coverage. I have only created Test Project to show that I have knowledge about Unit Testing and TDD.
7.	I used Kendo UI components for GUI 
8.  I did not commit the bin, obj, pakages directory, please use the nuget to dowload appropriate pakages 

SetUp Instruction

This Web Application follows code first approach. Please set your database server connection string in web config - KPMGDbConnection
So Application will be create data base if not exixts. 

**** Only Set Database ServerName&Path and DatabaseName ***********

Example - 

<add name="KPMGDbConnection" connectionString="Data Source=ServerName&Path;Initial Catalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=true" providerName="System.Data.SqlClient" />


Requirements Check
Completed  -	Has a clear interface with menu bar for navigation;
Completed  -	Allow the user to select a file and show the current file name (only one of XLSX or CSV needs to be supported at this stage);
Completed  -	Allow the user to upload the content of the current file to a table in a SQL database;
Completed  -	Each line of data should be validated before it is uploaded – all fields are required, Currency Code must be a valid ISO 4217 currency code and Amount must be a valid number;
Completed  -	On completion, the number of lines imported and the details of any lines ignored due to failed validation should be shown;
Completed  -	Allow user to see all the data transaction data in the system;
Completed  -	If time allows, allow user to remove or edit the transaction data;