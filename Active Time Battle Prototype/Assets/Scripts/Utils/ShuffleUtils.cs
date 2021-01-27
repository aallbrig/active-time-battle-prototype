using System.Collections.Generic;

namespace Utils
{
    public static class ShuffleUtils
    {
        private static readonly System.Random RandomNumberGenerator = new System.Random();  

        public static List<T> Shuffle<T>(this IList<T> list)
        {
            var newList = new List<T>();
            newList.AddRange(list);
            var n = list.Count;  
            while (n > 1) {  
                n--;  
                var k = RandomNumberGenerator.Next(n + 1);  
                T value = newList[k];  
                newList[k] = newList[n];  
                newList[n] = value;  
            }

            return newList;
        }
    }
}