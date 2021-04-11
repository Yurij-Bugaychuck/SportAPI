using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models; 
namespace SportAPI.Interfaces
{
    interface ISecurityService
    {
        bool CanRead<T>(User user, T obj);
    }
}
