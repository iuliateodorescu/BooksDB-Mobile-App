using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDB.Models
{
    public class UserDetails : User
    {
        private UserDetails()
        {
        }

        public void ClearUser()
        {
            UserId = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public static UserDetails Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDetails();
                }
                return instance;
            }
        }

        private static UserDetails instance = null;
    }
}
