/*
 * The namespace Roadbed.Sdk.NationalWeatherService.Repositories was removed on purpose and replaced with Roadbed.Sdk.NationalWeatherService so that no additional using statements are required.
 */

namespace Roadbed.Sdk.NationalWeatherService;

using Roadbed.Crud;
using Roadbed.Sdk.NationalWeatherService.Dtos;

/// <summary>
/// Entity contract for the List operation using a <see cref="NwsPhysicalAddress"/>.
/// </summary>
/// <typeparam name="TDtoType">Type of Data Transfer Object (DTO) object.</typeparam>
/// <typeparam name="TIdType">Data type for the ID.</typeparam>
public interface INwsLocationRepository<TDtoType, TIdType>
        where TDtoType : IDataTransferObject<TIdType>
{
    #region Public Methods

    /// <summary>
    /// List Operation for the Data Transfer Object (DTO) entity.
    /// </summary>
    /// <param name="request">Request for National Weather Service forecast.</param>
    /// <param name="cancellationToken">Token to notify when an operation should be canceled.</param>
    /// <returns>List of the Data Transfer Object (DTO) entites.</returns>
    Task<TDtoType> ListAsync(NwsPhysicalAddress request, CancellationToken cancellationToken);

    #endregion Public Methods
}