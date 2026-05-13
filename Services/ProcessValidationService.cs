using batch_processing.Models;

namespace batch_processing.Services;

public sealed class ProcessValidationService
{
    public IReadOnlyList<string> Validate(ProcessCaptureModel process, IEnumerable<ProcessCaptureModel> existingProcesses)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(process.ProgrammerName))
        {
            errors.Add("validation.programmerNameRequired");
        }

        if (process.ProgramId <= 0)
        {
            errors.Add("validation.programIdPositive");
        }

        if (existingProcesses.Any(existing => existing.ProgramId == process.ProgramId))
        {
            errors.Add("validation.programIdUnique");
        }

        if (process.EstimatedMaxTime <= 0)
        {
            errors.Add("validation.estimatedMaxTimePositive");
        }

        if (!IsFinite(process.Operation.LeftOperand) || !IsFinite(process.Operation.RightOperand))
        {
            errors.Add("validation.operandsValid");
        }

        if (process.Operation.OperationType is ProgramOperationType.Divide or ProgramOperationType.Modulo &&
            Math.Abs(process.Operation.RightOperand) < 0.0000001)
        {
            errors.Add("validation.divisionNonZero");
        }

        if (process.Operation.OperationType == ProgramOperationType.Modulo &&
            (!IsWholeNumber(process.Operation.LeftOperand) || !IsWholeNumber(process.Operation.RightOperand)))
        {
            errors.Add("validation.moduloInteger");
        }

        return errors;
    }

    private static bool IsFinite(double value)
    {
        return !double.IsNaN(value) && !double.IsInfinity(value);
    }

    private static bool IsWholeNumber(double value)
    {
        return Math.Abs(value % 1) < 0.0000001;
    }
}
