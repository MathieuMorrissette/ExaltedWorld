using HttpServer.websites.exalted.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.templates
{
    public class Register_Template : ITemplate
    {
        private User user;

        public Register_Template(User user)
        {
            this.user = user;
        }

        public Error[] Errors { get; set; }

        public string Render()
        {
            Header_Template headerTemplate = new Header_Template();
            Footer_Template footerTemplate = new Footer_Template();
            RightMenu_Template rightMenuTemplate = new RightMenu_Template(this.user);
            LeftMenu_Template leftMenu_Template = new LeftMenu_Template(false);


            string content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/main_layout.html");

            content = content.Replace("__HEADER__", headerTemplate.Render());
            content = content.Replace("__FOOTER__", footerTemplate.Render());
            content = content.Replace("__RIGHT_MENU__", rightMenuTemplate.Render());
            content = content.Replace("__LEFT_MENU__", leftMenu_Template.Render());
            content = content.Replace("__CONTENT__", File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/register.html"));

            string error_data = string.Empty;

            foreach (Error error in Errors)
            {
                Error_Template error_template = new Error_Template(error);
                error_data += error_template.Render();
            }

            content = content.Replace("__Error__", error_data);

            return content;
        }
    }
}
