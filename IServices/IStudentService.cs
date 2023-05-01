using Students.Models;

namespace Students.IServices
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        string AddStudent(Student student);
        string UpdateStudent(string name, Student student);
        string DeleteStudent(string name);
        double GetGirlsOrBoysAverage(string maleorfemale, string course);
        List<StudentAverage> GetAllStudentsAverage();
        string GetBestWorstStudent();
        List<TeacherAverage> GetBestWorstTeacher();
        string GetFailedStudents(string course);
        string GetRepeatStudents();
        public string GetMostFailedTeacher();
    }
}