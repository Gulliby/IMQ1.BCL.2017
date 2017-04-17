using IMQ1.BCL._2017.Infrastructure.Reader.Interface;
using System;

namespace IMQ1.BCL._2017.Infrastructure.Reader
{
    public class ConsoleReader : IReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
