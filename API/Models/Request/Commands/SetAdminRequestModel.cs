using API.Models.Control.ResultModels;
using FluentResults;
using MediatR;

namespace API.Models.Request.Commands
{
    public class SetAdminRequestModel : IRequest<InnerResult>
    {
        public int UserId { get; set; }
    }
}
