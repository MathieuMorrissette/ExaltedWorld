
using HttpServer.websites.exalted.managers;
using HttpServer.websites.exalted.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.api.responses
{
    public class CharacterResponse
    {
        public int id { get; set; }

        public int owner { get; set; }

        public string data { get; set; }

        public static CharacterResponse FromCharacter(Character character)
        {
            CharacterResponse response = new CharacterResponse();

            response.id = character.Id;
            response.owner = character.UserId;
            response.data = character.Data;

            return response;
        }
    }
}
