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

### SQLDataWriter

SQLDataWriter allows you to write data to your database. It only takes 2 arguments: the name of your database and the SQLArguments to be inserted.

```c#
var sqlDataWriter = new SQLDataWriter(sqlCommandExecutor, sqlParametersStringFactory);
ISQLArgument[] arguments = { new SQLArgument("age", 19), new SQLArgument("first_name", "Anatoliy"), new SQLArgument("last_name", "Oleynikov") };
sqlDataWriter.WriteData("humans", arguments); //equals to "INSERT INTO humans (age, first_name, last_name) VALUES (19, 'Anatoliy', 'Oleynikov')"
```

### SQLDataReader

SQLDataReader allows you to read data from a table, taking 3 arguments: the name of the table, the names of the columns to be selected (optional, if you pass an empty array, then in the final query will be \*), and the SQLArguments by which the data will be selected (optional).

```c#
var sqlDataReader = new SQLDataReader(sqlCommandExecutor, sqlParametersStringFactory);
DataTable data = sqlDataReader.GetData("humans", new string[] { }); //equals to "SELECT * FROM humans"
data = sqlDataReader.GetData("humans", new[] { "first_name" }, new SQLArgument[] { new("age", 19) }); //equals to "SELECT first_name FROM humans WHERE age = 19"
```

### SQLDataUpdater 

SQLDataUpdater allows you to update the data in your database. It takes 3 arguments, of which 2 are required: the name of the database and the SQLArguments by which the data will be replaced. The third argument is the SQLArguments that will be updated. If this argument is missing, all data in the table will be replaced, be careful.

```c#
var sqlDataUpdater = new SQLDataUpdater(sqlCommandExecutor, sqlParametersStringFactory);
var replacedArguments = new ISQLArgument[] { new SQLArgument("age", 20) };
var argumentsWhichChanging = new ISQLArgument[] { new SQLArgument("first_name", "Anatoliy") };

sqlDataUpdater.UpdateData("humans", replacedArguments, argumentsWhichChanging); //equals to "UPDATE humans SET age = 20 WHERE first_name = 'Anatoliy'"
sqlDataUpdater.UpdateData("humans", replacedArguments); //equals to "UPDATE humans SET age = 20"
```

### SQLDataDeleter

SQLDataDeleter allows you to delete data. It takes 2 arguments: the name of the database and the SQLArguments by which the data will be deleted.

```c#
var sqlDataDeleter = new SQLDataDeleter(sqlCommandExecutor, sqlParametersStringFactory);
ISQLArgument[] arguments = { new SQLArgument("age", 19), new SQLArgument("first_name", "Anatoliy"), new SQLArgument("last_name", "Oleynikov") };
sqlDataDeleter.DeleteData("humans", arguments); //equals to "DELETE FROM humans WHERE age = 19 AND first_name = 'Anatoliy' AND last_name = 'Oleynikov'"
```

## Conclusion

Now you know how to install and use the components of this library. Don't forget that if the library's CRUD components don't allow you to execute the commands you need or don't allow you to do all the necessary actions in one query, then you can always use SQLCommandsExecutor, which reduces command execution to 1 line and allows you to use the full power of SQL. Thanks for reading and using the library. Good luck with your projects!

From Atennop with ❤ and OOP.
