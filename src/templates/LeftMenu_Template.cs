using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.templates
{
    class LeftMenu_Template : ITemplate
    {
        private bool sheet;

        public LeftMenu_Template(bool sheet)
        {
            this.sheet = sheet;
        }

        public string Render()
        {
            string content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/left_menu.html");

            if (sheet)
            {
                content = content.Replace("__SHEET__", File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/left_sheet_menu.html"));
            }
            else
            {
                content = content.Replace("__SHEET__", string.Empty);
            }

            content = content.Replace("__TOOLS__", File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/left_tools_menu.html"));

            return content;
        }
    }
}
