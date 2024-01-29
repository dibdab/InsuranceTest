using InsuranceTest.Data.DataAccess.Interfaces;
using InsuranceTest.Data.Entities;
using Microsoft.Extensions.Logging;

namespace InsuranceTest.Data.DataAccess;

public class CompanyRepository : ICompanyRepository
{
    #region CompanyData

    private readonly List<Company> _companies = new()
    {
        new Company
        {
            Id = 1,
            Name = "Company Active Policy",
            Address1 = "lorem ipsum",
            Address2 = "lorem ipsum",
            Address3 = "lorem ipsum",
            Postcode = "LS30 TF1",
            Country = "United Kingdom",
            Active = true,
            InsuranceEndDate = DateTime.Now.AddDays(5)
        },
        new Company
        {
            Id = 2,
            Name = "Company Inactive Policy",
            Address1 = "lorem ipsum",
            Address2 = "lorem ipsum",
            Address3 = "lorem ipsum",
            Postcode = "LS30 TF1",
            Country = "United Kingdom",
            Active = true,
            InsuranceEndDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0))
        }
    };

    #endregion

    private readonly ILogger<CompanyRepository> _logger;

    public CompanyRepository(ILogger<CompanyRepository> logger)
    {
        _logger = logger;
    }

    public Company? GetById(int companyId)
    {
        _logger.LogTrace("Company GetById - CompanyId:{CompanyId}", companyId);
        return _companies.FirstOrDefault(x => x.Id == companyId);
    }
}