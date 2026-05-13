using batch_processing.Models;
using batch_processing.State;

namespace batch_processing.Services;

public sealed class SimulationEngineService
{
    private readonly SimulationState _simulationState;
    private readonly BatchBuilderService _batchBuilderService;
    private readonly OperationExecutionService _operationExecutionService;
    private CancellationTokenSource? _simulationCancellationTokenSource;
    private int _currentBatchIndex;
    private int _currentProcessIndex;

    public SimulationEngineService(
        SimulationState simulationState,
        BatchBuilderService batchBuilderService,
        OperationExecutionService operationExecutionService)
    {
        _simulationState = simulationState;
        _batchBuilderService = batchBuilderService;
        _operationExecutionService = operationExecutionService;
    }

    public Task StartAsync()
    {
        if (!_simulationState.CanStart)
        {
            return Task.CompletedTask;
        }

        CancelCurrentRun();

        var batches = _batchBuilderService.BuildBatches(_simulationState.CapturedProcesses);
        if (batches.Count == 0)
        {
            return Task.CompletedTask;
        }

        _currentBatchIndex = 0;
        _currentProcessIndex = 0;
        _simulationState.StartSimulation(batches);

        _simulationCancellationTokenSource = new CancellationTokenSource();
        _ = RunSimulationLoopAsync(_simulationCancellationTokenSource.Token);

        return Task.CompletedTask;
    }

    public void Reset()
    {
        CancelCurrentRun();
        _simulationState.ResetAll();
    }

    private async Task RunSimulationLoopAsync(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        try
        {
            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                if (_simulationState.RunStatus != SimulationRunStatus.Running || _simulationState.CurrentProcess is null)
                {
                    break;
                }

                _simulationState.AdvanceOneSecond();

                var currentProcess = _simulationState.CurrentProcess;
                if (currentProcess.RemainingTime > 0)
                {
                    continue;
                }

                var result = _operationExecutionService.Execute(currentProcess);
                _simulationState.CompleteCurrentProcess(result.NumericResult, result.ResultDisplay, notify: false);

                var currentBatch = _simulationState.CurrentBatch;
                if (currentBatch is null)
                {
                    _simulationState.CompleteSimulation();
                    break;
                }

                if (_currentProcessIndex + 1 < currentBatch.Processes.Count)
                {
                    _currentProcessIndex++;
                    _simulationState.ActivateProcess(currentBatch.Processes[_currentProcessIndex]);
                    continue;
                }

                _simulationState.MarkBatchCompleted(currentBatch.BatchNumber, notify: false);

                if (_currentBatchIndex + 1 < _simulationState.Batches.Count)
                {
                    _currentBatchIndex++;
                    _currentProcessIndex = 0;

                    var nextBatch = _simulationState.Batches[_currentBatchIndex];
                    _simulationState.ActivateBatch(nextBatch, notify: false);
                    _simulationState.ActivateProcess(nextBatch.Processes[_currentProcessIndex]);
                    continue;
                }

                _simulationState.CompleteSimulation();
                break;
            }
        }
        catch (OperationCanceledException)
        {
        }
    }

    private void CancelCurrentRun()
    {
        if (_simulationCancellationTokenSource is null)
        {
            return;
        }

        _simulationCancellationTokenSource.Cancel();
        _simulationCancellationTokenSource.Dispose();
        _simulationCancellationTokenSource = null;
    }
}
