using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.model
{
    public class User
    {
        public const string ID_FIELD = "Id";
        public const string USERNAME_FIELD = "Username";
        public const string PASSWORD_FIELD = "Password";

        public User(int id, string username, string hash)
        {
            this.Id = id;
            this.Username = username;
            this.Hash = hash;
        }
        
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Hash { get; private set;  }
    }
}
