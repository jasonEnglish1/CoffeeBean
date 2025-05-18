# CoffeeBean

Before running the solution please use migrations to create an instance of the db in Sql Server Management Studio.
Input the following into the package manager console:
__________________________________________
Update-Database
__________________________________________


This should create an db for the data to load into when the program is run. If there is any error with the above try running:
__________________________________________
Remove-Migration
Add-Migration CoffeeBean
__________________________________________


This should remove and re-add the migration.


The solution requires the nuget packages:
Quartz
Quartz.Extensions.DependencyInjection
Quartz.Extensions.Hosting
Swashbuckle.AspNetCore
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
__________________________________________

Entity Framework was selected as a simple way to quickly set up a SQL database using a code first approach.
Quartz and it's hosting is used to manage running the task once a day which updates the bean of the day.
SQL server is used as a relational database was required.
