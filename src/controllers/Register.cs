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
    public class Register : IController
    {
        private HttpListenerContext context;

        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            this.context = context;

            if (client.Connected())
            {
                context.Redirect("../home");
                return true;
            }

            if (context.GetMethod() == HttpMethod.GET)
            {
                this.ShowRegister(client);
            }
            else if (context.GetMethod() == HttpMethod.POST)
            {
                Dictionary<string, string> requestData = context.GetData();

                if (requestData.ContainsKey("username") && requestData.ContainsKey("password"))
                {
                    string username = requestData["username"];
                    string password = PasswordManager.Hash(requestData["password"]);

                    User user = UserManager.CreateUser(username, password);
                    if (user != null)
                    {
                        if (UserManager.Login(username, requestData["password"], client))
                        {
                            context.Redirect("../home");
                            return true;
                        }
                    }
                    else
                    {
                        this.ShowRegister(client, new Error("Couldn't create the user!"));
                        return false;
                    }
                }

                this.ShowRegister(client, new Error("Something wrong happened there!"));
            }

            return true;
        }

        private void ShowRegister(Client client, Error error = null)
        {
            if (client == null)
            {
                return;
            }

            HttpServer.websites.exalted.model.User user = UserManager.GetUser(UserManager.GetUserID(client));

            Register_Template registerTemplate = new Register_Template(user);
            registerTemplate.Errors = new Error[] { error };
            context.Send(registerTemplate.Render());
        }
    }
}
