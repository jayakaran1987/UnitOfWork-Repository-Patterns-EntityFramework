1.	This Web Application follows code first approach. Please set your database server connection string in web config - KPMGDbConnection
2.	Since the uploaded data has huge amount of records (100k rows), I have decided to use File Reading with chunk data tables so that while reading the data we can perform the write operations.
3.	To write the data, I have chosen SqlBulkCopy (Entity Framework Object relational mapping is not the good way to insert bulk data). 
4.	Entity Framework ORM approach is used for Data Management like CRUD operations
5.	This MVC Application use the Unit of Work and Repository Pattern design. Controller will get the access of Unit of work. Unit of work having the reference of all other repositories  
6.	Due to the limited time I did not cover all unit test coverage. I have only created Test Project to show that I have knowledge about Unit Testing and TDD.
7.	I used Kendo UI components for GUI 
