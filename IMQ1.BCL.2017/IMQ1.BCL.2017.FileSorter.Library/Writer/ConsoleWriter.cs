using System;
using IMQ1.BCL._2017.FileSorter.Library.Writer.Interface;

namespace IMQ1.BCL._2017.FileSorter.Library.Writer
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message ?? string.Empty);
        }
    }
}
