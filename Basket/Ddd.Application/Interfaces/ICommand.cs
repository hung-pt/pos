using MediatR;

namespace Ddd.Application.Interfaces;

public interface ICommand : IRequest { }
public interface ICommand<TResponse> : IRequest<TResponse> { }
