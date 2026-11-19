using Entities;

namespace ServiceContracts.DTO
{
    public class CountryResponse
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(CountryResponse))
                return false;

            return this.CountryId == ((CountryResponse)obj!).CountryId
                                    && CountryName == ((CountryResponse)obj!).CountryName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CountryId, CountryName);
        }


    }
    public static class CountryExtentions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName
            };
        }
    }
}
