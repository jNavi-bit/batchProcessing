using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using batch_processing;
using batch_processing.Services;
using batch_processing.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddScoped<SimulationState>();
builder.Services.AddScoped<AppPreferencesState>();
builder.Services.AddScoped<AppPreferencesService>();
builder.Services.AddScoped<ProcessValidationService>();
builder.Services.AddScoped<BatchBuilderService>();
builder.Services.AddScoped<OperationExecutionService>();
builder.Services.AddScoped<SimulationEngineService>();
builder.Services.AddScoped<UiTextService>();

await builder.Build().RunAsync();
