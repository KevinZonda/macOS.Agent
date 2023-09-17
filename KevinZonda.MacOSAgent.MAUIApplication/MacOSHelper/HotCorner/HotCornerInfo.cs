namespace KevinZonda.MacOSAgent.MAUIApplication.MacOSHelper.HotCorner;

public class HotCornerInfo
{
    public int TopLeft { get; init; }
    public int BottomLeft { get; init; }
    public int TopRight { get; init; }
    public int BottomRight { get; init; }

    public static HotCornerInfo LoadFromSystem()
    {
        return new()
        {
            TopLeft = Cmd.RunCmd("defaults", "read com.apple.dock \"wvous-tl-corner\"").ToInt(),
            BottomLeft = Cmd.RunCmd("defaults", "read com.apple.dock \"wvous-bl-corner\"").ToInt(),
            TopRight = Cmd.RunCmd("defaults", "read com.apple.dock \"wvous-tr-corner\"").ToInt(),
            BottomRight = Cmd.RunCmd("defaults", "read com.apple.dock \"wvous-br-corner\"").ToInt()
        };
    }
    
    public bool IsDefault => TopRight == 0 && TopLeft == 0 && BottomLeft == 0 && BottomRight == 0;

    public void SetToSystem()
    {
        Cmd.RunCmd("defaults", "write com.apple.dock \"wvous-bl-corner\" -int " + BottomLeft);
        Cmd.RunCmd("defaults", "write com.apple.dock \"wvous-tl-corner\" -int " + TopLeft);
        Cmd.RunCmd("defaults", "write com.apple.dock \"wvous-tr-corner\" -int " + TopRight);
        Cmd.RunCmd("defaults", "write com.apple.dock \"wvous-br-corner\" -int " + BottomRight);
        Cmd.RunCmd("killall", "Dock");
    }
    
    public static HotCornerInfo Zeros()
    {
        return new()
        {
            TopLeft = 0,
            BottomLeft = 0,
            TopRight = 0,
            BottomRight = 0
        };
    }

    public string ToString()
    {
        return string.Format($"LeftTop: {TopLeft}, LeftBottom: {BottomLeft}, RightTop: {TopRight}, RightBottom: {BottomRight}");
    }
}

public static class Ext
{
    public static int ToInt(this string s)
    {
        return int.Parse(s);
    }
}