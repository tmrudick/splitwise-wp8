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
    }

    public class FriendshipWrapper
    {
        public List<Friendship> Friendships { get; set; }

        // Return just the user objects. None of this friends mumbojumbo.
        public List<User> GetFriends()
        {
            List<User> friends = new List<User>();
            
            foreach (Friendship friendship in this.Friendships)
            {
                // TODO: Taking the first thing might not always work.
                // May need to pass in the current user and specifically check.
                friends.Add(friendship.Users[0]);
            }

            return friends;
        }
    }
}
