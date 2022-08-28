using Logic.Students.Commands;

namespace Logic.Students
{
    public sealed class UnregisterCommand : ICommand
    {
        public long Id { get; }

        public UnregisterCommand(long id)
        {
            Id = id;
        }
    }


}
