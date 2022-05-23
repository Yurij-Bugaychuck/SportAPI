using SportAPI.Models.User;

namespace SportAPI.Interfaces
{
    interface ISecurityService
    {
        bool CanRead<T>(User user, T obj);
    }
}
