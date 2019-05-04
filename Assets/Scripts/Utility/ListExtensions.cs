using System.Collections.Generic;

namespace Utility
{
    public static class ListExtensions
    {
        public static T Choice<T>(this IList<T> genericList)
        {
            return genericList[m_rng.Next(genericList.Count)];
        }

        private static readonly System.Random m_rng = new System.Random();
    }
}