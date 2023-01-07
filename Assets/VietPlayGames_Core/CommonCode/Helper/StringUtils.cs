using System.Collections;
using System.Collections.Generic;

public static class StringUtils
{
    public static string Replace(this string text, int start, int count,
                                     string replacement)
    {
        return text.Substring(0, start) + replacement
             + text.Substring(start + count);
    }

    public static string CutString(this string text, int length)
    {
        if (text.Length > length)
        {
            return text.Substring(0, length).Trim() + "...";
        }

        return text;
    }
}
