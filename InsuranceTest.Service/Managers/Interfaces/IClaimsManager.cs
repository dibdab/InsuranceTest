using InsuranceTest.Service.Dto;
using InsuranceTest.Service.Models;

namespace InsuranceTest.Service.Managers.Interfaces;

public interface IClaimsManager
{
    ClaimDto? GetClaim(string ucr);
    List<ClaimDto>? GetClaims(int companyId);
    ResponseModel UpdateClaim(ClaimDto claim);
}