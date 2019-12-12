using Xunit;

namespace FunctionalTests.Seedwork
{
    [CollectionDefinition(nameof(TestCollectionFixture))]
    public class TestCollectionFixture : ICollectionFixture<TestFixture>
    {
    }
}
