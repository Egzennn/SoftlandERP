using SoftlandERP.Data.Entities.Vocabularies.Staff;

namespace SoftlandERP.Core.Repositories.Interfaces
{
    public interface IUserAddressRepository : IRepository<UserAddress>
    {
        Task<UserAddress?> FindAddressByCountryCodeAsync(string? countryCode, string? city, string? street);

        Task<UserAddress?> FindAddressByCountryAsync(string? country, string? city, string? street);

        Task<IEnumerable<string?>?> FindCitiesByCountryAsync(string? country);

        Task<IEnumerable<string?>?> FindStreetsByCityAsync(string? city);
    }
}