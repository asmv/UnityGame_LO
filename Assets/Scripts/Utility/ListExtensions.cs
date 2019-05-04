using System.Collections.Generic;

namespace Utility
{
    public static class ListExtensions
    {
        /// <summary>
        /// Replicate's python's random.choice() method in a convenient extension class.
        /// </summary>
        /// <param name="genericList"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Choice<T>(this IList<T> genericList)
        {
            return genericList[m_rng.Next(genericList.Count)];
        }

        private static readonly System.Random m_rng = new System.Random();
    }
}