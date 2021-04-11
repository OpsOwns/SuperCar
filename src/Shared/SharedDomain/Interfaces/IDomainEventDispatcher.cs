using System.Threading.Tasks;

namespace SuperCar.Shared.Domain.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(params IDomainEvent[] events);
    }
}