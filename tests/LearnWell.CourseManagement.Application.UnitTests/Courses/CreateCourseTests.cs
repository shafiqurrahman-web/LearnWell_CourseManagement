using FluentAssertions;
using LearnWell.CourseManagement.Application.Abstractions.Clock;
using LearnWell.CourseManagement.Application.Courses.GenerateCourse;
using LearnWell.CourseManagement.Application.Exceptions;
using LearnWell.CourseManagement.Application.UnitTests.Users;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Users;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace LearnWell.CourseManagement.Application.UnitTests.Courses;
public class CreateCourseTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private static readonly CreateCourseCommand Command = new(Code: "TestCode",
        Title: "Test Title",
        Description: "Test Description",
        CreatedBy: Guid.NewGuid());





    private readonly CreateCourseCommandHandler _handler;
    private readonly IUserRepository _userRepositoryMock;

    private readonly ICourseRepository _courseRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    private readonly IDateTimeProvider _dateProviderMock;


    public CreateCourseTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();

        _courseRepositoryMock = Substitute.For<ICourseRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _dateProviderMock = Substitute.For<IDateTimeProvider>();
        _dateProviderMock.UtcNow.Returns(UtcNow);

        _handler = new CreateCourseCommandHandler(_courseRepositoryMock, _unitOfWorkMock, _dateProviderMock, _userRepositoryMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUserIsNull()
    {
        //Arrange                
        _userRepositoryMock
            .GetByIdAsync(new UserId(Command.CreatedBy), Arg.Any<CancellationToken>())
            .Returns((User?)null);

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(UserErrors.NotFound);
    }


    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenCourseIsNull()
    {
        //Arrange
        var user = UserData.Create();

        _userRepositoryMock
            .GetByIdAsync(new UserId(Command.CreatedBy), Arg.Any<CancellationToken>())
            .Returns(user);

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(CourseErrors.NotFound);
    }

    
    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkThrows()
    {
        //Arrange
        var user = UserData.Create();
        var course = CourseData.Create();

        _userRepositoryMock
            .GetByIdAsync(new UserId(Command.CreatedBy), Arg.Any<CancellationToken>())
            .Returns(user);

        _courseRepositoryMock
            .GetByIdAsync(new CourseId(Command.CreatedBy), Arg.Any<CancellationToken>())
            .Returns(course);

        _unitOfWorkMock
            .SaveChangesAsync()
            .ThrowsAsync(new ConcurrencyException("Concurrency", new Exception()));

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(CourseErrors.NotFound);
    }

   
     
}
