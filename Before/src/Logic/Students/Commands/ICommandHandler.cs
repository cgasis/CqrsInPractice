using CSharpFunctionalExtensions;

namespace Logic.Students.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }


}
