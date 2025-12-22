namespace Roadbed.Crud.Entities.Operations;

/// <summary>
/// Interface defining the Read Operation for an Entity.
/// </summary>
/// <typeparam name="TDtoType">Type of Data Transfer Object (DTO) object.</typeparam>
/// <typeparam name="TIdType">Data type for the ID.</typeparam>
public interface IRepositoryOperationRead<TDtoType, in TIdType>
        where TDtoType : IDataTransferObject<TIdType>
{
    #region Public Methods

    /// <summary>
    /// Get Operation for the entity.
    /// </summary>
    /// <param name="id">ID of the entity.</param>
    /// <param name="cancellationToken">Token to notify when an operation should be canceled.</param>
    /// <returns>Data Transfer Object (DTO) object.</returns>
    Task<TDtoType> ReadAsync(TIdType id, CancellationToken cancellationToken);

    #endregion Public Methods
}
