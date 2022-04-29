namespace Edr.Api.ApiModels;

public class UserProspectOptions 
{
    public IEnumerable<string> UserTypes
    {
        get
        {
            return new List<string>() { "all", "Student", "Facilitory" };
        }
        set { }
    }
    public IEnumerable<string> GradeLevels 
    {
        get
        {
            return new List<string>() { "Preschool", "K-8", "Highschool", "College" };

        }
        set { }
    }
    
}