using MudBlazor;

namespace OneLife.Web.Theme;

public static class OneLifeTheme
{
    public static readonly MudTheme Default = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#34C759",
            PrimaryDarken = "#28A745",
            PrimaryLighten = "#5DDC7A",
            AppbarBackground = "#FFFFFF",
            Background = "#F2F2F7",
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#34C759",
            PrimaryDarken = "#28A745",
            PrimaryLighten = "#5DDC7A",
        },
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "12px",
        },
        Typography = new Typography
        {
            Default = new Default
            {
                FontFamily = ["Inter", "Roboto", "sans-serif"],
            },
        },
    };
}
