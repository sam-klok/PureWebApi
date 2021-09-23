I'm playing with .Net Core Web Api project
It also uses "Code First" approach to create database with EF.

thank you
sam klok

Based on examples from internet. Links below:
Building an API with ASP.NET Core - Pluralsight


List of issues, and articles I used:
https://stackoverflow.com/questions/57066856/command-dotnet-ef-not-found-in-net-core-3
dotnet tool install --global dotnet-ef


My local SQL Server(s):
\\LIVING\\MSSQLSERVER01

Use command to migrate code to DB: 

F:\repo\PureWebApi\src>dotnet ef database update
Build started...
Build succeeded.
Applying migration '20180928134504_initialdb'.
Done.



Data Source=LIVING\MSSQLSERVER01;Initial Catalog=AdventureWorks2016CTP3;Integrated Security=True