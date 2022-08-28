using CSharpFunctionalExtensions;
using Logic.Utils;
using System;

namespace Logic.Students.Commands
{
    public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;
        private readonly UnitOfWork _unitOfWork;

        public RegisterCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = new StudentRepository(_unitOfWork);
            _courseRepository = new CourseRepository(_unitOfWork);
        }

        public Result Handle(RegisterCommand command)
        {
            var student = new Student(command.Name, command.Email);

            if (command.Course1 != null && command.Course1Grade != null)
            {
                Course course = _courseRepository.GetByName(command.Course1);
                student.Enroll(course, Enum.Parse<Grade>(command.Course1Grade));
            }
            else
            {
                return Result.Fail("Course1 and Course1Grade is null");
            }

            if (command.Course2 != null && command.Course2Grade != null)
            {
                Course course = _courseRepository.GetByName(command.Course2);
                student.Enroll(course, Enum.Parse<Grade>(command.Course2Grade));
            }
            else
            {
                return Result.Fail("Course2 and Course2Grade is null");
            }

            _studentRepository.Save(student);
            _unitOfWork.Commit();

            return Result.Ok();
        }
    }
}
