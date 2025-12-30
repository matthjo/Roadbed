namespace Roadbed.Test.Unit.Messaging;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Messaging;

/// <summary>
/// Unit tests for the MessagingMessageResponse class.
/// </summary>
[TestClass]
public class MessagingMessageResponseTests
{
    #region Public Methods

    /// <summary>
    /// Verifies that the constructor with all parameters accepts empty string for data.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_AcceptsEmptyStringData()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = "test-identifier-123";
        string data = string.Empty;

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data);

        // Assert
        Assert.AreEqual(string.Empty, response.Data);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters accepts empty string for identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_AcceptsEmptyStringIdentifier()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = string.Empty;
        string data = "Test response data";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data);

        // Assert
        Assert.AreEqual(string.Empty, response.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters accepts empty string for typeCodename.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_AcceptsEmptyStringTypeCodename()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = string.Empty;
        string identifier = "test-identifier-123";
        string data = "Test response data";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data);

        // Assert
        Assert.AreEqual(string.Empty, response.MessageTypeCodename);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters accepts null for data.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_AcceptsNullData()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = "test-identifier-123";
        string? data = null;

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data!);

        // Assert
        Assert.IsNull(response.Data);
        Assert.AreEqual(identifier, response.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters accepts null for identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_AcceptsNullIdentifier()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string? identifier = null;
        string data = "Test response data";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier!, data);

        // Assert
        Assert.IsNull(response.Identifier);
        Assert.AreEqual(data, response.Data);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters accepts null for typeCodename.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_AcceptsNullTypeCodename()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string? typeCodename = null;
        string identifier = "test-identifier-123";
        string data = "Test response data";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename!, identifier, data);

        // Assert
        Assert.IsNull(response.MessageTypeCodename);
        Assert.AreEqual(identifier, response.Identifier);
        Assert.AreEqual(data, response.Data);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters accepts special characters in parameters.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_AcceptsSpecialCharacters()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message-response_v1.0";
        string identifier = "test-id_123!@#";
        string data = "Data with special chars: !@#$%";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data);

        // Assert
        Assert.AreEqual(typeCodename, response.MessageTypeCodename);
        Assert.AreEqual(identifier, response.Identifier);
        Assert.AreEqual(data, response.Data);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters accepts a valid ULID as the identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_AcceptsValidUlidIdentifier()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = Ulid.NewUlid().ToString();
        string data = "Test response data";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data);

        // Assert
        Assert.AreEqual(identifier, response.Identifier);
        Assert.IsTrue(Ulid.TryParse(response.Identifier, out _));
    }

    /// <summary>
    /// Verifies that the constructor with all parameters leaves OriginalRequestIdentifier null.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_LeavesOriginalRequestIdentifierNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = "test-identifier-123";
        string data = "Test response data";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data);

        // Assert
        Assert.IsNull(response.OriginalRequestIdentifier);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters sets all properties correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_SetsAllProperties()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = "test-identifier-123";
        string data = "Test response data";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data);

        // Assert
        Assert.AreEqual(publisher, response.Publisher);
        Assert.AreEqual(typeCodename, response.MessageTypeCodename);
        Assert.AreEqual(identifier, response.Identifier);
        Assert.AreEqual(data, response.Data);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters sets CreatedOn to current UTC time.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_SetsCreatedOnToUtcNow()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = "test-identifier-123";
        string data = "Test response data";
        var beforeCreation = DateTimeOffset.UtcNow;

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data);
        var afterCreation = DateTimeOffset.UtcNow;

        // Assert
        Assert.IsTrue(response.CreatedOn >= beforeCreation);
        Assert.IsTrue(response.CreatedOn <= afterCreation);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters sets SourceCreatedOn to current UTC time.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_SetsSourceCreatedOnToUtcNow()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = "test-identifier-123";
        string data = "Test response data";
        var beforeCreation = DateTimeOffset.UtcNow;

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data);
        var afterCreation = DateTimeOffset.UtcNow;

        // Assert
        Assert.IsTrue(response.SourceCreatedOn >= beforeCreation);
        Assert.IsTrue(response.SourceCreatedOn <= afterCreation);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters works with complex generic types.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParameters_WorksWithComplexGenericType()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = "test-identifier-123";
        var data = new ResponseData { Success = true, Message = "Operation successful", StatusCode = 200 };

        // Act
        var response = new MessagingMessageResponse<ResponseData>(publisher, typeCodename, identifier, data);

        // Assert
        Assert.AreEqual(publisher, response.Publisher);
        Assert.AreEqual(typeCodename, response.MessageTypeCodename);
        Assert.AreEqual(identifier, response.Identifier);
        Assert.AreEqual(data, response.Data);
        Assert.IsTrue(response.Data!.Success);
        Assert.AreEqual("Operation successful", response.Data.Message);
        Assert.AreEqual(200, response.Data.StatusCode);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters including createdOn leaves OriginalRequestIdentifier null.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParametersIncludingCreatedOn_LeavesOriginalRequestIdentifierNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = "test-identifier-123";
        string data = "Test response data";
        var sourceCreatedOn = DateTimeOffset.UtcNow.AddHours(-1);

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data, sourceCreatedOn);

        // Assert
        Assert.IsNull(response.OriginalRequestIdentifier);
    }

    /// <summary>
    /// Verifies that the constructor with all parameters including createdOn preserves timezone information in sourceCreatedOn.
    /// </summary>
    [TestMethod]
    public void Constructor_WithAllParametersIncludingCreatedOn_PreservesTimezone()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        string identifier = "test-identifier-123";
        string data = "Test response data";
        var sourceCreatedOn = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.FromHours(-5));

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename, identifier, data, sourceCreatedOn);

        // Assert
        Assert.AreEqual(sourceCreatedOn, response.SourceCreatedOn);
        Assert.AreEqual(TimeSpan.FromHours(-5), response.SourceCreatedOn!.Value.Offset);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter generates a non-empty Identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_GeneratesNonEmptyIdentifier()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response = new MessagingMessageResponse<string>(publisher);

        // Assert
        Assert.IsFalse(string.IsNullOrWhiteSpace(response.Identifier));
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter generates a non-null Identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_GeneratesNonNullIdentifier()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response = new MessagingMessageResponse<string>(publisher);

        // Assert
        Assert.IsNotNull(response.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter generates unique Identifiers for multiple instances.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_GeneratesUniqueIdentifiers()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response1 = new MessagingMessageResponse<string>(publisher);
        var response2 = new MessagingMessageResponse<string>(publisher);

        // Assert
        Assert.AreNotEqual(response1.Identifier, response2.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter generates a valid ULID format for Identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_GeneratesValidUlidIdentifier()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response = new MessagingMessageResponse<string>(publisher);

        // Assert
        Assert.IsTrue(Ulid.TryParse(response.Identifier, out _));
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter leaves Data property null.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_LeavesDataNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response = new MessagingMessageResponse<string>(publisher);

        // Assert
        Assert.IsNull(response.Data);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter leaves MessageTypeCodename property null.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_LeavesMessageTypeCodenameNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response = new MessagingMessageResponse<string>(publisher);

        // Assert
        Assert.IsNull(response.MessageTypeCodename);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter leaves OriginalRequestIdentifier property null.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_LeavesOriginalRequestIdentifierNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response = new MessagingMessageResponse<string>(publisher);

        // Assert
        Assert.IsNull(response.OriginalRequestIdentifier);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter sets CreatedOn to a non-null value.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_SetsCreatedOnToNonNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response = new MessagingMessageResponse<string>(publisher);

        // Assert
        Assert.IsNotNull(response.CreatedOn);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter sets CreatedOn to current UTC time.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_SetsCreatedOnToUtcNow()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var beforeCreation = DateTimeOffset.UtcNow;

        // Act
        var response = new MessagingMessageResponse<string>(publisher);
        var afterCreation = DateTimeOffset.UtcNow;

        // Assert
        Assert.IsTrue(response.CreatedOn >= beforeCreation);
        Assert.IsTrue(response.CreatedOn <= afterCreation);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter sets the Publisher property correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_SetsPublisherProperty()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response = new MessagingMessageResponse<string>(publisher);

        // Assert
        Assert.AreEqual(publisher, response.Publisher);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter sets SourceCreatedOn to a non-null value.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_SetsSourceCreatedOnToNonNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response = new MessagingMessageResponse<string>(publisher);

        // Assert
        Assert.IsNotNull(response.SourceCreatedOn);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter sets SourceCreatedOn to current UTC time.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_SetsSourceCreatedOnToUtcNow()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var beforeCreation = DateTimeOffset.UtcNow;

        // Act
        var response = new MessagingMessageResponse<string>(publisher);
        var afterCreation = DateTimeOffset.UtcNow;

        // Assert
        Assert.IsTrue(response.SourceCreatedOn >= beforeCreation);
        Assert.IsTrue(response.SourceCreatedOn <= afterCreation);
    }

    /// <summary>
    /// Verifies that the constructor with publisher parameter works with complex generic types.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisher_WorksWithComplexGenericType()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");

        // Act
        var response = new MessagingMessageResponse<ResponseData>(publisher);

        // Assert
        Assert.IsNotNull(response);
        Assert.AreEqual(publisher, response.Publisher);
        Assert.IsNotNull(response.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with publisher and typeCodename parameters accepts codenames with special characters.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisherAndTypeCodename_AcceptsCodenameWithSpecialCharacters()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message-response_v1";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename);

        // Assert
        Assert.AreEqual(typeCodename, response.MessageTypeCodename);
    }

    /// <summary>
    /// Verifies that the constructor with publisher and typeCodename parameters accepts empty string for typeCodename.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisherAndTypeCodename_AcceptsEmptyStringTypeCodename()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = string.Empty;

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename);

        // Assert
        Assert.AreEqual(string.Empty, response.MessageTypeCodename);
        Assert.IsNotNull(response.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with publisher and typeCodename parameters accepts null for typeCodename.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisherAndTypeCodename_AcceptsNullTypeCodename()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string? typeCodename = null;

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename!);

        // Assert
        Assert.IsNull(response.MessageTypeCodename);
        Assert.IsNotNull(response.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with publisher and typeCodename parameters generates unique Identifiers for multiple instances.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisherAndTypeCodename_GeneratesUniqueIdentifiers()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";

        // Act
        var response1 = new MessagingMessageResponse<string>(publisher, typeCodename);
        var response2 = new MessagingMessageResponse<string>(publisher, typeCodename);

        // Assert
        Assert.AreNotEqual(response1.Identifier, response2.Identifier);
    }

    /// <summary>
    /// Verifies that the constructor with publisher and typeCodename parameters generates a valid ULID Identifier.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisherAndTypeCodename_GeneratesValidUlidIdentifier()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename);

        // Assert
        Assert.IsTrue(Ulid.TryParse(response.Identifier, out _));
    }

    /// <summary>
    /// Verifies that the constructor with publisher and typeCodename parameters leaves Data property null.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisherAndTypeCodename_LeavesDataNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename);

        // Assert
        Assert.IsNull(response.Data);
    }

    /// <summary>
    /// Verifies that the constructor with publisher and typeCodename parameters leaves OriginalRequestIdentifier null.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisherAndTypeCodename_LeavesOriginalRequestIdentifierNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename);

        // Assert
        Assert.IsNull(response.OriginalRequestIdentifier);
    }

    /// <summary>
    /// Verifies that the constructor with publisher and typeCodename parameters sets both properties correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisherAndTypeCodename_SetsBothProperties()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename);

        // Assert
        Assert.AreEqual(publisher, response.Publisher);
        Assert.AreEqual(typeCodename, response.MessageTypeCodename);
    }

    /// <summary>
    /// Verifies that the constructor with publisher and typeCodename parameters sets CreatedOn to current UTC time.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisherAndTypeCodename_SetsCreatedOnToUtcNow()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        var beforeCreation = DateTimeOffset.UtcNow;

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename);
        var afterCreation = DateTimeOffset.UtcNow;

        // Assert
        Assert.IsTrue(response.CreatedOn >= beforeCreation);
        Assert.IsTrue(response.CreatedOn <= afterCreation);
    }

    /// <summary>
    /// Verifies that the constructor with publisher and typeCodename parameters sets SourceCreatedOn to current UTC time.
    /// </summary>
    [TestMethod]
    public void Constructor_WithPublisherAndTypeCodename_SetsSourceCreatedOnToUtcNow()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        string typeCodename = "test.message.response";
        var beforeCreation = DateTimeOffset.UtcNow;

        // Act
        var response = new MessagingMessageResponse<string>(publisher, typeCodename);
        var afterCreation = DateTimeOffset.UtcNow;

        // Assert
        Assert.IsTrue(response.SourceCreatedOn >= beforeCreation);
        Assert.IsTrue(response.SourceCreatedOn <= afterCreation);
    }

    /// <summary>
    /// Verifies that the Data property can be updated after construction.
    /// </summary>
    [TestMethod]
    public void DataProperty_SetValue_UpdatesProperty()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var response = new MessagingMessageResponse<string>(publisher);
        string newData = "Updated response data";

        // Act
        response.Data = newData;

        // Assert
        Assert.AreEqual(newData, response.Data);
    }

    /// <summary>
    /// Verifies that the MessageTypeCodename property can be updated after construction.
    /// </summary>
    [TestMethod]
    public void MessageTypeCodenameProperty_SetValue_UpdatesProperty()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var response = new MessagingMessageResponse<string>(publisher);
        string newTypeCodename = "updated.message.type";

        // Act
        response.MessageTypeCodename = newTypeCodename;

        // Assert
        Assert.AreEqual(newTypeCodename, response.MessageTypeCodename);
    }

    /// <summary>
    /// Verifies that the OriginalRequestIdentifier property can be set to an empty string.
    /// </summary>
    [TestMethod]
    public void OriginalRequestIdentifierProperty_SetToEmptyString_AcceptsEmptyString()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var response = new MessagingMessageResponse<string>(publisher);

        // Act
        response.OriginalRequestIdentifier = string.Empty;

        // Assert
        Assert.AreEqual(string.Empty, response.OriginalRequestIdentifier);
    }

    /// <summary>
    /// Verifies that the OriginalRequestIdentifier property can be set to null.
    /// </summary>
    [TestMethod]
    public void OriginalRequestIdentifierProperty_SetToNull_AcceptsNull()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var response = new MessagingMessageResponse<string>(publisher);
        response.OriginalRequestIdentifier = "original-request-123";

        // Act
        response.OriginalRequestIdentifier = null;

        // Assert
        Assert.IsNull(response.OriginalRequestIdentifier);
    }

    /// <summary>
    /// Verifies that the OriginalRequestIdentifier property can be set to a ULID value.
    /// </summary>
    [TestMethod]
    public void OriginalRequestIdentifierProperty_SetToUlid_AcceptsUlid()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var response = new MessagingMessageResponse<string>(publisher);
        string originalRequestId = Ulid.NewUlid().ToString();

        // Act
        response.OriginalRequestIdentifier = originalRequestId;

        // Assert
        Assert.AreEqual(originalRequestId, response.OriginalRequestIdentifier);
        Assert.IsTrue(Ulid.TryParse(response.OriginalRequestIdentifier, out _));
    }

    /// <summary>
    /// Verifies that the OriginalRequestIdentifier property can be set to a valid value.
    /// </summary>
    [TestMethod]
    public void OriginalRequestIdentifierProperty_SetValue_UpdatesProperty()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var response = new MessagingMessageResponse<string>(publisher);
        string originalRequestId = "original-request-123";

        // Act
        response.OriginalRequestIdentifier = originalRequestId;

        // Assert
        Assert.AreEqual(originalRequestId, response.OriginalRequestIdentifier);
    }

    /// <summary>
    /// Verifies that the OriginalRequestIdentifier property accepts special characters.
    /// </summary>
    [TestMethod]
    public void OriginalRequestIdentifierProperty_SetWithSpecialCharacters_AcceptsValue()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var response = new MessagingMessageResponse<string>(publisher);
        string originalRequestId = "request-id_123!@#";

        // Act
        response.OriginalRequestIdentifier = originalRequestId;

        // Assert
        Assert.AreEqual(originalRequestId, response.OriginalRequestIdentifier);
    }

    /// <summary>
    /// Verifies that the OriginalRequestIdentifier property can be updated multiple times.
    /// </summary>
    [TestMethod]
    public void OriginalRequestIdentifierProperty_UpdateMultipleTimes_RetainsLatestValue()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var response = new MessagingMessageResponse<string>(publisher);

        // Act
        response.OriginalRequestIdentifier = "first-request-id";
        response.OriginalRequestIdentifier = "second-request-id";
        response.OriginalRequestIdentifier = "third-request-id";

        // Assert
        Assert.AreEqual("third-request-id", response.OriginalRequestIdentifier);
    }

    /// <summary>
    /// Verifies that the Publisher property can be updated after construction.
    /// </summary>
    [TestMethod]
    public void PublisherProperty_SetValue_UpdatesProperty()
    {
        // Arrange
        var initialPublisher = new MessagingPublisher("InitialPublisher");
        var response = new MessagingMessageResponse<string>(initialPublisher);
        var newPublisher = new MessagingPublisher("NewPublisher");

        // Act
        response.Publisher = newPublisher;

        // Assert
        Assert.AreEqual(newPublisher, response.Publisher);
        Assert.AreNotEqual(initialPublisher, response.Publisher);
    }

    /// <summary>
    /// Verifies that the SourceCreatedOn property can be updated after construction.
    /// </summary>
    [TestMethod]
    public void SourceCreatedOnProperty_SetValue_UpdatesProperty()
    {
        // Arrange
        var publisher = new MessagingPublisher("TestPublisher");
        var response = new MessagingMessageResponse<string>(publisher);
        var newSourceCreatedOn = DateTimeOffset.UtcNow.AddDays(-1);

        // Act
        response.SourceCreatedOn = newSourceCreatedOn;

        // Assert
        Assert.AreEqual(newSourceCreatedOn, response.SourceCreatedOn);
    }

    #endregion Public Methods

    #region Private Classes

    /// <summary>
    /// Response data class for testing response payloads.
    /// </summary>
    private class ResponseData
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether that response was successful.
        /// </summary>
        public bool Success { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// Complex test data class for testing with custom types.
    /// </summary>
    private class TestData
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int Value { get; set; }

        #endregion Public Properties
    }

    #endregion Private Classes
}