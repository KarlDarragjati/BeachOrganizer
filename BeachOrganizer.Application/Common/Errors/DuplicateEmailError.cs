using FluentResults;

namespace BeachOrganizer.Application.Common.Errors;

public class DuplicateEmailError : FluentResults.IError
{
    public string Message { get; }
    public Dictionary<string, object> Metadata { get; }
    public List<FluentResults.IError> Reasons { get; }
}