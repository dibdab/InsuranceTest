using InsuranceTest.Data.DataAccess.Interfaces;
using InsuranceTest.Service.Constants;
using InsuranceTest.Service.Dto;
using InsuranceTest.Service.Enums;
using InsuranceTest.Service.Managers.Interfaces;
using InsuranceTest.Service.Mappers.Dto;
using InsuranceTest.Service.Mappers.Entities;
using InsuranceTest.Service.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace InsuranceTest.Service.Managers;

public class ClaimsManager : IClaimsManager
{
    private readonly IClaimRepository _claimRepository;
    private readonly ILogger<ClaimsManager> _logger;

    public ClaimsManager(ILogger<ClaimsManager> logger, IClaimRepository claimRepository)
    {
        _logger = logger;
        _claimRepository = claimRepository;
    }

    public ClaimDto? GetClaim(string ucr)
    {
        _logger.LogTrace("ClaimDto GetClaim - UCR:{UCR}", ucr);

        return ucr.IsNullOrEmpty() ? null : _claimRepository.GetByUcr(ucr)?.ToDto();
    }

    public List<ClaimDto>? GetClaims(int companyId)
    {
        _logger.LogTrace("List<ClaimDto> GetClaims - CompanyId:{CompanyId}", companyId);

        var results = _claimRepository.GetListByCompanyId(companyId);

        return results?.Select(item => item.ToDto()).ToList();
    }

    public ResponseModel UpdateClaim(ClaimDto claim)
    {
        _logger.LogTrace("ResponseModel UpdateClaim - ClaimDto:{ClaimDto}", claim);

        try
        {
            return _claimRepository.Update(claim.ToEntity())
                ? new ResponseModel(ResponseStatus.UpdateComplete, ResponseMessages.UpdateComplete)
                : new ResponseModel(ResponseStatus.DataNotFound, ResponseMessages.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "ResponseModel UpdateClaim - Unexpected Exception - ClaimDto:{ClaimDto}, Exception:{Exception}", claim,
                ex);
            return new ResponseModel(ResponseStatus.UnexpectedError, ResponseMessages.UnexpectedError,
                ex.StackTrace ?? string.Empty);
        }
    }
}