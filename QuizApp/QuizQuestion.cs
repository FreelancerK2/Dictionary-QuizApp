using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class QuizQuestion
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public List<int> CorrectAnswers { get; set; }

        public QuizQuestion(string questionText, List<string> options, List<int> correctAnswers)
        {
            QuestionText = questionText;
            Options = options;
            CorrectAnswers = correctAnswers;
        }

        public bool CheckAnswer(List<int> userAnswers)
        {
            if (userAnswers.Count != CorrectAnswers.Count)
                return false;

            foreach (var answer in userAnswers)
            {
                if (!CorrectAnswers.Contains(answer))
                    return false;
            }

            return true;
        }

        public void DisplayQuestion()
        {
            Console.WriteLine(QuestionText);
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Options[i]}");
            }
            Console.WriteLine();
        }
    }
}
