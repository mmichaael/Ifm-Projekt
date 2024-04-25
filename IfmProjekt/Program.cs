using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Geben Sie eine mathematische Formel ein:");
        string input = Console.ReadLine();

        // Schritt 1: Klammerrechnung
        input = SolveBrackets(input);

        // Schritt 2: Punktrechnung
        input = SolveOperations(input, '*', '/');

        // Schritt 3: Strichrechnung
        input = SolveOperations(input, '+', '-');

        // Konvertierung des Ergebnisses in eine Gleitkommazahl und Ausgabe
        double result = double.Parse(input);
        Console.WriteLine("Ergebnis: " + result);
    }

    static string SolveBrackets(string input)
    {
        while (input.Contains('('))
        {
            int startIndex = input.LastIndexOf('(');
            int endIndex = input.IndexOf(')', startIndex);
            string expression = input.Substring(startIndex + 1, endIndex - startIndex - 1);
            double result = EvaluateExpression(expression);
            input = input.Remove(startIndex, endIndex - startIndex + 1).Insert(startIndex, result.ToString());
        }
        return input;
    }

    static string SolveOperations(string input, char op1, char op2)
    {
        int startIndex = 0;
        while (startIndex < input.Length)
        {
            int opIndex = input.IndexOfAny(new char[] { op1, op2 }, startIndex);
            if (opIndex == -1)
                break;

            int operand1Start = FindOperandStart(input, opIndex - 1);
            int operand2End = FindOperandEnd(input, opIndex + 1);

            double result = EvaluateExpression(input.Substring(operand1Start, operand2End - operand1Start + 1));
            input = input.Remove(operand1Start, operand2End - operand1Start + 1).Insert(operand1Start, result.ToString());

            startIndex = operand1Start + result.ToString().Length;
        }
        return input;
    }

    static int FindOperandStart(string input, int index)
    {
        while (index >= 0 && (char.IsDigit(input[index]) || input[index] == '.'))
        {
            index--;
        }
        return index + 1;
    }

    static int FindOperandEnd(string input, int index)
    {
        while (index < input.Length && (char.IsDigit(input[index]) || input[index] == '.'))
        {
            index++;
        }
        return index - 1;
    }

    static double EvaluateExpression(string expression)
    {
        string[] tokens = expression.Split(new char[] { '+', '-', '*', '/' });
        double operand1 = double.Parse(tokens[0]);
        double operand2 = double.Parse(tokens[1]);
        char op = expression[expression.IndexOfAny(new char[] { '+', '-', '*', '/' })];

        switch (op)
        {
            case '+':
                return operand1 + operand2;
            case '-':
                return operand1 - operand2;
            case '*':
                return operand1 * operand2;
            case '/':
                return operand1 / operand2;
            default:
                throw new ArgumentException("Ungültiger Operator");
        }
    }
}