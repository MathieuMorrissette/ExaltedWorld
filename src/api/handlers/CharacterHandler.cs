using HttpServer.websites.exalted.api.responses;
using HttpServer.websites.exalted.helpers;
using HttpServer.websites.exalted.managers;
using HttpServer.websites.exalted.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.api.handlers
{
    public class CharacterHandler : IController
    {
        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            if (!UserManager.Connected(client))
            {
                context.Send("not connected");
                return true;
            }

            User user = UserManager.GetUser(UserManager.GetUserID(client));

            if (user == null)
            {
                context.Send("user not found");
                return true;
            }

            if (context.Request.HttpMethod == "POST")
            {
                if (args.Length == 0)
                {
                    return true;
                }

                if (args[0] == "create")
                {
                    string body = new StreamReader(context.Request.InputStream).ReadToEnd();

                    JObject json = JObject.Parse(body);

                    string replaced = CharacterManager.DefaultCharacterJSON.Replace("__NAME__", json["charactername"].ToString());

                    JObject data = JObject.Parse(replaced); 

                    CharacterManager.CreateCharacter(user, JsonConvert.SerializeObject(data));

                    return true;
                }

                if ( args.Length == 2 && args[1] == "update")
                {
                    string body = new StreamReader(context.Request.InputStream).ReadToEnd();

                    JObject json = JObject.Parse(body);

                    // TODO add some validation here to make sure we don't receive junk
                    int characterId = Convert.ToInt32(args[0]);

                    CharacterManager.UpdateCharacter(characterId, JsonConvert.SerializeObject(json));
                }


                if (args.Length == 2)
                {
                    int characterId = Convert.ToInt32(args[0]);

                    Character character = CharacterManager.GetCharacter(characterId);

                    if (character == null)
                    {
                        return true;
                    }

                    if (character.UserId != user.Id)
                    {
                        return true;
                    }

                    if (args[1] == "delete")
                    {
                        CharacterManager.DeleteCharacter(characterId);
                    }

                    return true; 
                }
            }
            

            int id = Convert.ToInt32(args[0]);

            Character carac = CharacterManager.GetCharacter(id);

            context.Send(SerializationHelper.SerializeToJSON(CharacterResponse.FromCharacter(carac)));

            return true;
        }
    }
}
