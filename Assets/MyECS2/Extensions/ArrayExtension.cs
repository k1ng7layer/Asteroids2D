using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Extensions
{
    public static class ArrayExtension
    {
        public static void RemoveByIndex<T>(this T[] arrayIn, ref T[] array, int index) where T:struct
        {
            var newArray = new T[arrayIn.Length - 1];
            Array.Copy(arrayIn, 0, newArray, 0, index);
            Array.Copy(arrayIn, (index + 1), newArray, index, (arrayIn.Length - 1 - index));
            arrayIn = newArray;
        }
    }
}
