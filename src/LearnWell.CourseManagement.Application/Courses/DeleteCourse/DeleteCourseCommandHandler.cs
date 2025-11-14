using LearnWell.CourseManagement.Application.Abstractions.Clock;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Application.Courses.DeleteCourse
{
    public sealed class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand, Guid>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateProvider;
        private readonly IUserRepository _userRepository;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateProvider, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _dateProvider = dateProvider;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
        {
            try
            {

                await _courseRepository.DeleteByIdAsync(new CourseId(command.CourseId), cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success(command.CourseId);

            }
            catch (Exception)
            {
                return Result.Failure<Guid>(CourseErrors.NotUpdated);
            }

        }
    }
}
