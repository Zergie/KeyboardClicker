namespace KeyboardClicker.ShapeGenerators;
using KeyboardClicker.HintGenerators;

public class ShapeGenerator
{
    public ShapeGenerator(HintGenerator hintGenerator)
    {
        HintGenerator = hintGenerator;
        Brush = Brushes.Red;
        Pen = new Pen(Brush, 3);
        Font = new Font("Arial", 12);
    }

    protected HintGenerator HintGenerator { get; }
    protected List<Shape> Shapes { get; set; } = new();
    protected Brush Brush { get; set; }
    protected Pen Pen { get; set; }
    protected Font Font { get; set; }

    public Rectangle Rectangle { get; set; }

    public void Generate(string text)
        => Generate(GetRectangle(First(text)));

    public void Generate(Rectangle rect)
    {
        Rectangle = rect;
        Shapes = GenerateShapes(rect).ToList();
    }

    public void Paint(Graphics g, IEnumerable<Shape> shapes)
    {
        foreach (var shape in shapes)
        {
            Paint(g, shape);
        }
    }

    public void Paint(Graphics g, string text)
        => Paint(g, Filter(text));

    public Point GetPoint(string text)
        => First(text).Point;


    public bool Any(string text)
        => Shapes.Any(x => x.Text.StartsWith(text, StringComparison.OrdinalIgnoreCase));

    public bool Unique(string text)
        => Shapes.Count(x => x.Text.StartsWith(text, StringComparison.OrdinalIgnoreCase)) == 1;

    private IEnumerable<Shape> Filter(string text)
        => Shapes.Where(x => x.Text.StartsWith(text, StringComparison.OrdinalIgnoreCase))
                 .ToList();

    private Shape First(string text)
        => Shapes.First(x => x.Text.Equals(text, StringComparison.OrdinalIgnoreCase));




    private Dictionary<string, Rectangle> Rects { get; set; } = new();
    protected IEnumerable<Shape> GenerateShapes(Rectangle screen)
    {
        const int rows = 9;
        const int cols = 12;
        var width = Math.Max(30, screen.Width / cols);
        var height = Math.Max(20, screen.Height / rows);
        var index = 1;

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                var rect = new Rectangle(
                        screen.Left + col * width,
                        screen.Top + row * height,
                        width,
                        height
                        );

                var shape = new Shape
                {
                    Text = HintGenerator.GetHint(index),
                    Point = new Point(
                            rect.Left + rect.Width / 2,
                            rect.Top + rect.Height / 2
                            ),
                };
                
                if (screen.Contains(shape.Point))
                {
                    index++;
                    Rects[shape.Text] = rect;
                    yield return shape;
                }
            }
        }
    }

    protected Rectangle GetRectangle(Shape shape)
        => Rects[shape.Text];

    protected void Paint(Graphics g, Shape shape)
    {
        var rect = Rects[shape.Text];
        g.DrawRectangle(Pen, rect);

        var size = g.MeasureString(shape.Text, Font);
        var pt = new Point(
                rect.Left,
                rect.Top + 2
                );
        g.FillRectangle(Brush, new RectangleF(pt, size));
        g.DrawString(shape.Text, Font, Brushes.White, pt);
    }
}
