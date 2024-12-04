using FluentResults;
using MediatR;

namespace API.Models.Request
{
    public class SetAdminRequestModel : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
