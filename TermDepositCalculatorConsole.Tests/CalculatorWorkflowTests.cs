namespace TermDepositCalculatorConsole.Tests;

public class CalculatorWorkflowTests
{
    [Fact]
    public void ValidateAndStore_Should_Store_StartAmount_And_MoveTo_NextQuestion()
    {
        // Arrange
        CalculatorWorkflow.CurrentQuestion = TermDepositParameters.StartAmount;
        var input = "10000";

        // Act
        CalculatorWorkflow.ValidateAndStore(input);
        CalculatorWorkflow.MoveToNextQuestion();

        // Assert
        Assert.True(CalculatorWorkflow.IsCurrentUserInputValidatedAndStored);
        Assert.Equal(TermDepositParameters.InterestRate, CalculatorWorkflow.CurrentQuestion);
    }

    [Fact]
    public void ValidateAndStore_ShouldNot_Store_InvalidStartAmount()
    {
        // Arrange
        CalculatorWorkflow.CurrentQuestion = TermDepositParameters.StartAmount;
        var input = "hello haha";

        // Act
        CalculatorWorkflow.ValidateAndStore(input);

        // Assert
        Assert.False(CalculatorWorkflow.IsCurrentUserInputValidatedAndStored);
        Assert.Equal(TermDepositParameters.StartAmount, CalculatorWorkflow.CurrentQuestion);
    }

    [Fact]
    public void ValidateAndStore_ShouldStore_InterestRate_And_MoveToNextQuestion()
    {
        // Arrange
        CalculatorWorkflow.CurrentQuestion = TermDepositParameters.StartAmount;
        CalculatorWorkflow.ValidateAndStore("10000");
        CalculatorWorkflow.MoveToNextQuestion();

        var input = "1.10";

        // Act
        CalculatorWorkflow.ValidateAndStore(input);
        CalculatorWorkflow.MoveToNextQuestion();

        // Assert
        Assert.True(CalculatorWorkflow.IsCurrentUserInputValidatedAndStored);
        Assert.Equal(TermDepositParameters.TermYears, CalculatorWorkflow.CurrentQuestion);
    }

    [Fact]
    public void ValidateAndStore_ShouldNot_Store_InvalidInterestRate()
    {
        // Arrange
        CalculatorWorkflow.CurrentQuestion = TermDepositParameters.InterestRate;
        var input = "haha%";

        // Act
        CalculatorWorkflow.ValidateAndStore(input);

        // Assert
        Assert.False(CalculatorWorkflow.IsCurrentUserInputValidatedAndStored);
        Assert.Equal(TermDepositParameters.InterestRate, CalculatorWorkflow.CurrentQuestion);
    }

    [Fact]
    public void ValidateAndStore_Should_Store_TermYears_And_MoveToNextQuestion()
    {
        // Arrange
        CalculatorWorkflow.CurrentQuestion = TermDepositParameters.TermYears;
        var input = "3";

        // Act
        CalculatorWorkflow.ValidateAndStore(input);
        CalculatorWorkflow.MoveToNextQuestion();

        // Assert
        Assert.True(CalculatorWorkflow.IsCurrentUserInputValidatedAndStored);
        Assert.Equal(TermDepositParameters.TermMonths, CalculatorWorkflow.CurrentQuestion);
    }

    [Fact]
    public void ValidateAndStore_ShouldNot_Store_InvalidTermYears()
    {
        // Arrange
        CalculatorWorkflow.CurrentQuestion = TermDepositParameters.TermYears;
        var input = "Three";

        // Act
        CalculatorWorkflow.ValidateAndStore(input);

        // Assert
        Assert.False(CalculatorWorkflow.IsCurrentUserInputValidatedAndStored);
        Assert.Equal(TermDepositParameters.TermYears, CalculatorWorkflow.CurrentQuestion);
    }
    
    [Fact]
    public void MoveToPreviousQuestion_Should_GoBackTo_StartAmountFromInterestRate()
    {
        // Arrange
        CalculatorWorkflow.CurrentQuestion = TermDepositParameters.InterestRate;

        // Act
        CalculatorWorkflow.MoveToPreviousQuestion();

        // Assert
        Assert.Equal(TermDepositParameters.StartAmount, CalculatorWorkflow.CurrentQuestion);
    }

    [Fact]
    public void MoveToPreviousQuestion_ShouldNot_GoBack_FromStartAmount()
    {
        // Arrange
        CalculatorWorkflow.CurrentQuestion = TermDepositParameters.StartAmount;

        // Act
        CalculatorWorkflow.MoveToPreviousQuestion();

        // Assert
        Assert.Equal(TermDepositParameters.StartAmount,
            CalculatorWorkflow.CurrentQuestion);
    }

    [Fact]
    public void CalculateAndPrintFinalBalance_Should_Reset_After_Calculation()
    {
        // Arrange
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        CalculatorWorkflow.CurrentQuestion = TermDepositParameters.StartAmount;
        CalculatorWorkflow.ValidateAndStore("10000");
        CalculatorWorkflow.MoveToNextQuestion();
        CalculatorWorkflow.ValidateAndStore("1.10");
        CalculatorWorkflow.MoveToNextQuestion();
        CalculatorWorkflow.ValidateAndStore("3");
        CalculatorWorkflow.MoveToNextQuestion();
        CalculatorWorkflow.ValidateAndStore("0");
        CalculatorWorkflow.MoveToNextQuestion();
        CalculatorWorkflow.ValidateAndStore("monthly");

        // Act
        CalculatorWorkflow.MoveToNextQuestion();

        // Assert
        var output = consoleOutput.ToString();
        Assert.Contains("Final balance:", output);
        Assert.Equal(TermDepositParameters.StartAmount, CalculatorWorkflow.CurrentQuestion);
    }
}