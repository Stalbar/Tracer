using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Core.Structs;
using Tracer.Core.Interfaces;
using System.Diagnostics;
using System.Reflection;

namespace Tracer.Core
{
    public class Tracer : ITracer
    {
        private Stopwatch _stopwatch = new();
        private TraceResult _result = new();
        public TraceResult GetTraceResult() => _result;

        public void StartTrace()
        {
            _stopwatch.Start();
        }

        public void StopTrace()
        {
            _stopwatch.Stop();
            StackTrace st = new();
            StackFrame stackFrame = st.GetFrame(1);
            MethodBase mb = stackFrame.GetMethod();
            _result.AddResult(mb.Name, mb.DeclaringType.Name, st.GetFrame(2).GetMethod().Name, _stopwatch.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId);
            _stopwatch.Reset();
            _stopwatch.Start();
        }
    }
}
