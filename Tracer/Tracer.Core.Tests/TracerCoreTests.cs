using Tracer.Core.Interfaces;
namespace Tracer.Core.Tests
{
    [TestClass]
    public class TracerCoreTests
    {
        [TestMethod]
        public void Tracer_WithTwoMethods()
        {
            ITracer tracer = new Tracer();
            Method1 m1 = new(tracer);
            Method2 m2 = new(tracer);
            m1.InnerMethod();
            m2.InnerMethod();
            Assert.AreEqual(2, tracer.GetTraceResult().MethodNames.Count);
        }

        [TestMethod]
        public void Tracer_WithTwoThreads()
        {
            ITracer tracer = new Tracer();
            Method1 m1 = new(tracer);
            Method2 m2 = new(tracer);
            Thread thread = new Thread(new ThreadStart(m1.InnerMethod));
            thread.Start();
            m2.InnerMethod();
            Thread.Sleep(500);
            var res = tracer.GetTraceResult();
            Assert.AreEqual(2, new HashSet<int>(res.CurrentThreadsIds).Count);
        }

        [TestMethod]
        public void Tracer_WithNestedMethod()
        {
            ITracer tracer = new Tracer();
            Method1 m1 = new Method1(tracer);
            Method3 m3 = new Method3(tracer);
            m1.InnerMethod();
            m3.InnerMethod();
            var res = tracer.GetTraceResult();
            Assert.AreEqual(3, res.MethodNames.Count);
        }

        [TestMethod]
        public void Tracer_TestTime()
        {
            ITracer tracer = new Tracer();
            int waitTime = 100;
            Method4 m4 = new Method4(tracer, waitTime);
            m4.InnerMethod();
            var res = tracer.GetTraceResult();
            Assert.IsTrue(res.ElapsedTimeInMilliseconds[0] >= waitTime);
        }
    }

    public class Method1
    {
        private ITracer _tracer;

        public Method1(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
        }
    }

    public class Method2
    {
        private ITracer _tracer;

        public Method2(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
        }
    }

    public class Method3
    {
        private ITracer _tracer;
        private Method2 _method2;
        public Method3(ITracer tracer)
        {
            _tracer = tracer;
            _method2 = new Method2(_tracer);
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            _method2.InnerMethod();
            _tracer.StopTrace();
        }
    }

    public class Method4
    {
        private ITracer _tracer;
        private int waitTime;
        public Method4(ITracer tracer, int waitTime)
        {
            _tracer = tracer;
            this.waitTime = waitTime;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(waitTime);
            _tracer.StopTrace();
        }
    }
}