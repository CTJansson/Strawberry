using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BrawlServer
{
    class Program
    {
        private List<Character> Characters;

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Run(args);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

        }

        private static void Run(string[] args)
        {

        }
    }
}
