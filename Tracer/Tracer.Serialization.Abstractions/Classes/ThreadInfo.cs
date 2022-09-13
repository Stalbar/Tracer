using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Serialization.Abstractions.Classes
{
    public class ThreadInfo
    {
        public int Id { get; set; }
        public long Time { get; set; }

        public List<MethodInfo> Methods { get; set; }

        public ThreadInfo(int id, long time, List<MethodInfo> methods)
        {
            Id = id;
            Time = time;
            Methods = methods;
        }
    }
}
