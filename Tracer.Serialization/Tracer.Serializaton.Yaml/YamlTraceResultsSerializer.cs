using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Serialization.Abstractions;
using Tracer.Serialization.Abstractions.Classes;

namespace Tracer.Serializaton.Yaml
{
    public class YamlTraceResultsSerializer : ITraceResultsSerializer
    {
        public void Serialize(List<ThreadInfo> threads, Stream to)
        {
            StringBuilder sb = new();
            sb.AppendLine("threads: ");
            foreach (var thread in threads)
            {
                sb.AppendLine($"  - id: {thread.Id}");
                sb.AppendLine($"\ttime: {thread.Time}ms");
                sb.AppendLine($"\tmethods:");
                sb.Append(SerializeMethods(thread.Methods, "\t"));
            }
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
                sb.AppendLine($"{offset}  - name: {method.Name}");
                sb.AppendLine($"{offset}\tclass: {method.Class}");
                sb.AppendLine($"{offset}\ttime: {method.Time}ms");
                sb.Append(SerializeMethods(method.Methods, offset + "\t"));
            }
            return sb.ToString();
        }
    }
}
