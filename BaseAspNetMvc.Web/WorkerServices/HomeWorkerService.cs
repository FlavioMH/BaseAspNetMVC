using BaseAspNetMvc.Structure.WorkerServices;
using System;

namespace BaseAspNetMvc.Web.WorkerServices
{
    public interface IHomeWorkerService
    {
        void Test();
    }

    public class HomeWorkerService : BaseWorkerService, IHomeWorkerService
    {
        public void Test()
        {
            throw new NotImplementedException();
        }
    }
}
