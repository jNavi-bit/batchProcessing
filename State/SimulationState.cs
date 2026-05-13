using batch_processing.Models;
using batch_processing.Services;

namespace batch_processing.State;

public sealed class SimulationState
{
    private readonly List<ProcessCaptureModel> _capturedProcesses = [];
    private readonly List<BatchModel> _batches = [];
    private readonly List<FinishedLogEntry> _finishedLog = [];

    public event Action? OnChange;

    public IReadOnlyList<ProcessCaptureModel> CapturedProcesses => _capturedProcesses;

    public IReadOnlyList<BatchModel> Batches => _batches;

    public IReadOnlyList<FinishedLogEntry> FinishedLog => _finishedLog;

    public BatchModel? CurrentBatch { get; private set; }

    public ProcessExecutionModel? CurrentProcess { get; private set; }

    public int GlobalCounter { get; private set; }

    public SimulationRunStatus RunStatus { get; private set; } = SimulationRunStatus.Idle;

    public int ExpectedBatchCount => _capturedProcesses.Count == 0
        ? 0
        : (int)Math.Ceiling(_capturedProcesses.Count / (double)BatchBuilderService.MaxBatchSize);

    public int TotalBatchCount => _batches.Count > 0 ? _batches.Count : ExpectedBatchCount;

    public int PendingBatchCount => RunStatus switch
    {
        SimulationRunStatus.Idle => Math.Max(ExpectedBatchCount - 1, 0),
        SimulationRunStatus.Running when CurrentBatch is not null => Math.Max(_batches.Count - CurrentBatch.BatchNumber, 0),
        _ => 0
    };

    public int CompletedProcessCount => _batches
        .SelectMany(batch => batch.Processes)
        .Count(process => process.Status == ProcessStatus.Completed);

    public bool CanCapture => RunStatus == SimulationRunStatus.Idle;

    public bool CanStart => RunStatus == SimulationRunStatus.Idle && _capturedProcesses.Count > 0;

    public bool CanReset => _capturedProcesses.Count > 0 || _batches.Count > 0 || _finishedLog.Count > 0 || GlobalCounter > 0;

    public void AddCapturedProcess(ProcessCaptureModel process)
    {
        if (!CanCapture)
        {
            return;
        }

        _capturedProcesses.Add(process.Clone());
        NotifyStateChanged();
    }

    public void StartSimulation(IReadOnlyList<BatchModel> batches)
    {
        _batches.Clear();
        _batches.AddRange(batches);
        _finishedLog.Clear();

        foreach (var process in _batches.SelectMany(batch => batch.Processes))
        {
            process.ElapsedTime = 0;
            process.Result = null;
            process.ResultDisplay = null;
            process.Status = ProcessStatus.Ready;
        }

        CurrentBatch = _batches.FirstOrDefault();
        CurrentProcess = CurrentBatch?.Processes.FirstOrDefault();
        GlobalCounter = 0;
        RunStatus = SimulationRunStatus.Running;

        if (CurrentProcess is not null)
        {
            CurrentProcess.Status = ProcessStatus.Running;
        }

        if (CurrentBatch is not null)
        {
            _finishedLog.Add(FinishedLogEntry.BatchStarted(CurrentBatch.BatchNumber));
        }

        NotifyStateChanged();
    }

    public void AdvanceOneSecond()
    {
        if (RunStatus != SimulationRunStatus.Running || CurrentProcess is null)
        {
            return;
        }

        GlobalCounter++;
        CurrentProcess.ElapsedTime = Math.Min(CurrentProcess.ElapsedTime + 1, CurrentProcess.EstimatedMaxTime);
        NotifyStateChanged();
    }

    public void CompleteCurrentProcess(double numericResult, string resultDisplay, bool notify = true)
    {
        if (CurrentProcess is null)
        {
            return;
        }

        CurrentProcess.Result = numericResult;
        CurrentProcess.ResultDisplay = resultDisplay;
        CurrentProcess.Status = ProcessStatus.Completed;
        _finishedLog.Add(FinishedLogEntry.ProcessCompleted(CurrentProcess));

        if (notify)
        {
            NotifyStateChanged();
        }
    }

    public void MarkBatchCompleted(int batchNumber, bool notify = true)
    {
        _finishedLog.Add(FinishedLogEntry.BatchCompleted(batchNumber));

        if (notify)
        {
            NotifyStateChanged();
        }
    }

    public void ActivateBatch(BatchModel batch, bool notify = true)
    {
        CurrentBatch = batch;
        _finishedLog.Add(FinishedLogEntry.BatchStarted(batch.BatchNumber));

        if (notify)
        {
            NotifyStateChanged();
        }
    }

    public void ActivateProcess(ProcessExecutionModel process, bool notify = true)
    {
        if (CurrentProcess is not null && CurrentProcess.Status == ProcessStatus.Running)
        {
            CurrentProcess.Status = ProcessStatus.Ready;
        }

        CurrentProcess = process;
        CurrentProcess.Status = ProcessStatus.Running;

        if (notify)
        {
            NotifyStateChanged();
        }
    }

    public void CompleteSimulation()
    {
        CurrentBatch = null;
        CurrentProcess = null;
        RunStatus = SimulationRunStatus.CompletedPaused;
        NotifyStateChanged();
    }

    public void ResetAll()
    {
        _capturedProcesses.Clear();
        _batches.Clear();
        _finishedLog.Clear();
        CurrentBatch = null;
        CurrentProcess = null;
        GlobalCounter = 0;
        RunStatus = SimulationRunStatus.Idle;
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
