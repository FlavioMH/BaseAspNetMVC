using System;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Microsoft.Practices.Unity;

namespace BaseAspNetMvc.Structure.UnityDependencyInjection
{
    public class PerCallContextOrRequestLifeTimeManager : LifetimeManager, IDisposable
    {
        private string _key = string.Format("PerCallContextOrRequestLifeTimeManager_{0}", Guid.NewGuid());

        public override object GetValue()
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Items[_key];
            else
                return CallContext.GetData(_key);
        }

        public override void SetValue(object newValue)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items[_key] = newValue;
            else
                CallContext.SetData(_key, newValue);
        }

        public override void RemoveValue()
        {
            var obj = GetValue();
            HttpContext.Current.Items.Remove(obj);
        }

        public void Dispose()
        {
            this.RemoveValue();
        }
    }
}
