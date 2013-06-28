DapperDemo
==========

A very simple project to help developers demonstrate and experiment with the [Dapper micro-ORM](https://code.google.com/p/dapper-dot-net/).

It automates set-up of a local test database so that you can try things out straight away.

Getting Started
---------------
The demo is a simple C# class library (Visual Studio 2012 solution) containing some [NUnit](http://www.nunit.org/) tests. Your development environment will need to be set up to run NUnit tests, using the [Resharper test runner](http://www.jetbrains.com/resharper/features/unit_testing.html), [TestDriven.net](http://www.testdriven.net/) or the [NUnit GUI](http://www.nunit.org/).

Database set up is automated. The tests run against your SQL Server Express LocalDb instance (localDB)\v11.0, which should be available on any machine running Visual Studio 2012. Note that the main database is reset every time the web application restarts. See http://www.danmalcolm.com/2013/04/testing-your-database-with-tinynh.html for more about automated database setup.


