using AutoMapper;
using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Contracts.Authorization.Dto;
using KeepInformed.Contracts.Authorization.Queries.GetUsers;
using MediatR;

namespace KeepInformed.Application.Authorization.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUsersQueryResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetAll()
            .Select(x => _mapper.Map<UserDto>(x));

        return new GetUsersQueryResponse()
        {
            Users = users
        };
    }
}
