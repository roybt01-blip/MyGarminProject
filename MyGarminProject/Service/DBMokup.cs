using MyGarminProject.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGarminProject.Service
{
    public class DBMokup
    {
        private List<User> _users = new List<User>();
        public DBMokup()
        {
            _users.Add(new User { UserEmail = "admin@mail.com", UserPassword = "admin" }); //UserName = "admin",
            _users.Add(new User { UserEmail = "user1@mail.com", UserPassword = "pass1" }); //UserName = "user1",
            _users.Add(new User { UserEmail = "user2@mail.com", UserPassword = "pass2" }); // UserName = "user2",
        }
        public bool IsExist(string uEmail, string uPass)
        {
            return _users.Any(u => u.UserEmail == uEmail && u.UserPassword == uPass);
        }
    }
}
