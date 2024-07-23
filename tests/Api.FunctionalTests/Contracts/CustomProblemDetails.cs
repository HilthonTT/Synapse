using SharedKernel;

namespace Api.FunctionalTests.Contracts;

public sealed record CustomProblemDetails(
    string Type, 
    string Title, 
    int Status, 
    string Detail, 
    List<Error> Errors);
