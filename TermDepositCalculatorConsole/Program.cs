namespace TermDepositCalculatorConsole;

public static class Program
{
    public static void Main(string[] args)
    {
        PrintIntroduction();

        while (true)
        {
            CalculatorWorkflow.PrintCurrentQuestion();
            var input = GetUserInput();

            if (CalculatorWorkflow.IsMovingToPreviousQuestion)
            {
                CalculatorWorkflow.IsMovingToPreviousQuestion = false;
                continue;
            }

            if (!string.IsNullOrWhiteSpace(input))
            {
                CalculatorWorkflow.ValidateAndStore(input);

                if (CalculatorWorkflow.IsCurrentUserInputValidatedAndStored)
                {
                    CalculatorWorkflow.MoveToNextQuestion();
                }
            }
            else
            {
                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        }
    }

    private static void PrintIntroduction()
    {
        Console.WriteLine("[CALCULATE YOUR RETURNS CALCULATOR]: Use our deposit calculator to forecast the return on your term deposit investment");
        Console.WriteLine("*** Navigation: Enter to submit, Backspace to delete, Left Arrow to go back to previous, Ctrl+C to exit ***");
    }

    private static string GetUserInput()
    {
        var input = "";
        while (true)
        {
            var key = Console.ReadKey(intercept: true);
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    Console.WriteLine();
                    CalculatorWorkflow.MoveToPreviousQuestion();
                    CalculatorWorkflow.IsMovingToPreviousQuestion = true;
                    return "";
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    return input; 
                case ConsoleKey.Backspace when input.Length > 0:
                    input = input[..^1];
                    Console.Write("\b \b");
                    break;
                default:
                    if (!char.IsControl(key.KeyChar))
                    {
                        Console.Write(key.KeyChar);
                        input += key.KeyChar;
                    }
                    break;
            }
        }
    }
}
