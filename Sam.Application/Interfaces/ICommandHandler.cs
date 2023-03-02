namespace Ddd.Application.Interfaces;

public interface ICommandHandler<in TCommand> where TCommand : ICommand {
    int Handle(TCommand command);
}
