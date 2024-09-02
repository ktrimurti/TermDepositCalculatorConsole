namespace TermDepositCalculatorConsole.Tests;

public class TermDepositTests
{
    
    [Theory]
    [InlineData(10000, 1.10, 3, 0, InterestPaidFrequency.Monthly, 10335)]
    [InlineData(10000, 1.10, 3, 8, InterestPaidFrequency.Monthly, 10411)]
    [InlineData(15000, 1.50, 5, 0, InterestPaidFrequency.Annually, 16159)]
    [InlineData(20000, 2.00, 1, 3, InterestPaidFrequency.AtMaturity, 20500)]
    [InlineData(10000, 1.00, 2, 0, InterestPaidFrequency.Quarterly, 10202)]
    public void When_CalculateFinalBalance_Should_Return_ExpectedFinalBalance(
        decimal startAmount, decimal interestRate, decimal years, decimal months, 
        InterestPaidFrequency interestPaidFrequency, decimal expectedFinalBalance)
    {
        // Arrange
        var termDeposit = new TermDeposit(startAmount, interestRate, years, months, (decimal)interestPaidFrequency);

        // Act
        termDeposit.Calculate();

        // Assert
        Assert.Equal(expectedFinalBalance, termDeposit.FinalBalance);
    }
}
