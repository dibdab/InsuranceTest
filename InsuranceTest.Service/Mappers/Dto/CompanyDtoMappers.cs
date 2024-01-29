using InsuranceTest.Data.Entities;
using InsuranceTest.Service.Dto;

namespace InsuranceTest.Service.Mappers.Dto;

internal static class CompanyDtoMappers
{
    internal static CompanyDto ToDto(this Company entity)
    {
        return new CompanyDto
        {
            CompanyId = entity.Id,
            Name = entity.Name,
            Address1 = entity.Address1,
            Address2 = entity.Address2,
            Address3 = entity.Address3,
            Postcode = entity.Postcode,
            Country = entity.Country,
            Active = entity.Active,
            InsuranceEndDate = entity.InsuranceEndDate
        };
    }
}