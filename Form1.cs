namespace KeyboardClicker;
using KeyboardClicker.ShapeGenerators;
using KeyboardClicker.HintGenerators;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private ShapeGenerator ShapeGenerator { get; set; }
    private Stack<Rectangle> PreviousRects { get; set; } = new();

    private string _inputText = "";
    private string InputText
    {
        get => _inputText;
        set
        {
            if (value.Length == 0 || ShapeGenerator.Any(value))
            {
                _inputText = value;
            }
            else
            {
                Console.Beep();
            }
        }
    }

    private static void Beep() => Console.Beep();

    private void EnterArea()
    {
        _ = NativeMethods.SetCursorPos(ShapeGenerator.GetPoint(InputText));
        PreviousRects.Push(ShapeGenerator.Rectangle);
        ShapeGenerator.Generate(InputText);
        InputText = "";

        if (ShapeGenerator.Rectangle.Height < 30)
        {
            SimulateClick();
        }
    }

    private void SimulateClick()
    {
        Hide();
        NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN);
        NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTUP);
        Close();
    }

    private void SimulateDblClick()
    {
        Hide();
        NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN);
        NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTUP);
        Thread.Sleep(50);
        NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN);
        NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTUP);
        Close();
    }

    internal void Form_Shown(object sender, EventArgs e)
    {
        var hintGenerator = new HintGenerator();
        ShapeGenerator = new ShapeGenerator(hintGenerator);
        ShapeGenerator.Generate(Screen.PrimaryScreen.Bounds);
    }

    internal void Form_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Alt || e.Shift || e.Control)
        {
            Beep();
            return;
        }

        switch (e.KeyCode)
        {
            case Keys.Escape:
                Close();
                break;
            case Keys.OemPeriod:
                SimulateDblClick();
                break;
            case Keys.Oemcomma:
                SimulateClick();
                break;
            case Keys.Return:
                if (InputText.Length == 0)
                {
                    Beep();
                    return;
                }

                EnterArea();
                break;
            case Keys.Back:
                if (InputText.Length > 0)
                {
                    InputText = new string(InputText.SkipLast(1).ToArray());
                }
                else if (PreviousRects.Count > 0)
                {
                    ShapeGenerator.Generate(PreviousRects.Pop());
                }
                else
                {
                    Beep();
                }
                break;
            case >= Keys.D0 and <= Keys.D9:
            case >= Keys.NumPad0 and <= Keys.NumPad9:
            case >= Keys.A and <= Keys.Z:
                InputText += $"{(char)e.KeyCode}";

                if (ShapeGenerator.Unique(InputText))
                {
                    EnterArea();
                }
                break;
            default:
                break;
        }

        Invalidate(Bounds);
    }

    internal void Form_Paint(object sender, PaintEventArgs e)
        => ShapeGenerator.Paint(e.Graphics, InputText);
}
