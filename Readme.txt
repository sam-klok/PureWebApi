I'm playing with .Net Core Web Api project

So list:
1. .Net Core
2. Web Api
3. Adapter Pattern (AutoMapper extensions)
4. EF, code first
5. API versioning
6. 

It also uses "Code First" approach to create database with EF.
Added AutoMapper extensions for ASP.NET Core - a great tool when used for simple conversions.

Example of calls: 
	http://localhost:6600/api/camps/atl2018/talks?api-version=1.0
	http://localhost:6600/api/operations/reloadconfig?api-version=1.1

thank you
sam klok

Based on examples from internet. Links below:

Training #1
Building an API with ASP.NET Core by Shawn Wildermuth
The skill of building a web-facing API isn’t optional anymore. 
Whether you’re building a web site, mobile app, SPA, or enterprise tool, 
building a well-designed API is required.
https://app.pluralsight.com/library/courses/building-api-aspdotnet-core/table-of-contents

Training #2

Designing RESTful Web APIs by Shawn Wildermuth
Are you embarking on creating an API for your website or mobile app? 
If so, just striking forward with your API could be a mistake. 
In this course, you will learn how to design an API to meet the demands of your customers.
https://app.pluralsight.com/library/courses/designing-restful-web-apis/table-of-contents

Training #3 (...)
Implementing and Securing an API with ASP.NET Core by Shawn Wildermuth
Building an API with ASP.NET Core is an obvious choice for solutions that require cross-platform hosting, 
micro-service architecture, or just broad scale. This course will show you how to do just that.
https://app.pluralsight.com/library/courses/aspdotnetcore-implementing-securing-api/table-of-contents

A web API is an application programming interface for either a web server or a web browser.
https://en.wikipedia.org/wiki/Web_API

Microsoft Tutorial: Build secure REST APIs on any platform with C#
https://dotnet.microsoft.com/apps/aspnet/apis

3 Ways to Secure Your Web API:
https://medium.com/swlh/3-ways-to-secure-your-web-api-for-different-situations-8d5cd4762ab3
 
List of issues, and articles I used:
https://stackoverflow.com/questions/57066856/command-dotnet-ef-not-found-in-net-core-3
dotnet tool install --global dotnet-ef

Question: Why derive from ControllerBase vs Controller?
Answer: The Controller class derives from ControllerBase and adds some members that are only needed to support Views.
https://stackoverflow.com/questions/55239380/why-derive-from-controllerbase-vs-controller-for-asp-net-core-web-api

3 Ways to Secure Your Web API for Different Situations
https://newbedev.com/why-derive-from-controllerbase-vs-controller-for-asp-net-core-web-api

I use Postman app to test this web.api
I use "curl" too. 

SQL Server/Database details below:
My local SQL Server(s): \\LIVING\\MSSQLSERVER01

Use command to migrate code to DB:  
F:\repo\PureWebApi\src>dotnet ef database update
Build started...
Build succeeded.
Applying migration '20180928134504_initialdb'.
Done.

My SQL server connections strings: 
Data Source=LIVING\MSSQLSERVER01;Initial Catalog=AdventureWorks2016CTP3;Integrated Security=True

dotnet ef migrations add Identity

If error:
The entity type 'IdentityUserLogin<string>' requires a primary key to be defined.



Some files copied from:
https://github.com/DinkDev/DutchTreat/blob/master/DutchTreat/Data/DutchSeeder.cs
https://github.com/lonelydev/DutchTreat/blob/master/DutchTreat/Data/DutchRepository.cs
