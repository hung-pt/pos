using MediatR;

namespace Catalog.Application.Interfaces;

public interface ICommand : IRequest { }
public interface ICommand<TResponse> : IRequest<TResponse> { }
