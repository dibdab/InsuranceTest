using InsuranceTest.Data.Entities;

namespace InsuranceTest.Data.DataAccess.Interfaces;

public interface ICompanyRepository
{
    Company? GetById(int companyId);
}