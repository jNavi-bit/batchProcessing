namespace batch_processing.Models;

public sealed class OperationInputModel
{
    public ProgramOperationType OperationType { get; set; } = ProgramOperationType.Add;

    public double LeftOperand { get; set; }

    public double RightOperand { get; set; }
}
