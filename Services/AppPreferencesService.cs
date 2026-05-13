using Microsoft.JSInterop;
using batch_processing.Models;
using batch_processing.State;

namespace batch_processing.Services;

public sealed class AppPreferencesService
{
    private const string ThemeStorageKey = "batch-processing.theme";
    private const string LanguageStorageKey = "batch-processing.language";

    private readonly AppPreferencesState _appPreferencesState;
    private readonly IJSRuntime _jsRuntime;

    public AppPreferencesService(AppPreferencesState appPreferencesState, IJSRuntime jsRuntime)
    {
        _appPreferencesState = appPreferencesState;
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        var storedTheme = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", ThemeStorageKey);
        if (Enum.TryParse<AppThemeMode>(storedTheme, ignoreCase: true, out var themeMode))
        {
            _appPreferencesState.SetTheme(themeMode);
        }

        var storedLanguage = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", LanguageStorageKey);
        if (Enum.TryParse<AppLanguage>(storedLanguage, ignoreCase: true, out var language))
        {
            _appPreferencesState.SetLanguage(language);
        }
    }

    public async Task SetThemeAsync(AppThemeMode themeMode)
    {
        _appPreferencesState.SetTheme(themeMode);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", ThemeStorageKey, themeMode.ToString());
    }

    public async Task ToggleThemeAsync()
    {
        var nextThemeMode = _appPreferencesState.ThemeMode == AppThemeMode.Dark
            ? AppThemeMode.Light
            : AppThemeMode.Dark;

        await SetThemeAsync(nextThemeMode);
    }

    public async Task SetLanguageAsync(AppLanguage language)
    {
        _appPreferencesState.SetLanguage(language);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", LanguageStorageKey, language.ToString());
    }
}
