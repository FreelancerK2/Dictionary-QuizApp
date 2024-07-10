using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class QuizManager
    {
        private readonly QuizCategory easyCategory;
        private readonly QuizCategory mediumCategory;
        private readonly QuizCategory hardCategory;
        private readonly QuizCategory tooHardCategory;

        private const int MaxAttempts = 3;
        private int attemptCount = 0;

        private List<User> users;
        private User currentUser;

        public enum Menu { START = 1, VIEW, MANAGE, DELETEALL, CHANGE, EXIT = 0 }
        public enum Log { LOGIN = 1, REGISTER, EXIT = 0 }
        public enum Stage { EASY = 1, MEDIUM, HARD, TOOHARD, EXIT = 0 }
        public enum Option { ADD = 1, UPDATE, DELETE, BACK = 0 }
        public enum Choice { TEXT = 1, OPTION, ANSWER, EXIT = 0 }


        public QuizManager()
        {
            easyCategory = new QuizCategory();
            mediumCategory = new QuizCategory();
            hardCategory = new QuizCategory();
            tooHardCategory = new QuizCategory();

            users = new List<User>();

            // Initialize with some example questions
            InitializeQuestions();
        }

        public void DisplayMainMenu()
        {
            if (!LoginOrRegister())
            {
                return;
            }

            Console.WriteLine($"Welcome to the Quiz Application, {currentUser.Username}!");

            while (true)
            {
                int input = IntegerInput("Menu:\n1. Start Quiz\n2. View Previous Results\n3. Manage Question(Add, Update, Delete)\n4. Delete All Question\n5. Change Setting\n0. Exit\nEnter your choice: ");
                Console.Clear();
                if (input == (int)Menu.EXIT)
                {
                    Console.WriteLine("Exiting quiz application...");
                    break;
                }

                switch (input)
                {
                    case (int)Menu.START:
                        StartQuizMenu();
                        break;
                    case (int)Menu.VIEW:
                        ViewPreviousResultsMenu();
                        break;
                    case (int)Menu.MANAGE:
                        ManageQuestionsMenu();
                        break;
                    case (int)Menu.DELETEALL:
                        DeleteAllQuestionsMenu();
                        break;
                    case (int)Menu.CHANGE:
                        ChangeSettings();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 6.");
                        PressAnyKeyToContinue();
                        Console.Clear();
                        break;
                }
            }
        }

        private bool LoginOrRegister()
        {
            int choice = IntegerInput("1. Login\n2. Register\n0. Exit\nEnter your choice: ");
            Console.Clear();
            switch (choice)
            {
                case (int)Log.LOGIN:
                    return Login();
                case (int)Log.REGISTER:
                    return Register();
                case (int)Log.EXIT:
                    Console.WriteLine("You are exiting.");
                    return false;
                default:
                    Console.WriteLine("Invalid input. Please try again! ");
                    return false;
            }
        }

        private bool Login()
        {
            while (attemptCount < MaxAttempts)
            {
                string inputUsername = InputString("Enter login username: ");
                int inputPassword = IntegerInput("Enter login password: ");
                Console.Clear();
                foreach (var user in users)
                {
                    if (user.Username == inputUsername && user.Password == inputPassword)
                    {
                        currentUser = user;
                        Console.WriteLine("Login successful!");
                        return true;
                    }
                    attemptCount++;
                    Console.WriteLine("Login failed. Invalid username or password.");
                }
            }
            return false;
        }


        // Function store username & password 
        /*    private bool Login()
            {
                string Username = "Somnang";
                int Password = 21102000;
                string inputUsername = InputString("Enter login username: ");
                int inputPassword = IntegerInput("Enter login password: ");

                if (Username == inputUsername && Password == inputPassword)
                {
                    // currentUser = user;
                    Console.WriteLine("Login successful!");
                    return true;
                }
                Console.WriteLine("Login failed. Invalid username or password.");
                return false;
            }*/

        private bool Register()
        {
            string newUsername = InputString("Enter new username: ");
            int newPassword = IntegerInput("Enter new password: ");

            foreach (var user in users)
            {
                if (user.Username == newUsername)
                {
                    Console.WriteLine("Username already taken. Please try a different username.");
                    return false;
                }
            }

            users.Add(new User(newUsername, newPassword));
            Console.WriteLine("\nRegistration successful! Please login to continue.");
            PressAnyKeyToContinue();
            Console.Clear();
            return LoginOrRegister();
        }

        private void ChangeSettings()
        {
            while (attemptCount < MaxAttempts)
            {
                int inputPassword = IntegerInput("Enter current password: ");
                Console.Clear();
                if (inputPassword == currentUser.Password)
                {
                    string newUsername = InputString("Enter new username: ");
                    int newPassword = IntegerInput("Enter new password: ");

                    currentUser.Username = newUsername;
                    currentUser.Password = newPassword;
                    Console.WriteLine("Settings changed successfully!");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    break;
                }
                attemptCount++;
                Console.WriteLine("Current password is incorrect. Please try again");
            }
            return;
        }

        private void InitializeQuestions()
        {
            // Add some example questions to each category
            easyCategory.AddQuestion(new QuizQuestion("Which of the following are programming languages?", new List<string> { "Python", "HTML", "Java", "CSS" }, new List<int> { 0, 2 }));
            easyCategory.AddQuestion(new QuizQuestion("Which of the following are elements on the periodic table?", new List<string> { "Oxygen", "Hydrogen", "Water", "Carbon" }, new List<int> { 0, 3 }));
            easyCategory.AddQuestion(new QuizQuestion("Which of these are programming concepts?", new List<string> { "Loop", "Variable", "HTML", "Array" }, new List<int> { 0, 3 }));
            easyCategory.AddQuestion(new QuizQuestion("Which of these are social media platforms?", new List<string> { "Facebook", "Google", "Instagram", "Whatapp" }, new List<int> { 0, 2 }));
            easyCategory.AddQuestion(new QuizQuestion("Which of these are JavaScript frameworks?", new List<string> { "Angular", "React", "Django", "Vue" }, new List<int> { 0, 1     }));
            easyCategory.AddQuestion(new QuizQuestion("Which of these are official languages of the United Nations?", new List<string> { "English", "Russian", "German", "Spanish" }, new List<int> { 0, 3 }));
            easyCategory.AddQuestion(new QuizQuestion("Which of the following are fundamental forces in physics?", new List<string> { "Gravity", "Electromagnetism", "Water", "Carbon" }, new List<int> { 0, 3 }));
            easyCategory.AddQuestion(new QuizQuestion("Which of these are types of clouds?", new List<string> { "Cirrus", "Cumulus", "Stratus", "Nimbus" }, new List<int> { 0, 2 }));
            easyCategory.AddQuestion(new QuizQuestion("Which of the following are operating systems?", new List<string> { "Windows", "Android", "iPhone", "MacOS" }, new List<int> { 0, 3 }));
            easyCategory.AddQuestion(new QuizQuestion("Which of these are prime numbers?", new List<string> { "4", "7", "11", "15" }, new List<int> { 1, 2 }));

            mediumCategory.AddQuestion(new QuizQuestion("Which of the following are planets in our solar system?", new List<string> { "Pluto", "Mars", "Venus", "Alpha Centauri" }, new List<int> { 1, 2 }));
            mediumCategory.AddQuestion(new QuizQuestion("Which of these are famous painters?", new List<string> { "Leonardo da Vinci", "Albert Einstein", "Vincent van Gogh", "Nikola Tesla" }, new List<int> { 0, 2 }));
            mediumCategory.AddQuestion(new QuizQuestion("Which of the following are types of data structures?", new List<string> { "Array", "Linked List", "PDF", "CSS" }, new List<int> { 0, 1 }));
            mediumCategory.AddQuestion(new QuizQuestion("Which of the following are commonly used databases in web development?", new List<string> { "MongoDB", "SQLite", "Photoshop", "Illustrator" }, new List<int> { 0, 1 }));
            mediumCategory.AddQuestion(new QuizQuestion("Which of the following are examples of web development frameworks?", new List<string> { "React", "Angular", "Excel", "WordPress" }, new List<int> { 0, 2 }));
            mediumCategory.AddQuestion(new QuizQuestion("Which of these are types of databases?", new List<string> { "MySQL", "PostgreSQL", "JSON", "XML" }, new List<int> { 0, 1 }));
            mediumCategory.AddQuestion(new QuizQuestion("Which of the following are used to connect to the internet?", new List<string> { "Moderm", "Router", "Printer", "Scanner" }, new List<int> { 0, 1 }));
            mediumCategory.AddQuestion(new QuizQuestion("Which of the following are examples of system software?", new List<string> { "Operating System", "Device Drivers", "Microsoft Word", "Adobe Acrobat" }, new List<int> { 0, 1 }));
            mediumCategory.AddQuestion(new QuizQuestion("Which of these are types of malware?", new List<string> { "Virus", "Trojan Horse", "HTML", "URL" }, new List<int> { 0, 2 }));
            mediumCategory.AddQuestion(new QuizQuestion("Which of the following are examples of cloud storage services?", new List<string> { "Google Drive", "Dropbox", "Window Explorer", "VLC Media Player" }, new List<int> { 0, 1 }));

            hardCategory.AddQuestion(new QuizQuestion("Which of these are programming paradigms?", new List<string> { "Object-Oriented", "Functional", "Quantum", "Relational" }, new List<int> { 0, 1 }));
            hardCategory.AddQuestion(new QuizQuestion("Which of the following are renewable energy sources?", new List<string> { "Coal", "Solar", "Wind", "Natural Gas" }, new List<int> { 1, 2 }));
            hardCategory.AddQuestion(new QuizQuestion("Which two of the following languages are primarily used for backend web development?", new List<string> { "Ruby", "JavaScript", "PHP", "HTML" }, new List<int> { 0, 1 }));
            hardCategory.AddQuestion(new QuizQuestion("Which two of the following are features of object-oriented programming?", new List<string> { "Encapsulation", "Inheritance", "Pointers", "Recursion" }, new List<int> { 1, 2 }));
            hardCategory.AddQuestion(new QuizQuestion("Which of the following are commonly used version control systems?", new List<string> { "Git", "SVN", "Mercurial", "CVS" }, new List<int> { 0, 1 }));
            hardCategory.AddQuestion(new QuizQuestion("Which two of the following are valid ways to declare a variable in JavaScript?", new List<string> { "let", "var", "const", "define" }, new List<int> { 0, 1 }));
            hardCategory.AddQuestion(new QuizQuestion("Which two of the following are primitive data types in Java?", new List<string> { "String", "int", "double", "ArrayList" }, new List<int> { 1, 2 }));
            hardCategory.AddQuestion(new QuizQuestion("Which of the following are used to handle exceptions in Python?", new List<string> { "try", "except", "catch", "finally" }, new List<int> { 0, 1 }));
            hardCategory.AddQuestion(new QuizQuestion("Which two languages are primarily used for frontend web development?", new List<string> { "HTML", "CSS", "Python", "Java" }, new List<int> { 0, 1 }));
            hardCategory.AddQuestion(new QuizQuestion("Which two data structures are most commonly used to implement a LRU cache?", new List<string> { "Stack", "Linked List", "HashMap", "Queue" }, new List<int> { 1, 2 }));

            tooHardCategory.AddQuestion(new QuizQuestion("Which of these are units of time?", new List<string> { "Light-year", "Hour", "Meter", "Second" }, new List<int> { 1, 3 }));
            tooHardCategory.AddQuestion(new QuizQuestion("Which of these are types of economic systems?", new List<string> { "Communism", "Socialism", "Capitalism", "Nationalism" }, new List<int> { 0, 2 }));
            tooHardCategory.AddQuestion(new QuizQuestion("In object-oriented programming, which of the following are principles?", new List<string> { "Inheritance", "Polymorphism", "Reflection", "Abstraction" }, new List<int> { 0, 1 }));
            tooHardCategory.AddQuestion(new QuizQuestion("Which of the following are commonly used for version control?", new List<string> { "Git", "Subversion", "CVS", "Bitbucket" }, new List<int> { 0, 1 }));
            tooHardCategory.AddQuestion(new QuizQuestion("In Python, which of the following are valid string methods?", new List<string> { "append()", "split()", "join()", "pop()" }, new List<int> { 0, 2 }));
            tooHardCategory.AddQuestion(new QuizQuestion("In C++, which of the following are access specifiers?", new List<string> { "public", "private", "protected", "final" }, new List<int> { 0, 1 }));
            tooHardCategory.AddQuestion(new QuizQuestion("Which of the following are design patterns?", new List<string> { "Singleton", "Factory", "Encapsulation", "Inheritance" }, new List<int> { 0, 1 }));
            tooHardCategory.AddQuestion(new QuizQuestion("Which of these are valid Python loops?", new List<string> { "for", "while", "until", "loop" }, new List<int> { 0, 1 }));
            tooHardCategory.AddQuestion(new QuizQuestion("In SQL, which of the following are aggregate functions?", new List<string> { "SUM", "AVG", "SELECT", "INSERT" }, new List<int> { 0, 1 }));
            tooHardCategory.AddQuestion(new QuizQuestion("Which of the following are functional programming languages?", new List<string> { "Haskell", "Lisp", "C++", "JavaScript" }, new List<int> { 0, 1 }));
        }

        private void StartQuizMenu()
        {
            int categoryChoice = IntegerInput("Choose a category to start Quiz:\n1. Easy\n2. Medium\n3. Hard\n4. Too Hard\n0. Exit\nEnter your choice: ");
            Console.Clear();

            QuizCategory selectedCategory = null;
            switch (categoryChoice)
            {
                case (int)Stage.EASY:
                    selectedCategory = easyCategory;
                    break;
                case (int)Stage.MEDIUM:
                    selectedCategory = mediumCategory;
                    break;
                case (int)Stage.HARD:
                    selectedCategory = hardCategory;
                    break;
                case (int)Stage.TOOHARD:
                    selectedCategory = tooHardCategory;
                    break;
                case (int)Stage.EXIT:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 0 to 4.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    return;
            }

            selectedCategory.StartQuiz();
        }

        private void ViewPreviousResultsMenu()
        {
            int categoryChoice = IntegerInput("Choose a category to view previous results:\n1. Easy\n2. Medium\n3. Hard\n4. Too Hard\n0. Exit\nEnter your choice: ");
            Console.Clear();

            QuizCategory selectedCategory = null;
            switch (categoryChoice)
            {
                case (int)Stage.EASY:
                    selectedCategory = easyCategory;
                    break;
                case (int)Stage.MEDIUM:
                    selectedCategory = mediumCategory;
                    break;
                case (int)Stage.HARD:
                    selectedCategory = hardCategory;
                    break;
                case (int)Stage.TOOHARD:
                    selectedCategory = tooHardCategory;
                    break;
                case (int)Stage.EXIT:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 0 to 4.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    return;
            }
            selectedCategory.ViewPreviousResults();
        }

        private void ManageQuestionsMenu()
        {
            int categoryChoice = IntegerInput("Choose a category to manage question:\n1. Easy\n2. Medium\n3. Hard\n4. Too Hard\n0. Exit\nEnter your choice: ");
            Console.Clear();

            QuizCategory selectedCategory = null;
            switch (categoryChoice)
            {
                case (int)Stage.EASY:
                    selectedCategory = easyCategory;
                    break;
                case (int)Stage.MEDIUM:
                    selectedCategory = mediumCategory;
                    break;
                case (int)Stage.HARD:
                    selectedCategory = hardCategory;
                    break;
                case (int)Stage.TOOHARD:
                    selectedCategory = tooHardCategory;
                    break;
                case (int)Stage.EXIT:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 0 to 4.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    return;
            }

            int operationChoice = IntegerInput("Choose an operation:\n1. Add Question\n2. Update Question\n3. Delete Question\n0. Back to Main Menu\nEnter your choice: ");
            Console.Clear();
            switch (operationChoice)
            {
                case (int)Option.ADD:
                    AddQuestion(selectedCategory);
                    break;
                case (int)Option.UPDATE:
                    UpdateQuestion(selectedCategory);
                    break;
                case (int)Option.DELETE:
                    DeleteQuestion(selectedCategory);
                    break;
                case (int)Option.BACK:
                    Console.WriteLine("Returning to main menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 0 to 4.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    break;
            }
        }

        private void AddQuestion(QuizCategory category)
        {
            do
            {
                string questionText = InputString("Enter the question test: ");
                List<string> options = new List<string>();
                Console.WriteLine("Enter options for player (enter 'done' to finish):");
                while (true)
                {
                    Console.Write("Option: ");
                    string option = Console.ReadLine();
                    if (option.ToLower() == "done")
                        break;
                    options.Add(option);
                }
                category.AddQuestion(CreateQuestion(questionText, options));
                Console.WriteLine("Question Successfully added!");

            } while (AskQuestion());
            Console.Clear();
        }

        private void UpdateQuestion(QuizCategory category)
        {
            do
            {
                Console.WriteLine("Current questions:");
                category.DisplayQuestions();

                Console.Write("Enter the index of the question to update: ");
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    Console.Clear();
                    if (index >= 0 && index < category.Questions.Count)
                    {
                        int partChoice = IntegerInput("Which part do you want to update?\n1. Question Text\n2. Options\n3. Correct Answers\n0. Exit\nEnter your choice: ");

                        switch (partChoice)
                        {
                            case (int)Choice.TEXT:
                                Console.Write("Enter the updated question text: ");
                                string questionText = Console.ReadLine();
                                category.Questions[index].QuestionText = questionText;
                                break;
                            case (int)Choice.OPTION:
                                List<string> options = new List<string>();
                                Console.WriteLine("Enter updated options (enter 'done' to finish):");
                                while (true)
                                {
                                    Console.Write("Option: ");
                                    string option = Console.ReadLine();
                                    if (option.ToLower() == "done")
                                        break;
                                    options.Add(option);
                                }
                                category.Questions[index].Options = options;
                                break;
                            case (int)Choice.ANSWER:
                                Console.WriteLine("Enter the index of correct answer(s) (comma-separated, starting from 1):");
                                string correctAnswerInput = Console.ReadLine();
                                string[] correctAnswerParts = correctAnswerInput.Split(',');

                                List<int> correctAnswers = new List<int>();
                                foreach (var part in correctAnswerParts)
                                {
                                    if (int.TryParse(part.Trim(), out int answerIndex))
                                    {
                                        correctAnswers.Add(answerIndex - 1); // Convert to zero-based index
                                    }
                                }
                                category.Questions[index].CorrectAnswers = correctAnswers;
                                break;
                            case (int)Choice.EXIT:
                                Console.WriteLine("\nExiting");
                                break;
                            default:
                                Console.WriteLine("\nInvalid choice.");
                                PressAnyKeyToContinue();
                                Console.Clear();
                                return;
                        }

                        Console.WriteLine("\nQuestion updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid question index.");
                        PressAnyKeyToContinue();
                        Console.Clear();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a number.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    return;
                }
            } while (AskQuestion());
            Console.Clear();
        }


        private void DeleteQuestion(QuizCategory category)
        {
            do
            {
                Console.WriteLine("Current questions:");
                category.DisplayQuestions();

                Console.Write("Enter the index of the question to delete: ");
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    bool confirm = AskConfirmation("You are going to delete this question? (y/n): ");
                    if (confirm)
                    {
                        category.DeleteQuestion(index);
                        Console.WriteLine("\nQuestion deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("\nDeletion cancelled.");
                        PressAnyKeyToContinue();
                        Console.Clear();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            } while (AskQuestion());
            Console.Clear();
        }

        private void DeleteAllQuestionsMenu()
        {
            do
            {
                int categoryChoice = IntegerInput("Choose a category to delete entire question:\n1. Easy\n2. Medium\n3. Hard\n4. Too Hard\n0. Exit\nEnter your choice: ");

                QuizCategory selectedCategory = null;
                switch (categoryChoice)
                {
                    case (int)Stage.EASY:
                        selectedCategory = easyCategory;
                        break;
                    case (int)Stage.MEDIUM:
                        selectedCategory = mediumCategory;
                        break;
                    case (int)Stage.HARD:
                        selectedCategory = hardCategory;
                        break;
                    case (int)Stage.TOOHARD:
                        selectedCategory = tooHardCategory;
                        break;
                    case (int)Stage.EXIT:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 0 to 4.");
                        PressAnyKeyToContinue();
                        Console.Clear();
                        return;
                }
                bool confirm = AskConfirmation("You are going to delete this category? (y/n): ");
                if (confirm)
                {
                    selectedCategory.DeleteAllQuestions();
                    Console.WriteLine("\nQuestion deleted successfully.");
                }
                else
                {
                    Console.WriteLine("\nDeletion cancelled.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    return;
                }
            } while (AskQuestion());
            Console.Clear();
        }

        private QuizQuestion CreateQuestion(string questionText, List<string> options)
        {
            Console.Write("Enter two index of correct answer(s) with space: ");
            string correctAnswerInput = Console.ReadLine();
            Console.Clear();

            List<int> userAnswers = new List<int>();
            foreach (char c in correctAnswerInput)
            {
                if (int.TryParse(c.ToString(), out int answerIndex))
                {
                    userAnswers.Add(answerIndex - 1); // Convert to zero-based index
                }
            }

            return new QuizQuestion(questionText, options, userAnswers);

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
                    Console.Clear();
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
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine();
                // Check if the input is an integer
                if (int.TryParse(userInput, out _))
                {
                    Console.WriteLine("\nInvalid input, Please enter only string");
                    PressAnyKeyToContinue();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    return userInput;
                }
            }

        }

        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
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


    }

}
