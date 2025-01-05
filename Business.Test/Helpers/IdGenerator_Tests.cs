using ContactListApp.Business.Helpers;

namespace Business.Test.Helpers;

public class IdGenerator_Tests
{
    [Fact]
    public void GenerateUniqueId_ShouldReturnGuidAsString()
    {
        //Arrange


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
        // Arrange


        // Act
        var result_1 = UniqueIdentifierGenerator.GenerateUniqueId();
        var result_2 = UniqueIdentifierGenerator.GenerateUniqueId();

        // Assert
        Assert.NotEqual(result_1, result_2);
    }
}

