# MySQLManager [![Build Status](https://travis-ci.org/Muhand/MySQLManager.svg?branch=stable)] [![Nuget](https://buildstats.info/nuget/MySqlManager)](http://www.nuget.org/packages/MySQLManager)

A cross-platform MySQL database manager for C#

## Use
Download with [NuGet](https://www.nuget.org/packages/MySQLManager/), or download the [release](https://github.com/Muhand/MySQLManager/releases) and include **MySQLManager.dll** as a reference in your project.

You can even download use `Nuget Package Manager` in Visual Studio by typing

`Install-Package MySQLManager -Version 1.0.0`

Or using .NET CLI by typing

`dotnet add package MySQLManager --version 1.0.0`

## How it works
MySQLManager is simply a library which implements MySQL functionalties and takes care of handling connection opening and connection closing and everything else.

MySQLManager makes it possible to Create, Remove, Update and Read data to and from database with ease. MySQLManager is a corss-platform library which allows developers to communicate with MySQL database using this library by making different function cals.

A developer should make use of events to get notifications back, more will be explained in the API and Examples Sections.

## Examples
All examples can be found in the [Examples](Examples) folder.

### Creating data (Inserting data into database)
See [Insert](https://github.com/Muhand/MySQLManager/blob/Development/Examples/Insert/Insert/Program.cs) for a complete example.

```c#

       //Create database credntials
       //This is important because constructor takes credentials to open connections properly
       ConnectionCredentials credentials = new ConnectionCredentials { 
           Server = "127.0.0.1",
           Database = "mysqlmanager",
           Username = "root",
           Password = ""
       };
       
       //Create manager instance
       CRUDManager manager = new CRUDManager(credentials);
       
       //Subscribe to events
       manager.ConnectionOpenedSuccessfully += Manager_ConnectionOpenedSuccessfully; ;
       manager.ConnectionFailedToOpen += Manager_ConnectionFailedToOpen; ;
       manager.ConnectionClosedSuccessfully += Manager_ConnectionClosedSuccessfully;
       manager.ConnectionFailedToClose += Manager_ConnectionFailedToClose;
       manager.CreatedSuccessfully += Manager_CreatedSuccessfully;
       manager.FailedToCreate += Manager_FailedToCreate;
       
       //Create columns instance (The columns to insert data into)
       Columns columns = new Columns("name", "age");
       
       //Create values which will be inserted
       Values values = new Values(name, age.ToString());
       
       //Call create function and pass the table name, columns and values
       manager.Create("info", columns, values);
```

### Custom Query
Developers are not restricted on what queries are available; However, they can create their own query and execute it.

See [ExecuteCustomQuery](https://github.com/Muhand/MySQLManager/blob/Development/Examples/ExecuteCustomQuery/ExecuteCustomQuery/ExecuteCustomQuery/Program.cs) for a complete example.

```c#
       //Create database credntials
       //This is important because constructor takes credentials to open connections properly
       ConnectionCredentials credentials = new ConnectionCredentials
       {
           Server = "127.0.0.1",
           Database = "mysqlmanager",
           Username = "root",
           Password = ""
       };

       //Create manager instance
       CRUDManager manager = new CRUDManager(credentials);

       //Subscribe to events
       manager.ConnectionOpenedSuccessfully += Manager_ConnectionOpenedSuccessfully; ;
       manager.ConnectionFailedToOpen += Manager_ConnectionFailedToOpen; ;
       manager.ConnectionClosedSuccessfully += Manager_ConnectionClosedSuccessfully;
       manager.ConnectionFailedToClose += Manager_ConnectionFailedToClose;
       manager.CustomQueryExecutedSuccessfully += Manager_CustomQueryExecutedSuccessfully;
       manager.FailedToExecuteCustomQuery += Manager_FailedToExecuteCustomQuery;
       
       //Build up the query
       string query = "SELECT * FROM info WHERE name='Muhand Jumah' AND age = '22'";
       
       //Create a multidimensional list which will hold the results from the SELECT query
       List<List<string>> res = new List<List<string>>();
       
       //Execute the custom query and pass the query, the type of execution, and the output list
       manager.ExecuteCustomQuery(query,ExecutionOptions.ExecuteReader, out res);
```

## API

Please check the [API page](https://github.com/Muhand/MySQLManager/wiki/API-Documentation) in the [WIKI](https://github.com/Muhand/MySQLManager/wiki) for more information,

## Need Help? Found a bug?

If you went through our [API WIKI](https://github.com/Muhand/MySQLManager/wiki/API-Documentation) and you still seem to have troubles or there is a bug in the API then please don't hesitate to [Submit an issue](https://github.com/Muhand/MySQLManager/issues) and cc @Muhand. And, also of course, feel free to submit pull requests with bug fixes or changes (If you don't wish to be credited let me know).
