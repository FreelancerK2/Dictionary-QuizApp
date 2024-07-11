using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuizApp.QuizManager;

namespace QuizApp
{
    public class QuizCategory
    {
        public List<QuizQuestion> Questions { get; set; }
        public List<int> Scores { get; set; }

        public QuizCategory()
        {
            Questions = new List<QuizQuestion>();
            Scores = new List<int>();
        }

        public void AddQuestion(QuizQuestion question)
        {
            Questions.Add(question);
        }

        public void UpdateQuestion(int index, QuizQuestion updatedQuestion)
        {
            if (index >= 0 && index < Questions.Count)
            {
                Questions[index] = updatedQuestion;
            }
            else
            {
                Console.WriteLine("Invalid question index.");
            }
        }

        public void DeleteQuestion(int index)
        {
            if (index >= 0 && index < Questions.Count)
            {
                Questions.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Invalid question index.");
            }
        }

        public void DeleteAllQuestions()
        {
            Questions.Clear();
        }

        public void DisplayQuestions()
        {
            foreach (var question in Questions)
            {
                question.DisplayQuestion();
            }
        }

        public void StartQuiz()
        {
            Console.WriteLine($"Starting quiz...");
            int score = 0;

            foreach (var question in Questions)
            {
                question.DisplayQuestion();
                Console.Write("Enter your answers numbers: ");
                string answerInput = Console.ReadLine();

                List<int> userAnswers = new List<int>();
                foreach (char c in answerInput)
                {
                    if (int.TryParse(c.ToString(), out int answerIndex))
                    {
                        userAnswers.Add(answerIndex - 1); // Convert to zero-based index
                    }
                }

                if (question.CheckAnswer(userAnswers))
                {
                    Console.WriteLine("\nCorrect!");
                    score++;
                }
                else
                {
                    Console.WriteLine("\nIncorrect!");
                }
                QuizManager.PressAnyKeyToContinue();
                Console.Clear();
            }
            Scores.Add(score);
            Console.WriteLine($"Quiz complete! Your score: {score} out of {Questions.Count}");
            QuizManager.PressAnyKeyToContinue();
            Console.Clear();
        }

        public void ViewPreviousResults()
        {
            Console.WriteLine($"Previous results:");
            for (int i = 0; i < Scores.Count; i++)
            {
                Console.WriteLine($"Quiz attempt {i + 1}: Score = {Scores[i]} out of {Questions.Count}");
            }
            Console.WriteLine();
            QuizManager.PressAnyKeyToContinue();
            Console.Clear();
        }

    }
}
