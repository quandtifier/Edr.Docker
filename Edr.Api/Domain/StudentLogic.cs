
using Edr.Api.Interfaces;

namespace Edr.Api.Domain;
public class StudentLogic : IStudentLogic
{
    private readonly ILogger<StudentLogic> _logger;

    public StudentLogic(ILogger<StudentLogic> logger)
    {
        _logger = logger;
    }

    public Guid CreateStudent(string name)
    {
        var guid = Guid.NewGuid();
        _logger.LogInformation(message: "From StudentLogic.CreateStudent Guid for {name} is {guid}",name, guid);
        return guid;
    }
}