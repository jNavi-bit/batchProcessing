using MudBlazor;

namespace batch_processing.Themes;

public static class BatchProcessingTheme
{
    public static MudTheme Dashboard => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#2563EB",
            Secondary = "#0F766E",
            Info = "#4F46E5",
            Success = "#059669",
            Warning = "#D97706",
            Error = "#DC2626",
            Background = "#F4F7FB",
            Surface = "#FFFFFF",
            AppbarBackground = "#FFFFFF",
            AppbarText = "#111827",
            DrawerBackground = "#F8FAFC",
            DrawerText = "#334155",
            DrawerIcon = "#475569",
            TextPrimary = "#111827",
            TextSecondary = "#475569",
            ActionDefault = "#64748B"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#5C8F86",
            Secondary = "#9A8B6E",
            Info = "#7A6F90",
            Success = "#4D8068",
            Warning = "#B0893C",
            Error = "#B85C5C",
            Background = "#030303",
            Surface = "#0A0A0A",
            AppbarBackground = "#030303",
            AppbarText = "#E5E5E5",
            DrawerBackground = "#050505",
            DrawerText = "#D4D4D4",
            DrawerIcon = "#A3A3A3",
            TextPrimary = "#EDEDED",
            TextSecondary = "#C4C4C4",
            ActionDefault = "#A3A3A3"
        }
    };
}
