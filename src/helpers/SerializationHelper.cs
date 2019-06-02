using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.helpers
{
    public static class SerializationHelper
    {

        public static string SerializeToJSON(object obj)
        {

            string result = JsonConvert.SerializeObject(obj);


            return result;
        }
    }
}
