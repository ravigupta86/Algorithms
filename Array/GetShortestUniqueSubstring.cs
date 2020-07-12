using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Algorithm
    {
        /// <summary>
        /// minimum-window-substring problem. 
        /// Given a char array <paramref name="arr"/> and a string <paramref name="str"/>, find the minimum window in <paramref name="str"/> 
        /// which will contain all the characters in <paramref name="arr"/> in complexity O(n).
        /// </summary>
        /// <param name="arr">Array of unique characters</param>
        /// <param name="str">input string in which substring needs be found</param>
        /// <returns>minimum length substring containing all characters from arr</returns>
        /// <example>
        /// GetShortestUniqueSubstring(new char[] { 'x', 'y', 'z' }, "axyz") returns xyz
        /// </example>
        public string GetShortestUniqueSubstring(char[] arr, string str)
        {
            //Pointers to keep track of our window
            int left = 0, right = 0;
            //Counter to check how many unique characters are used, if this is equal to arr.length
            int incrementUniqueCharCnt = 0;
            //stores result, could have used two separate variables as well
            Tuple<int, int> resultWindow = null;

            //Keep track of used characters in HashMap
            Dictionary<char, int> map = PopulateHash(arr);

            //Keep looping till either left or right pointer is less than length of target string
            while (right < str.Length || left < str.Length)
            {
                if (incrementUniqueCharCnt == arr.Length)
                {
                    //if all unique characters are consumed, check if we have result 
                    //update previous result only if current window is smaller than previous one
                    if (resultWindow == null || (right - left) < (resultWindow.Item2 - resultWindow.Item1))
                    {
                        resultWindow = new Tuple<int, int>(left, right);
                    }
                    if (map.ContainsKey(str[left]))
                    {
                        //if current char on left side of window is among unique char list then decrement it's occurence count
                        map[str[left]]--;
                        if (map[str[left]] == 0)
                        {
                            //if all previous occurences of current char are removed then it means our window is not valid anymore and we should grow it on right
                            incrementUniqueCharCnt--;
                        }
                    }
                    //shrink window
                    left++;
                }
                else
                {
                    //if our window is not having all unique characters and there is no space to grow then we need break from loop
                    if (right == str.Length)
                    {
                        break;
                    }
                    //Below if check is required since current character may be outside of unique char list
                    if (map.ContainsKey(str[right]))
                    {
                        if (map[str[right]] == 0)
                        {
                            //if it's first occurrence of current char then increment unique char counter
                            incrementUniqueCharCnt++;
                        }
                        //if current char on right side of window is among unique char list then increment it's occurence count
                        map[str[right]]++;
                    }
                    //grow window
                    right++;
                }
            }

            //Console.WriteLine($"Input: arr:{string.Join("", arr)} -- str:{str}");
            //if (resultWindow != null)
            //{
            //    Console.WriteLine($"Result: ({resultWindow.Item1},{resultWindow.Item2})");
            //}
            //else
            //{
            //    Console.WriteLine("Not found");
            //}
            return resultWindow == null ? string.Empty : str.Substring(resultWindow.Item1, resultWindow.Item2 - resultWindow.Item1);
        }

        private Dictionary<char, int> PopulateHash(char[] input)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            for (int i = 0; i < input.Length; i++)
            {
                if (!map.ContainsKey(input[i]))
                {
                    map.Add(input[i], 0);
                }
            }
            return map;
        }
    }
}
