using HttpServer.websites.exalted.helpers;
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
    class UserController : IController
    {
        Dictionary<string, Action> methods = new Dictionary<string, Action>();
        private string[] args;
        private Client client;
        private HttpListenerContext context;

        public UserController()
        {
            methods.Add("logout", new Action(Logout));
        }

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

            this.client = client;
            this.context = context;

            if (args.Length <= 0)
            {
                context.Send("{ \"id\" : \"" + user.Id + "\", \"name\" : \"" + user.Username + "\" }");
                return true;
            }

            this.args = args;

            if (methods.ContainsKey(args[0]))
            {
                methods[args[0]].Invoke();
                return true;
            }            

            return true;


        }

        

        public void Logout()
        {
            if (UserManager.Logout(this.client))
            {
                this.context.Redirect("../login");
            }
        }
    }
}
