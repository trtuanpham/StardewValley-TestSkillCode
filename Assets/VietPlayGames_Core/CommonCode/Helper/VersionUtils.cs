using System;

public static class VersionUtils
{
    public static int CompareVersion(string newVersion, string oldVersion)
    {
        if (!string.IsNullOrEmpty(oldVersion) && !string.IsNullOrEmpty(newVersion))
        {
            string[] oldVersionArray = oldVersion.Split('.');
            string[] newVersionArray = newVersion.Split('.');

            int oldCount = oldVersionArray.Length;
            int newCount = newVersionArray.Length;

            int maxCount = Math.Max(oldCount, newCount);
            for (int i = 0; i < maxCount; i++)
            {
                int oldNumber = 0;
                int newNumber = 0;
                int.TryParse(i < oldCount ? oldVersionArray[i] : "0", out oldNumber);
                int.TryParse(i < newCount ? newVersionArray[i] : "0", out newNumber);

                if (newNumber > oldNumber)
                {
                    return 1;
                }

                if (oldNumber > newNumber)
                {
                    return -1;
                }
            }
        }
        else
        {
            return string.Compare(newVersion, oldVersion);
        }

        return 0;
    }

    public static bool NewVersion(string newVersion, string oldVersion)
    {
        return CompareVersion(newVersion, oldVersion) == 1;
    }
}