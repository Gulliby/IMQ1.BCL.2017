using System;
using System.Collections.Generic;
using System.Linq;
using IMQ1.BCL._2017.FileSorter.Library.Entities.Interface;

namespace IMQ1.BCL._2017.FileSorter.Library.Entities
{
    public class Rule : IRule
    {
        public string Destination { get; set; }

        public string Patter { get; set; }
    }
}
