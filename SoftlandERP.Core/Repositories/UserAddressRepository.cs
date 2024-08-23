using Microsoft.EntityFrameworkCore;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Vocabularies.Staff;

namespace SoftlandERP.Core.Repositories
{
    public class UserAddressRepository : Repository<UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(MainContext mainContext)
            : base(mainContext)
        {
        }

        public async Task<UserAddress?> FindAddressByCountryCodeAsync(string? countryCode, string? city, string? street)
        {
            try
            {
                return await this.mainContext.UserAddressVocabulary.Where(x => (x.TwoLetterISOCountryName ?? string.Empty) == (countryCode ?? string.Empty) && (x.City ?? string.Empty) == (city ?? string.Empty) && (x.Street ?? string.Empty) == (street ?? string.Empty)).FirstOrDefaultAsync().ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserAddress?> FindAddressByCountryAsync(string? country, string? city, string? street)
        {
            try
            {
                return await this.mainContext.UserAddressVocabulary.Where(x => (x.Country ?? string.Empty) == (country ?? string.Empty) && (x.City ?? string.Empty) == (city ?? string.Empty) && (x.Street ?? string.Empty) == (street ?? string.Empty)).FirstOrDefaultAsync().ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<string?>?> FindCitiesByCountryAsync(string? country)
        {
            try
            {
                return await this.mainContext.UserAddressVocabulary.Where(x => (x.Country ?? string.Empty) == (country ?? string.Empty)).Select(x => x.City).Distinct().ToListAsync().ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<string?>?> FindStreetsByCityAsync(string? city)
        {
            try
            {
                return await this.mainContext.UserAddressVocabulary.Where(x => (x.City ?? string.Empty) == (city ?? string.Empty)).Select(x => x.Street).Distinct().ToListAsync().ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }
    }
}