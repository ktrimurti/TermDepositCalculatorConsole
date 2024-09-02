namespace TermDepositCalculatorConsole;


public enum InterestPaidFrequency
{
    Monthly = 12,
    Quarterly = 4,
    Annually = 1,
    AtMaturity = 0
}

public class TermDeposit
{
    private decimal StartAmount { get; }
    private decimal InterestRate { get; }
    private decimal TermYears { get; }
    private decimal TermMonths { get; }
    private decimal InterestPaidFrequency { get; }
    public decimal FinalBalance { get; private set; }

    public TermDeposit(decimal startAmount, decimal interestRate, decimal termYears, decimal termMonths, decimal interestPaidFrequency)
    {
        StartAmount = startAmount;
        InterestRate = interestRate;
        TermYears = termYears;
        TermMonths = termMonths;
        InterestPaidFrequency = interestPaidFrequency;
    }

    public void Calculate()
    {
        var term = TermYears + (TermMonths / 12);
        if (InterestPaidFrequency > 0)
        {
            CalculateCompoundInterest(term);
        }
        else
        {
            CalculateAtMaturityInterest(term);
        }

        FinalBalance = Math.Round(FinalBalance, 0);
    }

    private void CalculateCompoundInterest(decimal term)
    {
        var totalPeriods = term * InterestPaidFrequency;
        var ratePerPeriod = InterestRate / (InterestPaidFrequency * 100);
        FinalBalance = StartAmount * (decimal)Math.Pow((double)(1 + ratePerPeriod), (double)totalPeriods);
    }

    private void CalculateAtMaturityInterest(decimal term)
    {
        FinalBalance = StartAmount * (1 + InterestRate / 100 * term);
    }
}