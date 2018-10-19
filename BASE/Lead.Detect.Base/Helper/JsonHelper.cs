using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fastJSON;

namespace Lead.Detect.Helper
{
    public class JsonHelper
    {

        public static T Load<T>(string file)
        {
            return JSON.ToObject<T>(File.ReadAllText(file), new JSONParameters() { UseExtensions = false });
        }

        public static void Save(object obj, string file)
        {
            var json = JSON.ToNiceJSON(obj, new JSONParameters() { UseExtensions = false });
            using (var fs = new FileStream(file, FileMode.Create))
            {
                var buffer = Encoding.ASCII.GetBytes(json);
                fs.Write(buffer, 0, buffer.Length);
            }
        }

    }
}
