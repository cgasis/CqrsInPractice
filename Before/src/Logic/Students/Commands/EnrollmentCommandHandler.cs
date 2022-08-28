using CSharpFunctionalExtensions;
using Logic.Utils;
using System;

namespace Logic.Students.Commands
{
    public sealed class EnrollmentCommandHandler : ICommandHandler<EnrollmentCommand>
    {
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;
        private readonly UnitOfWork _unitOfWork;

        public EnrollmentCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = new StudentRepository(_unitOfWork);
            _courseRepository = new CourseRepository(_unitOfWork);
        }

        public Result Handle(EnrollmentCommand command)
        {
            Student student = _studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            if (string.IsNullOrWhiteSpace(command.Grade))
                return Result.Fail("Grade is required");

            bool success = Enum.TryParse(command.Grade, out Grade grade);
            if (!success)
                return Result.Fail($"Grade is incorrect: '{command.Grade}'");

            Course course = _courseRepository.GetByName(command.Course);
            if (course == null)
                return Result.Fail("Course is null");

            student.Enroll(course, grade);

            _unitOfWork.Commit();

            return Result.Ok();
        }
    }


}
