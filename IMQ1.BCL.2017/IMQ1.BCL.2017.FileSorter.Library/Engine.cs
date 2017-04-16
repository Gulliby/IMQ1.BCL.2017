using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMQ1.BCL._2017.FileSorter.Library.Entities;
using IMQ1.BCL._2017.FileSorter.Library.Entities.Interface;
using IMQ1.BCL._2017.FileSorter.Library.Writer;
using IMQ1.BCL._2017.FileSorter.Library.Writer.Interface;

namespace IMQ1.BCL._2017.FileSorter.Library
{
    public class Engine
    {
        private readonly IRule _defaultRule = new Rule {Destination = "default"};
        private readonly IWriter _writer;
        private readonly IEnumerable<IRule> _rules;
        private readonly IEnumerable<string> _folders;

        private readonly  IList<FileSystemWatcher> _watchers;

        public Engine(IEnumerable<Rule> rules, IEnumerable<string> folders) : this(rules, folders, new ConsoleWriter())
        { 
        }

        public Engine(IEnumerable<Rule> rules, IEnumerable<string> folders, IWriter writer)
        {
            if (folders == null)
            {
                throw new ArgumentNullException($"{nameof(folders)} can't be null");
            }

            if (writer == null)
            {
                throw new ArgumentNullException($"{nameof(writer)} can't be null");
            }

            _rules = rules;
            _folders = folders;
            _writer = writer;

            _watchers = new List<FileSystemWatcher>();
        }

        public void StartWatch()
        {
            foreach (var folder in _folders)
            {
                var fsw = new FileSystemWatcher {Path = folder};
                fsw.Created += OnCreated;
                fsw.EnableRaisingEvents = true;
                _watchers.Add(fsw);
            }
            Console.ReadKey();
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            _writer.Write($"File: {e.Name} was created. Date: {File.GetCreationTime(e.FullPath)}");
            MoveFile(e.FullPath, e.Name);
        }

        private void MoveFile(string filePath, string name)
        {
            foreach (var rule in _rules)
            {
                if (name != rule.Patter) continue;
                _writer.Write($"Rule with pattern {rule.Patter} and destination {rule.Destination} was founded!");
                MoveFile(filePath, name, rule);
                return;
            }
            _writer.Write("Rule with pattern was not founded!");
            MoveFile(filePath, name, _defaultRule);
        }

        private void MoveFile(string filePath, string name, IRule rule)
        {
            Directory.CreateDirectory(rule.Destination);
            if (!File.Exists(rule.Destination + "//" + name))
            {
                try
                {
                    File.Move(filePath, rule.Destination + "//" + name);
                }
                catch (Exception ex)
                {
                    _writer.Write(ex.Message);
                }
            }
            else
            {
                _writer.Write($"File with {name} already exist");
            }
        }
    }
}
