using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Core.Structs
{
    public struct TraceResult
    {
        public List<string> MethodNames;
        public List<string> ClassNames;
        public List<string> ParentMethodsNames;
        public List<long> ElapsedTimeInMilliseconds;
        public List<int> CurrentThreadsIds;

        public TraceResult()
        {
            MethodNames = new List<string>();
            ClassNames = new List<string>();
            ParentMethodsNames = new List<string>();
            ElapsedTimeInMilliseconds = new List<long>();
            CurrentThreadsIds = new List<int>();
        }

        public void AddResult(string methodName, string className, string parentMethodName, long elapsedMilliseconds, int currentThreadId)
        {
            MethodNames.Add(methodName);
            ClassNames.Add(className);
            ParentMethodsNames.Add(parentMethodName);
            ElapsedTimeInMilliseconds.Add(elapsedMilliseconds);
            CurrentThreadsIds.Add(currentThreadId);
        }
    }
}
