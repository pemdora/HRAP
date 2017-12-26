using UnityEngine;
using System.Linq;

namespace Crosstales.RTVoice
{
    /// <summary>Various extension methods.</summary>
    public static class ExtensionMethods
    {
        private static readonly System.Random rd = new System.Random();

        /// <summary>
        /// Extension method for strings.
        /// Case insensitive 'Replace'.
        /// </summary>
        /// <param name="str">String-instance.</param>
        /// <param name="oldString">String to replace.</param>
        /// <param name="newString">New replacement string.</param>
        /// <param name="comp">StringComparison-method (default: StringComparison.OrdinalIgnoreCase, optional)</param>
        /// <returns>Replaced string.</returns>
        public static string CTReplace(this string str, string oldString, string newString, System.StringComparison comp = System.StringComparison.OrdinalIgnoreCase)
        {
            int index = str.IndexOf(oldString, comp);

            bool MatchFound = index >= 0;

            if (MatchFound)
            {
                str = str.Remove(index, oldString.Length);

                str = str.Insert(index, newString);
            }

            return str;
        }

        /// <summary>
        /// Extension method for strings.
        /// Case insensitive 'Equals'.
        /// </summary>
        /// <param name="str">String-instance.</param>
        /// <param name="toCheck">String to check.</param>
        /// <param name="comp">StringComparison-method (default: StringComparison.OrdinalIgnoreCase, optional)</param>
        /// <returns>True if the string contains the given string.</returns>
        public static bool CTEquals(this string str, string toCheck, System.StringComparison comp = System.StringComparison.OrdinalIgnoreCase)
        {
            if (str == null)
                throw new System.ArgumentNullException("str");

            if (toCheck == null)
                throw new System.ArgumentNullException("toCheck");

            return str.Equals(toCheck, comp);
        }

