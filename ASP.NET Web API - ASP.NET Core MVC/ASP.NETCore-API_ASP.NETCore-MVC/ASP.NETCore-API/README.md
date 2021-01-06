# How to Build CRUD REST APIs with ASP.NET Core 3.1, Create JWT Tokens, and Secure APIs
In this blog, I am going to provide a walkthrough on developing REST APIs using ASP.NET Core 3.1, creating a JWT token, and securing APIs. I am going to develop a sample application for an inventory REST service with basic operations.

## What is a REST API?
Due to the increasing number of different varieties of clients (mobile apps, browser-based SPAs, desktop apps, IOT apps, etc.), we need better ways for transferring data from servers to clients, independent of technology and server stacks.

REST APIs solve this problem. REST stands for representational state transfer. REST APIs are HTTP-based and provide applications the ability to communicate using lightweight JSON format. They run on web servers.

REST consists of the following entities:

**Resource**: Resources are uniquely identifiable entities (for example: data from a database, images, or any data).

**Endpoint**: A resource can be accessed through a URL identifier.

**HTTP method**: HTTP method is the type of request a client sends to a server. Operations we perform on the resource should follow this.

**HTTP header**: An HTTP header is a key-value pair used to share additional information between a client and server, such as:

- Type of data being sent to server (JSON, XML).
- Type of encryption supported by client.
- Authentication-related token.
- Customer data based on application need.
- Data format: JSON is a common format to send and receive - data through REST APIs.

**Data format**: JSON is a common format to send and receive data through REST APIs.

## What is a JWT Token?
In the previous section, we saw what a REST API is, and here we will see what a JWT bearer token is, which secures the REST APIs.

JWT stands for JSON Web Token. It is open standard and defines a better way for transferring data securely between two entities (client and server).

A JWT is digitally signed using a secret key by a token provider or authentication server. A JWT helps the resource server verify the token data using the same secret key, so that you can trust the data.

JWT consists of the following three parts:

**Header**: encoded data of token type and the algorithm used to sign the data.

**Payload**: encoded data of claims intended to share.

**Signature**: created by signing (encoded header + encoded payload) using a secret key.

The final JWT token will be like this: **Header.Payload.Signature**. Please find the token workflow in the following.

**Step 1: Client requesting token**

The client sends a request to the authentication server with the necessary information to prove its identity.

**Step 2: Token creation**

The authentication server receives the token request and verifies the identity. If it is found valid, a token will be created (as explained previously) with the necessary claims, and a JWT token will be sent back to the client.

**Step 3: Client sends token to resource server**

For each request to Resource or the API server, the client needs to include a token in the header and request the resource using its URI.

**Step 4: Resource server verifies the token**

Follow these steps to verify the token:

- Read the token from authentication header.
- Split the header, payload, and signature from token.
- Create signature of received header and payload using the same secret key used when creating the token.
- Check whether both newly created signature and signature received from token are valid.
- If the signatures are the same, the tokens are valid (not altered in the middle) and they provide access to the requested resource.
- If the signatures are different, an unauthorized response will be sent back to the client. (In the middle, if claims are alerted, they will generate a different signature, hence resource access will be restricted.)

Don’t share confidential information using a JWT, since a JWT can be decoded and the claims or data it possesses can be viewed.

The following section explains how to create a REST API and secure it using a token.

## Create an ASP.NET Core REST API application
Follow these steps to create an ASP.NET Core application in Visual Studio 2019:

**Step 1**: Go to **File > New**, and then select Project.

**Step 2**: Choose **Create a new project**.

**Step 3**: Select **ASP.NET Core Web Application** template.

**Step 4**:  Enter the **Project name**, and then click **Create**. The Project template dialog will be displayed.

