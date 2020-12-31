using LinQDemo.Data;

namespace LinQDemo.Service
{
    public class ServiceBase
    {
        protected readonly LinQDemoContext Context;

        public ServiceBase(LinQDemoContext context)
        {
            Context = context;
        }
    }
}
