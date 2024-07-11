using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dictionary
{
    public class DictionaryManager
    {
        private List<LanguageDictionary> dictionaries = new List<LanguageDictionary>();

        public enum Option { CREATE = 1, ADDWORD, REPLACE, DELETEWORD, DELETDICTIONARY, SEARCH, VIEW, EXIT = 0 }
        public void DisplayMenu()
        {
            while (true)
            {
                int choice = LanguageDictionary.IntegerInput("\nDictionary Application Menu:\n1. Create a dictionary\n2. Add a word and its translation\n3. Replace a word or its translation\n4. Delete a word or a translation\n5. Delete dictionary\n6. Search for translation of a word\n7. View all word\n0. Exit\nSelect an option: ");

                try
                {
                    Console.Clear();
                    switch (choice)
                    {
                        case (int)Option.CREATE:
                            CreateDictionary();
                            break;
                        case (int)Option.ADDWORD:
                            AddWord();
                            break;
                        case (int)Option.REPLACE:
                            ReplaceWordOrTranslation();
                            break;
                        case (int)Option.DELETEWORD:
                            DeleteWordOrTranslation();
                            break;
                        case (int)Option.DELETDICTIONARY:
                            DeleteDictionary();
                            break;
                        case (int)Option.SEARCH:
                            SearchTranslation();
                            break;
                        case (int)Option.VIEW:
                            ViewAllWords();
                            break;
                        case (int)Option.EXIT:
                            return;
                        default:
                            Console.WriteLine("\nPlease try again.");
                            LanguageDictionary.PressAnyKeyToContinue();
                            Console.Clear();
                            break;
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }

        private void CreateDictionary()
        {
            do
            {
                string type = LanguageDictionary.InputString("Enter dictionary type (e.g., English-Russian) or type Exit to 'exit': ");
                if (type.ToLower() == "exit")
                {
                    Console.WriteLine("\nDictionary creation cancelled.");
                    LanguageDictionary.PressAnyKeyToContinue();
                    Console.Clear();
                    return;
                }
                dictionaries.Add(new LanguageDictionary(type));
                Console.WriteLine($"\nDictionary '{type}' created.");
            } while (LanguageDictionary.AskQuestion());
            Console.Clear();

        }

        private void AddWord()
        {
            LanguageDictionary dictionary = SelectDictionary();
            if (dictionary != null)
            {
                do
                {
                    Console.Clear();
                    string[] languages = dictionary.Type.Split('-');
                    string word = LanguageDictionary.InputString($"\nEnter the {languages[0]} word = ");
                    if (dictionary.WordExists(word))
                    {
                        Console.WriteLine("\nWord already exist try another one!");
                        LanguageDictionary.PressAnyKeyToContinue();
                        Console.Clear();
                        return;
                    }
                    string translation = LanguageDictionary.InputString($"Enter the {languages[1]} word = ");
                    dictionary.AddWord(word, translation);
                    Console.WriteLine("\nWord and translation added.");
                } while (LanguageDictionary.AskQuestion());
                Console.Clear();
            }
        }

        private void ReplaceWordOrTranslation()
        {
            LanguageDictionary dictionary = SelectDictionary();
            if (dictionary != null)
            {
                do
                {
                    Console.Clear();
                    string[] languages = dictionary.Type.Split('-');
                    string word = LanguageDictionary.InputString($"\nEnter {languages[0]} word to replace = ");
                    Console.Clear();
                    while (!dictionary.WordExists(word))
                    {
                        Console.WriteLine("\nWord not found. Please try again.");
                        word = LanguageDictionary.InputString("\nEnter word to Replace: ");
                        Console.Clear();
                    }
                    string newWord = LanguageDictionary.InputString($"\nEnter new {languages[0]} word = ");
                    string newTranslation = LanguageDictionary.InputString($"Enter new {languages[1]} word = ");
                    dictionary.ReplaceWordOrTranslation(word, newWord, newTranslation);
                    Console.WriteLine("\nWord or translation replaced.");
                } while (LanguageDictionary.AskQuestion());
                Console.Clear();
            }
        }

        private void DeleteWordOrTranslation()
        {
            LanguageDictionary dictionary = SelectDictionary();
            if (dictionary != null)
            {
                do
                {
                    Console.Clear();
                    string[] languages = dictionary.Type.Split('-');
                    string word = LanguageDictionary.InputString($"\nEnter {languages[0]} word to delete = ");
                    Console.Clear();
                    while (!dictionary.WordExists(word))
                    {
                        Console.WriteLine("\nWord not found. Please try again.");
                        word = LanguageDictionary.InputString($"\nEnter {languages[0]} word to delete = ");
                        Console.Clear();
                    }
                    bool confirm = LanguageDictionary.AskConfirmation("\nYou are going to delete this word? (y/n): ");
                    Console.Clear();
                    if (confirm)
                    {
                        dictionary.DeleteWord(word);
                        Console.WriteLine("\nWord and its translations deleted.");
                    }
                    else
                    {
                        Console.WriteLine("\nDeletion cancelled.");
                    }

                } while (LanguageDictionary.AskQuestion());
                Console.Clear();
            }
        }

        private void SearchTranslation()
        {
            LanguageDictionary dictionary = SelectDictionary();
            if (dictionary != null)
            {
                do
                {
                    Console.Clear();
                    string[] languages = dictionary.Type.Split('-');
                    string word = LanguageDictionary.InputString($"\nEnter {languages[0]} word to search or type Exit to 'exit' = ");
                    Console.Clear();
                    if (word.ToLower() == "exit")
                    {
                        Console.WriteLine("\nSearch cancelled.");
                        LanguageDictionary.PressAnyKeyToContinue();
                        Console.Clear();
                        return;
                    }

                    while (!dictionary.WordExists(word))
                    {
                        Console.WriteLine("\nWord not found.");
                        word = LanguageDictionary.InputString($"\nEnter {languages[0]} word to search or type Exit to 'exit' = ");
                        Console.Clear();
                        if (word.ToLower() == "exit")
                        {
                            Console.WriteLine("\nSearch cancelled.");
                            LanguageDictionary.PressAnyKeyToContinue();
                            Console.Clear();
                            return;
                        }
                    }
                    var translations = dictionary.SearchTranslation(word);
                    Console.WriteLine($"{languages[0]} -> {languages[1]}");
                    Console.WriteLine($"\n  {word} -> {string.Join(", ", translations)}");

                } while (LanguageDictionary.AskQuestion());
                Console.Clear();
            }
        }

        private void ViewAllWords()
        {
            LanguageDictionary dictionary = SelectDictionary();
            if (dictionary != null)
            {
                Console.Clear();
                var words = dictionary.GetAllWords();
                if (words.Count > 0)
                {
                    Console.WriteLine("\nWords in dictionary:");
                    foreach (var word in words)
                    {
                        Console.WriteLine(word);
                    }
                }
                else
                {
                    Console.WriteLine("\nNo word available.");
                }
                LanguageDictionary.PressAnyKeyToContinue();
                Console.Clear();
            }

        }

        private void DeleteDictionary()
        {
            if (dictionaries.Count == 0)
            {
                Console.WriteLine("No dictionaries available to delete.");
                LanguageDictionary.PressAnyKeyToContinue();
                Console.Clear();
                return;
            }
            Console.WriteLine("Available dictionaries:");
            for (int i = 0; i < dictionaries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dictionaries[i].Type}");
            }
            Console.Write("Select a dictionary to delete: ");
            int choice = int.Parse(Console.ReadLine()) - 1;
            if (choice >= 0 && choice < dictionaries.Count)
            {
                Console.WriteLine($"\nDictionary '{dictionaries[choice].Type}' deleted.");
                dictionaries.RemoveAt(choice);
                LanguageDictionary.PressAnyKeyToContinue();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("\nInvalid choice. Please try again.");
                LanguageDictionary.PressAnyKeyToContinue();
                Console.Clear();
            }
        }


        private LanguageDictionary SelectDictionary()
        {
            int choice;
            if (dictionaries.Count == 0)
            {
                Console.WriteLine("\nNo dictionaries available. Please create a dictionary first.");
                LanguageDictionary.PressAnyKeyToContinue();
                Console.Clear();
                return null;
            }
            while (true)
            {
                Console.WriteLine("\nAvailable dictionaries:");
                for (int i = 0; i < dictionaries.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {dictionaries[i].Type}");
                }
                Console.WriteLine("0. Return to main menu");

                Console.Write("Select a dictionary: ");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.Clear();
                    Console.WriteLine("\nInvalid input. Please enter a number.");
                }
                else if (choice == 0)
                {
                    Console.Clear();
                    return null;
                }
                else if (choice > 0 && choice <= dictionaries.Count)
                {
                    return dictionaries[choice - 1];
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nInvalid choice. Please try again.");

                }
            }
        }
    }
}
