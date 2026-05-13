namespace batch_processing.Models;

public sealed class BatchModel
{
    public int BatchNumber { get; init; }

    public IReadOnlyList<ProcessExecutionModel> Processes { get; init; } = [];
}
