# Batch Processing Simulator

**Operating systems coursework** — a small **didactic** Blazor app for teaching **batch processing** and fixed-size queues (max **4** processes per batch). Implemented in **C#** on **.NET 10** with **Blazor WebAssembly**: capture programs in order, run a **1-second** simulation tick, and inspect KPIs, the active batch, the running process, the queue, and a **finished log** (results + execution time). **MudBlazor** UI, light/dark theme, **EN / ES / DE** copy.

**Flow:** validate capture → `BatchBuilderService` builds batches → `SimulationEngineService` ticks and completes work via `OperationExecutionService` → `SimulationState` holds the log.

**Stack:** C#, .NET 10 (`net10.0`), Blazor WASM, MudBlazor, scoped services for state and i18n.

```bash
cd batch_processing
dotnet run
```
