namespace KeyboardClicker;
using System.Runtime.InteropServices;

static internal class NativeMethods
{
    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    public static Point GetCursorPos()
    {
        POINT lpPoint;
        _ = GetCursorPos(out lpPoint);
        return new Point(lpPoint.X, lpPoint.Y);
    }




    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    public static bool SetCursorPos(Point pt)
        => SetCursorPos(pt.X, pt.Y);




    public const int MOUSEEVENTF_LEFTDOWN = 0x02;
    public const int MOUSEEVENTF_LEFTUP = 0x04;

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    public static void mouse_event(int dwFlags)
    {
        POINT lpPoint;
        _ = GetCursorPos(out lpPoint);
        mouse_event(dwFlags, lpPoint.X, lpPoint.Y, 0, 0);
    }
}
