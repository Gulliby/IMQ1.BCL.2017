using System;
using System.Collections.Generic;
using System.Text;
using IMQ1.BCL._2017.FileSorter.Library;
using IMQ1.BCL._2017.FileSorter.Library.Entities;

namespace IMQ1.BCL._2017.FileSorter.Executor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var rules = new List<Rule>()
            {
                new Rule
                {
                    Patter = "ilya.txt",
                    Destination = "mainFolder"
                },
                new Rule
                {
                    Patter = "ilya2.txt",
                    Destination = "mainFolder2"
                }
            };
            var engine = new Engine(rules, new List<string>{ @"Test" });
            engine.StartWatch();
            Console.ReadKey();
        }
    }
}
