using LearnWell.CourseManagement.Application.Abstractions.Clock;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Application.Courses.GenerateCourse
{
    public sealed class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, Guid>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateProvider;
        private readonly IUserRepository _userRepository;

        public CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateProvider, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _dateProvider = dateProvider;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(new UserId(request.CreatedBy), cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);

            try
            {
                var course = Course.Create(
                    code: request.Code,
                    title: request.Title,
                    description: request.Description,
                    userId: user.Id,
                    _dateProvider.UtcNow);
                _courseRepository.Add(course);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return course.Id.Value;
            }
            catch (Exception)
            {
                return Result.Failure<Guid>(CourseErrors.NotCreated);
            }

        }
    }
}
