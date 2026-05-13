using batch_processing.Models;

namespace batch_processing.Services;

public sealed class BatchBuilderService
{
    public const int MaxBatchSize = 4;

    public IReadOnlyList<BatchModel> BuildBatches(IReadOnlyList<ProcessCaptureModel> capturedProcesses)
    {
        var batches = new List<BatchModel>();

        for (var index = 0; index < capturedProcesses.Count; index += MaxBatchSize)
        {
            var batchNumber = batches.Count + 1;
            var processes = capturedProcesses
                .Skip(index)
                .Take(MaxBatchSize)
                .Select(process => new ProcessExecutionModel
                {
                    BatchNumber = batchNumber,
                    ProgrammerName = process.ProgrammerName,
                    ProgramId = process.ProgramId,
                    EstimatedMaxTime = process.EstimatedMaxTime,
                    OperationType = process.Operation.OperationType,
                    LeftOperand = process.Operation.LeftOperand,
                    RightOperand = process.Operation.RightOperand
                })
                .ToList();

            batches.Add(new BatchModel
            {
                BatchNumber = batchNumber,
                Processes = processes
            });
        }

        return batches;
    }
}
