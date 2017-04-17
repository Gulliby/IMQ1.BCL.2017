using IMQ1.BCL._2017.Infrastructure.Reader;
using IMQ1.BCL._2017.Infrastructure.Writer;
using IMQ1.BCL._2017.RegularExpression.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMQ1.BCL._2017.RegularExpression.Executor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Unidecoder.DecodeData(new List<string> { "Test", "some.txt" },
                new ConsoleWriter(), 
                new UnidecoderCustomSpeaker(new ConsoleReader(), new ConsoleWriter()));

            Console.ReadKey();
        }
    }
}
