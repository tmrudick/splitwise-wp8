using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Models
{
    public class Friendship
    {
        public long Id { get; set; }
        public List<User> Users { get; set; }
        public string Balance { get; set; }

        public User Friend
        {
            get
            {
                return this.Users[0];
            }
        }
    }

    public class FriendshipsWrapper
    {
        public List<Friendship> Friendships { get; set; }
    }

    public class FriendshipWrapper
    {
        public Friendship Friendship { get; set; }
    }
}
