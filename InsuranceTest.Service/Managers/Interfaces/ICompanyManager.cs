using InsuranceTest.Service.Dto;

namespace InsuranceTest.Service.Managers.Interfaces;

public interface ICompanyManager
{
    CompanyDto? GetCompany(int companyId);
}