**Step 5**: Select **.NET Core**, **ASP.NET Core 3.1**, and **API** template (highlighted in the following).
![alt text](https://www.syncfusion.com/blogs/wp-content/uploads/2019/12/Select-.NET-Core-ASP.NET-Core-3.1-and-API-template.png)

**Step 6**: Click **Create**. The sample ASP.NET Core API application will be created. Find the project structure in the following screenshot.

![alt text](https://www.syncfusion.com/blogs/wp-content/uploads/2019/12/The-project-structure.png)

By default, a sample **WeatherForecast** API is created. We can remove this.

Default data from example api ![image](https://i.ibb.co/g6TxH8F/image.png)

## Make *CRUD* with *HTTPGET, HTTPPOST, HTTPPUT, HTTPDELETE* methods
``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP.NETCore_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new List<string>
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Count)]
            })
            .ToArray();
        }

        [HttpGet("all")]
        public IEnumerable<string> GetAll()
        {
            return Summaries;
        }

        [HttpPost("add")]
        public IEnumerable<string> Add([FromBody] string value)
        {
            Summaries.Add(value);
            return Summaries;
        }

        [HttpPut("update/{oldValue}")]
        public IEnumerable<string> Update(string oldValue,[FromBody]string newValue)
        {
            Summaries = Summaries.Select(x => x == oldValue ? newValue : x).ToList();
            return Summaries;
        }


        [HttpDelete("delete/{value}")]
        public IEnumerable<string> Delete(string value)
        {
            Summaries = Summaries.Where(x => !x.Contains(value)).ToList();
            return Summaries;
        }
    }
}
```
**Try those with postmand app**: 
- Get method (Get all data list): ![image](https://i.ibb.co/51sP51p/image.png)
- Post method (Add "new Value" to data list): ![image](https://i.ibb.co/fxQk314/image.png)
- Put method (Update "Freezing" to "new Freezing"): ![image](https://i.ibb.co/sbX5zP7/image.png)
- Delete method (Delete "new Freezing" in current list): ![image](https://i.ibb.co/hVF0N72/image.png)

## Create a **JWT**
We can consume and test our API using Postman, but the problem here is anyone who knows the endpoint can consume it. So this is not the case, we need an option to control who can consume our service. This is achieved by a JWT bearer token.

Here, we will see how to create a token:

**Step1**: Add the necessary Nuget package 
- System.IdentityModel.Tokens.Jwt -Version
- Microsoft.AspNetCore.Authentication.JwtBearer

**Step2** Create an **empty API** controller called **TokenController**

**Step3**: Paste the below JWT configuration into the **appsetting.json** file (you can set with any text).
``` json
"Jwt": {
    "Key": "aaaaaaaabbbbbbbbbbbccccccccccccc",
    "Issuer": "My server",
    "Audience": "Any clients",
    "Subject": "My access token"
  }
```
**Step4**: Add the action method under **TokenController** to perform the following operations:

- Accept username and password as input.
- Check users’ credentials with database **(in this example we would mock some user data instead of database)** to ensure users’ identity:
If valid, access token will be returned.
If invalid, bad request error will be returned.

The following code example demonstrates how to create a token.

``` C#
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ASP.NETCore_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ASP.NETCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public UserInfo _currentUser;
        public TokenController(IConfiguration config)
        {
            _configuration = config;
            // mock an user
            _currentUser = new UserInfo()
            {
                Email = "foo@gmail.com",
                FirstName = "foo",
                LastName = "bar",
                Password = "password",
                UserId = 1,
                UserName = "foo bar",
                CreatedDate = DateTime.Now
            };
        }

        [HttpPost]
        public IActionResult Post(UserInfo _userData)
        {

            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("UserName", user.UserName),
                    new Claim("Email", user.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private UserInfo GetUser(string email, string password)
        {
            if (_currentUser.Email != email || _currentUser.Password != password)
                return null;
            return _currentUser;
        }
    }
}
```
Follow these steps to check the token endpoint using Postman:

**Step 1**: Enter this endpoint https://localhost:44387/api/token.

**Step 2**: Choose the **POST** method and set the header to **‘Content-Type’: ‘application/json’.**

**Step 3**: Under Body > Raw, choose type **JSON (application/javascript)** and paste the product details.

![image](https://i.ibb.co/h1bX7Bw/image.png)

## Secure API endpoint
Now, we have a JWT token and will see how to secure our API.

**Step 1**: Add the following namespaces to the **Startup** file:
``` c#
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
```
**Step 2**: Configure authorization middleware in the Startup > **configureService** method.

``` C#
 // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
        }
```
We have passed the security key used when creating the token, and we have also enabled validation of Issuer and Audience.

Also, we have set **SaveToken** to true, which stores the bearer token in **HTTP Context**. So we can access the token in the controller when needed.

**Step 3**: Inject the authorization middleware into the Request pipeline (**Startup > Configure**).
``` C#
app.UseAuthentication();
```
**Step 4**: Add authorization attribute (**[Authorize]**) into the **WeatherForecastController** example controller.

``` C#
namespace ASP.NETCore_API.Controllers
{
    [Authorize] // this one
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    ...
}

```
**Step 5**: Try this with Bearer token **Bearer + tokenReult**

Choose the GET (POST, PUT, DELTE) method and then click Send. Now, you can see the Status code is **401 Unauthorized**.
- Get method without bearer token: ![image](https://i.ibb.co/6sk36vS/image.png)
The anonymous access has been blocked and the APIs have been secured.
- Get method (Get all data list) with bearer token: ![image](https://i.ibb.co/qy4z3cn/image.png)

## Conclusion

In this blog, we have learned how to create a REST API using ASP.NET Core 3.1 to perform basic CRUD operations, create a JWT token, and secure the API. I hope you found this blog useful.
