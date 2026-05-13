namespace batch_processing.Models;

public sealed class ProcessCaptureModel
{
    public string ProgrammerName { get; set; } = string.Empty;

    public int ProgramId { get; set; }

    public int EstimatedMaxTime { get; set; }

    public OperationInputModel Operation { get; set; } = new();

    public ProcessCaptureModel Clone()
    {
        return new ProcessCaptureModel
        {
            ProgrammerName = ProgrammerName,
            ProgramId = ProgramId,
            EstimatedMaxTime = EstimatedMaxTime,
            Operation = new OperationInputModel
            {
                OperationType = Operation.OperationType,
                LeftOperand = Operation.LeftOperand,
                RightOperand = Operation.RightOperand
            }
        };
    }
}
