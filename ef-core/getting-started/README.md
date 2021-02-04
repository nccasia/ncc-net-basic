# Getting Started with EF Core
Require: .NET Core SDK

## Create a new project
  ```
  dotnet new console
  ```

## Install Entity Framework Core
  ```
  dotnet add package Microsoft.EntityFrameworkCore.Sqlite
  ```

## Create the model
Define a context class and entity classes that make up the model.
  ```
  using System.Collections.Generic;
  using Microsoft.EntityFrameworkCore;

  namespace EFGetStarted
  {
      public class BloggingContext : DbContext
      {
          public DbSet<Blog> Blogs { get; set; }
          public DbSet<Post> Posts { get; set; }

          protected override void OnConfiguring(DbContextOptionsBuilder options)
              => options.UseSqlite("Data Source=blogging.db");
      }

      public class Blog
      {
          public int BlogId { get; set; }
          public string Url { get; set; }

          public List<Post> Posts { get; } = new List<Post>();
      }

      public class Post
      {
          public int PostId { get; set; }
          public string Title { get; set; }
          public string Content { get; set; }

          public int BlogId { get; set; }
          public Blog Blog { get; set; }
      }
  }
  ```

## Create the database
* .NET Core CLI
  ```
  dotnet tool install --global dotnet-ef
  dotnet add package Microsoft.EntityFrameworkCore.Design
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```
* Visual Studio Package Manager Console (PMC)
  ```
  Install-Package Microsoft.EntityFrameworkCore.Tools
  Add-Migration InitialCreate
  Update-Database
  ```

## Create, read, update & delete
Update Program.cs:
```
using System;
using System.Linq;

namespace EFGetStarted
{
    internal class Program
    {
        private static void Main()
        {
            using (var db = new BloggingContext())
            {
                // Note: This sample requires the database to be created before running.

                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();

                // Update
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                blog.Posts.Add(
                    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
                db.SaveChanges();

                // Delete
                Console.WriteLine("Delete the blog");
                db.Remove(blog);
                db.SaveChanges();
            }
        }
    }
}
```

## Run the app
* .NET Core CLI
 
* Visual Studio
  Visual Studio uses an inconsistent working directory when running .NET Core console apps. (see dotnet/project-system#3619) This results in an exception being thrown: no such table: Blogs. To update the working directory:
  * Right-click on the project and select Edit Project File
  * Just below the TargetFramework property, add the following:
    ```
    <StartWorkingDirectory>$(MSBuildProjectDirectory)</StartWorkingDirectory>
    ```
* Save the file
* Run the app: Debug > Start Without Debugging