using CoreEntity = StrykerMutation.Core.Person;
using FluentAssertions;

namespace StrykerMutationTests.Core.Person;

public class PersonTests
{
    [Fact]
    public void Instantiate()
    {
        const string validName = "Fabio de Stefani";
        const int validAge = 38;

        var action = () => new CoreEntity.Person(validName, validAge);
        
        var person = action.Should().NotThrow().Which;
        person.Id.Should().NotBeEmpty();
        person.Age.Should().Be(validAge);
        person.Name.Should().Be(validName);
    }
    
    [Theory]
    [InlineData(14, false)]
    [InlineData(29, true)]
    [InlineData(17, false)]
    [InlineData(18, true)]
    public void OverMajorityAgeShouldReturnProperly(
        int age, 
        bool expectedResult)
    {
        var person = new CoreEntity.Person("Fabio de Stefani", age);
        var result = person.IsOverMajorityAge();
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("Invalid")]
    [InlineData(null)]
    public void WhenInstantiatingInvalidNameShouldThrow(string invalidName)
    { 
        var action = () => new CoreEntity.Person(invalidName, 38);
        action.Should().Throw<Exception>()
            .WithMessage("Name should be composed by name and surname");
        
    }
    
    [Theory]
    [InlineData("Fabio Stefani")]
    [InlineData("Fabio de Stefani")]
    public void WhenInstantiatingValidNameShouldNotThrow(string validName)
    { 
        var action = () => new CoreEntity.Person(validName, 38);
        action.Should().NotThrow();
        
    }
}