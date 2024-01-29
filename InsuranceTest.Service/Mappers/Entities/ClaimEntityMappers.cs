using InsuranceTest.Data.Entities;
using InsuranceTest.Service.Dto;

namespace InsuranceTest.Service.Mappers.Entities;

internal static class ClaimEntityMappers
{
    internal static Claim ToEntity(this ClaimDto entity)
    {
        return new Claim
        {
            Ucr = entity.Ucr,
            CompanyId = entity.CompanyId,
            ClaimDate = entity.ClaimDate,
            LossDate = entity.LossDate,
            AssuredName = entity.AssuredName,
            IncurredLoss = entity.IncurredLoss,
            Closed = entity.Closed
        };
    }
}