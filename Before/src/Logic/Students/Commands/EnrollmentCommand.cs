﻿namespace Logic.Students.Commands
{
    public sealed class EnrollmentCommand : ICommand
    {
        public long Id { get; }
        public string Course { get; }
        public string Grade { get; }

        public EnrollmentCommand(long id, string course, string grade)
        {
            Id = id;
            Course = course;
            Grade = grade;
        }
    }


}
