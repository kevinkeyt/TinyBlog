# Tiny Blog
Tiny Blog is meant to be a small simple blogging engine to allow you to quickly setup a new blog without the need of a database, etc.
It's build using .net core 2.1 and uses Razor Pages.
Blog posts are saved to the local file system as JSON files. I drew inspiration for this from Mads Kristensen's project 
MiniBlog https://github.com/madskristensen/MiniBlog

There are a lot of new features we'd like to add to help improve Tiny Blog so if you're interested in helping feel free to get involved!


## Authentication
The site uses simple cookie authentication. To authenticate you'll need to add the following AppSettings to your project or site.

For more info on this process checkout the MS docs: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-2.1&tabs=aspnetcore2x


Here's an example using dotnet user secrets.
```json
  dotnet users-ecrets set AppSettings:LoginEmail "joe@tesing.com"
  dotnet user-secrets set AppSettings:LoginPassword "Passw0rd"
  dotnet user-secrets set AppSettings:FullName "Joe Tester"
```