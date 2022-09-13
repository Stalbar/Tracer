using Tracer.Core.Interfaces;
using Tracer.Core.Structs;
using Tracer.Core;
namespace Tracer.Example
{
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();

            _bar.InnerMethod();

            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
        }
    }

    public class C
    {
        private ITracer _tracer;

        public C(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void M0()
        {
            M1();
            M2();
        }

        private void M1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        private void M2()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _tracer.StopTrace();
        }
    }

    public class Example
    {
        static void Main(string[] args)
        {
            ITracer tracer = new Core.Tracer();
            C c = new(tracer);
            Thread thread = new(new ThreadStart(c.M0));
            thread.Start();
            Foo foo = new(tracer);
            foo.MyMethod();
            Thread.Sleep(500); 
            TraceResult traceResult = tracer.GetTraceResult();
            TraceResultsParser parser = new();
            var hierarchy = parser.GetMethodsAndThreadsHierarchy(traceResult);
        }
    }
}