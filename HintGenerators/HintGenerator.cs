using System.Text;

namespace KeyboardClicker.HintGenerators;

public class HintGenerator
{
    public HintGenerator()
    {
        Alphabet = "arstneioWFPLUYcdhBGVJMK"
        //Alphabet = "0123456789ABCDEF"
            .Select(x => x.ToString().ToUpperInvariant())
            .ToList();
    }

    private List<string> Alphabet { get; }

    public string GetHint(int index)
    {
        var result = new StringBuilder();
        var number = index;

        do
        {
            var remainder = number % Alphabet.Count;
            _ = result.Insert(0, Alphabet[remainder]);
            number /= Alphabet.Count;
        } while (number > 0);

        return result.ToString();
    }
}

