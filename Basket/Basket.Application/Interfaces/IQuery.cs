using MediatR;

namespace Ddd.Application.Interfaces;

public interface IQuery : IRequest { }
public interface IQuery<TResponse> : IRequest<TResponse> { }
