namespace KeyboardClicker;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    internal void Form_Shown(object sender, EventArgs e)
    {
        var desktopPtr = NativeMethods.GetDC(IntPtr.Zero);
        using (var g = Graphics.FromHdc(desktopPtr))
        {
            var rect = new Rectangle(0, 0, 1920, 1080);
            g.FillRectangle(Brushes.Magenta, rect);

            using (var pen = new Pen(Brushes.Red, 3))
            {
                var pt1 = new Point(500, 0);
                var pt2 = new Point(500, 500);
                g.DrawLine(pen, pt1, pt2);
            }
        }
        NativeMethods.ReleaseDC(IntPtr.Zero, desktopPtr);
    }

    internal void Form_Closed(object sender, EventArgs e)
    {
        _ = NativeMethods.InvalidateRect(0, 0, true);
    }

    internal void Form_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Alt || e.Shift || e.Control)
        {
            return;
        }

        switch (e.KeyCode)
        {
            case Keys.Escape:
                Close();
                break;
            default:
                break;
        }
    }
}
