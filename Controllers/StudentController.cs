using Microsoft.AspNetCore.Mvc;
using Students.IServices;
using Students.Services;
using Students.Models;

namespace Students.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService studentService;
    public StudentController(IStudentService studentService)
    {
        this.studentService = studentService;
    }

    [HttpGet("allstudents")]
    public ActionResult<List<Student>> GetAllStudents()
    {
        var studentList = studentService.GetStudents();
        return Ok(studentList);
    }

    [HttpGet("averagemalefemalegrade")]
    public ActionResult<double> GetGirlsOrBoysAverage(string maleorfemale, string course)
    {
        var averagegrade = studentService.GetGirlsOrBoysAverage(maleorfemale, course);
        return Ok("The average grade for " + maleorfemale + " students in " + course + " course is: " + averagegrade + ".");    
    }

    [HttpGet("averageallgrade")]
    public ActionResult<List<Student>> GetAllStudentsAverage()
    {
        var studentsAverageGrades = studentService.GetAllStudentsAverage();
        return Ok(studentsAverageGrades);
    }

    [HttpGet("bestworst")]
    public ActionResult<string> GetBestWorstStudent()
    {
        var bestworst = studentService.GetBestWorstStudent();
        return Ok(bestworst);
    }

    [HttpGet("failedstudents")]
    public ActionResult<string> GetFailedStudents(string course)
    {
        var failedStudents = studentService.GetFailedStudents(course);
        return Ok(failedStudents);
    }

    [HttpGet("repeatcoursestudents")]
    public ActionResult<string> GetRepeatStudents()
    {
        var repeatStudents = studentService.GetRepeatStudents();
        return Ok(repeatStudents);  
    }

    [HttpPost("addstudent")]
    public ActionResult AddStudent(Student student)
    {
        var studentadded = studentService.AddStudent(student);
        if (studentadded == "error")
        {
            return BadRequest("Student already existis.");
        }
        else
        {
            return Ok("Student addded.");
        }
    }

    [HttpPut("updatestudent")]
    public ActionResult UpdateStudent(string name, Student student)
    {
        var studentupdated = studentService.UpdateStudent(name, student);
        if (studentupdated == "error")
        {
            return NotFound("Student not found.");
        }
        else
        {
            return Ok("Student updated.");
        }
    }

    [HttpDelete("deletestudent")]
    public ActionResult DeleteStudent(string name)
    {
        var studentdeleted = studentService.DeleteStudent(name);
        if (studentdeleted == "error")
        {
            return NotFound("Student not found.");
        }
        else
        {
            return Ok("Student deleted.");
        }
    }
}
