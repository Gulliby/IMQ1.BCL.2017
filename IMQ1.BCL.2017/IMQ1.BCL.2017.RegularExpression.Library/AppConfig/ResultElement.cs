using System.Configuration;

namespace IMQ1.BCL._2017.RegularExpression.Library.AppConfig
{
    public class ResultElement: ConfigurationElement
    {
        [ConfigurationProperty("format", IsRequired = true)]
        public string Format
        {
            get
            {
                return (string)this["format"];
            }
            set
            {
                this["format"] = value;
            }
        }
    }
}
