using API.Models.Domain;
using API.Models.Request;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers
{
    public class GetAllRolesRequestHandler : IRequestHandler<GetAllRolesRequestModel, Result<IEnumerable<Role>>>
    {
        private IRoleDataService _roleDataService;

        public GetAllRolesRequestHandler(IRoleDataService roleDataService)
        {
            _roleDataService = roleDataService;
        }

        public Task<Result<IEnumerable<Role>>> Handle(GetAllRolesRequestModel request, CancellationToken cancellationToken)
        {
            var res = _roleDataService.GetAll();
            return Task.FromResult(res);
        }
    }
}
