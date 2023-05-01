using Microsoft.VisualBasic;
using Students.IServices;
using Students.Models;
using System;
using System.Collections.Concurrent;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using static Students.Services.StudentService;

namespace Students.Services
{
    public class StudentService : IStudentService
    {
        private List<Student> studentsList = new List<Student>
            {
            new Student { firstName = "Emily", lastName = "Snow", gender = "female", isForeign = true, mathsgrade = 5, englishgrade = 5, spanishgrade = 5, physicsgrade = 5, informaticsgrade = 5, phoneNumber = "230980042"},
            new Student { firstName = "Jorge", lastName = "Rodriguez", gender = "male", isForeign = false, mathsgrade = 4, englishgrade = 2, spanishgrade = 2, physicsgrade = 5, informaticsgrade = 3, phoneNumber = "464783267"},
            new Student { firstName = "Antonio", lastName = "Ochoa", gender = "male", isForeign = false, mathsgrade = 2, englishgrade = 2, spanishgrade = 2, physicsgrade = 3, informaticsgrade = 5, phoneNumber = "068432627"},
            new Student { firstName = "Manuel ", lastName = "Miro", gender = "male", isForeign = false, mathsgrade = 3, englishgrade = 2, spanishgrade = 3, physicsgrade = 5, informaticsgrade = 4, phoneNumber = "374665867"},
            new Student { firstName = "Cecilia ", lastName = "Soria", gender = "female", isForeign = false, mathsgrade = 5, englishgrade = 2, spanishgrade = 5, physicsgrade = 5, informaticsgrade = 3, phoneNumber = "079657436"},
            new Student { firstName = "Brad", lastName = "Smith", gender = "male", isForeign = true, mathsgrade = 2, englishgrade = 3, spanishgrade = 3, physicsgrade = 5, informaticsgrade = 4, phoneNumber = "484580903"},
            new Student { firstName = "Kim", lastName = "Miller", gender = "female", isForeign = true, mathsgrade = 5, englishgrade = 2, spanishgrade = 5, physicsgrade = 5, informaticsgrade = 2, phoneNumber = "823627678"},
            new Student { firstName = "Julia", lastName = "Bonilla", gender = "female", isForeign = false, mathsgrade = 2, englishgrade = 2, spanishgrade = 4, physicsgrade = 5, informaticsgrade = 4, phoneNumber = "245498492"},
            new Student { firstName = "Alfonso", lastName = "Herrera", gender = "male", isForeign = false, mathsgrade = 2, englishgrade = 2, spanishgrade = 3, physicsgrade = 5, informaticsgrade = 4, phoneNumber = "087913441"},
            new Student { firstName = "Daniel ", lastName = "Aparicio", gender = "male", isForeign = false, mathsgrade = 5, englishgrade = 5, spanishgrade = 4, physicsgrade = 5, informaticsgrade = 2, phoneNumber = "105298235"},
            new Student { firstName = "Estrella ", lastName = "Yuste", gender = "female", isForeign = false, mathsgrade = 4, englishgrade = 2, spanishgrade = 2, physicsgrade = 2, informaticsgrade = 3, phoneNumber = "140985347"},
            new Student { firstName = "Lily ", lastName = "Brown", gender = "female", isForeign = true, mathsgrade = 4, englishgrade = 2, spanishgrade = 5, physicsgrade = 5, informaticsgrade = 5, phoneNumber = "230458221"},
            new Student { firstName = "Henry", lastName = "Johnson", gender = "male", isForeign = true, mathsgrade = 5, englishgrade = 4, spanishgrade = 5, physicsgrade = 4, informaticsgrade = 3, phoneNumber = "028497236"},
            new Student { firstName = "Enrique ", lastName = "Arnau", gender = "male", isForeign = false, mathsgrade = 2, englishgrade = 2, spanishgrade = 5, physicsgrade = 5, informaticsgrade = 4, phoneNumber = "352980250"},
            new Student { firstName = "Yasmin ", lastName = "Jones", gender = "female", isForeign = true, mathsgrade = 2, englishgrade = 2, spanishgrade = 2, physicsgrade = 2, informaticsgrade = 2, phoneNumber = "240863462"}
            };

        public List<Student> GetStudents()
        {
            return studentsList;
        }
        public string AddStudent(Student student)
        {
            var existingStudent = studentsList.Find(e => e.firstName == student.firstName);
            if (existingStudent != null)
            {
                return "error";
            }
            else
            {
                studentsList.Add(student);
                return "success";
            }
        }

