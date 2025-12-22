namespace Roadbed.Crud.Entities.Operations;

/// <summary>
/// Interface defining the List Operation for an Entity.
/// </summary>
/// <typeparam name="TDtoType">Type of Data Transfer Object (DTO) object.</typeparam>
/// <typeparam name="TIdType">Data type for the ID.</typeparam>
public interface IRepositoryOperationList<TDtoType, in TIdType>
        where TDtoType : IDataTransferObject<TIdType>
{
    #region Public Methods

    /// <summary>
    /// List Operation for the Data Transfer Object (DTO) entity.
    /// </summary>
    /// <param name="cancellationToken">Token to notify when an operation should be canceled.</param>
    /// <returns>List of the Data Transfer Object (DTO) entites.</returns>
    Task<IList<TDtoType>> ListAsync(CancellationToken cancellationToken);

    #endregion Public Methods
}
