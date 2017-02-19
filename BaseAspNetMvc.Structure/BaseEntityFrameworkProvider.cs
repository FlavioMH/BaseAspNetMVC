using log4net;
using System;
using System.Data.Entity;

namespace BaseAspNetMvc.Structure
{
    public class BaseEntityFrameworkProvider<TEntitiesType> : IDisposable where TEntitiesType : new()
    {
        protected TEntitiesType DbContext { get; private set; }
        protected readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected bool IsDisposed = false;

        public BaseEntityFrameworkProvider(TEntitiesType dbContext)
        {
            if (dbContext != null)
            {
                this.DbContext = new TEntitiesType();
            }

            if (dbContext is DbContext)
                (dbContext as DbContext).Database.Log += Logger;
        }

        private void Logger(string obj)
        {
            System.Diagnostics.Debug.WriteLine(obj);
            Log.DebugFormat("EF LOGGER : {0}", obj);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (!disposing || DbContext == null)
                return;

            if (!(DbContext is IDisposable))
                return;

            (DbContext as IDisposable).Dispose();
            GC.SuppressFinalize(DbContext);
            IsDisposed = true;
        }
    }
}
