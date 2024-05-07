using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class ExtentionMethods
    {
        /// <summary>
        /// Checks if the objects are within the bounds of the 2D array.
        /// </summary>
        /// <param name="array">The 2D array to check.</param>
        /// <param name="x">The row index.</param>
        /// <param name="y">The column index.</param>
        /// <returns>True if both objects are within the bounds of the array; otherwise, false.</returns>
        public static bool IsInBounds<T>(this T[,] array, int x, int y)
        {
            return x >= 0 && y >= 0 && x < array.GetLength(0) && y < array.GetLength(1);
        }

        /// <summary>
        /// Trims all white spaces from the string and converts it to lower case.
        /// </summary>
        /// <param name="input">The string to process.</param>
        /// <returns>A new string with all white spaces removed and all characters in lower case.</returns>
        public static string TrimAndLower(this string input)
        {
            return new string(input.TrimAndLower());
        }

        /// <summary>
        /// Checks if an array is not null and contains more than zero elements.
        /// </summary>
        /// <param name="array">The array to check.</param>
        /// <returns>True if the array is not null and contains one or more elements; otherwise, false.</returns>
        public static bool IsNotNullOrEmpty<T>(this T[] array)
        {
            return array != null && array.Length > 0;
        }
    }
}

