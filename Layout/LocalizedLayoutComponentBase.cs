using Microsoft.AspNetCore.Components;
using batch_processing.Models;
using batch_processing.Services;
using batch_processing.State;

namespace batch_processing.Layout;

public abstract class LocalizedLayoutComponentBase : LayoutComponentBase, IDisposable
{
    [Inject]
    protected UiTextService UiTextService { get; set; } = default!;

    [Inject]
    protected AppPreferencesState AppPreferencesState { get; set; } = default!;

    protected override void OnInitialized()
    {
        AppPreferencesState.OnChange += HandlePreferencesChanged;
    }

    protected string T(string key)
    {
        return UiTextService.Get(AppPreferencesState.Language, key);
    }

    protected string Tf(string key, params object[] arguments)
    {
        return UiTextService.Format(AppPreferencesState.Language, key, arguments);
    }

    protected static string GetLanguageLabelKey(AppLanguage language)
    {
        return language switch
        {
            AppLanguage.Spanish => "app.language.spanish",
            AppLanguage.German => "app.language.german",
            _ => "app.language.english"
        };
    }

    private void HandlePreferencesChanged()
    {
        _ = InvokeAsync(StateHasChanged);
    }

    public virtual void Dispose()
    {
        AppPreferencesState.OnChange -= HandlePreferencesChanged;
    }
}