        public double GetGirlsOrBoysAverage(string maleorfemale, string course)
        {
            List<Student> maleFemaleList = new List<Student>();
            var maleFemale = studentsList.FindAll(e => e.gender == maleorfemale);
            maleFemaleList = maleFemale.ToList();
            double averagegrade;
            if (course == "mathematics")
            {
                averagegrade = maleFemaleList.Average(e => e.mathsgrade);
                return averagegrade;
            }
            else if (course == "physics")
            {
                averagegrade = maleFemaleList.Average(e => e.physicsgrade);
                return averagegrade;
            }
            else if (course == "english")
            {
                averagegrade = maleFemaleList.Average(e => e.englishgrade);
                return averagegrade;
            }
            else if (course == "spanish")
            {
                averagegrade = maleFemaleList.Average(e => e.spanishgrade);
                return averagegrade;
            }
            else if (course == "informatics")
            {
                averagegrade = maleFemaleList.Average(e => e.informaticsgrade);
                return averagegrade;
            }
            else
            {
                return 0;
                ; }
        }

        public List<StudentAverage> GetAllStudentsAverage()
        {
            List<StudentAverage> studentAverage = new List<StudentAverage>();
            for (int i = 0; i < studentsList.Count(); i++)
            {
                double singleAverage = (studentsList[i].mathsgrade + studentsList[i].spanishgrade + studentsList[i].englishgrade + studentsList[i].physicsgrade + studentsList[i].informaticsgrade) / (5.00);
                studentAverage.Add(new StudentAverage { firstName = studentsList[i].firstName, average = singleAverage });
            }
            return studentAverage;
        }

        public string GetBestWorstStudent()
        {
            var studentAverage = GetAllStudentsAverage();
           
            var bestStudent = studentAverage.OrderByDescending(i => i.average).First();
            var worststudent = studentAverage.OrderByDescending(i => i.average).Last();
           
            return $"The best student is {bestStudent.firstName} with the average grade of {bestStudent.average}. The worst student is {worststudent.firstName} with the average grade of {worststudent.average}.";
        }

        public string UpdateStudent(string name, Student student)
        {
            var existingStudent = studentsList.Find(e => e.firstName == name);
            if (existingStudent == null)
            {
                return "error";
            }
            else
            {
                existingStudent.firstName = student.firstName;
                existingStudent.lastName = student.lastName;
                existingStudent.isForeign = student.isForeign;
                existingStudent.gender = student.gender;
                existingStudent.phoneNumber = student.phoneNumber;
                existingStudent.spanishgrade = student.spanishgrade;
                existingStudent.physicsgrade = student.physicsgrade;
                existingStudent.englishgrade = student.englishgrade;
                existingStudent.mathsgrade = student.mathsgrade;
                existingStudent.informaticsgrade = student.informaticsgrade;
                return "success";
            }
        }

        public string DeleteStudent(string name)
        {
            var existingStudent = studentsList.Find(e => e.firstName == name);
            if (existingStudent != null)
            {
                studentsList.Remove(existingStudent);
                return "success";
            }
            else
            {
                return "error";
            }
        }

        public List<TeacherAverage> GetBestWorstTeacher()
        {
            List<TeacherAverage> teacherAverage = new List<TeacherAverage>();
            double x = studentsList.Count();
            double englishAverage = (studentsList.Sum(e => e.englishgrade)) / x;
            double spanishAverage = (studentsList.Sum(e => e.spanishgrade)) / x;
            double mathsAverage = (studentsList.Sum(e => e.mathsgrade)) / x;
            double physicsAverage = (studentsList.Sum(e => e.physicsgrade)) / x;
            double informaticsAverage = (studentsList.Sum(e => e.informaticsgrade)) / x;
            teacherAverage.Add( new TeacherAverage { course = "english", courseAverage = englishAverage} );
            teacherAverage.Add( new TeacherAverage { course = "spanish", courseAverage = spanishAverage} );
            teacherAverage.Add( new TeacherAverage { course = "mathematics", courseAverage = mathsAverage} );
            teacherAverage.Add( new TeacherAverage { course = "physics", courseAverage = physicsAverage} );
            teacherAverage.Add( new TeacherAverage { course = "informatics", courseAverage = informaticsAverage} );
            return teacherAverage;
        }

