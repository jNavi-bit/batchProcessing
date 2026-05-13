using batch_processing.Utilities;

namespace batch_processing.Models;

public sealed class ProcessExecutionModel
{
    public int BatchNumber { get; set; }

    public string ProgrammerName { get; init; } = string.Empty;

    public int ProgramId { get; init; }

    public int EstimatedMaxTime { get; init; }

    public ProgramOperationType OperationType { get; init; }

    public double LeftOperand { get; init; }

    public double RightOperand { get; init; }

    public int ElapsedTime { get; set; }

    public ProcessStatus Status { get; set; } = ProcessStatus.Pending;

    public double? Result { get; set; }

    public string? ResultDisplay { get; set; }

    public int RemainingTime => Math.Max(EstimatedMaxTime - ElapsedTime, 0);

    public string OperationDisplay => OperationFormatter.FormatExpression(LeftOperand, OperationType, RightOperand);
}
