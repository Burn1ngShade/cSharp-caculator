//static class so instance dosnt need to / cant be created
static class Caculator
{
    const decimal Pi = (decimal)3.1415926535897;

    //hold all operators which effect 2 numbers
    static char[] doubleOperators = { '*', '+', '^', '-', '/' };
    //hold all operators which effect 1 number
    static char[] singleOperators = { '%'};
    //hold all shorthand speak eg: p instead of 3.141592....
    static char[] shortHandOperators = {'p'};

    //enter caculator loop : only part of class that can be called from outside
    public static void CaculatorInterface()
    {
        Console.WriteLine("\n__________  Isaac's Caculator __________\n");
        while (true)
        {
            //get user question find answer return repeat
            string userInput = ValidateString(Console.ReadLine() + "");
            if (userInput == "Error") continue;
            decimal result = ValidatedCaculationStringToIntAnswer(userInput);
            Logger.LogLine(result.ToString(), ConsoleColor.Blue);
        }
    }

    private static decimal ValidatedCaculationStringToIntAnswer(string caculationString)
    {
        //creates 2 new lists one for holding location of the operator and one for holding the priority of it
        List<char> operatorSigns = new List<char>();
        List<int> operatorPriority = new List<int>();
        //list of all numbers in caculation
        List<decimal> caculationNums = new List<decimal>();

        //finds all operators and sets priorty
        // get all unique numbers
        string currentNum = "";
        for (int i = 0; i < caculationString.Length; i++)
        {
            if (doubleOperators.Contains(caculationString[i]) && i != 0 || singleOperators.Contains(caculationString[i]))
            {
                operatorSigns.Add(caculationString[i]);

                if (caculationString[i] == doubleOperators[2] || caculationString[i] == singleOperators[0]) operatorPriority.Add(0);
                if (caculationString[i] == doubleOperators[0] || caculationString[i] == doubleOperators[doubleOperators.Length - 1]) operatorPriority.Add(1);
                if (caculationString[i] == doubleOperators[1] || caculationString[i] == doubleOperators[doubleOperators.Length - 2]) operatorPriority.Add(2);

                if (currentNum != "")
                {
                    decimal.TryParse(currentNum, out decimal num);
                    caculationNums.Add(num);
                    currentNum = "";
                }
            }
            else
            {
                currentNum += caculationString[i];
            }
        }
        decimal.TryParse(currentNum, out decimal num2);
        caculationNums.Add(num2);
        currentNum = "";

        //while there are still operations to be complete loop
        int x = 0;
        bool foundCaculation = false;
        while (operatorSigns.Count != 0)
        {
            foundCaculation = false;
            for (int i = 0; i < operatorSigns.Count; i++)
            {
                if (operatorPriority[i] == x)
                {
                    if (doubleOperators.Contains(operatorSigns[i]))
                    {
                        //works out and changes array accordinlgy
                        decimal caculationResult = CaculationWithCharOperator(operatorSigns[i], caculationNums[i], caculationNums[i + 1]);
                        operatorSigns.RemoveAt(i);
                        operatorPriority.RemoveAt(i);
                        caculationNums[i] = caculationResult;
                        caculationNums.RemoveAt(i + 1);
                        foundCaculation = true;
                    }
                    else
                    {
                        //works out and changes array accordinlgy
                        decimal caculationResult = CaculationWithCharOperator(operatorSigns[i], caculationNums[i]);
                        operatorSigns.RemoveAt(i);
                        operatorPriority.RemoveAt(i);
                        caculationNums[i] = caculationResult;
                        foundCaculation = true;
                    }

                    break;
                }
            }
            //if all caculations at this priority have been done, move to the next priority
            if (foundCaculation == false) x += 1;
        }
        //return answer
        return caculationNums[0];
    }

    private static decimal CaculationWithCharOperator(char operatorChar, decimal num1, decimal num2)
    {
        switch (operatorChar)
        {
            case '+':
                return num1 + num2;
            case '-':
                return num1 - num2;
            case '*':
                return num1 * num2;
            case '/':
                return num1 / num2;
            case '^':
                return (decimal)Math.Pow((double)num1, (double)num2);

        }
        return 0;

    }

    private static decimal CaculationWithCharOperator(char operatorChar, decimal num)
    {
        switch (operatorChar)
        {
            case '%':
                return (decimal)Math.Sqrt((double)num);
        }
        return 0;
    }

    private static string ValidateString(string input)
    {
        if (input.Length == 0)
        {
            Logger.LogLine("Error: Caculation Can Not Be 0 Charchters Long", ConsoleColor.DarkRed);
            return "Error";
        }

        if (input[0] == '/')
        {
            return Debug(input);
        }

        string newInput = "";

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != ' ')
            {
                newInput += input[i];
            }
        }

        for (int i = 0; i < newInput.Length - 1; i++)
        {
            if (doubleOperators.Contains(newInput[i]) && doubleOperators.Contains(newInput[i + 1]))
            {
                Logger.LogLine("Error: Multiple Operators Cant Be Used One After Another", ConsoleColor.DarkRed);
                return "Error";
            }
            if (newInput[i] == '.' && newInput[i + 1] == '.')
            {
                Logger.LogLine("Error: Multiple Decimal Points Cant Be Used One After Another", ConsoleColor.DarkRed);
                return "Error";
            }
            if (!doubleOperators.Contains(newInput[i]) && !singleOperators.Contains(newInput[i]) && !Char.IsDigit(newInput[i]) && newInput[i] != '.')
            {
                Logger.LogLine("Error: Unreadable Character In Caculation", ConsoleColor.DarkRed);
                return "Error";
            }
        }
      
        if (doubleOperators.Contains(newInput[newInput.Length - 1]) || singleOperators.Contains(newInput[newInput.Length - 1]))
        {
            Logger.LogLine("Error: Last Character Of A Caculation Can Not Be A Operator", ConsoleColor.DarkRed);
            return "Error";
        }
        

        return newInput;
    }

    private static string Debug(string input)
    {
        if (input == "/test" || input == "/tests")
        {
            string[] testCaculations = { "1 + 2 - 3 * 4 / 5", "1000*1000/1000/1000+1000-1000", "1000 / 1000 / 1000 / 1000 / 1000", "1.5 * 2", "1 / 2 * 3 - 4 + 5", "5 ^ 2", "2 ^ 2.1", "%100+%100", "%10+%10" };
            string[] expectedCaculationResults = { "0.6", "1", "0.000000001", "3.0", "2.5", "25", "4.28709385014517", "20", "6.32455532033676" };
            bool[] testCaculationsSuccessResults = new bool[testCaculations.Length];

            for (int i = 0; i < testCaculations.Length; i++)
            {
                testCaculationsSuccessResults[i] = ValidatedCaculationStringToIntAnswer(testCaculations[i]).ToString() == expectedCaculationResults[i];
            }

            for (int i = 0; i < testCaculations.Length; i++)
            {
                if (testCaculationsSuccessResults[i] == true) Logger.LogLine($"Test Caculation {i + 1} Was A Success", ConsoleColor.DarkGreen);
                else
                {
                    Logger.LogLine($"Test Caculation {i + 1} Was A Failure, Expected Result Was {expectedCaculationResults[i]}, Actual Result Was {ValidatedCaculationStringToIntAnswer(testCaculations[i])}", ConsoleColor.DarkRed);
                }

            }
        }
        else Logger.LogLine($"Error: {input} Is Not A Valid Command", ConsoleColor.DarkRed);
        return "Error";
    }
}