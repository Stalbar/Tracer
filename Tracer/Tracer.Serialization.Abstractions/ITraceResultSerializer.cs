using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Serialization.Abstractions.Classes;

namespace Tracer.Serialization.Abstractions
{
    public interface ITraceResultsSerializer
    {
        void Serialize(List<ThreadInfo> results, Stream to);
    }
}
