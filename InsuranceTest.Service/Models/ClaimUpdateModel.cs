using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsuranceTest.Service.Models;

/// <summary>
///     The request body for updating claims.
/// </summary>
public class ClaimUpdateModel
{
    /// <summary>
    ///     The UCR for the Claim that should be updated.
    /// </summary>
    [Required]
    [JsonPropertyName("UCR")]
    [MaxLength(20)]
    public string? Ucr { get; set; }

    /// <summary>
    ///     The Id of the Company the Claim should be assigned to.
    /// </summary>
    [Required]
    public int? CompanyId { get; set; }

    /// <summary>
    ///     The date the claim was raised.
    /// </summary>
    [Required]
    public DateTime? ClaimDate { get; set; }

    /// <summary>
    ///     The date the loss the claim was against occurred.
    /// </summary>
    [Required]
    public DateTime? LossDate { get; set; }

    /// <summary>
    ///     The name of the assured item.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string? AssuredName { get; set; }

    /// <summary>
    ///     The amount of loss against the claim.
    /// </summary>
    [Required]
    // Ensures 2 decimal places max
    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    // Allows only numbers in this range
    [Range(0, 999999999999999.99)]
    public decimal? IncurredLoss { get; set; }

    /// <summary>
    ///     Whether the claim is open or closed.
    /// </summary>
    [Required]
    public bool? Closed { get; set; }
}