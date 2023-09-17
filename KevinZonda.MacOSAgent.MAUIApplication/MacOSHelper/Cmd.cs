using System.Diagnostics;

namespace KevinZonda.MacOSAgent.MAUIApplication.MacOSHelper;

public static class Cmd
{
    public static string RunCmd(string exec, string args = "")
    {
        var cmdsi = new ProcessStartInfo(exec);
        cmdsi.Arguments = args;
        cmdsi.RedirectStandardOutput = true;
        cmdsi.UseShellExecute = false;
        var cmd = Process.Start(cmdsi);
        var output = cmd.StandardOutput.ReadToEnd();
        cmd.WaitForExit();
        return output;
    }
}