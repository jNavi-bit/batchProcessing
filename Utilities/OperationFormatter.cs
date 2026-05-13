using System.Globalization;
using batch_processing.Models;

namespace batch_processing.Utilities;

public static class OperationFormatter
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    public static string FormatExpression(double leftOperand, ProgramOperationType operationType, double rightOperand)
    {
        return $"{FormatNumber(leftOperand)} {GetOperatorSymbol(operationType)} {FormatNumber(rightOperand)}";
    }

    public static string FormatNumber(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
        {
            return value.ToString(Culture);
        }

        var roundedValue = Math.Round(value, 4);

        return Math.Abs(roundedValue % 1) < 0.0001
            ? roundedValue.ToString("0", Culture)
            : roundedValue.ToString("0.####", Culture);
    }

    public static string GetOperatorLabel(ProgramOperationType operationType)
    {
        return operationType switch
        {
            ProgramOperationType.Add => "Addition",
            ProgramOperationType.Subtract => "Subtraction",
            ProgramOperationType.Multiply => "Multiplication",
            ProgramOperationType.Divide => "Division",
            ProgramOperationType.Modulo => "Modulo",
            ProgramOperationType.Power => "Power",
            _ => "Operation"
        };
    }

    private static string GetOperatorSymbol(ProgramOperationType operationType)
    {
        return operationType switch
        {
            ProgramOperationType.Add => "+",
            ProgramOperationType.Subtract => "-",
            ProgramOperationType.Multiply => "*",
            ProgramOperationType.Divide => "/",
            ProgramOperationType.Modulo => "%",
            ProgramOperationType.Power => "^",
            _ => "?"
        };
    }
}
