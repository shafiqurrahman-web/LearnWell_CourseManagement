using LearnWell.CourseManagement.Application.Abstractions.Authentication;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Users;
using LearnWell.CourseManagement.Domain.Entities.Users.ValueObjects;

namespace LearnWell.CourseManagement.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IAuthenticationService authenticationService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            request.FirstName,
            request.LastName,
            new Email(request.Email));

        var identityId = await _authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);

        user.SetIdentityId(identityId);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync();

        return user.Id.Value;
    }
}
