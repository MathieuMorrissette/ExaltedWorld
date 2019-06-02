using HttpServer.websites.exalted.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.templates
{
    class Map_Template : ITemplate
    {
        public Map_Template()
        {
        }

        public string Render()
        {
            string content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/map.html");

            return content;
        }
    }
}
