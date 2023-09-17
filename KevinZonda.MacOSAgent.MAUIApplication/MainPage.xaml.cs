using System.Text.Json;
using KevinZonda.MacOSAgent.MAUIApplication.MacOSHelper.HotCorner;

namespace KevinZonda.MacOSAgent.MAUIApplication;

public partial class MainPage : ContentPage
{
    private HotCornerInfo? cached;

    public MainPage()
    {
        InitializeComponent();
        loadHotCornerFromSys();
    }

    private void OnDisableCornerClicked(object sender, EventArgs e)
    {
        HotCornerInfo.Zeros().SetToSystem();
        loadHotCornerFromSys();
    }

    private void OnRecoverCornerClicked(object sender, EventArgs e)
    {
        if (cached != null && !cached.IsDefault) cached?.SetToSystem();
        else getSavedValue()?.SetToSystem();
        loadHotCornerFromSys();
    }

    private void OnRefreshCornerClicked(object sender, EventArgs e)
    {
        loadHotCornerFromSys();
    }

    private void loadHotCornerFromSys()
    {
        var x = HotCornerInfo.LoadFromSystem();
        if (!x.IsDefault) File.WriteAllText(getSavePath(), JsonSerializer.Serialize(x));
        cached = x;

        CornerInfoLbl.Text = cached.ToString();
    }

    private string getSavePath()
    {
        return Path.Join(FileSystem.Current.AppDataDirectory, "hotcorner.json");
    }

    private HotCornerInfo? getSavedValue()
    {
        string input = "";
        try
        {
            input = File.ReadAllText(getSavePath());
        }
        catch
        {
            input = "";
        }

        if (input == "") return null;
        return JsonSerializer.Deserialize<HotCornerInfo?>(input);
    }
}