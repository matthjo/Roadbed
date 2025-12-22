/*
 * The namespace Roadbed.Crud.Entities was removed on purpose and replaced with Roadbed.Crud so that no additional using statements are required.
 */
namespace Roadbed.Crud;

using System.Collections.Generic;

/// <summary>
/// Data Transfer Object (DTO) Entity With An ID.
/// </summary>
/// <typeparam name="TIdDataType">Data type for the ID.</typeparam>
public interface IDataTransferObject<TIdDataType>
{
    /// <summary>
    /// Gets the ID of the Data Transfer Object (DTO) entity.
    /// </summary>
    TIdDataType? Id
    {
        get;
    }

    /// <summary>
    /// Gets the error messages for the Data Transfer Object (DTO) entity.
    /// </summary>
    List<string>? Errors
    {
        get;
    }
}