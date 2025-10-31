using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using MediatR;

namespace LearnWell.CourseManagement.Application.Abstractions.Messaging;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
