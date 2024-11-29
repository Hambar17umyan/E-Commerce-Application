using API.Models.Domain;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Interfaces
{
    public interface IUserDataRepository : IDataRepository<User>
    {

    }
}
