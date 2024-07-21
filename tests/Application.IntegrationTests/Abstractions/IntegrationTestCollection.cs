namespace Application.IntegrationTests.Abstractions;

[CollectionDefinition(nameof(IntegrationTestWebAppFactory))]
public sealed class IntegrationTestCollection : ICollectionFixture<IntegrationTestWebAppFactory>
{
}
