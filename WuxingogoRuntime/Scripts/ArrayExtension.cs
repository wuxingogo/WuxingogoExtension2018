using System;
using System.Collections.Generic;



public static class ArrayExtension
{
    public static U[] AllocArrayFormOther<T, U>(T[] _array1)
    {
        U[] _array2 = new U[_array1.Length];
        return _array2;
    }
}
