using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpServer.websites.exalted.controllers
{
    public class FileProvider : IController
    {
        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            List<string> segments = context.Request.Url.Segments.ToList();
            segments.RemoveAt(0);
            string path = Path.Combine(segments.ToArray());

            // Remote file disclosure
            path = WebSite.WEBSITE_ROOT_PATH + "public/" + path;
            if (File.Exists(path))
            {
                context.Response.ContentType = MimeMapping.GetMimeMapping(path);

                byte[] buffer = File.ReadAllBytes(path);
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Flush();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
