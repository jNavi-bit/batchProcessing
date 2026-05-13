using batch_processing.Models;
using batch_processing.Utilities;

namespace batch_processing.Services;

public sealed class OperationExecutionService
{
    public (double NumericResult, string ResultDisplay) Execute(ProcessExecutionModel process)
    {
        var result = process.OperationType switch
        {
            ProgramOperationType.Add => process.LeftOperand + process.RightOperand,
            ProgramOperationType.Subtract => process.LeftOperand - process.RightOperand,
            ProgramOperationType.Multiply => process.LeftOperand * process.RightOperand,
            ProgramOperationType.Divide => process.LeftOperand / process.RightOperand,
            ProgramOperationType.Modulo => (int)process.LeftOperand % (int)process.RightOperand,
            ProgramOperationType.Power => Math.Pow(process.LeftOperand, process.RightOperand),
            _ => throw new InvalidOperationException("Unsupported operation type.")
        };

        return (result, OperationFormatter.FormatNumber(result));
    }
}
