namespace KeyboardClicker.HintGenerators;

public class HintGenerator
{
    public HintGenerator()
    {
        Alphabet = "arstneioWFPLUYcdhBGVJMK"
            .Select(x => x.ToString().ToUpperInvariant())
            .ToList();
    }

    private List<string> Alphabet { get; }

    public string GetHint(int index)
    {
        var result = "";
        var number = index;

        while (number > 0)
        {
            var remainder = number % Alphabet.Count;
            result += Alphabet[remainder];
            number /= Alphabet.Count;
        }

        return result;
    }
}

