using ServiceBusRelay.Contract;
using System;

namespace ServiceBusRelay.Server
{
    class ConsoleService : IConsoleService
    {
        public void Write(string text)
        {
            Console.WriteLine(text); ;
        }
    }
}
