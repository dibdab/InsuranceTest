using System.ComponentModel.DataAnnotations;
using InsuranceTest.Api.Controllers;
using InsuranceTest.Service.Constants;
using InsuranceTest.Service.Dto;
using InsuranceTest.Service.Enums;
using InsuranceTest.Service.Managers.Interfaces;
using InsuranceTest.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace InsuranceTest.Tests.Controllers;

public class ClaimsControllerTests
{
    private readonly ILogger<ClaimController> _logger = Substitute.For<ILogger<ClaimController>>();

    [Fact]
    internal void GetClaims_ReturnsAllClaims()
    {
        // Arrange
        var claims = new List<ClaimDto>
        {
            new()
            {
                Ucr = "lorem ipsum",
                CompanyId = 32,
                ClaimDate = DateTime.Now,
                LossDate = DateTime.Now,
                AssuredName = "lorem ipsum",
                IncurredLoss = 2.0M,
                Closed = true
            },
            new()
            {
                Ucr = "lorem ipsum",
                CompanyId = 32,
                ClaimDate = DateTime.Now,
                LossDate = DateTime.Now,
                AssuredName = "lorem ipsum",
                IncurredLoss = 2.0M,
                Closed = true
            }
        };
        var claimsManager = Substitute.For<IClaimsManager>();
        claimsManager.GetClaims(Arg.Any<int>()).Returns(claims);
        var controller = new ClaimController(_logger, claimsManager);

        // Act
        var result = controller.GetClaims(32) as OkObjectResult;
        var resultObject = result?.Value as List<ClaimDto>;

        // Assert
        Assert.NotNull(resultObject);
        Assert.Equal(200, result?.StatusCode);
        Assert.IsType<List<ClaimDto>>(resultObject);
        Assert.Equal(2, resultObject.Count);
    }

    [Fact]
    public void GetClaims_ReturnsNotFound()
    {
        // Arrange
        List<ClaimDto>? claims = null;
        var claimsManager = Substitute.For<IClaimsManager>();
        claimsManager.GetClaims(Arg.Any<int>()).Returns(claims);
        var controller = new ClaimController(_logger, claimsManager);

        // Act
        var result = controller.GetClaims(32) as NotFoundObjectResult;
        var resultObject = result?.Value as ResponseModel;

        // Assert
        Assert.NotNull(resultObject);
        Assert.Equal(404, result?.StatusCode);
        Assert.IsType<ResponseModel>(resultObject);
        Assert.Equal(ResponseStatus.DataNotFound, resultObject.InternalStatus);
    }

    [Fact]
    public void GetClaims_ReturnsBadRequest_ForInvalidId()
    {
        // Arrange
        List<ClaimDto>? claims = null;
        var claimsManager = Substitute.For<IClaimsManager>();
        claimsManager.GetClaims(Arg.Any<int>()).Returns(claims);
        var controller = new ClaimController(_logger, claimsManager);

        // Act
        var result = controller.GetClaims(0) as ObjectResult;
        var resultObject = result?.Value as ValidationProblemDetails;

        // Assert
        Assert.NotNull(resultObject);
        Assert.IsType<ValidationProblemDetails>(resultObject);
        Assert.Contains("CompanyId", resultObject.Errors.Keys);
    }

    [Fact]
    public void GetClaim_ReturnsClaim()
    {
        // Arrange
        var claim = new ClaimDto
        {
            Ucr = "lorem ipsum",
            CompanyId = 32,
            ClaimDate = DateTime.Now,
            LossDate = DateTime.Now,
            AssuredName = "lorem ipsum",
            IncurredLoss = 2.0M,
            Closed = true
        };
        var claimsManager = Substitute.For<IClaimsManager>();
        claimsManager.GetClaim(Arg.Any<string>()).Returns(claim);
        var controller = new ClaimController(_logger, claimsManager);

        // Act
        var result = controller.GetClaim("test") as OkObjectResult;
        var resultObject = result?.Value as ClaimDto;

        // Assert
        Assert.NotNull(resultObject);
        Assert.Equal(200, result?.StatusCode);
        Assert.IsType<ClaimDto>(resultObject);
        Assert.Equal(32, resultObject.CompanyId);
    }

    [Fact]
    public void GetClaim_ReturnsNotFound()
    {
        // Arrange
        ClaimDto? claim = null;
        var claimsManager = Substitute.For<IClaimsManager>();
        claimsManager.GetClaim(Arg.Any<string>()).Returns(claim);
        var controller = new ClaimController(_logger, claimsManager);

        // Act
        var result = controller.GetClaim("test") as NotFoundObjectResult;
        var resultObject = result?.Value as ResponseModel;

        // Assert
        Assert.NotNull(resultObject);
        Assert.Equal(404, result?.StatusCode);
        Assert.IsType<ResponseModel>(resultObject);
        Assert.Equal(ResponseStatus.DataNotFound, resultObject.InternalStatus);
    }

