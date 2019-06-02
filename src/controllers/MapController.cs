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
    class MapController : IController
    {
        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            Map_Template template = new Map_Template();
            context.Send(template.Render());

            return true;
        }
    }
}
