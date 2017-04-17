using System.Configuration;

namespace IMQ1.BCL._2017.RegularExpression.Library.AppConfig
{
    public class FormatSection : ConfigurationSection
    {
        [ConfigurationProperty("file")]
        public FileElement File
        {
            get
            {
                return (FileElement)this["file"];
            }
            set
            { this["file"] = value; }
        }

        [ConfigurationProperty("substring")]
        public SubstringElement Substring
        {
            get
            {
                return (SubstringElement)this["substring"];
            }
            set
            { this["substring"] = value; }
        }

        [ConfigurationProperty("result")]
        public ResultElement Result
        {
            get
            {
                return (ResultElement)this["result"];
            }
            set
            { this["result"] = value; }
        }
    }
}
