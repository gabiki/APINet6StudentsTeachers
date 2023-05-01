using Microsoft.AspNetCore.Mvc;
using Students.IServices;
using Students.Services;
using Students.Models;

namespace Students.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService teacherService;
    public TeacherController(ITeacherService teacherService)
    {
        this.teacherService = teacherService;
    }

    [HttpGet("allteachers")]
    public ActionResult<List<Teacher>> GetAllTeachers()
    {
        var teacherList = teacherService.GetTeachers();
        return Ok(teacherList);
    }

    [HttpGet("mostFailedTeacher")]
    public ActionResult<string> GetMostFailedTeacher()
    {
        var mostFailedTeacher = teacherService.GetMostFailedTeacher();
        return Ok(mostFailedTeacher);
    }

    [HttpGet("oneteacher")]
    public ActionResult<Teacher> GetOneTeacher(string course)
    {
        var teacherList = teacherService.GetTeachers();
        var oneTeacher = teacherList.Find(e => e.course == course);
        return Ok(oneTeacher);
    }

    [HttpGet("bestworstteacher")]
    public ActionResult<string> GetBestWorstTeacher()
    {

        var bestworst = teacherService.GetBestWorstTeacher();
        return Ok(bestworst);
    }

    [HttpPost("addteacher")]
    public ActionResult AddTeacher(Teacher teacher)
    {
        var teacherAdded = teacherService.AddTeacher(teacher);
        if (teacherAdded == "error")
        {
            return BadRequest("Teacher already exists.");
        }
        else
        {
            return Ok("Created");
        }
        //var teacherList = teacherService.GetTeachers();
        //teacherList.Add(teacher);
        //var resourceUrl = Request.Path.ToString() + "/" + teacher.firstName;
        //return Created(resourceUrl, teacher);

    }

    [HttpPut("updateteacher")]
    public ActionResult UpdateTeacher(string name, Teacher teacher)
    {
        var teacherUpdated = teacherService.UpdateTeacher(name, teacher);
        if (teacherUpdated == "error")
        {
            return NotFound("Teacher not found.");
        } 
        else
        {
            return Ok("Teacher updated.");
        }
    }

    [HttpDelete("deleteteacher")]
    public ActionResult DeleteTeacher(string name)
    {
        var teacherdeleted = teacherService.DeleteTeacher(name);
        if (teacherdeleted == "error") 
        {
            return NotFound("Teacher not found.");
        }
        else
        {
            return Ok("Teacher deleted.");
        };
    }
}

// Crear en domain: estudiantes, profesores. Con al menos 5 properties diferentes y que tengan string, int, bool, double u otro
// Cada profesor tiene sus estudiantes
// Injecion de dependencia / Crear un Service de estudiantes y otro Service de profesores. Solo crear lista de estudiantes y de profesores. 15 estudiantes y 3 profesores
// Crear dos controllers, con CRUD, con get de 1 est/profe y de todos
// nombre, apellido, tel, email - est

// estudiantes -> aparte de nombre tb 5 properties de las asignatura de notas --> int
// profesor - tendra propiedad lista de estudiantes
// estudiantes - tendra un parametro - un profesor

// ESTUDIANTES
// en controller - > en endpoint pasar primero la palabra chicocs o chicas
// segundo parametro el nombre de asignatura string
// resultado nota average de las chicas o de los chiccos 





// en los Service y pasar a los contoller - para profe y estudiantes -->
// --> hacer la media de las notas 
// --> enh contorller que devuelva la mlista de todos los estudiantes con sus notas medias
// --> ek mejor y peor estudiante

// post profesor no tiene estudiantes
// remove - remove por nonmbre o por asignatura al profesor

// profesor - tendra propiedad lista de estudiantes
// estudiantes - tendra un parametro - un profesor

// estudiantes - chicas y chicos --> dos get - uno de chicas y de chicos

// PROFESORES
// cada profesor tiene una asignatura
// quien es el mejor profesor - segun la nota media de todos 

// que profesor tiene mas suspendidos ()

// suspendidos - parametro asignatura y sacar solo nombres

// quien repite curso (quien ha suspendido mas de 2 asignaturas) -> mostrar nombres