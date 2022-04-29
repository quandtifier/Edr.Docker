
using Edr.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Edr.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly IStudentLogic _studentLogic;
    
    public StudentController(ILogger<StudentController> logger, IStudentLogic studentLogic)
    {
        _logger = logger;
        _studentLogic = studentLogic;
    }

    [HttpPost]
    public  Guid CreateStudent(string name)
    {
        _logger.LogInformation(message: "Starting controller actions CreateStudent for {name}", name);
        
        var guid = _studentLogic.CreateStudent(name);

        return guid;
    }
}