# "OOP-SQL-Library" or "Simple-ORM"

![badge](https://img.shields.io/static/v1?label=Language&message=C%23&color=blueviolet&style=for-the-badge)
![badge](https://img.shields.io/static/v1?label=architecture&message=Pure-Model&color=red&style=for-the-badge)
![badge](https://img.shields.io/static/v1?label=Paradigm&message=OOP&color=green&style=for-the-badge)

## About project

- This is a simple SQL library that I made as part of my learning new things in Unity and strict OOP Research
- In fact, this is just a wrapper over another library - [Npgsql](https://www.npgsql.org/)
- The library contains classes for using basic SQL commands(CRUD), but allows you to execute any command in any mode (Reader, NonQuery, Scalar and async versions of them)

### Specifics
- Project using **OOP** and **SOLID**
- Procedural SQL algorithms deisgned to be **True OOP**
- Clearest **DI** and **SRP**
- My first library that I published

## How to install

```cmd
cd DirectoryWIthYourProject
dotnet add package Simple-ORM
dotnet restore
```
Or you can visit [package page on Nuget](https://www.nuget.org/packages/OOP-SQL-Library) and install package from there

## How to use

To get started, you will need components such as SQLConnector and SQLCommandExecutor. This is the base for all other components, as they allow you to execute SQL queries.

```c#
var sqlConnector = new SQLConnector(@"Server=your server;Port=your port;User Id=your user id;Password=your password;Database=your DB name");
var sqlCommandExecutor = new SQLCommandsExecutor(sqlConnector);
```

This is enough for you to execute any SQL queries, but to make everything more convenient and you do not have to write basic commands manually, you need to use the remaining components. They will be based on SQLCommandExecutor and SQLParametersStringFactory, which will convert the arguments to a string.

```c#
var sqlParametersStringFactory = new SQLParametersStringFactory();
```

### SQLDataReader

SQLDataReader allows you to read data from a table, taking 3 arguments: the name of the table, the names of the columns to be selected (optional, if you pass an empty array, then in the final query will be \*), and the arguments by which the data will be selected (optional)

```c#
var sqlDataReader = new SQLDataReader(sqlCommandExecutor, sqlParametersStringFactory);
var data = sqlDataReader.GetData("humans", new string[] { }, new SQLArgument[] { }); //equals to "SELECT * FROM humans"
data = sqlDataReader.GetData("humans", new[] { "first_name" }, new SQLArgument[] { new("age", 19) }); //equals to "SELECT first_name FROM humans WHERE age = 19"
```

