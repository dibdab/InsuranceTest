using InsuranceTest.Data.DataAccess.Interfaces;
using InsuranceTest.Data.Entities;
using Microsoft.Extensions.Logging;

namespace InsuranceTest.Data.DataAccess;

public class ClaimRepository : IClaimRepository
{
    #region ClaimData

    private readonly List<Claim> _claims = new()
    {
        new Claim
        {
            Ucr = "claim1",
            CompanyId = 1,
            ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
            LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
            AssuredName = "Bob Bobbington",
            IncurredLoss = new decimal(123456789123456.23),
            Closed = false
        },
        new Claim
        {
            Ucr = "claim2",
            CompanyId = 1,
            ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
            LossDate = DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0)),
            AssuredName = "Ted Teddington",
            IncurredLoss = new decimal(12.99),
            Closed = false
        },
        new Claim
        {
            Ucr = "claim4",
            CompanyId = 2,
            ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
            LossDate = DateTime.Now.Subtract(new TimeSpan(20, 0, 0, 0)),
            AssuredName = "Fred Freddington",
            IncurredLoss = new decimal(12.99),
            Closed = false
        }
    };

    #endregion

    private readonly ILogger<ClaimRepository> _logger;

    public ClaimRepository(ILogger<ClaimRepository> logger)
    {
        _logger = logger;
    }

    public Claim? GetByUcr(string ucr)
    {
        if (string.IsNullOrEmpty(ucr)) return null;

        _logger.LogTrace("Claim GetByUcr - UCR:{Ucr}", ucr);
        return _claims.FirstOrDefault(x => x.Ucr == ucr);
    }

    public IEnumerable<Claim> GetListByCompanyId(int companyId)
    {
        _logger.LogTrace("Claim GetByCompanyId - CompanyId:{CompanyId}", companyId);
        return _claims.Where(x => x.CompanyId == companyId);
    }


    /// <summary>
    ///     Attempts to update claim by replacing the index in the claimList based on ucr, throws ClaimNotFoundException on
    ///     failure
    /// </summary>
    public bool Update(Claim claim)
    {
        _logger.LogTrace("Claim Update - UCR:{Ucr} UpdateData:{Claim}", claim.Ucr, claim);
        var indexToUpdate = _claims.FindIndex(x => x.Ucr == claim.Ucr);

        // Claim found with ucr so overwrite
        if (indexToUpdate >= 0)
        {
            _claims[indexToUpdate] = claim;
            return true;
        }

        // Should not happen unless FE sends wrong UCR
        _logger.LogError("Claim Update - Claim not found, no update made - UCR:{Ucr}", claim.Ucr);
        return false;
    }
}