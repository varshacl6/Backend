# Backend
1. Create Basic CRUD API
2. Connect to MSSQL 
3. Setup basic JWT Authentication
4. Integrate with Swagger
5. Entity Framework
6. Move the Dockerfile to root folder as there was minor path issue
7. Basic auditing - automatically generate EnteredBy, UpdatedBy, EnteredOn, UpdatedOn (refer https://github.com/varshacl6/DemoApplication/blob/master/DemoApplication/Data/ApplicationDbContext.cs)
8. EFCore.NamingConventions framework to map all the column names to snake_case
9. Unit Testing using xUnit and Moq

Commands:

1. Create migrations:

```dotnet ef migrations add <migration_name>```

2. Run migrations:

```dotnet ef database update```


Note:

This project uses ASP.NET 6 version


TESTING FRAMEWORK COMPARISION:

1 NUnit - there is no isolation of test cases 
2. xUnit isolates tests (same ). No [SetUp] or [TearDown]
