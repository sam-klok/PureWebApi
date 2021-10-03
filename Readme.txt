I'm playing with .Net Core Web Api project

So list:
1. .Net Core
2. Web Api
3. Adapter Pattern (AutoMapper extensions)
4. EF, code first

It also uses "Code First" approach to create database with EF.
Added AutoMapper extensions for ASP.NET Core - a great tool when used for simple conversions.

thank you
sam klok

Based on examples from internet. Links below:

Building an API with ASP.NET Core
by Shawn Wildermuth
The skill of building a web-facing API isn’t optional anymore. 
Whether you’re building a web site, mobile app, SPA, or enterprise tool, 
building a well-designed API is required.
 
List of issues, and articles I used:
https://stackoverflow.com/questions/57066856/command-dotnet-ef-not-found-in-net-core-3
dotnet tool install --global dotnet-ef

Question: Why derive from ControllerBase vs Controller?
https://newbedev.com/why-derive-from-controllerbase-vs-controller-for-asp-net-core-web-api

I use Postman app to test this web.api

SQL Server/Database details below:

My local SQL Server(s):
\\LIVING\\MSSQLSERVER01

Use command to migrate code to DB: 

F:\repo\PureWebApi\src>dotnet ef database update
Build started...
Build succeeded.
Applying migration '20180928134504_initialdb'.
Done.



Data Source=LIVING\MSSQLSERVER01;Initial Catalog=AdventureWorks2016CTP3;Integrated Security=True