using HttpServer.websites.exalted.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.templates
{
    class RightMenu_Template : ITemplate
    {
        private User user;

        public RightMenu_Template(User user)
        {
            this.user = user;
        }

        public string Render()
        {

            string content = "";            

            if (user == null)
            {
                content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/right_menu_notlogged.html");
            }
            else
            {
                content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/right_menu.html");

            }

            return content;
        }
    }
}
