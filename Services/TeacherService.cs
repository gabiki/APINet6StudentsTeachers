using Microsoft.AspNetCore.Mvc;
using Students.Controllers;
using Students.IServices;
using Students.Models;

namespace Students.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IStudentService studentService;
        private static List<Student> studentList;
        public TeacherService(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        private List<Teacher> teachersList = new List<Teacher>
            {
            new Teacher { firstName = "Juan", lastName = "Gomez", course = "mathematics", givesOnlineClass = false, phoneNumber = "123543957"},
            new Teacher { firstName = "John", lastName = "Perry", course = "english", givesOnlineClass = false, phoneNumber = "365685686"},
            new Teacher { firstName = "Macarena", lastName = "Segura", course = "spanish", givesOnlineClass = true, phoneNumber = "865745231"},
            new Teacher { firstName = "Jorge", lastName = "Garcia", course = "physics", givesOnlineClass = false,  phoneNumber = "598074452"},
            new Teacher { firstName = "Camila", lastName = "Parejo", course = "informatics", givesOnlineClass = true, phoneNumber = "346745890"},
            };

        public List<Teacher> GetTeachers()
        {
            return teachersList;
        }

        public string AddTeacher(Teacher teacher)
        {
            var existingTeacher = teachersList.Find(e => e.firstName == teacher.firstName);
            if (existingTeacher != null)
            {
                return "error";
            }
            else
            {
                teachersList.Add(teacher);
                return "success";
            }
        }

        public string UpdateTeacher(string name, Teacher teacher)
        {
            var existingTeacher = teachersList.Find(e => e.firstName == name);
            if (existingTeacher == null)
            {
                return "error";
            }
            else
            {
                existingTeacher.firstName = teacher.firstName;
                existingTeacher.lastName = teacher.lastName;
                existingTeacher.phoneNumber = teacher.phoneNumber;
                existingTeacher.givesOnlineClass = teacher.givesOnlineClass;
                existingTeacher.course = teacher.course;
                return "success";
            }
        }

        public string DeleteTeacher(string name)
        {
            var existingTeacher = teachersList.Find(e => e.firstName == name);
            if (existingTeacher == null)
            {
                return "error";
            } 
            else
            {
                teachersList.Remove(existingTeacher);
                return "success";
            }
        }

        public string GetBestWorstTeacher()
        {
            var bestworstTeacher = studentService.GetBestWorstTeacher();
            var bestTeacher = bestworstTeacher.OrderByDescending(i => i.courseAverage).First();
            var bestTeacherName = teachersList.Find(e => e.course == bestTeacher.course);
            var worstTeacher = bestworstTeacher.OrderByDescending(i => i.courseAverage).Last();
            var worstTeacherName = teachersList.Find(e => e.course == worstTeacher.course);

            return $"The best teacher is {bestTeacherName.firstName} in {bestTeacher.course} with an average student's grade of {bestTeacher.courseAverage}. The worst teacher is {worstTeacherName.firstName} in {worstTeacher.course} with average student's grade of {worstTeacher.courseAverage}.";
        }
        public string GetMostFailedTeacher()
        {
            var mostFailedTeacher = studentService.GetMostFailedTeacher();
            return mostFailedTeacher;
        }


    }
}
