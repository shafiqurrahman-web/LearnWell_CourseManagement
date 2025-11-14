using LearnWell.CourseManagement.Application.Abstractions.Clock;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Application.Classes.UpdateClass;


public sealed class UpdateClassCommandHandler : ICommandHandler<UpdateClassCommand, Guid>
{
    private readonly IClassRepository _classRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateProvider;
    private readonly IUserRepository _userRepository;

    public UpdateClassCommandHandler(IClassRepository classRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateProvider, IUserRepository userRepository)
    {
        _classRepository = classRepository;
        _unitOfWork = unitOfWork;
        _dateProvider = dateProvider;
        _userRepository = userRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(new UserId(request.UpdatedBy), cancellationToken);
        if (user is null)
            return Result.Failure<Guid>(UserErrors.NotFound);

        var course = await _classRepository.GetByIdAsync(new ClassId( request.id), cancellationToken);
        if (course is null)
            return Result.Failure<Guid>(CourseErrors.NotFound);

        try
        {
            
            var classUpdate = new Class()
            {
                Code = request.Code,
                Title = request.Title,
                Description = request.Description,
                CreatedBy = user.Id,
                CreatedAt = _dateProvider.UtcNow
            };




            _classRepository.Update(classUpdate);


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return request.id;
        }
        catch (Exception)
        {
            return Result.Failure<Guid>(CourseErrors.NotUpdated);
        }

    }
}
