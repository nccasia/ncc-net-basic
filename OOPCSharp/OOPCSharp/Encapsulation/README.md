# Encapsulation

**Encapsulation** is the process of hiding the internal workings of our classes. That means we specify a public specification, used by consumers of our class, while the actual work is hidden away. The advantage is that a class can change how it works without needing to change its consumers.

In **C#** we have four **access modifiers keywords** which enable five ways of controlling code visibility:

- private — only visible to the containing class.
- protected — only visible to the containing class and inheritors.
- internal — only visible to classes in the same assembly.
- protected internal — only visible to the same assembly or in derived classes, even in other assemblies.
- public — visible to everyone.

Let’s say we’re building some class that runs queries on a database. Obviously we need some method of RunQuery that’s visible to every consumer of our class. The method for accessing the database could be different for every database, so perhaps we’re leaving that open for inheritors. Additionally, we use some helper class that’s only visible to our project. Last, we need to store some private state, which may not be altered from outside our class as it could leave it in an invalid state.

*Access Modifiers*:

```C#
public class QueryRunner
{
    private IDbConnection connection;

    public void RunQuery(string query)
    {
        Helper helper = new Helper();
        if (helper.Validate(query))
        {
            OpenConnection();
            // Run the query...
            CloseConnection();
        }
    }

    protected void OpenConnection()
    {
        // ...
    }

    protected void CloseConnection()
    {
        // ...
    }
}

internal class Helper
{
    internal bool Validate(string query)
    {
        // ...
        return true;
    }
}
```

If we were to compile this into an assembly and access it from another project we’d only be able to see the **QueryRunner** class. If we’d create an instance of the **QueryRunner** we could only call the RunQuery method. If we were to inherit **QueryRunner** we could also access **OpenConnection** and **CloseConnection**. The Helper class and the connection field will be forever hidden from us though.

I should mention that classes can contain nested **private** classes, classes that are only visible to the containing class. Private classes can access **private** members of their containing classes.
Likewise, an object can access **private** members of other objects of the same type.

*A private nested class*:

```C#
public class SomeClass
{
    private string someField;

    public void SomeMethod(SomeClass otherInstance)
    {
        otherInstance.someField = "Some value";
    }

    private class InnerClass
    {
        public void SomeMethod(SomeClass param)
        {
            param.someField = "Some value";
        }
    }
}
```

When omitting an access modifier a default is assigned (internal for classes and private for everything else). I’m a big fan of explicitly adding access modifiers though.

A last remark, before moving on, is that subclasses cannot have an accessibility greater than their base class. So if some class has the internal access modifier an inheriting class cannot be made public (but it could be private).
