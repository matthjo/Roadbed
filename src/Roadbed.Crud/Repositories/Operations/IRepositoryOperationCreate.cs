namespace Roadbed.Crud.Entities.Operations;

/// <summary>
/// Interface defining the Create Operation for an Entity.
/// </summary>
/// <typeparam name="TDtoType">Type of Data Transfer Object (DTO) object.</typeparam>
/// <typeparam name="TIdType">Data type for the ID.</typeparam>
public interface IRepositoryOperationCreate<in TDtoType, TIdType>
        where TDtoType : IDataTransferObject<TIdType>
{
    #region Public Methods

    /// <summary>
    /// Create Operation for the entity.
    /// </summary>
    /// <param name="dto">Data Transfer Object (DTO) object.</param>
    /// <param name="cancellationToken">Token to notify when an operation should be canceled.</param>
    /// <returns>ID of the Data Transfer Object (DTO) object.</returns>
    Task<TIdType> CreateAsync(TDtoType dto, CancellationToken cancellationToken);

    #endregion Public Methods
}
