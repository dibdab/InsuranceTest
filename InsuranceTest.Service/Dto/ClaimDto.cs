using System.Text.Json.Serialization;

namespace InsuranceTest.Service.Dto;

public class ClaimDto
{
    [JsonPropertyName("UCR")] public string Ucr { get; set; }

    public int CompanyId { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime LossDate { get; set; }
    public string AssuredName { get; set; }
    public decimal IncurredLoss { get; set; }
    public bool Closed { get; set; }
    public int ClaimAgeInDays => (DateTime.Now.Date - ClaimDate.Date).Days;
}