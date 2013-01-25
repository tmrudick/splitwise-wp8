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

        /// <summary>
        /// A friendship consists of two users: yourself and the other person. This is yourself.
        /// </summary>
        public User Self { get; set; }

        /// <summary>
        /// A friendship consists of two users: yourself and the other person. This is the other person.
        /// </summary>
        public User Friend
        {
            get
            {
                // Return the user that *isn't* yourself.
                if (Self != null)
                {
                    return this.Users.Single(user => user.Id != Self.Id);
                }
                else
                {
                    throw new ArgumentNullException("Self", "First set the 'self' user before getting your friend.");
                }
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
