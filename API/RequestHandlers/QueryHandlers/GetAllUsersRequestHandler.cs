using API.Models.Control.ResultModels;
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
    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequestModel, InnerResult<IEnumerable<UserOutputModel>>>
    {
        private IUserDataService _userDataService;
        private IMapper _mapper;

        public GetAllUsersRequestHandler(IUserDataService userDataService, IMapper mapper)
        {
            _userDataService = userDataService;
            _mapper = mapper;
        }

        public async Task<InnerResult<IEnumerable<UserOutputModel>>> Handle(GetAllUsersRequestModel request, CancellationToken cancellationToken)
        {
            var res = _userDataService.GetAll();
            if (res.IsSuccess)
            {
                return InnerResult<IEnumerable<UserOutputModel>>.Ok(
                        _mapper.Map<IEnumerable<User>, IEnumerable<UserOutputModel>>(res.Value));
            }
            return InnerResult<IEnumerable<UserOutputModel>>.Fail(res.Errors, res.StatusCode);
        }
    }
}
