using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Logger
    {
        private readonly string _format;
        public Logger(string TimeFormat)
        {
            _format = TimeFormat;
        }

        public void Log(string msg)
        {
            Console.WriteLine($"{DateTime.Now.ToString(_format)}:{msg}");
        }
    }
}
