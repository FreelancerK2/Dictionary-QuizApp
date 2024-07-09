using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class User
    {
        public string Username { get; set; }
        public int Password { get; set; }

        public User(string username, int password)
        {
            Username = username;
            Password = password;
        }
    }
}
