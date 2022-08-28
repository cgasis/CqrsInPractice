namespace Logic.Students.Query
{
    public sealed class ListStudentsQuery : IQuery
    {
        public string enrolled { get; }
        public int? number { get; }

        public ListStudentsQuery(string enrolled, int? number)
        {
            this.enrolled = enrolled;
            this.number = number;
        }
    }


}
