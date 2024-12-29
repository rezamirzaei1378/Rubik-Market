namespace Rubik_Market.Application.Extenstions;

public static class CardShowFormat
{
    public static string ChangeCardShowFormat(this string input)
    {
        return string.Join("-", Enumerable.Range(0, 4).Select(i => input.Substring(i * 4, 4)));
    }
}