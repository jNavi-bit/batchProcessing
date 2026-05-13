using batch_processing.Models;

namespace batch_processing.State;

public sealed class AppPreferencesState
{
    public event Action? OnChange;

    public AppThemeMode ThemeMode { get; private set; } = AppThemeMode.Dark;

    public AppLanguage Language { get; private set; } = AppLanguage.English;

    public bool IsDarkMode => ThemeMode == AppThemeMode.Dark;

    public void SetTheme(AppThemeMode themeMode)
    {
        if (ThemeMode == themeMode)
        {
            return;
        }

        ThemeMode = themeMode;
        OnChange?.Invoke();
    }

    public void SetLanguage(AppLanguage language)
    {
        if (Language == language)
        {
            return;
        }

        Language = language;
        OnChange?.Invoke();
    }
}
