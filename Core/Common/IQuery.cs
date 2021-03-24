using MediatR;

namespace Core.Common
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}