# MVC tutorial 

[![Build Status](https://travis-ci.org/lonelydev/DutchTreat.svg?branch=master)](https://travis-ci.org/lonelydev/DutchTreat)

As I was following Shawn Wildermuth's tutorial on Pluralsight to build an ASP.Net Core project using MVC, Entity Framework Core, Bootstrap and Angular, I thought I would keep track of my progress. 
Adding my progress to git was a good way to see how often I went through the course. 


The course can viewed here: https://app.pluralsight.com/library/courses/aspnetcore-mvc-efcore-bootstrap-angular-web/table-of-contents

## DutchTreat

DutchTreat is the name of the application that was built as part of the tutorial. It is a very simple online shop that allows users to purchase art by Dutch artists. 

The author of the course takes you through building an MVC based website using nothing but ASP .NET and bootstrap at first. 

Then goes on to introduce front-end technologies like Angular to update parts of the same website and then connects the front end with the MVC API that you built earlier. 

What I was looking to learn was how to productionise a simple ASP .NET Core web application on Azure and this tutorial was an excellent starting point. 

I am hoping to make further enhancements to the website like the following:

  * All of UI done in Angular
  * Unit Tests
  * Continuous integration and deployment using Cake
  * A Payment processing feature - the checkout doesn't really do anything at the moment
  * Ability to view orders associated to a user
  * Cancel an order
  * Re-order previously ordered item

I will probably get more ideas as I go on this adventure. 

### What did I learn

  * ASP .NET MVC application structure
    * Controllers, Views, ViewModels
    * Layouts and sharing common parts among Views
    * TagHelpers
    * Some Razor syntax
    * Basics of injecting and configuring Services that your application needs
    * JWToken authentication
    * `config.json` and its importance
  * Entity Framework Core and Code First DB Migrations
    * `dotnet ef database drop`
    * `dotnet ef database update`
    * Creating a Data Seeder to initialize your app with some data
  * Identity in .NET Core
  * Bootstrap < 4.0 and some useful classes
  * Bower
  * npm
  * Gulp - similar to Grunt which I'm more familiar with
  * Basics of Angular 5 and Angular CLI
    * angular-cli.json 
    * `ng build` and optional `--watch`
  * Very small intro to `RxJs`
  * Basics of Typescript, `tsconfig.json` and json2ts.com
  * Visual Studio 2017
    * TaskRunner integration to run Gulp tasks within visual studio
    * Typescript transpiling with the help of `tsconfig.json`
    * Publishing to Azure directly from VS2017

I'm sure there is a lot more to add there. I have only given like a trailer of what is covered as I built the app. 