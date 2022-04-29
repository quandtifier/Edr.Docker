using Edr.Api.ApiModels;

namespace Edr.Api.Interfaces;
public interface IUserProspectLogic
{
    UserProspectOptions GetUserProspectOptions(string category);
}
