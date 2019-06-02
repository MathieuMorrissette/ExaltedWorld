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
using System.Reflection;
using System.IO;

namespace HttpServer.websites.exalted.managers
{
    public static class CharacterManager
    {
        public static string DefaultCharacterJSON;

        static CharacterManager()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "HttpServer.websites.exalted.api.default_character_json.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    DefaultCharacterJSON = reader.ReadToEnd();
                }
            }
        }

        public static Character CreateCharacter(User user, string data)
        {
            if (data == string.Empty)
            {
                return null;
            }

            string userId = user == null ? string.Empty : user.Id.ToString();

            IDbDataParameter paramUserID = WebSite.Database.CreateParameter("@UserId", userId);
            IDbDataParameter paramData = WebSite.Database.CreateParameter("@Data", data);

            WebSite.Database.ExecuteNonQuery("INSERT INTO characters (UserId, Data) VALUES (@UserId, @Data)", paramUserID, paramData);


            return null;
        }

        public static void UpdateCharacter(int id, string data)
        {
            if (data == string.Empty)
            {
                return;
            }
            
            IDbDataParameter paramData = WebSite.Database.CreateParameter("@Data", data);
            IDbDataParameter paramId = WebSite.Database.CreateParameter("@Id", id);

            WebSite.Database.ExecuteNonQuery("UPDATE characters SET Data = @Data WHERE Id = @Id", paramData, paramId);
            
            return;
        }

        public static Character GetCharacter(int id)
        {
            if (id == -1)
            {
                return null;
            }

            IDbDataParameter parameter = WebSite.Database.CreateParameter("@ID", id);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT * FROM characters WHERE Id=@ID", parameter);

            if (table == null)
            {
                return null;
            }

            if (table.Rows.Count < 1)
            {
                return null;
            }

            DataRow dataRow = table.Rows[0];

            return CharacterHelper.FromDataRow(dataRow);
        }

        public static Character GetCharacter(string characterName)
        {
            IDbDataParameter parameter = WebSite.Database.CreateParameter("@Name", characterName);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT * FROM characters WHERE Name=@Name", parameter);

            if (table == null)
            {
                return null;
            }

            if (table.Rows.Count < 1)
            {
                return null;
            }

            DataRow dataRow = table.Rows[0];

            return CharacterHelper.FromDataRow(dataRow);
        }

        public static Character[] GetCharacters(User user)
        {
            IDbDataParameter parameter = WebSite.Database.CreateParameter("@UserId", user.Id);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT * FROM characters WHERE UserId=@UserId", parameter);

            if (table == null)
            {
                return new Character[0];
            }

            if (table.Rows.Count < 1)
            {
                return new Character[0];
            }

            List<Character> characters = new List<Character>();

            foreach (DataRow row in table.Rows)
            {
                characters.Add(CharacterHelper.FromDataRow(row));
            }

            return characters.ToArray();
        }

        public static void DeleteCharacter(int id)
        {
            IDbDataParameter paramUserID = WebSite.Database.CreateParameter("@Id", id);

            WebSite.Database.ExecuteNonQuery("DELETE FROM characters WHERE Id=@Id", paramUserID);
        }

        public static bool Exists(string name)
        {
            IDbDataParameter paramUsername = WebSite.Database.CreateParameter("@Name", name);
            object result = WebSite.Database.ExecuteScalar("SELECT COUNT(*) FROM characters WHERE Name=@Name", paramUsername);

            int count = 0;

            int.TryParse(result.ToString(), out count);

            return (count > 0);
        }
    }
}
