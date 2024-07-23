namespace Api.FunctionalTests.Abstractions;

public abstract class BaseFunctionalTest(FunctionalTestWebAppFactory factory) 
    : IClassFixture<FunctionalTestWebAppFactory>
{
    protected const string ApiVersion = "v1";

    protected const string UsersEndpoint = $"api/{ApiVersion}/users";

    protected HttpClient HttpClient { get; set; } = factory.CreateClient();
}
