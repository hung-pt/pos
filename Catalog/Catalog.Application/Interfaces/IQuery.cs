using MediatR;

namespace Catalog.Application.Interfaces;

public interface IQuery : IRequest { }
public interface IQuery<TResponse> : IRequest<TResponse> { }
