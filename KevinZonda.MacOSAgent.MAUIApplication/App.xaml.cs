namespace KevinZonda.MacOSAgent.MAUIApplication;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}