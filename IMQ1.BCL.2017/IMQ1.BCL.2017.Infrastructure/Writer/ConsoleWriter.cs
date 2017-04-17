using IMQ1.BCL._2017.Infrastructure.Writer.Interface;
using System;

namespace IMQ1.BCL._2017.Infrastructure.Writer
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message ?? string.Empty);
        }
    }
}
