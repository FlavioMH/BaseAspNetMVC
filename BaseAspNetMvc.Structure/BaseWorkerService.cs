using log4net;
using System;

namespace BaseAspNetMvc.Structure
{
    public class BaseWorkerService
    {
        protected readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }

    public class BaseWorkerService<T> : IDisposable
    {
        protected T Provider { get; private set; }
        protected readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected bool IsDisposed = false;

        public BaseWorkerService(T provider)
        {
            if (provider != null)
                this.Provider = provider;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
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
