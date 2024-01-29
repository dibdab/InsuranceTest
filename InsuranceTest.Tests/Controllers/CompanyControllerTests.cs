using InsuranceTest.Api.Controllers;
using InsuranceTest.Service.Dto;
using InsuranceTest.Service.Enums;
using InsuranceTest.Service.Managers.Interfaces;
using InsuranceTest.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace InsuranceTest.Tests.Controllers;

public class CompanyControllerTests
{
    private readonly ILogger<CompanyController> _logger = Substitute.For<ILogger<CompanyController>>();

    [Fact]
    public void GetClaimById_ReturnsClaim()
    {
        // Arrange
        var company = new CompanyDto
        {
            CompanyId = 32,
            Name = "lorem ipsum",
            Address1 = "lorem ipsum",
            Address2 = "lorem ipsum",
            Address3 = "lorem ipsum",
            Postcode = "lorem ipsum",
            Country = "lorem ipsum",
            Active = true,
            InsuranceEndDate = DateTime.Now
        };
        var companyManager = Substitute.For<ICompanyManager>();
        companyManager.GetCompany(Arg.Any<int>()).Returns(company);
        var controller = new CompanyController(_logger, companyManager);

        // Act
        var result = controller.GetCompany(32) as OkObjectResult;
        var resultObject = result?.Value as CompanyDto;

        // Assert
        Assert.NotNull(resultObject);
        Assert.Equal(200, result?.StatusCode);
        Assert.IsType<CompanyDto>(resultObject);
        Assert.Equal(32, resultObject.CompanyId);
    }

    [Fact]
    public void GetClaim_ReturnsNotFound()
    {
        // Arrange
        CompanyDto? company = null;
        var companyManager = Substitute.For<ICompanyManager>();
        companyManager.GetCompany(Arg.Any<int>()).Returns(company);
        var controller = new CompanyController(_logger, companyManager);

        // Act
        var result = controller.GetCompany(1) as NotFoundObjectResult;
        var resultObject = result?.Value as ResponseModel;

        // Assert
        Assert.NotNull(resultObject);
        Assert.Equal(404, result?.StatusCode);
        Assert.IsType<ResponseModel>(resultObject);
        Assert.Equal(ResponseStatus.DataNotFound, resultObject.InternalStatus);
    }

    [Fact]
    public void GetClaimById_ReturnsBadRequest_ForInvalidId()
    {
        // Arrange
        CompanyDto? company = null;
        var companyManager = Substitute.For<ICompanyManager>();
        companyManager.GetCompany(Arg.Any<int>()).Returns(company);
        var controller = new CompanyController(_logger, companyManager);

        // Act
        var result = controller.GetCompany(0) as ObjectResult;
        var resultObject = result?.Value as ValidationProblemDetails;

        // Assert
        Assert.NotNull(resultObject);
        Assert.IsType<ValidationProblemDetails>(resultObject);
        Assert.Contains("CompanyId", resultObject.Errors.Keys);
    }
}