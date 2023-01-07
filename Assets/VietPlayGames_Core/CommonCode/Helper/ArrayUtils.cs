using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayUtils
{
    public static System.Random rng = new System.Random();
    public static T[] Shuffle<T>(this T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
        return array;
    }
}
