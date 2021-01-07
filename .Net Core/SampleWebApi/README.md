# Sample ASP.NET Core Web API

.NET CLI webapi documents: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#webapi

### Creates a new web API application in the current folder:
```
mkdir SampleWebApi
cd SampleWebApi
dotnet new webapi
```

### Add a model class
Create new folder Models in the project's folder and add School.cs
```
using System;

namespace SampleWebApi.Models
{
    public class School
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
```

### Add a database context
The database context is the main class that coordinates Entity Framework functionality for a data model. This class is created by deriving from the Microsoft.EntityFrameworkCore.DbContext class.

- Add NuGet packages:
  - Using Visual Studio:
    - From the Tools menu, select NuGet Package Manager > Manage NuGet Packages for Solution.
    - Select the Browse tab, and then enter Microsoft.EntityFrameworkCore.SqlServer in the search box.
    - Select Microsoft.EntityFrameworkCore.SqlServer in the left pane.
    - Select the Project check box in the right pane and then select Install.

  - Using dotnet cli
    - From SampleWebApi project folder
    ```
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.InMemory
    ```

- Add ApplicationDbContext class
  ```
  using Microsoft.EntityFrameworkCore;

  namespace SampleWebApi.Models
  {
      public class ApplicationDbContext : DbContext
      {
          public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
          {
          }

          public DbSet<School> Schools { get; set; }
      }
  }
  ```

- Register the database context in Startup.cs

  In ASP.NET Core, services such as the DB context must be registered with the dependency injection (DI) container. The container provides the service to controllers.
  ```
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("Application"));

      services.AddControllers();
  }
  ```

- Add a controller for CRUD operations with School model
  ```
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using SampleWebApi.Models;
  using System;
  using System.Threading.Tasks;

  namespace SampleWebApi.Controllers
  {
      [Route("api/[controller]")]
      [ApiController]
      public class SchoolsController : ControllerBase
      {
          private readonly ApplicationDbContext dbContext;

          public SchoolsController(ApplicationDbContext dbContext)
          {
              this.dbContext = dbContext;
          }

          [HttpGet]
          [Route("{id}")]
          public async Task<ActionResult<School>> Get(Guid id)
          {
              var school = await dbContext.Schools.FirstOrDefaultAsync(i => i.Id == id);
              if (school == null)
              {
                  return NotFound($"School with id {id} not found");
              }

              return Ok(school);
          }

          [HttpPost]
          [Route("")]
          public async Task<ActionResult<School>> Create([FromBody] School school)
          {
              if (school == null || string.IsNullOrWhiteSpace(school.Name))
              {
                  return BadRequest("Invalid input");
              }

              school.Id = Guid.NewGuid();
              dbContext.Schools.Add(school);

              await dbContext.SaveChangesAsync();

              return Ok(school);
          }

          [HttpPut]
          [Route("")]
          public async Task<ActionResult<School>> Update([FromBody] School school)
          {
              if (school == null || string.IsNullOrWhiteSpace(school.Name))
              {
                  return BadRequest("Invalid input");
              }

              var existingSchool = await dbContext.Schools.FirstOrDefaultAsync(i => i.Id == school.Id);
              if (existingSchool == null)
              {
                  return NotFound($"School with id {school.Id} not found");
              }

              existingSchool.Name = school.Name;

              await dbContext.SaveChangesAsync();

              return Ok(existingSchool);
          }

          [HttpDelete]
          [Route("{id}")]
          public async Task<ActionResult<School>> Delete(Guid id)
          {
              var school = await dbContext.Schools.FirstOrDefaultAsync(i => i.Id == id);
              if (school == null)
              {
                  return NotFound($"School with id {id} not found");
              }

              dbContext.Schools.Remove(school);

              await dbContext.SaveChangesAsync();

              return Ok();
          }
      }
  }
  ```
  In the preceding code:
  - The class is mark with `[ApiController]` attribute to indicate that this controller response to web API request.
  - Use DI to inject the database context `ApplicationDbContext` into the controller.
  - `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]` is used to indicate the http methods of the http request.
  - Routing and URL paths are defined using `[Route]` attribute.

- Test the SampleWebApi with Postman
  - `GET /api/schools/{id}`
  - `POST /api/schools/`
  - `PUT /api/schools/`
  - `DELETE /api/schools/{id}`