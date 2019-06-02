using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.model
{
    public class Character
    {
        public const string ID_FIELD = "Id";
        public const string USER_ID_FIELD = "UserId";
        public const string DATA_FIELD = "Data";

        public Character(int id, int userId, string data)
        {
            this.Id = id;
            this.UserId = userId;
            this.Data = data;
        }
        
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Data { get; private set;  }
    }
}
