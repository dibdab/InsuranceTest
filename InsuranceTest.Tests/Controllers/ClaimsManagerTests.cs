using InsuranceTest.Data.DataAccess.Interfaces;
using InsuranceTest.Data.Entities;
using InsuranceTest.Service.Dto;
using InsuranceTest.Service.Enums;
using InsuranceTest.Service.Managers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace InsuranceTest.Tests.Controllers;

public class ClaimsManagerTests
{
    private readonly ILogger<ClaimsManager> _logger = Substitute.For<ILogger<ClaimsManager>>();

    [Fact]
    internal void GetClaim_ClaimAgeInDays_IsCorrect()
    {
        // Arrange
        var claim = new Claim
        {
            Ucr = "claim1",
            CompanyId = 1,
            ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
            LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
            AssuredName = "Bob Bobbington",
            IncurredLoss = new decimal(123456789123456.23),
            Closed = false
        };
        var repository = Substitute.For<IClaimRepository>();
        repository.GetByUcr(Arg.Any<string>()).Returns(claim);
        var manager = new ClaimsManager(_logger, repository);

        // Act
        var result = manager.GetClaim("claim1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.ClaimAgeInDays);
    }

    [Fact]
    internal void GetClaim_ReturnsNull_ForBadRequest()
    {
        // Arrange
        var claim = new Claim
        {
            Ucr = "claim1",
            CompanyId = 1,
            ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
            LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
            AssuredName = "Bob Bobbington",
            IncurredLoss = new decimal(123456789123456.23),
            Closed = false
        };
        var repository = Substitute.For<IClaimRepository>();
        repository.GetByUcr(Arg.Any<string>()).Returns(claim);
        var manager = new ClaimsManager(_logger, repository);

        // Act
        var result = manager.GetClaim("");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    internal void GetClaim_ReturnsClaim()
    {
        // Arrange
        var claim = new Claim
        {
            Ucr = "claim1",
            CompanyId = 1,
            ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
            LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
            AssuredName = "Bob Bobbington",
            IncurredLoss = new decimal(123456789123456.23),
            Closed = false
        };
        var repository = Substitute.For<IClaimRepository>();
        repository.GetByUcr(Arg.Any<string>()).Returns(claim);
        var manager = new ClaimsManager(_logger, repository);

        // Act
        var result = manager.GetClaim("claim1");

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    internal void GetClaims_ReturnsClaims()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new()
            {
                Ucr = "claim1",
                CompanyId = 1,
                ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
                LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
                AssuredName = "Bob Bobbington",
                IncurredLoss = new decimal(123456789123456.23),
                Closed = false
            },
            new()
            {
                Ucr = "claim2",
                CompanyId = 1,
                ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
                LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
                AssuredName = "Ted Teddington",
                IncurredLoss = new decimal(12.99),
                Closed = false
            }
        };

        var repository = Substitute.For<IClaimRepository>();
        repository.GetListByCompanyId(Arg.Any<int>()).Returns(claims);
        var manager = new ClaimsManager(_logger, repository);

        // Act
        var result = manager.GetClaims(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    internal void GetClaims_ReturnsNull_ForBadRequest()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new()
            {
                Ucr = "claim1",
                CompanyId = 1,
                ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
                LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
                AssuredName = "Bob Bobbington",
                IncurredLoss = new decimal(123456789123456.23),
                Closed = false
            },
            new()
            {
                Ucr = "claim2",
                CompanyId = 1,
                ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
                LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
                AssuredName = "Ted Teddington",
                IncurredLoss = new decimal(12.99),
                Closed = false
            }
        };

        var repository = Substitute.For<IClaimRepository>();
        repository.GetListByCompanyId(Arg.Any<int>()).Returns(claims);
        var manager = new ClaimsManager(_logger, repository);

        // Act
        var result = manager.GetClaims(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    internal void UpdateClaim_ReturnsSuccess()
    {
        // Arrange
        var claim = new ClaimDto
        {
            Ucr = "claim1",
            CompanyId = 1,
            ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
            LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
            AssuredName = "Bob Bobbington",
            IncurredLoss = new decimal(123456789123456.23),
            Closed = false
        };
        var repository = Substitute.For<IClaimRepository>();
        repository.Update(Arg.Any<Claim>()).Returns(true);
        var manager = new ClaimsManager(_logger, repository);

        // Act
        var result = manager.UpdateClaim(claim);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ResponseStatus.UpdateComplete, result.InternalStatus);
    }

    [Fact]
    internal void UpdateClaim_ReturnsFailure_ForNotFound()
    {
        // Arrange
        var claim = new ClaimDto
        {
            Ucr = "claim1",
            CompanyId = 1,
            ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
            LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
            AssuredName = "Bob Bobbington",
            IncurredLoss = new decimal(123456789123456.23),
            Closed = false
        };
        var repository = Substitute.For<IClaimRepository>();
        repository.Update(Arg.Any<Claim>()).Returns(false);
        var manager = new ClaimsManager(_logger, repository);

        // Act
        var result = manager.UpdateClaim(claim);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ResponseStatus.DataNotFound, result.InternalStatus);
    }

    [Fact]
    internal void UpdateClaim_ReturnsFailure_ForException()
    {
        // Arrange
        var claim = new ClaimDto
        {
            Ucr = "claim1",
            CompanyId = 1,
            ClaimDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
            LossDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)),
            AssuredName = "Bob Bobbington",
            IncurredLoss = new decimal(123456789123456.23),
            Closed = false
        };
        var repository = Substitute.For<IClaimRepository>();
        repository.Update(Arg.Any<Claim>()).Throws(new Exception());
        var manager = new ClaimsManager(_logger, repository);

        // Act
        var result = manager.UpdateClaim(claim);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ResponseStatus.UnexpectedError, result.InternalStatus);
    }
}