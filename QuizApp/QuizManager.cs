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
            easyCategory.AddQuestion(new QuizQuestion("What is the capital of France?", new List<string> { "Paris", "London", "Berlin", "Madrid" }, new List<int> { 0, 1 }));
            easyCategory.AddQuestion(new QuizQuestion("Which planet is known as the Red Planet?", new List<string> { "Mars", "Venus", "Jupiter", "Saturn" }, new List<int> { 0, 3 }));

            mediumCategory.AddQuestion(new QuizQuestion("Who wrote the play 'Hamlet'?", new List<string> { "William Shakespeare", "Charles Dickens", "Jane Austen", "Leo Tolstoy" }, new List<int> { 0 }));
            mediumCategory.AddQuestion(new QuizQuestion("What is the largest mammal in the world?", new List<string> { "Elephant", "Blue whale", "Giraffe", "Hippo" }, new List<int> { 1 }));

            hardCategory.AddQuestion(new QuizQuestion("In which year did World War I begin?", new List<string> { "1914", "1918", "1920", "1939" }, new List<int> { 0 }));
            hardCategory.AddQuestion(new QuizQuestion("Who painted the Mona Lisa?", new List<string> { "Leonardo da Vinci", "Vincent van Gogh", "Pablo Picasso", "Michelangelo" }, new List<int> { 0 }));

            tooHardCategory.AddQuestion(new QuizQuestion("What is the chemical symbol for the element gold?", new List<string> { "Go", "Au", "Ag", "Pt" }, new List<int> { 1 }));
            tooHardCategory.AddQuestion(new QuizQuestion("In computer science, what does 'HTTP' stand for?", new List<string> { "HyperText Transfer Protocol", "HyperText Transmission Protocol", "HyperTransfer Text Protocol", "Hyper Transmission Transfer Protocol" }, new List<int> { 0 }));
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
            Console.Write("Enter the index of correct answer(s) with space: ");
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
