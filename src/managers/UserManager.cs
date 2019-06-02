using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using HttpServer.websites.exalted;
using HttpServer.helpers;
using HttpServer.websites.exalted.model;

namespace HttpServer.websites.exalted.managers
{
    public static class UserManager
    {
        public static int GetUserID(Client client)
        {
            if (Connected(client))
            {
                return (int)client.Dictionary["UID"];
            }
            else
            {
                return -1;
            }
        }

        public static bool Connected(this Client client)
        {
            return client.Dictionary.ContainsKey("UID");
        }

        public static User CreateUser(string username, string password)
        {
            if (username == string.Empty || password == string.Empty)
            {
                return null;
            }

            if (UserManager.Exists(username))
            {
                return null;
            }

            IDbDataParameter paramUsername = WebSite.Database.CreateParameter("@Username", username);
            IDbDataParameter paramPassword = WebSite.Database.CreateParameter("@Password", password);

            WebSite.Database.ExecuteNonQuery("INSERT INTO users (Username, Password) VALUES (@Username, @Password)", paramUsername, paramPassword);
            
            return UserManager.GetUser(username);
        }        

        public static User GetUser(int id)
        {
            if (id == -1)
            {
                return null;
            }

            IDbDataParameter parameter = WebSite.Database.CreateParameter("@ID", id);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT Id, Username, Password FROM users WHERE Id=@ID", parameter);

            if (table == null)
            {
                return null;
            }

            if (table.Rows.Count < 1)
            {
                return null;
            }

            DataRow dataRow = table.Rows[0];

            return new User((int)dataRow[User.ID_FIELD], (string)dataRow[User.USERNAME_FIELD], (string)dataRow[User.PASSWORD_FIELD]);
        }

        public static User GetUser(string username)
        {
            IDbDataParameter parameter = WebSite.Database.CreateParameter("@Username", username);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT Id, Username, Password FROM users WHERE Username=@Username", parameter);

            if (table == null)
            {
                return null;
            }

            if (table.Rows.Count < 1)
            {
                return null;
            }

            DataRow dataRow = table.Rows[0];

            return new User((int)dataRow[User.ID_FIELD], (string)dataRow[User.USERNAME_FIELD], (string)dataRow[User.PASSWORD_FIELD]);
        }

        public static bool Exists(string username)
        {
            IDbDataParameter paramUsername = WebSite.Database.CreateParameter("@Username", username);
            object result = WebSite.Database.ExecuteScalar("SELECT COUNT(*) FROM users WHERE Username=@Username", paramUsername);

            int count = 0;

            int.TryParse(result.ToString(), out count);

            return (count > 0);
        }

        public static bool Logout(Client client)
        {
            if (client == null)
            {
                return false;
            }

            if (client.Dictionary.ContainsKey("UID"))
            {
                client.Dictionary.Remove("UID");
                return true;
            }

            return false;
        }

        public static bool Login(string username, string password, Client client)
        {
            User user = UserManager.GetUser(username);

            if (user == null)
            {
                return false;
            }

            if (PasswordManager.Verify(password, user.Hash))
            {
                client.Dictionary.Add("UID", user.Id);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
