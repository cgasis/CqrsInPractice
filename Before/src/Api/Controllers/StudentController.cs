using Logic.Dtos;
using Logic.Students;
using Logic.Students.Commands;
using Logic.Students.Query;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/students")]
    public sealed class StudentController : BaseController
    {
        private readonly Messages _message;

        public StudentController(UnitOfWork unitOfWork, Messages message)
        {
            _message = message;
        }

        [HttpGet]
        public IActionResult GetList(string enrolled, int? number)
        {
            var result = _message.Dispatch<List<StudentDto>>(new ListStudentsQuery(enrolled, number));
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Register([FromBody] NewStudentDto dto)
        {
            var command = new RegisterCommand(dto.Name, dto.Email, dto.Course1, dto.Course1Grade, dto.Course1DisenrollmentComment, dto.Course2, dto.Course2Grade, dto.Course2DisenrollmentComment);
            var result = _message.Dispatch(command);

            return FromResult(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Unregister(long id)
        {
            var command = new UnregisterCommand(id);
            var result = _message.Dispatch(command);

            return FromResult(result);
        }

        [HttpPut("{id}/enrollment")]
        public IActionResult Enroll(long id, [FromBody] StudentEnrollDto dto)
        {
            var command = new EnrollmentCommand(id, dto.Course, dto.Grade);
            var result = _message.Dispatch(command);

            return FromResult(result);
        }

        [HttpPut("{id}/enrollments/{enrollmentNumber}")]
        public IActionResult Transfer(long id, int enrollmentNumber, [FromBody] StudentTrasferDto dto)
        {
            var command = new TransferCommand(id, enrollmentNumber, dto.Course, dto.Grade);
            var result = _message.Dispatch(command);

            return FromResult(result);
        }

        [HttpPost("{id}/enrollments/{enrollmentNumber}/deletion")]
        public IActionResult Disenroll(long id, int enrollmentNumber, [FromBody] StudentDisenrollmentDto dto)
        {
            var command = new DisenrollCommand(id, enrollmentNumber, dto.Course, dto.Grade, dto.Comment);
            var result = _message.Dispatch(command);

            return FromResult(result);
        }

        [HttpPut("{id}/enrollment/editpersonalinfo")]
        public IActionResult EditPersonalInfo(long id, [FromBody] StudentPersonalInfoDto dto)
        {
            var command = new EditPersonalInfoCommand(id, dto.Name, dto.Email);
            var result = _message.Dispatch(command);

            return FromResult(result);
        }
    }
}
