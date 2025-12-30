/*
 * The namespace Roadbed.Common.Entities was removed on purpose and replaced with Roadbed.Common so that no additional using statements are required.
 */

namespace Roadbed.Common;

using Newtonsoft.Json.Linq;

/// <summary>
/// Entity for Common Message Requester related operations.
/// </summary>
public class CommonMessagePublisher
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommonMessagePublisher"/> class.
    /// </summary>
    public CommonMessagePublisher()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommonMessagePublisher"/> class.
    /// </summary>
    /// <param name="identifier">Unique identifier for the publisher.</param>
    public CommonMessagePublisher(string identifier)
    {
        this.Identifier = identifier;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommonMessagePublisher"/> class.
    /// </summary>
    /// <param name="identifier">Unique identifier for the publisher.</param>
    /// <param name="name">Name of the publisher.</param>
    public CommonMessagePublisher(string identifier, string name)
    {
        this.Name = name;
        this.Identifier = identifier;
    }

    #region Public Properties

    /// <summary>
    /// Gets or sets the attribute key.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the attribute value.
    /// </summary>
    required public string Identifier { get; set; }

    #endregion Public Properties
}
