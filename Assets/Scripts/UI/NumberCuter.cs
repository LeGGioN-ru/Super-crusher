using System;
using System.Collections.Generic;

[Serializable]
public static class NumberCuter
{
    private static Dictionary<long, string> _cuts = new Dictionary<long, string>()
    {
        {1000,"T" },
        {1000000,"M" },
        {1000000000,"B" }
    };

    public static string Execute(long value)
    {
        string finalString = string.Empty;

        foreach (var cut in _cuts)
        {
            if (value >= cut.Key)
            {
                string roundedValue = (Convert.ToSingle(value) / cut.Key).ToString("#.#");
                finalString = $"{roundedValue} {cut.Value}";
            }
        }

        return finalString == string.Empty ? value.ToString() : finalString;
    }
}
