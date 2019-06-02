using HttpServer.websites.exalted.managers;
using HttpServer.websites.exalted.model;
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
    class Characters : IController
    {
        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            if (!UserManager.Connected(client))
            {
                context.Redirect("../login");
                return true;
            }

            User user = UserManager.GetUser(UserManager.GetUserID(client));

            if (user == null)
            {
                context.Redirect("../login");
                return true;
            }

            Characters_Template template = new Characters_Template(user);
            context.Send(template.Render());

            return true;
        }
    }
}
