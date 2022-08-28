namespace Logic.Students.Commands
{
    public sealed class DisenrollCommand : ICommand
    {
        public long Id { get; }
        public int enrollmentNumber { get; }
        public string Course { get; set; }
        public string Grade { get; }
        public string Comment { get; set; }

        public DisenrollCommand(long id, int enrollmentNumber, string course, string grade, string comment)
        {
            Id = id;
            this.enrollmentNumber = enrollmentNumber;
            Course = course;
            Grade = grade;
            Comment = comment;
        }
    }
}
