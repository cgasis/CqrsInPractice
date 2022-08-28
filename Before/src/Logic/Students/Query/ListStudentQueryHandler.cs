using CSharpFunctionalExtensions;
using Logic.Dtos;
using Logic.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Students.Query
{
    public sealed class ListStudentQueryHandler : IQueryHandler<ListStudentsQuery, List<StudentDto>>
    {
        private readonly StudentRepository _studentRepository;
        private readonly UnitOfWork _unitOfWork;

        public ListStudentQueryHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<StudentDto> Handle(ListStudentsQuery query)
        {
            return new StudentRepository(_unitOfWork)
                    .GetList(query.enrolled, query.number)
                    .Select(x => ConvertToDto(x))
                    .ToList();
        }

        private StudentDto ConvertToDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Course1 = student.FirstEnrollment?.Course?.Name,
                Course1Grade = student.FirstEnrollment?.Grade.ToString(),
                Course1Credits = student.FirstEnrollment?.Course?.Credits,
                Course2 = student.SecondEnrollment?.Course?.Name,
                Course2Grade = student.SecondEnrollment?.Grade.ToString(),
                Course2Credits = student.SecondEnrollment?.Course?.Credits,
            };
        }
    }


}
