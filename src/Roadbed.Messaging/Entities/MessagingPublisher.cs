/*
 * The namespace Roadbed.Messaging.Entities was removed on purpose and replaced with Roadbed.Messaging so that no additional using statements are required.
 */

namespace Roadbed.Messaging;

/// <summary>
/// Entity for Message Publisher related operations.
/// </summary>
public class MessagingPublisher
{
    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MessagingPublisher"/> class.
    /// </summary>
    /// <remarks>
    /// The identifier is generated as a new ULID.
    /// </remarks>
    public MessagingPublisher()
    {
        this.Identifier = Ulid.NewUlid().ToString();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MessagingPublisher"/> class.
    /// </summary>
    /// <param name="name">Name of the publisher.</param>
    public MessagingPublisher(string name)
    {
        this.Name = name;
        this.Identifier = Ulid.NewUlid().ToString();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MessagingPublisher"/> class.
    /// </summary>
    /// <param name="name">Name of the publisher.</param>
    /// <param name="identifier">Unique identifier for the publisher.</param>
    public MessagingPublisher(string name, string identifier)
    {
        this.Name = name;
        this.Identifier = identifier;
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Gets or sets the attribute value.
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Gets or sets the attribute key.
    /// </summary>
    public string? Name { get; set; }

    #endregion Public Properties
}