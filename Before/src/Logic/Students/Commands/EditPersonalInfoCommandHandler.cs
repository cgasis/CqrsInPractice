using CSharpFunctionalExtensions;
using Logic.Utils;

namespace Logic.Students.Commands
{
    public sealed class EditPersonalInfoCommandHandler : ICommandHandler<EditPersonalInfoCommand>
    {
        private readonly StudentRepository _studentRepository;
        private readonly UnitOfWork _unitOfWork;

        public EditPersonalInfoCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = new StudentRepository(unitOfWork);
        }

        public Result Handle(EditPersonalInfoCommand command)
        {

            // there gows the implementation
            Student student = _studentRepository.GetById(command.Id);

            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            if (string.IsNullOrEmpty(command.Name))
                return Result.Fail($"Name is required");

            if (string.IsNullOrEmpty(command.Email))
                return Result.Fail($"Email is required");

            student.Name = command.Name;
            student.Email = command.Email;

            _unitOfWork.Commit();

            return Result.Ok();

        }
    }


}
