using LearnWell.CourseManagement.Application.Abstractions.Clock;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Application.Classes.GenerateClass;

public sealed class CreateClassCommandHandler : ICommandHandler<CreateClassCommand, Guid>
{
    private readonly IClassRepository _classRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateProvider;
    private readonly IUserRepository _userRepository;

    public CreateClassCommandHandler(IClassRepository classRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateProvider, IUserRepository userRepository)
    {
        _classRepository = classRepository;
        _unitOfWork = unitOfWork;
        _dateProvider = dateProvider;
        _userRepository = userRepository;
    }

    public async Task<Result<Guid>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(new UserId(request.CreatedBy), cancellationToken);
        if (user is null)
            return Result.Failure<Guid>(UserErrors.NotFound);

        try
        {
            var course = Class.Create(
                code: request.Code,
                title: request.Title,
                description: request.Description,
                userId: user.Id,
                _dateProvider.UtcNow);


            _classRepository.Add(course);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return course.Id.Value;
        }
        catch (Exception)
        {
            return Result.Failure<Guid>(CourseErrors.NotCreated);
        }

    }
}
