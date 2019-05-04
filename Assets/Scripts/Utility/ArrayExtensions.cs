using System;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

namespace Utility
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Copies an array (normal copy where <see cref="T"/> is a struct, shallow copy where <see cref="T"/> is a reference) and returns the newly created array.
        /// </summary>
        /// <param name="source">The array to copy.</param>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <returns>A new array with the copied contents of the <see cref="source"/> array.</returns>
        public static T[] Copy<T>(this T[] source)
        {
            var ret = new T[source.Length];
            Array.Copy(ret, source, source.Length);
            return ret;
        }

        /// <summary>
        /// Multiplies all elements of an integer array and returns the resulting value.
        /// This is not generic because generic multiplication requires Expression-type workarounds.
        /// </summary>
        /// <param name="source">The array whose elements to multiply by one-another.</param>
        /// <returns>The result of multiplying all elements of the array by each-other.</returns>
        public static int ElementWiseMultiply(this int[] source)
        {
            return ElementWiseMultiply(source, 0, source.Length);
        }
        
        /// <summary>
        /// Multiplies the specified elements in a range of an integer array and returns the resulting value.
        /// This is not generic because generic multiplication requires Expression-type workarounds.
        /// </summary>
        /// <param name="source">The array whose elements to multiply by one-another.</param>
        /// <param name="startIndex">The index at which to start multiplying.</param>
        /// <param name="endIndex">The index at which to end multiplying.</param>
        /// <returns>The result of multiplying elements from  <paramref name="endIndex"/> elements of the array by each-other.</returns>
        public static int ElementWiseMultiply(this int[] source, int startIndex, int endIndex)
        {
            Debug.Assert(startIndex < source.Length && endIndex >= 0);
            
            int ret = 1;
            for (int i = startIndex; i < endIndex; ++i)
            {
                ret *= source[i];
            }
            return ret;
        }

        /// <summary>
        /// Sets all elements of an array to the specified value.
        /// </summary>
        /// <param name="source">Array to set values of.</param>
        /// <param name="val">Value to set every element of the array to.</param>
        /// <typeparam name="T">Type of the array.</typeparam>
        public static void SetAll<T>(this T[] source, T val)
        {
            for (int i = 0; i < source.Length; ++i)
            {
                source[i] = val;
            }
        }
        
    }
}