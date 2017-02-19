using log4net;
using System;

namespace BaseAspNetMvc.Structure.WorkerServices
{
    public class BaseWorkerService : IDisposable
    {
        protected bool IsDisposed = false;
        protected readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (!disposing)
                return;         
        }
    }

    public class BaseWorkerService<T> : BaseWorkerService, IDisposable
    {
        protected T Provider { get; private set; }   

        public BaseWorkerService(T provider)
        {
            if (provider != null)
                this.Provider = provider;
        }

        public override void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (!disposing)
                return;

            if (!(Provider is IDisposable))
                return;

            (Provider as IDisposable).Dispose();
            IsDisposed = true;
        }
    }
}
