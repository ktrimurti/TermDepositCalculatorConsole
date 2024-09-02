# Term Deposit Calculator Console App

Based on: [Calculator URL](https://www.bendigobank.com.au/calculators/deposit-and-savings/)

## Parameters

| Name                       | Valid examples                                                         | Invalid examples |
|----------------------------|------------------------------------------------------------------------|------------------|
| Start deposit amount       | $10,000.00, 100000 (contains decimal)                                  | abc              |
| Interest rate              | 1.10%, 1.10 (contains decimal)                                         | %                |
| Investment term in year/s  | 3 (whole number)                                                       | abc, 2.2         |
| Investment term in month/s | 0 (whole number)                                                       | 2.2              |
| Interest paid              | monthly, quarterly, annually, at maturity (ignore capital / lowercase) | all other values |

## Navigation
- Press enter to submit entry
- Backspace to delete characters
- Left arrow key to go to and modify previous answer. NOTE: any current question answer value will not be saved and would need to be re-entered again.
- Ctrl+C to exit console app

## Requirements

- .NET 8.0 SDK: [Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Running the Application

1. Clone the repository:

   ```bash
   git clone <url>
   
2. Navigate to the project directory:
    ```bash
   cd TermDepositCalculatorConsole

3. Run the application:
    ```bash
   dotnet run

## Running the Tests
1. Navigate to the test project directory:
    ```bash
   cd ../TermDepositCalculatorConsole.Tests
   
2. Run the tests:
    ```bash
   dotnet test
