# Unit Testing Standards for C# Projects

## Test Naming Convention
All unit tests must follow the Roy Osherove naming pattern:
```
{UnitOfWork}_{StateUnderTest}_{ExpectedBehavior}
```

### Rules
1. **Alphanumeric Characters**: Use only alphanumeric characters (a-z, A-Z, 0-9) and underscores
2. **Pascal Case**: All alphabetic characters should be Pascal case (Upper Camel Case)
3. **Three Sections**: Separated by underscores

### Examples
- `Constructor_NoParameters_InitializesWithDefaultValues`
- `MakeRequestAsync_NullRequest_ThrowsArgumentNullException`
- `ConnectionString_SqliteType_GeneratesCorrectConnectionString`

## Test Structure

### XML Documentation
Every test method must have XML documentation:
```csharp
/// <summary>
/// Unit test to verify that [description of what is being tested].
/// </summary>
[TestMethod]
public void MethodName_Scenario_ExpectedResult()
```

### Arrange/Act/Assert Pattern
Use the AAA pattern with Given/When/Then comments:
```csharp
[TestMethod]
public void Method_Scenario_Result()
{
    // Arrange (Given)
    var input = "test";
    
    // Act (When)
    var result = SomeMethod(input);
    
    // Assert (Then)
    Assert.AreEqual(
        expectedValue,
        result,
        "Detailed assertion message explaining what should happen.");
}
```

### Region Directives
Organize code with regions:
```csharp
[TestClass]
public class MyClassTests
{
    #region Public Methods
    
    [TestMethod]
    public void Test1() { }
    
    [TestMethod]
    public void Test2() { }
    
    #endregion Public Methods
    
    #region Private Methods
    
    private void HelperMethod() { }
    
    #endregion Private Methods
}
```

### Detailed Assertion Messages
Every Assert must include a descriptive message:
```csharp
Assert.IsNotNull(
    result,
    "Result should not be null when valid input is provided.");
    
Assert.AreEqual(
    expected,
    actual,
    "Property should return the value that was set.");
```

## Exception Testing

**IMPORTANT**: Use try/catch blocks. `Assert.ThrowsException` does not exist in this MSTest version.

### Synchronous Exception Testing
```csharp
/// <summary>
/// Unit test to verify that method throws exception when parameter is null.
/// </summary>
[TestMethod]
public void Method_NullParameter_ThrowsArgumentNullException()
{
    // Arrange (Given)
    object? nullParam = null;
    bool exceptionThrown = false;

    // Act (When)
    try
    {
        SomeMethod(nullParam!);
    }
    catch (ArgumentNullException)
    {
        exceptionThrown = true;
    }

    // Assert (Then)
    Assert.IsTrue(
        exceptionThrown,
        "Method should throw ArgumentNullException when parameter is null.");
}
```

### Alternative with Parameter Validation
```csharp
[TestMethod]
public void Method_NullParameter_ThrowsArgumentNullException()
{
    // Arrange (Given)
    object? nullParam = null;
    ArgumentNullException? caughtException = null;

    // Act (When)
    try
    {
        SomeMethod(nullParam!);
    }
    catch (ArgumentNullException ex)
    {
        caughtException = ex;
    }

    // Assert (Then)
    Assert.IsNotNull(
        caughtException,
        "Method should throw ArgumentNullException when parameter is null.");
    Assert.AreEqual(
        "parameterName",
        caughtException.ParamName,
        "Exception should indicate correct parameter name.");
}
```

### Async Exception Testing
```csharp
/// <summary>
/// Unit test to verify that async method throws exception when parameter is null.
/// </summary>
[TestMethod]
public async Task MethodAsync_NullParameter_ThrowsArgumentNullException()
{
    // Arrange (Given)
    object? nullParam = null;
    bool exceptionThrown = false;

    // Act (When)
    try
    {
        await SomeMethodAsync(nullParam!);
    }
    catch (ArgumentNullException)
    {
        exceptionThrown = true;
    }

    // Assert (Then)
    Assert.IsTrue(
        exceptionThrown,
        "MethodAsync should throw ArgumentNullException when parameter is null.");
}
```

## Record Type Testing

When creating new instances of records, use **object initializer syntax** (not `with` expressions):

### Correct Approach
```csharp
[TestMethod]
public void CreateNewInstance_ModifyProperty_CreatesNewRecord()
{
    // Arrange (Given)
    var original = new MyRecord("Name", "Value");
    string newValue = "NewValue";

    // Act (When)
    var modified = new MyRecord
    {
        Name = original.Name,
        Value = newValue
    };

    // Assert (Then)
    Assert.AreEqual(
        original.Name,
        modified.Name,
        "Name should remain the same.");
    Assert.AreEqual(
        newValue,
        modified.Value,
        "Value should have the new value.");
}
```

### Avoid This
```csharp
// Don't use 'with' expressions
var modified = original with { Value = newValue };
```

## Class Structure Example
```csharp
namespace Roadbed.Test.Unit.SomeNamespace;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.SomeNamespace;
using System;

/// <summary>
/// Contains unit tests for verifying the behavior of the MyClass class.
/// </summary>
[TestClass]
public class MyClassTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that constructor initializes properties correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_NoParameters_InitializesWithDefaultValues()
    {
        // Arrange (Given)

        // Act (When)
        var instance = new MyClass();

        // Assert (Then)
        Assert.IsNotNull(
            instance,
            "Instance should be created successfully.");
    }

    /// <summary>
    /// Unit test to verify that property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void Property_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var instance = new MyClass();
        string expectedValue = "test";

        // Act (When)
        instance.Property = expectedValue;

        // Assert (Then)
        Assert.AreEqual(
            expectedValue,
            instance.Property,
            "Property should return the value that was set.");
    }

    #endregion Public Methods
}
```

## Key Principles

1. **One assertion concept per test** (but multiple Assert statements are OK if testing the same concept)
2. **Test behavior, not implementation**
3. **Tests should be independent** - no shared state between tests
4. **Clear, descriptive names** - the test name should explain what's being tested
5. **Comprehensive coverage** - test happy paths, edge cases, and error conditions
6. **Consistent formatting** - follow the patterns above exactly

## Framework Information

- **Test Framework**: MSTest
- **Assertion Library**: MSTest Assert class
- **.NET Version**: Modern .NET (supports nullable reference types, records, etc.)