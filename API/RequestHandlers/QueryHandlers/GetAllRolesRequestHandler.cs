using API.Models.Domain.Concrete;
using API.Models.Request.Queries;
using API.Models.Response.Output;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.DataServices;
using AutoMapper;
using FluentResults;
using MediatR;

namespace API.RequestHandlers.QueryHandlers
{
    public class GetAllRolesRequestHandler : IRequestHandler<GetAllRolesRequestModel, Result<IEnumerable<RoleOutputModel>>>
    {
        private IRoleDataService _roleDataService;
        private IMapper _mapper;

        public GetAllRolesRequestHandler(IRoleDataService roleDataService, IMapper mapper)
        {
            _roleDataService = roleDataService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoleOutputModel>>> Handle(GetAllRolesRequestModel request, CancellationToken cancellationToken)
        {
            var res = _roleDataService.GetAll();
            if (res.IsSuccess)
            {
                return Result.Ok(
                        _mapper.Map<IEnumerable<Role>, IEnumerable<RoleOutputModel>>(res.Value));
            }
            return Result.Fail(res.Errors);
        }
    }
}