    [Fact]
    public void GetClaim_ReturnsBadRequest_ForInvalidId()
    {
        // Arrange
        ClaimDto? claim = null;
        var claimsManager = Substitute.For<IClaimsManager>();
        claimsManager.GetClaim(Arg.Any<string>()).Returns(claim);
        var controller = new ClaimController(_logger, claimsManager);

        // Act
        var result = controller.GetClaim("") as ObjectResult;
        var resultObject = result?.Value as ValidationProblemDetails;

        // Assert
        Assert.NotNull(resultObject);
        Assert.IsType<ValidationProblemDetails>(resultObject);
        Assert.Contains("Ucr", resultObject.Errors.Keys);
    }

    [Fact]
    public void UpdateClaim_ReturnsSuccess()
    {
        // Arrange
        var updateRequest = new ClaimUpdateModel
        {
            Ucr = "lorem ipsum",
            CompanyId = 32,
            ClaimDate = DateTime.Now,
            LossDate = DateTime.Now,
            AssuredName = "lorem ipsum",
            IncurredLoss = 2.0M,
            Closed = true
        };
        var response = new ResponseModel(ResponseStatus.UpdateComplete, ResponseMessages.UpdateComplete);
        var claimsManager = Substitute.For<IClaimsManager>();
        claimsManager.UpdateClaim(Arg.Any<ClaimDto>()).Returns(response);
        var controller = new ClaimController(_logger, claimsManager);

        // Act
        var result = controller.UpdateClaim(updateRequest) as OkObjectResult;
        var resultObject = result?.Value as ResponseModel;

        // Assert
        Assert.NotNull(resultObject);
        Assert.Equal(200, result?.StatusCode);
        Assert.IsType<ResponseModel>(resultObject);
        Assert.Equal(ResponseStatus.UpdateComplete, resultObject.InternalStatus);
    }

    [Fact]
    public void UpdateClaim_ReturnsNotFound()
    {
        // Arrange
        var updateRequest = new ClaimUpdateModel
        {
            Ucr = "lorem ipsum",
            CompanyId = 32,
            ClaimDate = DateTime.Now,
            LossDate = DateTime.Now,
            AssuredName = "lorem ipsum",
            IncurredLoss = 2.0M,
            Closed = true
        };
        var response = new ResponseModel(ResponseStatus.DataNotFound, ResponseMessages.DataNotFound);
        var claimsManager = Substitute.For<IClaimsManager>();
        claimsManager.UpdateClaim(Arg.Any<ClaimDto>()).Returns(response);
        var controller = new ClaimController(_logger, claimsManager);

        // Act
        var result = controller.UpdateClaim(updateRequest) as NotFoundObjectResult;
        var resultObject = result?.Value as ResponseModel;

        // Assert
        Assert.NotNull(resultObject);
        Assert.Equal(404, result?.StatusCode);
        Assert.IsType<ResponseModel>(resultObject);
        Assert.Equal(ResponseStatus.DataNotFound, resultObject.InternalStatus);
    }

    [Fact]
    public void UpdateClaim_ValidateModel()
    {
        // Arrange
        var updateRequest = new ClaimUpdateModel
        {
            Ucr = "loremipsum4434334fdsloremipsum4434334fdsgr34tg43g43loremipsum4434334fdsgr34tg43g43gr34tg43g43",
            ClaimDate = DateTime.Now,
            LossDate = DateTime.Now,
            AssuredName =
                "loremierererererewqrggewgtgrtwgtloremierererererewqrggewgtgrtwgtrhyhetypsumloremierererererewqrggewgtgrtwgtrhyhetypsumloremierererererewqrggewgtgrtwgtrhyhetypsumloremierererererewqrggewgtgrtwgtrhyhetypsumloremierererererewqrggewgtgrtwgtrhyhetypsumrhyhetypsum",
            IncurredLoss = 23443444343543534534456456546.0M,
            Closed = true
        };

        // Act
        var result = ValidateModel(updateRequest).ToList();
        var memberNames = result.SelectMany(x => x.MemberNames).ToList();

        // Assert
        Assert.Contains("Ucr", memberNames);
        Assert.Contains("AssuredName", memberNames);
        Assert.Contains("IncurredLoss", memberNames);
        Assert.Contains("CompanyId", memberNames);
    }

    public static IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, ctx, validationResults, true);
        return validationResults;
    }
}