using HttpServer.websites.exalted.managers;
using HttpServer.websites.exalted.templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.controllers
{
    class Home : IController
    {
        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            HttpServer.websites.exalted.model.User user = UserManager.GetUser(UserManager.GetUserID(client));

            Home_Template template = new Home_Template(user);
            context.Send(template.Render());

            return true;
        }
    }
}
