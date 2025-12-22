namespace Roadbed.Crud.Entities.Operations;

/// <summary>
/// Interface defining the Update Operation for an Entity.
/// </summary>
/// <typeparam name="TDtoType">Type of Data Transfer Object (DTO) object.</typeparam>
/// <typeparam name="TIdType">Data type for the ID.</typeparam>
public interface IRepositoryOperationUpdate<in TDtoType, TIdType>
        where TDtoType : IDataTransferObject<TIdType>
{
    #region Public Methods

    /// <summary>
    /// Create Operation for the entity.
    /// </summary>
    /// <param name="dto">Data Transfer Object (DTO) object.</param>
    /// <param name="cancellationToken">Token to notify when an operation should be canceled.</param>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    Task UpdateAsync(TDtoType dto, CancellationToken cancellationToken);

    #endregion Public Methods
}
