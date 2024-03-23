using admete.Enums;

namespace admete.Responses;

public class ErrorResponse(EnumErrorCode errorCode)
{
    public EnumErrorCode ErrorCode { get; set; } = errorCode;
    public string ErrorMessage { get; set; } = errorCode switch
    {
        EnumErrorCode.InvalidStartDate => "Start date must be later than 2023-01-01",
        EnumErrorCode.InvalidDate => "End time must be greater than start time",
        _ => "Unknown error"
    };
}