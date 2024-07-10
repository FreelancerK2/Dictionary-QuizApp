using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public class LanguageDictionary
    {
        public string Type { get; private set; }
        private Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

        public LanguageDictionary(string type)
        {
            Type = type;
        }

        public void AddWord(string word, string translation)
        {
            if (!dictionary.ContainsKey(word))
            {
                dictionary[word] = new List<string>();
            }
            dictionary[word].Add(translation);
        }

        public void ReplaceWordOrTranslation(string word, string newWord, string newTranslation)
        {
            if (dictionary.ContainsKey(word))
            {
                dictionary.Remove(word);
                AddWord(newWord, newTranslation);
            }
        }

        public void DeleteWord(string word)
        {
            if (dictionary.ContainsKey(word))
            {
                dictionary.Remove(word);
            }
        }

        public List<string> SearchTranslation(string word)
        {
            if (dictionary.ContainsKey(word))
            {
                return dictionary[word];
            }
            return null;
        }

        public List<string> GetAllWords()
        {
            return new List<string>(dictionary.Keys);
        }

        public bool WordExists(string word)
        {
            return dictionary.ContainsKey(word);
        }

        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static bool AskQuestion()
        {
            while (true)
            {
                Console.Write("\nDo you want to make more? (y/n): ");
                string response = Console.ReadLine().Trim().ToLower();
                Console.Clear();

                if (response == "y")
                {
                    return true;
                }
                else if (response == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter 'y' or 'n'.");
                }
            }
        }

        public static bool AskConfirmation(string confirm)
        {
            while (true)
            {
                Console.Write(confirm);
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "y")
                {
                    return true;
                }
                else if (input == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }


        public static int IntegerInput(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                // Try to parse the input to an integer
                if (int.TryParse(input, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please only integer.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                }
            }
        }

        public static string InputString(string prompt)
        {
            Console.Write(prompt);
            string userInput = Console.ReadLine();

            // Check if the input is an integer
            if (int.TryParse(userInput, out _))
            {
                throw new ArgumentException("\nInvalid input, Please enter only string");
            }
            else
            {
                return userInput;
            }
        }
    }
}
