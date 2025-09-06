using System.Collections;
using System.Collections.Generic;

public class ListManipulator
{
    /// <summary>
    /// Shuffle a list using Fisher-Yates algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<T> ShuffleList<T>(List<T> list)
    {
        System.Random rng = new();
        int n = list.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            (list[k], list[n]) = (list[n], list[k]);
        }
        return list;
    }
}
