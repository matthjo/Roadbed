namespace Roadbed.Crud.Repositories;

using Roadbed.Crud.Entities.Operations;

/// <summary>
/// Entity contract for the List only operation.
/// </summary>
/// <typeparam name="TDtoType">Type of Data Transfer Object (DTO) object.</typeparam>
/// <typeparam name="TIdType">Data type for the ID.</typeparam>
public interface IBaseRepositoryWithListOnly<TDtoType, TIdType>
        : IRepositoryOperationList<TDtoType, TIdType>
        where TDtoType : IDataTransferObject<TIdType>
{
}