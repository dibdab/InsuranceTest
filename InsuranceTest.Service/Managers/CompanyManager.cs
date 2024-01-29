using InsuranceTest.Data.DataAccess.Interfaces;
using InsuranceTest.Service.Dto;
using InsuranceTest.Service.Managers.Interfaces;
using InsuranceTest.Service.Mappers.Dto;
using Microsoft.Extensions.Logging;

namespace InsuranceTest.Service.Managers;

public class CompanyManager : ICompanyManager
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ILogger<CompanyManager> _logger;

    public CompanyManager(ILogger<CompanyManager> logger, ICompanyRepository companyRepository)
    {
        _logger = logger;
        _companyRepository = companyRepository;
    }

    public CompanyDto? GetCompany(int companyId)
    {
        _logger.LogTrace("CompanyDto GetCompany - CompanyId:{CompanyId}", companyId);

        return companyId == 0 ? null : _companyRepository.GetById(companyId)?.ToDto();
    }
}