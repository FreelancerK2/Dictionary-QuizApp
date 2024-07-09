using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            QuizManager quizManager = new QuizManager();
            quizManager.DisplayMainMenu();
        }
    }
}
