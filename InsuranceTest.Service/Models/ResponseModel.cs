using InsuranceTest.Service.Enums;

namespace InsuranceTest.Service.Models;

public class ResponseModel
{
    public ResponseModel(ResponseStatus internalStatus, string? message)
    {
        InternalStatus = internalStatus;
        Message = message;
    }

    public ResponseModel(ResponseStatus internalStatus, string message, string stackTrace)
    {
        InternalStatus = internalStatus;
        Message = message;
        StackTrace = stackTrace;
    }

    public ResponseStatus InternalStatus { get; set; }
    public string? Message { get; set; }
    public string? StackTrace { get; set; }
}