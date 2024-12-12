using Presentation.Console.MainApp.Helpers;

namespace Business.Test.Helpers;

public class IdGenerator_Tests
{
    [Fact]
    public void GenerateUniqueId_ShouldReturnGuidAsString()
    {
        // Act
        var result = UniqueIdentifierGenerator.GenerateUniqueId();

        // Assert
        Assert.True(Guid.TryParse(result, out _));
        Assert.NotEmpty(result);
        Assert.NotNull(result);
    }

    [Fact]
    public void GenerateUniqueId_ShouldReturnUniqueValues()
    {
        // Act
        var result1 = UniqueIdentifierGenerator.GenerateUniqueId();
        var result2 = UniqueIdentifierGenerator.GenerateUniqueId();

        // Assert
        Assert.NotEqual(result1, result2);
    }
}

