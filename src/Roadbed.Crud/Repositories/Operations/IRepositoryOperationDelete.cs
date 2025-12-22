namespace Roadbed.Crud.Entities.Operations;

#pragma warning disable S2326 // Type parameter used as part of the Where clause
/// <summary>
/// Interface defining the Delete Operation for an Entity.
/// </summary>
/// <typeparam name="TDtoType">Type of Data Transfer Object (DTO) object.</typeparam>
/// <typeparam name="TIdType">Data type for the ID.</typeparam>
public interface IEntityOperatioDelete<TDtoType, in TIdType>
        where TDtoType : IDataTransferObject<TIdType>
{
    #region Public Methods

    /// <summary>
    /// Create Operation for the entity.
    /// </summary>
    /// <param name="id">ID of the Data Transfer Object (DTO) object.</param>
    /// <param name="cancellationToken">Token to notify when an operation should be canceled.</param>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    Task DeleteAsync(TIdType id, CancellationToken cancellationToken);

    #endregion Public Methods
}
#pragma warning restore S2326 // Unused type parameters should be removed
