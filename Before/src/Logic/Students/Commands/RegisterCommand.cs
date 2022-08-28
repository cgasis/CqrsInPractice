namespace Logic.Students.Commands
{
    public sealed class RegisterCommand : ICommand
    {


        public string Name { get; }
        public string Email { get; }
        public string Course1 { get; }
        public string Course1Grade { get; }
        public string Course1DisenrollmentComment { get; }
        public string Course2 { get; }
        public string Course2Grade { get; }
        public string Course2DisenrollmentComment { get; }

        public RegisterCommand(string name, string email, string course1, string course1Grade, string course1DisenrollmentComment, string course2, string course2Grade, string course2DisenrollmentComment)
        {
            this.Name = name;
            Email = email;
            Course1 = course1;
            Course1Grade = course1Grade;
            Course1DisenrollmentComment = course1DisenrollmentComment;
            Course2 = course2;
            Course2Grade = course2Grade;
            Course2DisenrollmentComment = course2DisenrollmentComment;
        }
    }
}
