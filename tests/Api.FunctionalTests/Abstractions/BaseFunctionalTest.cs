namespace Api.FunctionalTests.Abstractions;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory) =>
        HttpClient = factory.CreateClient();

    protected const string ApiVersion = "v1";

    protected const string UsersEndpoint = $"api/{ApiVersion}/users";

    protected HttpClient HttpClient { get; set; }
}
