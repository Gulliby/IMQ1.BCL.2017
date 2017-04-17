using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMQ1.BCL._2017.Infrastructure.Speakers.Interface
{
    public interface ISpeaker
    {
        void Speak<T>(T data);
    }
}
