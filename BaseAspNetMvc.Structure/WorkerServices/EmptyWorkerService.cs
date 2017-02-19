using BaseAspNetMvc.Structure.Providers;

namespace BaseAspNetMvc.Structure.WorkerServices
{
    public class EmptyWorkerService : BaseWorkerService<IEmptyProvider>
    {
        public EmptyWorkerService(IEmptyProvider provider)
            : base(provider)
        {
        }
    }
}
