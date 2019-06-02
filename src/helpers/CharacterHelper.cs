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
using HttpServer.websites.exalted.api.responses;

namespace HttpServer.websites.exalted.managers
{
    public static class CharacterHelper
    {
        public static Character FromDataRow(DataRow dataRow)
        {
            return new Character((int)dataRow[Character.ID_FIELD], (int)dataRow[Character.USER_ID_FIELD], (string)dataRow[Character.DATA_FIELD]);
        }

        public static CharacterResponse[] ToResponse(Character[] characters)
        {
            List<CharacterResponse> responses = new List<CharacterResponse>();

            foreach (Character character in characters)
            {
                responses.Add(CharacterResponse.FromCharacter(character));
            }

            return responses.ToArray();
        }
    }
}
