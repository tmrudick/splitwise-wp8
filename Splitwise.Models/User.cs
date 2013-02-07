using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Models
{
    public class User
    {
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public string AbbreviatedName
        {
            get
            {
                return string.Format("{0} {1}.", this.FirstName, this.LastName[0]);
            }
        }

        public Dictionary<string, string> Picture { get; set; }

        public Uri PhotoUrl
        {
            get
            {
                return new Uri(this.Picture["medium"]);
            }
        }
    }

    public class UserWrapper
    {
        public User User { get; set; }
    }
}
