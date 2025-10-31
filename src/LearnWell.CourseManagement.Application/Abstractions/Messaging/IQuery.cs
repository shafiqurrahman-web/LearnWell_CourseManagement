using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using MediatR;

namespace LearnWell.CourseManagement.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
