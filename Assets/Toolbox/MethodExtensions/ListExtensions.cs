using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace Toolbox.MethodExtensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Checks if the list is empty 
        /// </summary>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IList<T> target)
        {
            return target.Count == 0;
        }
    
        /// <summary>
        /// Shuffle the list 
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    
        /// <summary>
        /// Return a random item from the list.
        /// Sampling with replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomItem<T>(this IList<T> list)
        {
            if (list.IsEmpty()) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Removes a random item from the list, returning that item.
        /// Sampling without replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RemoveRandom<T>(this IList<T> list)
        {
            if (list.IsEmpty()) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>(this IList<T> list, int index)
        {
            if (index < 0) index = list.Count + index;
            else if (index > list.Count - 1) index %= list.Count;

            return list[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public static void SetAt<T>(this IList<T> list, int index, T item)
        {
            if (index < 0) index = list.Count + index;
            else if (index > list.Count - 1) index %= list.Count;

            list.Insert(index, item);
        }

        /// <summary>
        /// Add list from same type to current list 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="otherList"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddList<T>(this IList<T> list, List<T> otherList)
        {
            foreach (var item in otherList)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// remove list from same type from current list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="otherList"></param>
        /// <typeparam name="T"></typeparam>
        public static void RemoveList<T>(this IList<T> list, IList<T> otherList)
        {
            foreach (var item in otherList)
            {
                if(!list.Contains(item)) continue;
                bool removed = list.Remove(item);
            }
        }

        /// <summary>
        /// Changes the given number to a number inside of the list size and returns it.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index">The current index</param>
        /// <param name="looped">if true we loop through array until we are at last number if false returns list.lenght - 1 or 0</param>
        /// <returns></returns>
        public static int GetNumberBetweenSize<T>(this IList<T> list, int index, bool looped = false)
        {
            if (list.IsEmpty()) return index;
        
            if (!looped)
            {
                return index.FindClosestIndex(new[] { 0, (list.Count - 1) }).First();
            }
            
            if (index < 0) index = list.Count + index;
            else if (index > list.Count - 1) index %= list.Count;

            return index;
        }

        
        /// <summary>
        /// ContainsSlot checks if the given number is between the list size.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool ContainsSlot<T>(this IList<T> list, int index)
        {
            return index >= 0 && list.Count - 1 >= index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldList"></param>
        /// <typeparam name="TY"></typeparam>
        /// <typeparam name="TU"></typeparam>
        public static List<TU> ConvertListItemsTo<TY, TU>(this IList<TY> oldList) where TU : class
        {
            return oldList.Select(oldItem => oldItem as TU).ToList();
        }

    }
}
