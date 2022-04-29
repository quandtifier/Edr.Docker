using Microsoft.AspNetCore.Mvc;
using Edr.Api.ApiModels;
using Edr.Api.Interfaces;
using Serilog;

namespace Edr.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserProspectController : ControllerBase
{
    private readonly IUserProspectLogic _userProspectLogic;

    public UserProspectController(IUserProspectLogic userProspectLogic)
    {
        _userProspectLogic = userProspectLogic;
    }

    [HttpGet]
    public  UserProspectOptions GetUserProspectOptions(string category = "all")
    {
        Log.ForContext("Category", category)
            .Information("Starting controller actions GetUserProspectOptions");
        return _userProspectLogic.GetUserProspectOptions(category);
    }
}