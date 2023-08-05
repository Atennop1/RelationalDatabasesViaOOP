# **Relational-Databases-via-OOP**

![badge](https://img.shields.io/static/v1?label=Language&message=C%23&color=blueviolet&style=for-the-badge)
![badge](https://img.shields.io/static/v1?label=architecture&message=Pure-Model&color=red&style=for-the-badge)
![badge](https://img.shields.io/static/v1?label=Paradigm&message=OOP&color=green&style=for-the-badge)

## **About project**

- This is a simple library for work with relational databases that I made as part of my learning new things in Unity and **strict OOP Research**
- In fact, this is just a wrapper over another library - [**Npgsql**](https://www.npgsql.org/)
- The library contains classes for using basic SQL commands(CRUD), but allows you to execute any command in any mode

### **Specifics**
- Project using **OOP** and **SOLID**
- Procedural algorithms of SQL request building designed to be **true OOP**
- Clearest **DI** and **SRP**
- My first library that I published

## **How to install**

### **Via NuGet into C# project**

```cmd
cd DirectoryWithYourProject
dotnet add package RelationalDatabasesViaOOP
dotnet restore
```
Or you can visit [package page on Nuget](https://www.nuget.org/packages/RelationalDatabasesViaOOP) and install package from there

### **Via UPM into Unity project**

Make sure you have standalone Git installed first.<br>
Reboot after installation.<br>
In Unity, open "Window" -> "Package Manager". Click the "+" sign on top left corner -> "Add package from git URL...".<br>
Paste this: ```https://github.com/Atennop1/Relational-Databases-via-OOP.git```

## **How to use**

A little warning for those who don't know:<br>
Relational databases are databases **based on the relational data model** and SQL is a **relational database query language**. In the code of this library, you will not find a single mention of SQL, because all components **provide work with relational databases**, executing and building SQL queries in their implementation.

### **RelationalDatabase**

To get started, you will need to create the RelationalDatabase and EnumerationStringFactory components. The first one allows you to query a relational database in any mode (NonQuery, Scalar, Reading and async versions of them) and has an IDatabase interface, while the second one is used by other components for convenience and has an IEnumerationStringFactory interface. You won't need it to run queries directly.

```c#
var database = new RelationalDatabase(@"Server=*your server*;Port=*your port*;User Id=*your user id*;Password=*your password*;Database=*your DB name*");
var enumerationStringFactory = new EnumerationStringFactory();
```

### **RelationalDatabaseValue**

Also, for all components, you will need objects of the RelationalDatabaseValue class, which has the IDatabaseValue interface and is a name-value pair with validation, isolation and other cool things ;)

```c#
var value = new RelationalDatabaseValue("first_name", "Anatoliy");
```

### **RelationalDatabaseDataWriter**

RelationalDatabaseDataWriter allows you to write data to your relational database and has the IDatabaseDataWriter interface. It only takes 2 arguments: the name of database table and the IDatabaseValues to be inserted.

```c#
var relationalDatabaseDataWriter = new RelationalDatabaseDataWriter(database, enumerationStringFactory);
IDatabaseValue[] values = { new RelationalDatabaseValue("age", 19), new RelationalDatabaseValue("first_name", "Anatoliy"), new RelationalDatabaseValue("last_name", "Oleynikov") };
relationalDatabaseDataWriter.Write("humans", values); //equals to "INSERT INTO humans (age, first_name, last_name) VALUES (19, 'Anatoliy', 'Oleynikov')"
```

### **RelationalDatabaseDataReader**

RelationalDatabaseDataReader allows you to read data from your relational database and has the IDatabaseDataReader interface. It takes 3 arguments: the name of the table, the names of the columns to be selected (optional, if you pass an empty array, then in the final query will be \*), and the IDatabaseValues by which the data will be selected (optional).

```c#
var relationalDatabaseDataReader = new RelationalDatabaseDataReader(database, enumerationStringFactory);
DataTable dataTable = relationalDatabaseDataReader.Read("humans", new string[] { }); //equals to "SELECT * FROM humans"
dataTable = relationalDatabaseDataReader.Read("humans", new[] { "first_name" }, new IDatabaseValue[] { new RelationalDatabaseValue("age", 19) }); //equals to "SELECT first_name FROM humans WHERE age = 19"
```

### **RelationalDatabaseDataUpdater** 

RelationalDatabaseDataUpdater allows you to update data in your relational database and has the IDatabaseDataUpdater interface. It takes 3 arguments, of which 2 are required: the name of the database table and the IDatabaseValues by which the data will be replaced. The third argument is the IDatabaseValues that will be updated. If this argument is missing, all data in the table will be replaced, be careful.

```c#
var relationalDatabaseDataUpdater = new RelationalDatabaseDataUpdater(database, enumerationStringFactory);
var replacedArguments = new IDatabaseValue[] { new RelationalDatabaseValue("age", 20) };
var argumentsWhichChanging = new IDatabaseValue[] { new RelationalDatabaseValue("first_name", "Anatoliy") };

relationalDatabaseDataUpdater.Update("humans", replacedArguments, argumentsWhichChanging); //equals to "UPDATE humans SET age = 20 WHERE first_name = 'Anatoliy'"
relationalDatabaseDataUpdater.Update("humans", replacedArguments); //equals to "UPDATE humans SET age = 20"
```

### **RelationalDatabaseDataDeleter**

RelationalDatabaseDataDeleter allows you to delete data from your relational database and has the IDatabaseDataDeleter interface. It takes 2 arguments: the name of the database table and the IDatabaseValues by which the data will be deleted.

```c#
var relationalDatabaseDataDeleter = new RelationalDatabaseDataDeleter(database, enumerationStringFactory);
IDatabaseValue[] values = { new RelationalDatabaseValue("age", 19), new RelationalDatabaseValue("first_name", "Anatoliy"), new RelationalDatabaseValue("last_name", "Oleynikov") };
relationalDatabaseDataDeleter.Delete("humans", values); //equals to "DELETE FROM humans WHERE age = 19 AND first_name = 'Anatoliy' AND last_name = 'Oleynikov'"
```

## **Conclusion**

Now you know how to install and use the components of this library. Don't forget that if the library's CRUD components don't allow you to execute the commands you need or don't allow you to do all the necessary actions in one query, then you can always use RelationalDatabase, which reduces command execution to one line and allows you to use the full power of SQL. Thanks for reading this and using the library. Good luck with your projects!

From Atennop with OOP and ❤
<br>Thanks to my colleague and friend [**Farid**](https://github.com/Farid357) for the good renaming according to all the canons of OOP
