using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class XUtils
{

    public static D[] ToArray<T, D>(T[] array, Func<T, D> onDest)
    {
        D[] destArray = new D[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            var t = array[i];
            if(t != null && onDest != null)
            {
                destArray[i] = onDest(t);
            }
        }
        return destArray;
    }
    public static List<D> ToList<T, D>(List<T> array, Func<T, D> onDest)
    {
        List<D> destArray = new List <D>();
        for (int i = 0; i < array.Count; i++)
        {
            var t = array[i];
            if (t != null && onDest != null)
            {
                destArray.Add(onDest(t));
            }
        }
        return destArray;
    }
}
