using FluentAssertions;

namespace CoursePlataform.DomainTest;

public class MyFirstTest
{
    [Fact]
    public void Variable1MustBeEqualVariable2()
    {
        //ArrangeActionAssert
        
        //ARRANGE
        var variable1 = 1;
        var variable2 = 2;

        //ACTION
        variable2 = variable1;
        
        //ASSERT
        Assert.Equal(variable1, variable2);
        variable1.Should().Be(variable2);
    }
}