using System.Net;
using InsuranceTest.Service.Constants;
using InsuranceTest.Service.Dto;
using InsuranceTest.Service.Enums;
using InsuranceTest.Service.Managers.Interfaces;
using InsuranceTest.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InsuranceTest.Api.Controllers;

/// <summary>
///     Methods for interacting with Company data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyManager _companyManager;
    private readonly ILogger<CompanyController> _logger;

    public CompanyController(ILogger<CompanyController> logger, ICompanyManager companyManager)
    {
        _logger = logger;
        _companyManager = companyManager;
    }

    /// <summary>
    ///     Returns a single Company based on provided Id
    /// </summary>
    /// <param name="companyId">Our internal Id for the company, assigned in the DB</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CompanyDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ResponseModel))]
    public IActionResult GetCompany([FromQuery] [BindRequired] int companyId)
    {
        _logger.LogInformation(
            "GetCompany - Request received - CompanyId:{CompanyId}.", companyId);

        if (companyId == 0)
            ModelState.AddModelError("CompanyId", ResponseMessages.InvalidId);

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = _companyManager.GetCompany(companyId);

        if (result == null)
            return NotFound(new ResponseModel(ResponseStatus.DataNotFound, ResponseMessages.DataNotFound));

        return Ok(result);
    }
}