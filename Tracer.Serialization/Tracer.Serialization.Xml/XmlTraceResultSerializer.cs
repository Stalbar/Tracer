using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Serialization.Abstractions;
using Tracer.Serialization.Abstractions.Classes;

namespace Tracer.Serialization.Xml
{
    public class XmlTraceResultsSerializer : ITraceResultsSerializer
    {
        public void Serialize(List<ThreadInfo> threads, Stream to)
        {
            StringBuilder sb = new();
            sb.AppendLine("<root>");
            foreach (var thread in threads)
            {
                sb.AppendLine($"\t<thread id =\"{thread.Id}\" time=\"{thread.Time}ms>\"");
                sb.Append(SerializeMethods(thread.Methods, "\t\t"));
                sb.AppendLine($"\t</thread>");
            }
            sb.AppendLine("</root>");
            using (StreamWriter writer = new(to))
            {
                writer.WriteLine(sb.ToString());
            }
        }

        private string SerializeMethods(List<MethodInfo> methods, string offset)
        {
            StringBuilder sb = new();
            foreach (var method in methods)
            {
                sb.AppendLine($"{offset}<method name=\"{method.Name}\" time=\"{method.Time}ms\" class=\"{method.Class}\">");
                sb.Append(SerializeMethods(method.Methods, offset + "\t"));
                sb.AppendLine($"{offset}</method>");
            }
            return sb.ToString();
        }
    }
}
