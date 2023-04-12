using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinVideoEditor
{
    public class Utils
    {
        public static void Log(string message)
        {
            string path = "./log.txt";
            using (StreamWriter writer = new StreamWriter(path))
            {
               writer.WriteLine(message);
            }
        }
    }
}
