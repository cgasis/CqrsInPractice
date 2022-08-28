using CSharpFunctionalExtensions;
using Logic.Utils;

namespace Logic.Students.Commands
{
    public sealed class DisenrollCommandHandler : ICommandHandler<DisenrollCommand>
    {
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;
        private readonly UnitOfWork _unitOfWork;

        public DisenrollCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = new StudentRepository(_unitOfWork);
            _courseRepository = new CourseRepository(_unitOfWork);
        }
        public Result Handle(DisenrollCommand command)
        {
            Student student = _studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            if (string.IsNullOrWhiteSpace(command.Comment))
                return Result.Fail($"Disenrollment comment is required");

            Enrollment enrollment = student.GetEnrollment(command.enrollmentNumber);
            if (enrollment == null)
                return Result.Fail($"No enrollment found with number '{command.enrollmentNumber}'");

            student.RemoveEnrollment(enrollment, command.Comment);

            return Result.Ok();
        }
    }


}
