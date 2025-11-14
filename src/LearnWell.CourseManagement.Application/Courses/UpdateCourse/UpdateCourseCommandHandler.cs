using LearnWell.CourseManagement.Application.Abstractions.Clock;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Application.Courses.UpdateCourse
{
    public sealed class UpdateCourseCommandHandler : ICommandHandler<UpdateCourseCommand, Guid>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateProvider;
        private readonly IUserRepository _userRepository;

        public UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateProvider, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _dateProvider = dateProvider;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(new UserId(request.UpdatedBy), cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);

            var course = await _courseRepository.GetByIdAsync(new CourseId( request.id), cancellationToken);
            if (course is null)
                return Result.Failure<Guid>(CourseErrors.NotFound);

            try
            {
                var courseUpdate = new Course() { 
                Code = request.Code,
                Title = request.Title,
                Description = request.Description,
                CreatedBy = user.Id,
                CreatedAt = _dateProvider.UtcNow
                };

                _courseRepository.Update(courseUpdate);


                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return request.id;
            }
            catch (Exception)
            {
                return Result.Failure<Guid>(CourseErrors.NotUpdated);
            }

        }
    }
}
