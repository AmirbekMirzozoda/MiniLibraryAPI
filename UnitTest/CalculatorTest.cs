using MiniLibraryAPI;

namespace UnitTest;

public class CalculatorTest
{
    [Fact]
    public void Add_Test()
    {
        // Arrange
        var calculator = new Calculator();
        double a = 5;
        double b = 3;
        
        // Act
        double result = calculator.Add(a, b);
        
        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void Subtract_Test()
    {
        // Arrange
        var calculator = new Calculator();
        double a = 5;
        double b = 3;

        // Act
        double result = calculator.Subtract(a, b);

        // Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void Multiply_Test()
    {
        // Arrange
        var calculator = new Calculator();
        double a = 5;
        double b = 3;

        // Act
        double result = calculator.Multiply(a, b);
        
        // Assert
        Assert.Equal(15, result);
    }
}