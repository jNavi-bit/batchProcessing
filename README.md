# Batch Processing Simulator

**Operating systems coursework** — a small **didactic** Blazor app for teaching **batch processing** and fixed-size queues (max **4** processes per batch). Implemented in **C#** on **.NET 10** with **Blazor WebAssembly**: capture programs in order, run a **1-second** simulation tick, and inspect KPIs, the active batch, the running process, the queue, and a **finished log** (results + execution time). **MudBlazor** UI, light/dark theme, **EN / ES / DE** copy.

**Flow:** validate capture → `BatchBuilderService` builds batches → `SimulationEngineService` ticks and completes work via `OperationExecutionService` → `SimulationState` holds the log.

**Stack:** C#, .NET 10 (`net10.0`), Blazor WASM, MudBlazor, scoped services for state and i18n.

```bash
cd batch_processing
dotnet run
```

Publish: `dotnet publish -c Release`

## GitHub Pages

This repo is set up for **GitHub Actions → Pages** (see `.github/workflows/gh-pages.yml`).

1. On GitHub: **Settings → Pages → Build and deployment → Source**: choose **GitHub Actions** (not “Deploy from a branch”).
2. Push to **`main`** or **`master`** (or run the workflow manually under **Actions**).
3. The workflow sets `<base href>` to `/<repository-name>/` (or `/` if the repo is a `*.github.io` user site). It adds **`.nojekyll`** (so Jekyll does not strip `_framework`) and **`404.html`** (SPA fallback).

**Monorepo:** If `batch_processing` is not the git root, move the workflow to the repository root and add `defaults.run.working-directory: batch_processing` (or prefix paths with `batch_processing/`).
