using Microsoft.Practices.Unity;
using System;

namespace BaseAspNetMvc.Structure.UnityDependencyInjection
{
    public sealed class UnityContainerRepository
    {
        private static volatile IUnityContainer _container;// = new UnityContainer();
        private static object syncRoot = new Object();

        public static UnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (syncRoot)
                    {
                        if (_container == null)
                        {
                            _container = new UnityContainer();
                        }
                    }
                }
                return (UnityContainer)_container;
            }
        }
    }
}
