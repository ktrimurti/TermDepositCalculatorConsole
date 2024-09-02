using System.Text.RegularExpressions;

namespace TermDepositCalculatorConsole
{
    public static class CalculatorWorkflow
    {
        private static readonly Dictionary<TermDepositParameters, string> Questions = new()
        {
            { TermDepositParameters.StartAmount, "Start deposit amount (e.g. $10,000):" },
            { TermDepositParameters.InterestRate, "Interest rate (e.g. 1.10%):" },
            { TermDepositParameters.TermYears, "Investment term years (e.g. 3 years or 3):" },
            { TermDepositParameters.TermMonths, "Investment term months (e.g. 10 months or 10):" },
            { TermDepositParameters.InterestPaidFrequency, "Interest paid (monthly, quarterly, annually, at maturity):" }
        };

        private static readonly Dictionary<TermDepositParameters, decimal> UserInput = [];

        public static TermDepositParameters CurrentQuestion { get; set; } = TermDepositParameters.StartAmount;

        public static bool IsMovingToPreviousQuestion { get; set; }
        public static bool IsCurrentUserInputValidatedAndStored { get; private set; }

        public static void ValidateAndStore(string input)
        {
            IsCurrentUserInputValidatedAndStored = false;
            input = Clean(input);

            switch (CurrentQuestion)
            {
                case TermDepositParameters.StartAmount:
                case TermDepositParameters.InterestRate:
                    if (decimal.TryParse(input, out decimal number))
                    {
                        Store(Math.Abs(number));
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please provide a valid format.");
                    }
                    break;

                case TermDepositParameters.TermYears:
                case TermDepositParameters.TermMonths:
                    if (int.TryParse(input, out int numberTerm))
                    {
                        Store(numberTerm);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please provide a valid whole number format.");
                    }
                    break;

                case TermDepositParameters.InterestPaidFrequency:
                    ProcessInterestPaidFrequency(input);
                    break;

                default:
                    Console.WriteLine("Invalid parameter.");
                    break;
            }
        }

        public static void PrintCurrentQuestion()
        {
            if (Questions.TryGetValue(CurrentQuestion, out string? question))
            {
                Console.WriteLine(question);
            }
        }

        public static void MoveToNextQuestion()
        {
            if (CurrentQuestion < TermDepositParameters.InterestPaidFrequency)
            {
                CurrentQuestion++;
            }
            else
            {
                CalculateAndPrintFinalBalance();
                ClearSession();
            }
        }

        private static void ClearSession()
        {
            UserInput.Clear();
            CurrentQuestion = TermDepositParameters.StartAmount;
        }

        public static void MoveToPreviousQuestion()
        {
            if (CurrentQuestion > 0)
            {
                CurrentQuestion--;
            }
            else
            {
                Console.WriteLine("There is no previous question to go back to from the current question.");
            }
        }

        private static void Store(decimal input)
        {
            UserInput[CurrentQuestion] = input;
            IsCurrentUserInputValidatedAndStored = true;
        }

        private static void ProcessInterestPaidFrequency(string input)
        {
            if (Enum.TryParse(input, true, out InterestPaidFrequency frequency) 
                && Enum.IsDefined(typeof(InterestPaidFrequency), frequency))
            {
                Store((decimal)frequency);
            }
            else
            {
                Console.WriteLine("Invalid input, please provide a valid interest paid frequency.");
            }
        }

        private static string Clean(string input)
        {
            return CurrentQuestion switch
            {
                TermDepositParameters.StartAmount => Regex.Replace(input, @"[^\d.]", ""),
                TermDepositParameters.InterestRate => input.Replace("%", "").Trim(),
                TermDepositParameters.TermYears or TermDepositParameters.TermMonths => Regex.Replace(input, @"\D", ""),
                TermDepositParameters.InterestPaidFrequency => input.ToLower().Replace(" ", "").Trim(),
                _ => input
            };
        }

        private static void CalculateAndPrintFinalBalance()
        {
            var startAmount = UserInput[TermDepositParameters.StartAmount];
            var interestRate = UserInput[TermDepositParameters.InterestRate];
            var termYears = UserInput[TermDepositParameters.TermYears];
            var termMonths = UserInput[TermDepositParameters.TermMonths];
            var interestPaidFrequency = (InterestPaidFrequency)(int)UserInput[TermDepositParameters.InterestPaidFrequency];

            var termDeposit = new TermDeposit(startAmount, interestRate, termYears, termMonths, (decimal)interestPaidFrequency);
            termDeposit.Calculate();

            var interestPaidFrequencyName = Enum.GetName(typeof(InterestPaidFrequency), interestPaidFrequency) ?? "Unknown";
            
            Console.WriteLine($"Start amount: {startAmount}, Interest rate: {interestRate}%, Term: {termYears} years {termMonths} months, " +
                                $"Interest paid: {interestPaidFrequencyName}");
            Console.WriteLine($"Final balance: ${termDeposit.FinalBalance}");
            Console.WriteLine("*** Final balance calculated, starting new calculator session ***");
        }
    }
}
