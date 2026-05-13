namespace batch_processing.Models;

public sealed class FinishedLogEntry
{
    public required FinishedLogEntryType Type { get; init; }

    public int? BatchNumber { get; init; }

    public int? ProgramId { get; init; }

    public string? OperationDisplay { get; init; }

    public string? ResultDisplay { get; init; }

    /// <summary>Seconds the process ran in the simulator when it completed (snapshot).</summary>
    public int? DurationSeconds { get; init; }

    public static FinishedLogEntry BatchStarted(int batchNumber)
    {
        return new FinishedLogEntry
        {
            Type = FinishedLogEntryType.BatchStarted,
            BatchNumber = batchNumber
        };
    }

    public static FinishedLogEntry BatchCompleted(int batchNumber)
    {
        return new FinishedLogEntry
        {
            Type = FinishedLogEntryType.BatchCompleted,
            BatchNumber = batchNumber
        };
    }

    public static FinishedLogEntry ProcessCompleted(ProcessExecutionModel process)
    {
        return new FinishedLogEntry
        {
            Type = FinishedLogEntryType.ProcessCompleted,
            BatchNumber = process.BatchNumber,
            ProgramId = process.ProgramId,
            OperationDisplay = process.OperationDisplay,
            ResultDisplay = process.ResultDisplay,
            DurationSeconds = process.ElapsedTime
        };
    }
}
