### Visit the live link on **https://hikegroop.fly.dev**

## How I worked on this project

My goal is to simulate building real-world web application.

- I used feature branches and Pull Requests: [Example PR](https://github.com/cathleys/HikeGroop/pull/28)

## How to navigate this project

- Somewhat complex application logic: [Example code](https://github.com/cathleys/HikeGroop/blob/main/HikeGroop/Controllers/GroupController.cs#L123)
- This app implements Repository patterns: [Example database call](https://github.com/cathleys/HikeGroop/blob/main/HikeGroop/Repositories/DestinationRepository.cs)
- CI/CD pipeline : [Example image](HikeGroop/wwwroot/assets/images/ci.JPG)
- Unit test : [Example test](https://github.com/cathleys/HikeGroop/blob/main/HikeGroop.Tests/Controllers/GroupControllerTests.cs#L35)

## Why I built the project this way

C# is a great object-oriented and type-safe programming language. C# enables developers to build many types of secure and robust applications that run in .NET.

ASP.NET Core MVC is a great web framework to build dynamic web apps and help developers to build them quicker and efficiently.

Testing is essential part of app development. XUnit with FluentAssertions is what I used to unit test some parts of my code.

## If I had more time I would change this

- Implement Unit of Work pattern to centralized my repositories
- Remove "?" in the Domain Models [Example Model](https://github.com/cathleys/HikeGroop/blob/main/HikeGroop/Models/Destination.cs)

\***\*\*\*\*\***The End\***\*\*\*\*\*\***

## The Application

HikeGroop is an online meetup platform for mountain hikers to connect and find their fellows in their area and as well as join hiking activities.

![The running application](HikeGroop/wwwroot/assets/images/hikegroop.gif)

## Getting Started

This project is built with ASP.NET Core MVC, C#, HTML, CSS, Bootstrap, Javascript.

1. Go into directory where you plan on keeping project and run.

```bash
  git fork https://github.com/cathleys/HikeGroop.git
```

2. Create a local database. I use SQLite for development mode. Install the SQLite in Nuget package manager. You can see it in HikeGroop.csproj

3. Add connection string to appsettings.Development.json.
   Would look something like this or go to my firsts branches to see what it looked like before:

```bash
 "ConnectionStrings" : {
    "DefaultConnection" : "Data source = hikegroop.db"
 }
```
