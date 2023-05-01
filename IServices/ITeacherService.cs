using Students.Models;

namespace Students.IServices
{
    public interface ITeacherService
    {
        List<Teacher> GetTeachers();
        string AddTeacher(Teacher teacher);
        string UpdateTeacher(string name, Teacher teacher);
        string DeleteTeacher(string name);
        string GetBestWorstTeacher();
        public string GetMostFailedTeacher();
    }
}