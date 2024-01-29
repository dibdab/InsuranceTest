using InsuranceTest.Data.Entities;
using InsuranceTest.Service.Dto;
using InsuranceTest.Service.Models;

namespace InsuranceTest.Service.Mappers.Dto;

public static class ClaimDtoMappers
{
    internal static ClaimDto ToDto(this Claim entity)
    {
        return new ClaimDto
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

    public static ClaimDto ToDto(this ClaimUpdateModel model)
    {
        return new ClaimDto
        {
            // disable nullability warnings for this method as the controller checks for nulls
#pragma warning disable CS8601 ,CS8629
            Ucr = model.Ucr,
            CompanyId = (int)model.CompanyId,
            ClaimDate = (DateTime)model.ClaimDate,
            LossDate = (DateTime)model.LossDate,
            AssuredName = model.AssuredName,
            IncurredLoss = (decimal)model.IncurredLoss,
            Closed = (bool)model.Closed
#pragma warning restore CS8601
        };
    }
}