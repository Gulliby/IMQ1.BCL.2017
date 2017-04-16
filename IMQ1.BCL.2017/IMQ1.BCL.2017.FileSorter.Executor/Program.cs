using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMQ1.BCL._2017.FileSorter.Library;
using IMQ1.BCL._2017.FileSorter.Library.Entities;

namespace IMQ1.BCL._2017.FileSorter.Executor
{
    class Program
    {
        static void Main(string[] args)
        {
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
            var engine = new Engine(rules, new List<string>{ @"D:\Test" });
            engine.StartWatch();
            while (Console.Read() != 'q')
            {
            }
        }

        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }
    }
}
