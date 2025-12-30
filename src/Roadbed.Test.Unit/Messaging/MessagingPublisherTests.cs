namespace Roadbed.Test.Unit.Messaging;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Messaging;

/// <summary>
/// Unit tests for the MessagingPublisher class.
/// </summary>
[TestClass]
public class MessagingPublisherTests
{
    #region Public Methods

    /// <summary>
    /// Verifies that the default constructor generates an Identifier with expected ULID length of 26 characters.
    /// </summary>
    [TestMethod]
    public void Constructor_DefaultConstructor_GeneratesIdentifierWithCorrectLength()
    {
        // Act
        var publisher = new MessagingPublisher();

        // Assert
        Assert.AreEqual(26, publisher.Identifier.Length);
    }

    /// <summary>
    /// Verifies that the default constructor generates a non-empty Identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_DefaultConstructor_GeneratesNonEmptyIdentifier()
    {
        // Act
        var publisher = new MessagingPublisher();

        // Assert
        Assert.IsFalse(string.IsNullOrWhiteSpace(publisher.Identifier));
    }

    /// <summary>
    /// Verifies that the default constructor generates a non-null Identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_DefaultConstructor_GeneratesNonNullIdentifier()
    {
        // Act
        var publisher = new MessagingPublisher();

        // Assert
        Assert.IsNotNull(publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the default constructor generates unique Identifiers for multiple instances.
    /// </summary>
    [TestMethod]
    public void Constructor_DefaultConstructor_GeneratesUniqueIdentifiers()
    {
        // Act
        var publisher1 = new MessagingPublisher();
        var publisher2 = new MessagingPublisher();

        // Assert
        Assert.AreNotEqual(publisher1.Identifier, publisher2.Identifier);
    }

    /// <summary>
    /// Verifies that the default constructor generates a valid ULID format for Identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_DefaultConstructor_GeneratesValidUlidIdentifier()
    {
        // Act
        var publisher = new MessagingPublisher();

        // Assert
        Assert.IsTrue(Ulid.TryParse(publisher.Identifier, out _));
    }

    /// <summary>
    /// Verifies that the default constructor sets Name property to null.
    /// </summary>
    [TestMethod]
    public void Constructor_DefaultConstructor_SetsNameToNull()
    {
        // Act
        var publisher = new MessagingPublisher();

        // Assert
        Assert.IsNull(publisher.Name);
    }

    /// <summary>
    /// Verifies that the constructor with name and identifier parameters accepts a custom non-ULID identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameAndIdentifier_AcceptsCustomIdentifier()
    {
        // Arrange
        string name = "TestPublisher";
        string identifier = "custom-guid-12345";

        // Act
        var publisher = new MessagingPublisher(name, identifier);

        // Assert
        Assert.AreEqual(name, publisher.Name);
        Assert.AreEqual(identifier, publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name and identifier parameters accepts empty string for the identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameAndIdentifier_AcceptsEmptyStringIdentifier()
    {
        // Arrange
        string name = "TestPublisher";
        string identifier = string.Empty;

        // Act
        var publisher = new MessagingPublisher(name, identifier);

        // Assert
        Assert.AreEqual(name, publisher.Name);
        Assert.AreEqual(string.Empty, publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name and identifier parameters accepts empty string for the name.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameAndIdentifier_AcceptsEmptyStringName()
    {
        // Arrange
        string name = string.Empty;
        string identifier = "test-identifier";

        // Act
        var publisher = new MessagingPublisher(name, identifier);

        // Assert
        Assert.AreEqual(string.Empty, publisher.Name);
        Assert.AreEqual(identifier, publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name and identifier parameters accepts null for the identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameAndIdentifier_AcceptsNullIdentifier()
    {
        // Arrange
        string name = "TestPublisher";
        string? identifier = null;

        // Act
        var publisher = new MessagingPublisher(name, identifier!);

        // Assert
        Assert.AreEqual(name, publisher.Name);
        Assert.IsNull(publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name and identifier parameters accepts null for the name.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameAndIdentifier_AcceptsNullName()
    {
        // Arrange
        string? name = null;
        string identifier = "test-identifier";

        // Act
        var publisher = new MessagingPublisher(name!, identifier);

        // Assert
        Assert.IsNull(publisher.Name);
        Assert.AreEqual(identifier, publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name and identifier parameters accepts special characters in both parameters.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameAndIdentifier_AcceptsSpecialCharacters()
    {
        // Arrange
        string name = "Test-Publisher_123!";
        string identifier = "test-id_456@#";

        // Act
        var publisher = new MessagingPublisher(name, identifier);

        // Assert
        Assert.AreEqual(name, publisher.Name);
        Assert.AreEqual(identifier, publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name and identifier parameters accepts a valid ULID as the identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameAndIdentifier_AcceptsValidUlidIdentifier()
    {
        // Arrange
        string name = "TestPublisher";
        string identifier = Ulid.NewUlid().ToString();

        // Act
        var publisher = new MessagingPublisher(name, identifier);

        // Assert
        Assert.AreEqual(name, publisher.Name);
        Assert.AreEqual(identifier, publisher.Identifier);
        Assert.IsTrue(Ulid.TryParse(publisher.Identifier, out _));
    }

    /// <summary>
    /// Verifies that the constructor with name and identifier parameters sets both properties correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameAndIdentifier_SetsBothProperties()
    {
        // Arrange
        string expectedName = "TestPublisher";
        string expectedIdentifier = "test-identifier-123";

        // Act
        var publisher = new MessagingPublisher(expectedName, expectedIdentifier);

        // Assert
        Assert.AreEqual(expectedName, publisher.Name);
        Assert.AreEqual(expectedIdentifier, publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name parameter accepts empty string as a valid name value.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameParameter_AcceptsEmptyStringName()
    {
        // Arrange
        string name = string.Empty;

        // Act
        var publisher = new MessagingPublisher(name);

        // Assert
        Assert.AreEqual(string.Empty, publisher.Name);
        Assert.IsNotNull(publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name parameter accepts null as a valid name value.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameParameter_AcceptsNullName()
    {
        // Arrange
        string? name = null;

        // Act
        var publisher = new MessagingPublisher(name!);

        // Assert
        Assert.IsNull(publisher.Name);
        Assert.IsNotNull(publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name parameter accepts special characters in the name.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameParameter_AcceptsSpecialCharactersInName()
    {
        // Arrange
        string name = "Test-Publisher_123!@#";

        // Act
        var publisher = new MessagingPublisher(name);

        // Assert
        Assert.AreEqual(name, publisher.Name);
    }

    /// <summary>
    /// Verifies that the constructor with name parameter accepts whitespace as a valid name value.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameParameter_AcceptsWhitespaceName()
    {
        // Arrange
        string name = "   ";

        // Act
        var publisher = new MessagingPublisher(name);

        // Assert
        Assert.AreEqual("   ", publisher.Name);
        Assert.IsNotNull(publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name parameter generates a non-null Identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameParameter_GeneratesNonNullIdentifier()
    {
        // Arrange
        string name = "TestPublisher";

        // Act
        var publisher = new MessagingPublisher(name);

        // Assert
        Assert.IsNotNull(publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name parameter generates unique Identifiers for multiple instances.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameParameter_GeneratesUniqueIdentifiers()
    {
        // Arrange
        string name = "TestPublisher";

        // Act
        var publisher1 = new MessagingPublisher(name);
        var publisher2 = new MessagingPublisher(name);

        // Assert
        Assert.AreNotEqual(publisher1.Identifier, publisher2.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with name parameter generates a valid ULID format for Identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameParameter_GeneratesValidUlidIdentifier()
    {
        // Arrange
        string name = "TestPublisher";

        // Act
        var publisher = new MessagingPublisher(name);

        // Assert
        Assert.IsTrue(Ulid.TryParse(publisher.Identifier, out _));
    }

    /// <summary>
    /// Verifies that the constructor with name parameter sets the Name property correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNameParameter_SetsNameProperty()
    {
        // Arrange
        string expectedName = "TestPublisher";

        // Act
        var publisher = new MessagingPublisher(expectedName);

        // Assert
        Assert.AreEqual(expectedName, publisher.Name);
    }

    /// <summary>
    /// Verifies that the Identifier property can be set to an empty string.
    /// </summary>
    [TestMethod]
    public void IdentifierProperty_SetToEmptyString_AcceptsEmptyString()
    {
        // Arrange
        var publisher = new MessagingPublisher();

        // Act
        publisher.Identifier = string.Empty;

        // Assert
        Assert.AreEqual(string.Empty, publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the Identifier property can be set to null.
    /// </summary>
    [TestMethod]
    public void IdentifierProperty_SetToNull_AcceptsNull()
    {
        // Arrange
        var publisher = new MessagingPublisher();

        // Act
        publisher.Identifier = null!;

        // Assert
        Assert.IsNull(publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the Identifier property can be set to a new value.
    /// </summary>
    [TestMethod]
    public void IdentifierProperty_SetValue_UpdatesProperty()
    {
        // Arrange
        var publisher = new MessagingPublisher();
        string newIdentifier = "new-identifier-123";

        // Act
        publisher.Identifier = newIdentifier;

        // Assert
        Assert.AreEqual(newIdentifier, publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the Identifier property can be updated multiple times.
    /// </summary>
    [TestMethod]
    public void IdentifierProperty_UpdateMultipleTimes_RetainsLatestValue()
    {
        // Arrange
        var publisher = new MessagingPublisher();
        string firstId = publisher.Identifier;

        // Act
        publisher.Identifier = "SecondId";
        publisher.Identifier = "ThirdId";

        // Assert
        Assert.AreEqual("ThirdId", publisher.Identifier);
        Assert.AreNotEqual(firstId, publisher.Identifier);
    }

    /// <summary>
    /// Verifies that the Name property can be set to an empty string.
    /// </summary>
    [TestMethod]
    public void NameProperty_SetToEmptyString_AcceptsEmptyString()
    {
        // Arrange
        var publisher = new MessagingPublisher("InitialName");

        // Act
        publisher.Name = string.Empty;

        // Assert
        Assert.AreEqual(string.Empty, publisher.Name);
    }

    /// <summary>
    /// Verifies that the Name property can be set to null.
    /// </summary>
    [TestMethod]
    public void NameProperty_SetToNull_AcceptsNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("InitialName");

        // Act
        publisher.Name = null;

        // Assert
        Assert.IsNull(publisher.Name);
    }

    /// <summary>
    /// Verifies that the Name property can be set to a new value.
    /// </summary>
    [TestMethod]
    public void NameProperty_SetValue_UpdatesProperty()
    {
        // Arrange
        var publisher = new MessagingPublisher();
        string newName = "UpdatedPublisher";

        // Act
        publisher.Name = newName;

        // Assert
        Assert.AreEqual(newName, publisher.Name);
    }

    /// <summary>
    /// Verifies that the Name property can be updated multiple times.
    /// </summary>
    [TestMethod]
    public void NameProperty_UpdateMultipleTimes_RetainsLatestValue()
    {
        // Arrange
        var publisher = new MessagingPublisher("FirstName");

        // Act
        publisher.Name = "SecondName";
        publisher.Name = "ThirdName";

        // Assert
        Assert.AreEqual("ThirdName", publisher.Name);
    }

    #endregion Public Methods
}