# How to Build a CRUD Application with ASP.NET Core 3.1 (MVC) & Entity Framework 3.1 Using Visual Studio 2019

In this blog, I am going to provide a walk-through on developing a web application using ASP.NET Core 3.1, connecting it to a database (database-first) using the Entity Framework Core 3.1 command, and performing CRUD operations using scaffolding (code generator). I am going to develop a sample application for inventory management with basic operations.

ASP.NET Core is a web framework from Microsoft. It is an open-source, cross-platform, cloud-optimized web framework that runs on Windows using .NET Framework and .NET Core, and on other platforms using .NET Core. It is a complete rewrite that unites ASP.NET MVC and Web API into a single programming model and removes system-related dependencies. This helps in deploying applications to non-Windows servers and improves performance.

## MVC Partern

### *What is MVC Pattern ?*

- MVC stands for Model-View-Controller (MVC), it is a software design pattern that decouples various concerns in an application.

- It is a powerful and effective way of designing applications that separates the UI (User Interface) logic from the data access and data manipulation logic.
It’s explicit separation of concerns adds some extra complexity to an application’s design, but it provides enormous benefits to the application’s stability, functionality and testability.

### *Why MVC Pattern ?*

The MVC separates an application into three main aspects:

- Model: A set of classes, which are basically the data you’re working with along with the business logic and rules that describes how the data can be manipulated.
- View: It defines the UI of the application, in other words it’s the representation of the data that model contains.
- Controller: A set of classes that handles user input and  acts upon the model to generate required view.

![image](https://i.ibb.co/K5kBhy6/MVC-Diagram.jpg)

If you look at the above diagram, In a typical MVC design pattern. The end User interacts with the View, which is basically the UI layer. Upon the user action e.a user clicks any button or mouse hover event, the View invokes corresponding Controller. The controller then determines the Model and updates it as per the requirement. Once the Model is updated then the Controller generates the View and updates it for the end user.

## Prerequisites

A .NET Core application can be developed using these IDEs:

- Visual Studio
- Visual Studio Code
- Command Prompt

Here, I am using Visual Studio to build the application. Be sure that the necessary software is installed:

- Visual Studio 2019
- NET Core 3.1
- SQL Server >= 2017

## Create database

Let’s create a database on your local SQL Server. I hope you have installed SQL Server 2017 in your machine (you can use SQL Server 2008, 2012, 2016, or 2019, as well).

**Step 1**: Create new Database called (**Inventory**) with SQL Server or Visual studio

**Step 2**: For this application, I am going to create a table called **Products** with basic attributes. Paste the following SQL query into the Query window to create a **Products** table.

``` SQL
Create Table Products(
ProductId Int Identity(1,1) Primary Key,
Name Varchar(100) Not Null,
Category Varchar(100),
Color Varchar(20),
UnitPrice Decimal Not Null,
AvailableQuantity Int Not Null
)
```

**Step3**: **Run** above command

## Create an ASP.NET Core application

Follow these steps to create an ASP.NET Core application.

**Step 1**: In Visual Studio 2019, click on File -> New -> Project.

**Step 2**: Choose the Create a new project option.

**Step 3**: Select the ASP.NET Core Web Application template. ![image](https://i.ibb.co/cvW1X71/image.png)

**Step 4**:  Enter project name and click Create.

**Step 5**: Select .NET Core and ASP.NET Core 3.1 and choose the Web Application (Model-View-Controller) template. ![image](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/Create-a-new-ASP.NET-Core-Web-application.png) Click Create. Then the sample ASP.NET Core application will be created with this project structure.
## Install NuGet packages

The following NuGet packages should be added to work with the SQL Server database and scaffolding. Run these commands in **Package Manager Console**:

- Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design -Version 3.1.4
- Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.1.8
- Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.8

## Scaffolding

ASP.NET Core has a feature called scaffolding, which uses T4 templates to generate code of common functionalities to help keep developers from writing repeat code. We use scaffolding to perform the following operations:

- Generate entity POCO classes and a context class for the database.
- Generate code for create, read, update, and delete (CRUD) operations of the database model using Entity Framework Core, which includes **controllers** and **views**.

## Connect application with database

Run the following scaffold command in **Package Manager Console** to reverse engineer the database to create database context and entity POCO classes from tables. The scaffold command will create POCO class only for the tables that have a primary key.

```
Scaffold-DbContext "Server=********;Database=Inventory;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```

- **Connection**—Sets connection string of the database.
- **Provider**—Sets which provider to use to connect database.
- **OutputDir**—Sets the directory where the POCO classes are to be generated.

In our case, the Products class and Inventory context class will be created. 

![image](https://i.ibb.co/P6wtcPD/image.png)

Open the Inventory Context class file. You will see the database credentials are hard coded in the **OnConfiguring** method.

It’s not good practice to have SQL Server credentials in C# class, considering the security issues. So, remove this **OnConfiguring** method from context file. ![image](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/OnConfiguring-method.png)

And move the connection string to the appsettings.json file.

``` Json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "InventoryDatabase": "Server=*****;Database=Inventory;Integrated Security=True;"
  }
}
```

Then we can register the database context service (**InventoryContext**) during application startup. In the following code, the connection string is read from the **appsettings** file and passed to the context service.

``` C#
// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    var connection = Configuration.GetConnectionString("InventoryDatabase");
    services.AddDbContext<InventoryContext>(options => options.UseSqlServer(connection));
    services.AddControllersWithViews();
}
```

Then this context service is injected with the required controllers via dependency injection.

### Perform CRUD operations

Now we set up the database and configure it to work with Entity Framework Core. We’ll see how to perform CRUD operations.

Right-click on the controller folder, select **add new item**, and then select **controller**. Then this dialog will be displayed. ![image](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/Adding-MVC-Controller-with-views-1.png)
Select the **MVC Controller with views, using Entity Framework** option and click Add.

We need to choose a database **model class** and **data context class**, which were created earlier, and click **Add**. ![image](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/Adding-model-and-context-calss-1.png)

That’s it, we’re done. The scaffolding engine uses T4 templates to generate code for controller actions and views in their respective folders. This is the basic version of code; we can modify it as needed.

Please find the files created, 
![image](https://i.ibb.co/LZTL766/image.png)

Now we have fully functional CRUD operations on the Products table.

Then, change the default application route to load the **Products Controller** instead of the home controller. Open the **Startup.cs** file and under the Configure method, change the default controller to **Products**. ![image](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/Updating-startup-page-1.png)
With the help of the scaffolding engine, developers need not write CRUD operations for each database model.

## Run application

Click **Run** to view the application. A new browser tab will open and we’ll be able to see the product listing page. Since there is no product in the inventory, it’s empty. ![image](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/Empty-Product-Listing-1.png)

Click **Create New** to add new products to the inventory.![imgage](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/Adding-new-product-to-inventory-1.png)

After entering the details, click **Create**. Now we should see newly created products in the listing page as in the following screenshot. I have added three more products. ![image](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/Product-Listing-updated-1.png)

Click **Details** to view the product details. ![image](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/Details-of-the-product-2.png)

Click **Edit** to update product details. ![image](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/Editing-the-product-2.png)

Click **Delete** to delete a product. Confirmation will be requested before it’s deleted from the database. ![image](https://www.syncfusion.com/blogs/wp-content/uploads/2019/09/Deleting-the-product-1.png)

Without writing a single line of code, we are able to create an application with basic CRUD operations with the help of the scaffolding engine.

## Conclusion

In this blog, we have learned how to create an ASP.NET Core application and connect it to a database to perform basic CRUD operations using Entity Framework Core 3.1 and a code generation tool. I hope it was useful.
