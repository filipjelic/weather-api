using MediatR;

namespace Weather.Application.Abstraction;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}