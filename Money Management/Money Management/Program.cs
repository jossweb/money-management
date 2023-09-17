using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Management
{
    class Program
    {

    }
    public class User
    {
        /// <summary>
        /// Create user object
        /// </summary>
        public int id;
        public string name;
        public string firstName;
        public string keypass;
        public DateTime birthday;
        public DateTime accountCreationDate;
        public User(int id, string name, string firstName, string keypass, DateTime birthday, DateTime accountCreationDate)
        {
            this.id = id;
            this.name = name;
            this.firstName = firstName;
            this.keypass = keypass;
            this.birthday = birthday;
            this.accountCreationDate = accountCreationDate;

        }
    }
}
