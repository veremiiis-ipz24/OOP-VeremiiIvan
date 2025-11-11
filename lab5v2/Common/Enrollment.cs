namespace Domain
{
    public class Enrollment
    {
        public int Id { get; }
        public string StudentName { get; }
        public double Grade { get; }
        public int CourseId { get; }

        public Enrollment(int id, string studentName, double grade, int courseId)
        {
            Id = id;
            StudentName = studentName;
            Grade = grade;
            CourseId = courseId;
        }
    }
}

