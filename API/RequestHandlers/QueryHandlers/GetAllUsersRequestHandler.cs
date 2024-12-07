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
    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequestModel, Result<IEnumerable<UserOutputModel>>>
    {
        private IUserDataService _userDataService;
        private IMapper _mapper;

        public GetAllUsersRequestHandler(IUserDataService userDataService, IMapper mapper)
        {
            _userDataService = userDataService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserOutputModel>>> Handle(GetAllUsersRequestModel request, CancellationToken cancellationToken)
        {
            var res = _userDataService.GetAll();
            if (res.IsSuccess)
            {
                return Result.Ok(
                        _mapper.Map<IEnumerable<User>, IEnumerable<UserOutputModel>>(res.Value));
            }
            return Result.Fail(res.Errors);
        }
    }
}
