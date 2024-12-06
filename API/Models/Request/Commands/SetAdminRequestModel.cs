using FluentResults;
using MediatR;

namespace API.Models.Request.Commands
{
    public class SetAdminRequestModel : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
