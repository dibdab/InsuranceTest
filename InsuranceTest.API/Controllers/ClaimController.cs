using System.Net;
using InsuranceTest.Service.Constants;
using InsuranceTest.Service.Dto;
using InsuranceTest.Service.Enums;
using InsuranceTest.Service.Managers.Interfaces;
using InsuranceTest.Service.Mappers.Dto;
using InsuranceTest.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace InsuranceTest.Api.Controllers;

/// <summary>
///     Methods for interacting with Claim data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClaimController : ControllerBase
{
    private readonly IClaimsManager _claimsManager;
    private readonly ILogger<ClaimController> _logger;

    public ClaimController(ILogger<ClaimController> logger, IClaimsManager claimsManager)
    {
        _logger = logger;
        _claimsManager = claimsManager;
    }

    /// <summary>
    ///     Returns a single Claim based on provided UCR
    /// </summary>
    /// <param name="ucr">The UCR for the claim</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ClaimDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ResponseModel))]
    public IActionResult GetClaim([FromQuery] [BindRequired] string ucr)
    {
        _logger.LogInformation(
            "GetClaim - Request received - UCR:{UCR}.", ucr);

        if (ucr.IsNullOrEmpty())
            ModelState.AddModelError("Ucr", ResponseMessages.InvalidId);

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = _claimsManager.GetClaim(ucr);

        if (result == null)
            return NotFound(new ResponseModel(ResponseStatus.DataNotFound, ResponseMessages.DataNotFound));

        return Ok(result);
    }

    /// <summary>
    ///     Returns a list of Claims for the provided company Id.
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("list")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ClaimDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ResponseModel))]
    public IActionResult GetClaims([FromQuery] [BindRequired] int companyId)
    {
        _logger.LogInformation(
            "GetClaims - Request received - CompanyId:{CompanyId}.", companyId);

        if (companyId == 0)
            ModelState.AddModelError("CompanyId", ResponseMessages.InvalidId);

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = _claimsManager.GetClaims(companyId);

        if (result == null)
            return NotFound(new ResponseModel(ResponseStatus.DataNotFound, ResponseMessages.DataNotFound));

        return Ok(result);
    }

    /// <summary>
    ///     Preforms a full update on the Claim specified by the UCR. Partial updates are not supported.
    /// </summary>
    /// <param name="claim"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ClaimDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseModel))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ResponseModel))]
    public IActionResult UpdateClaim([FromBody] [BindRequired] ClaimUpdateModel claim)
    {
        _logger.LogInformation(
            "UpdateClaim - Request received - UCR:{UCR}.", claim.Ucr);
        _logger.LogTrace(
            "UpdateClaim - Request received - UCR:{UCR}. Request:{Claim}", claim.Ucr, claim);

        var result = _claimsManager.UpdateClaim(claim.ToDto());

        switch (result.InternalStatus)
        {
            case ResponseStatus.DataNotFound:
                return NotFound(result);
            case ResponseStatus.UpdateComplete:
                return Ok(result);
            case ResponseStatus.UnexpectedError:
            default:
                return StatusCode(500, result);
        }
    }
}