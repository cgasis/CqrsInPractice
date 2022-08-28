using CSharpFunctionalExtensions;
using Logic.Utils;
using System;

namespace Logic.Students.Commands
{
    public sealed class TransferCommandHandler : ICommandHandler<TransferCommand>
    {
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;
        private readonly UnitOfWork _unitOfWork;

        public TransferCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = new StudentRepository(_unitOfWork);
            _courseRepository = new CourseRepository(_unitOfWork);
        }

        public Result Handle(TransferCommand command)
        {
            Student student = _studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            Course course = _courseRepository.GetByName(command.Course);
            if (course == null)
                return Result.Fail($"Course is incorrect: {course}");

            bool success = Enum.TryParse(command.Grade, out Grade grade);
            if (!success)
                return Result.Fail($"Grade is incorrect: '{command.Grade}'");

            Enrollment enrollment = student.GetEnrollment(command.enrollmentNumber);
            if (enrollment == null)
                return Result.Fail($"No enrollment found with number '{command.enrollmentNumber}'");

            enrollment.Update(course, grade);

            _unitOfWork.Commit();

            return Result.Ok();
        }
    }


}
