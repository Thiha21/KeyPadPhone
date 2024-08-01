using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyPadPhone
{
    public static class Phone
    {
        public static string OldPhonePad(string input)
        {
            var keypad = new Dictionary<int, string>
            {
                { 1, "&'(" },
                { 2, "abc" },
                { 3, "def" },
                { 4, "ghi" },
                { 5, "jkl" },
                { 6, "mno" },
                { 7, "pqrs" },
                { 8, "tuv" },
                { 9, "wxyz" },
                { 0, " " }
            };

            return input.StringToList().ConvertToText(keypad).ToUpper();
        }

        public static string ConvertToText(this List<string> inputSequences, Dictionary<int, string> keypad)
        {
            var result = string.Empty;

            foreach (var sequence in inputSequences)
            {
                if (string.IsNullOrEmpty(sequence))
                    continue;

                int number = sequence[0] - '0'; // Convert the first character to a number
                int count = sequence.Length; // Number of times the button is pressed

                if (keypad.TryGetValue(number, out string characters))
                {
                    int index = (count - 1) % characters.Length; // Calculate the index
                    result += characters[index]; // Append the character to the result
                }
            }

            return result;
        }

        private static List<string> StringToList(this string data)
        {
            List<string> resultList = new List<string>();
            List<int> currentSequence = new List<int>();
            int previousValue = -1;

            foreach (char c in data)
            {
                if(c == '0')
                {
                    if (currentSequence.Count > 0)
                    {
                        resultList.Add(string.Join("", currentSequence));
                        currentSequence.Clear();
                    }
                    resultList.Add(c.ToString());
                    currentSequence.Clear();
                    previousValue = -1;
                }
                else if(c == '*')
                {
                    currentSequence.Clear();
                    previousValue = -1;
                }
                else if (char.IsDigit(c))
                {
                    int currentValue = int.Parse(c.ToString());
                    if(previousValue == currentValue)
                    {
                        currentSequence.Add(currentValue);
                    }
                    else
                    {
                        if(currentSequence.Count > 0)
                        {
                            resultList.Add(string.Join("",currentSequence));
                            currentSequence.Clear();
                        }
                        currentSequence.Add(currentValue);
                    }
                    previousValue = currentValue;
                }
                else
                {
                    if (currentSequence.Count > 0)
                    {
                        resultList.Add(string.Join("", currentSequence));
                        currentSequence.Clear();
                    }
                }
            }
            return resultList;
        }
    }
}
