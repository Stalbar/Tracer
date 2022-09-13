using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Serialization.Abstractions.Classes
{
    public class MethodInfo
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public long Time { get; set; }
        public List<MethodInfo> Methods { get; set; }

        public MethodInfo(string name, string @class, long time, List<MethodInfo> methods)
        {
            Name = name;
            Class = @class;
            Time = time;
            Methods = methods;
        }
    }
}
