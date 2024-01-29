using InsuranceTest.Data.Entities;

namespace InsuranceTest.Data.DataAccess.Interfaces;

public interface IClaimRepository
{
    Claim? GetByUcr(string ucr);
    IEnumerable<Claim>? GetListByCompanyId(int companyId);
    bool Update(Claim claim);
}