        /// <summary>
        /// Extension method for strings.
        /// Case insensitive 'Contains'.
        /// </summary>
        /// <param name="str">String-instance.</param>
        /// <param name="toCheck">String to check.</param>
        /// <param name="comp">StringComparison-method (default: StringComparison.OrdinalIgnoreCase, optional)</param>
        /// <returns>True if the string contains the given string.</returns>
        public static bool CTContains(this string str, string toCheck, System.StringComparison comp = System.StringComparison.OrdinalIgnoreCase)
        {
            if (str == null)
                throw new System.ArgumentNullException("str");

            if (toCheck == null)
                throw new System.ArgumentNullException("toCheck");

            return str.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Extension method for strings.
        /// Contains any given string.
        /// </summary>
        /// <param name="str">String-instance.</param>
        /// <param name="searchTerms">Search terms separated by the given split-character.</param>
        /// <param name="splitChar">Split-character (default: ' ', optional)</param>
        /// <returns>True if the string contains any parts of the given string.</returns>
        public static bool CTContainsAny(this string str, string searchTerms, char splitChar = ' ')
        {
            if (str == null)
                throw new System.ArgumentNullException("str");

            if (string.IsNullOrEmpty(searchTerms))
            {
                return true;
            }

            char[] split = new char[] { splitChar };

            return searchTerms.Split(split, System.StringSplitOptions.RemoveEmptyEntries).Any(searchTerm => str.CTContains(searchTerm));
        }

        /// <summary>
        /// Extension method for strings.
        /// Contains all given strings.
        /// </summary>
        /// <param name="str">String-instance.</param>
        /// <param name="searchTerms">Search terms separated by the given split-character.</param>
        /// <param name="splitChar">Split-character (default: ' ', optional)</param>
        /// <returns>True if the string contains all parts of the given string.</returns>
        public static bool CTContainsAll(this string str, string searchTerms, char splitChar = ' ')
        {
            if (str == null)
                throw new System.ArgumentNullException("str");

            if (string.IsNullOrEmpty(searchTerms))
            {
                return true;
            }

            char[] split = new char[] { splitChar };

            return searchTerms.Split(split, System.StringSplitOptions.RemoveEmptyEntries).All(searchTerm => str.CTContains(searchTerm));
        }

        /// <summary>
        /// Extension method for Lists.
        /// Shuffles a List.
        /// </summary>
        /// <param name="list">List-instance to shuffle.</param>
        public static void CTShuffle<T>(this System.Collections.Generic.IList<T> list)
        {
            if (list == null)
                throw new System.ArgumentNullException("list");

            int n = list.Count;

            while (n > 1)
            {
                int k = rd.Next(n--);
                T temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }

        /// <summary>
        /// Extension method for Arrays.
        /// Shuffles an Array.
        /// </summary>
        /// <param name="array">Array-instance to shuffle.</param>
        public static void CTShuffle<T>(this T[] array)
        {
            if (array == null || array.Length <= 0)
                throw new System.ArgumentNullException("array");

            int n = array.Length;
            while (n > 1)
            {
                int k = rd.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        /// <summary>
        /// Extension method for Arrays.
        /// Dumps an array to a string.
        /// </summary>
        /// <param name="array">Array-instance to dump.</param>
        /// <returns>String with lines for all array entries.</returns>
        public static string CTDump<T>(this T[] array)
        {
            if (array == null || array.Length <= 0)
                throw new System.ArgumentNullException("array");

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (T element in array)
            {
                if (0 < sb.Length)
                {
                    sb.Append(System.Environment.NewLine);
                }
                sb.Append(element.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Extension method for Arrays.
        /// Generates a string array with all entries (via ToString).
        /// </summary>
        /// <param name="array">Array-instance to ToString.</param>
        /// <returns>String array with all entries (via ToString).</returns>
        public static string[] CTToString<T>(this T[] array)
        {
            if (array == null || array.Length <= 0)
                throw new System.ArgumentNullException("array");

            string[] result = new string[array.Length];

            for (int ii = 0; ii < array.Length; ii++)
            {
                result[ii] = array[ii].ToString();
            }

            return result;
        }

        /// <summary>
        /// Extension method for Lists.
        /// Dumps a list to a string.
        /// </summary>
        /// <param name="list">List-instance to dump.</param>
        /// <returns>String with lines for all list entries.</returns>
        public static string CTDump<T>(this System.Collections.Generic.List<T> list)
        {
            if (list == null)
                throw new System.ArgumentNullException("list");

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (T element in list)
            {
                if (0 < sb.Length)
                {
                    sb.Append(System.Environment.NewLine);
                }
                sb.Append(element.ToString());
            }

            return sb.ToString();
        }


        /// <summary>
        /// Extension method for Lists.
        /// Generates a string list with all entries (via ToString).
        /// </summary>
        /// <param name="list">List-instance to ToString.</param>
        /// <returns>String list with all entries (via ToString).</returns>
        public static System.Collections.Generic.List<string> CTToString<T>(this System.Collections.Generic.List<T> list)
        {
            if (list == null)
                throw new System.ArgumentNullException("list");

            System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>(list.Count);

            foreach (T element in list)
            {
                result.Add(element.ToString());
            }

            return result;
        }
/*
#if !UNITY_WSA
        /// <summary>
        /// Extension method for MonoBehaviour.
        /// Invoke with a real method name instead of a string.
        /// </summary>
        /// <param name="mb">MonoBehaviour-instance.</param>
        /// <param name="methodName">Mehod as Action.</param>
        /// <param name="time">Delay time of the invoke in seconds.</param>
        public static void CTInvoke(this MonoBehaviour mb, System.Action methodName, float time)
        {
            if (mb == null)
                throw new System.ArgumentNullException("mb");

            if (methodName == null)
                throw new System.ArgumentNullException("methodName");


            mb.Invoke(methodName.Method.Name, time);
        }

        /// <summary>
        /// Extension method for MonoBehaviour.
        /// InvokeRepeating with a real method name instead of a string.
        /// </summary>
        /// <param name="mb">MonoBehaviour-instance.</param>
        /// <param name="methodName">Mehod as Action.</param>
        /// <param name="time">Delay time of the invoke in seconds.</param>
        /// <param name="repeatRate">Repeat-time of the invoke in seconds.</param>
        public static void CTInvokeRepeating(this MonoBehaviour mb, System.Action methodName, float time, float repeatRate)
        {
            if (mb == null)
                throw new System.ArgumentNullException("mb");

            if (methodName == null)
                throw new System.ArgumentNullException("methodName");


            mb.InvokeRepeating(methodName.Method.Name, time, repeatRate);
        }

        /// <summary>
        /// Extension method for MonoBehaviour.
        /// IsInvoking with a real method name instead of a string.
        /// </summary>
        /// <param name="mb">MonoBehaviour-instance.</param>
        /// <param name="methodName">Mehod as Action.</param>
        /// <returns>True if the given method invoke is pending.</returns>
        public static bool CTIsInvoking(this MonoBehaviour mb, System.Action methodName)
        {
            if (mb == null)
                throw new System.ArgumentNullException("mb");

            if (methodName == null)
                throw new System.ArgumentNullException("methodName");


            return mb.IsInvoking(methodName.Method.Name);
        }
#endif
*/
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)