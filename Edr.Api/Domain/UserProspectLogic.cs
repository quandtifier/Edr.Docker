using Edr.Api.ApiModels;
using Edr.Api.Interfaces;

namespace Edr.Api.Domain;

public class UserProspectLogic : IUserProspectLogic
{
    private readonly ILogger<UserProspectLogic> _logger;
    private readonly List<string> _validStudents = new List<string>();

    public UserProspectLogic(ILogger<UserProspectLogic> logger)
    {
        _logger = logger;
    }

    public  UserProspectOptions GetUserProspectOptions(string category)
    {
        _logger.LogInformation("Starting logic to get products for {category}", category);

        UserProspectOptions options = new UserProspectOptions();
        if (!options.UserTypes.Any(t =>
            string.Equals(category, t, StringComparison.InvariantCultureIgnoreCase)))
        {
            throw new ApplicationException($"Unrecognized user type: {category}." +
                $"Valid categories are: [{string.Join(",", options.UserTypes)}]");
        }
        else if (category == "all")
        {
            return options = new UserProspectOptions();
        }
        else
        {
            switch (category)
            {
                case "Student"://...
                default:
                    return new UserProspectOptions();
                    break;
            }
        }

    }
}