        public class FailedStudents
        {
            public string firstName;
        }
        public string GetFailedStudents(string course)
        {
            List<FailedStudents> failedStudents = new List<FailedStudents>();
            
            List<string> repeatStudents = new List<string>();

            if (course == "mathematics")
            {
                int xFailed = studentsList.Count(e => e.mathsgrade < 3 == true);
                List<Student> failedStudent = studentsList.FindAll(e => e.mathsgrade < 3 == true);
                failedStudent.ForEach(e => repeatStudents.Add(e.firstName));
                string concat = string.Join(", ", repeatStudents);
                return $"There are {xFailed} students that failed {course} course: {concat}.";
            }
            else if (course == "english")
            {
                int xFailed = studentsList.Count(e => e.englishgrade < 3 == true);
                List<Student> failedStudent = studentsList.FindAll(e => e.mathsgrade < 3 == true);
                failedStudent.ForEach(e => repeatStudents.Add(e.firstName));
                string concat = string.Join(", ", repeatStudents);
                return $"There are {xFailed} students that failed {course} course: {concat}";
            }
            else if (course == "spanish")
            {
                int xFailed = studentsList.Count(e => e.spanishgrade < 3 == true);
                List<Student> failedStudent = studentsList.FindAll(e => e.mathsgrade < 3 == true);
                failedStudent.ForEach(e => repeatStudents.Add(e.firstName));
                string concat = string.Join(", ", repeatStudents);
                return $"There are {xFailed} students that failed {course} course: {concat}";
            }
            else if (course == "physics")
            {
                int xFailed = studentsList.Count(e => e.physicsgrade < 3 == true);
                List<Student> failedStudent = studentsList.FindAll(e => e.mathsgrade < 3 == true);
                failedStudent.ForEach(e => repeatStudents.Add(e.firstName));
                string concat = string.Join(", ", repeatStudents);
                return $"There are {xFailed} students that failed {course} course: {concat}";
            }
            else if (course == "informatics")
            {
                int xFailed = studentsList.Count(e => e.informaticsgrade < 3 == true);
                List<Student> failedStudent = studentsList.FindAll(e => e.mathsgrade < 3 == true);
                failedStudent.ForEach(e => repeatStudents.Add(e.firstName));
                string concat = string.Join(", ", repeatStudents);
                return $"There are {xFailed} students that failed {course} course: {concat}";
            } 
            else
            {
                return "The course doesn't exist.";
            }
        }
         

        public class RepeatStudent
        {
            public string firstName;
            public int xFailedCourses;
        }
        public string GetRepeatStudents()
        {
            List<RepeatStudent> repeatStudent = new List<RepeatStudent>();

            for (int i = 0; i < studentsList.Count(); i++)
            {
                int xFailedCourses = 0;
                if (studentsList[i].englishgrade < 3)
                {
                    xFailedCourses +=1;
                }
                if (studentsList[i].spanishgrade < 3)
                {
                    xFailedCourses += 1;
                } 
                if (studentsList[i].informaticsgrade < 3)
                {
                    xFailedCourses += 1;
                }
                if (studentsList[i].mathsgrade < 3)
                {
                    xFailedCourses += 1;
                }
                if (studentsList[i].physicsgrade < 3)
                {
                    xFailedCourses += 1;
                }
                if (xFailedCourses > 0)
                {
                    repeatStudent.Add(new RepeatStudent { firstName = studentsList[i].firstName, xFailedCourses = xFailedCourses });
                }
            }
            List<RepeatStudent> failedStudents = repeatStudent.FindAll(e => e.xFailedCourses > 2);
            List<string> failedNames = new List<string>();
            for (int i = 0; i < failedStudents.Count; i++)
            {
                failedNames.Add(failedStudents[i].firstName);
            }
            string concat = string.Join(", ", failedNames);
            return $"Students that have to repeat course are: {concat}.";
        }

        public string GetMostFailedTeacher()
        {
            string mostFailedteacher = "";
            int mathsFailed = studentsList.Count(e => e.mathsgrade < 3 == true);
            int englishFailed = studentsList.Count(e => e.englishgrade < 3 == true);
            int physicsFailed = studentsList.Count(e => e.physicsgrade < 3 == true);
            int informaticsFailed = studentsList.Count(e => e.informaticsgrade < 3 == true);
            int spanishFailed = studentsList.Count(e => e.spanishgrade < 3 == true);
            var max = (new List<int>() {mathsFailed, englishFailed, physicsFailed, informaticsFailed, spanishFailed}).OrderByDescending(i => i).First();
            if (mathsFailed == max)
            {
                mostFailedteacher = "Juan";
            } 
            else if (englishFailed == max)
            {
                mostFailedteacher = "John";
            }
            else if (spanishFailed == max)
            {
                mostFailedteacher = "Macarena";
            }
            else if (physicsFailed == max)
            {
                mostFailedteacher = "Jorge";
            }
            else if (informaticsFailed == max)
            {
                mostFailedteacher = "Camila";
            }
            return $"The teacher that has most failed students is {mostFailedteacher}.";
        }
    }
}

    
