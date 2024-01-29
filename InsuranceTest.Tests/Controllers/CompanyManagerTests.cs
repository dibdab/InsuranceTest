using InsuranceTest.Data.DataAccess.Interfaces;
using InsuranceTest.Data.Entities;
using InsuranceTest.Service.Managers;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace InsuranceTest.Tests.Controllers;

public class CompanyManagerTests
{
    private readonly ILogger<CompanyManager> _logger = Substitute.For<ILogger<CompanyManager>>();

    [Fact]
    internal void GetCompany_IsPolicyActive_IsCorrect()
    {
        // Arrange
        var claim = new Company
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
        };
        var repository = Substitute.For<ICompanyRepository>();
        repository.GetById(Arg.Any<int>()).Returns(claim);
        var manager = new CompanyManager(_logger, repository);

        // Act
        var result = manager.GetCompany(1);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsPolicyActive);
    }

    [Fact]
    internal void GetCompany_ReturnsNull_ForBadRequest()
    {
        // Arrange
        var claim = new Company
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
        };
        var repository = Substitute.For<ICompanyRepository>();
        repository.GetById(Arg.Any<int>()).Returns(claim);
        var manager = new CompanyManager(_logger, repository);

        // Act
        var result = manager.GetCompany(0);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    internal void GetCompany_ReturnsCompany()
    {
        // Arrange
        var claim = new Company
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
        };
        var repository = Substitute.For<ICompanyRepository>();
        repository.GetById(Arg.Any<int>()).Returns(claim);
        var manager = new CompanyManager(_logger, repository);

        // Act
        var result = manager.GetCompany(1);

        // Assert
        Assert.NotNull(result);
    }
}