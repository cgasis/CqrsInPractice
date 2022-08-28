using CSharpFunctionalExtensions;
using Logic.Students.Commands;
using Logic.Utils;

namespace Logic.Students
{
    public sealed class UnregisterCommandHandler : ICommandHandler<UnregisterCommand>
    {
        private readonly StudentRepository _studentRepository;
        private readonly UnitOfWork _unitOfWork;

        public UnregisterCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = new StudentRepository(_unitOfWork);
        }

        public Result Handle(UnregisterCommand command)
        {
            Student student = _studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            _studentRepository.Delete(student);
            _unitOfWork.Commit();

            return Result.Ok();
        }
    }


}
