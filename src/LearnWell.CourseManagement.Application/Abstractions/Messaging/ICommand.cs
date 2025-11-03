using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using MediatR;

namespace LearnWell.CourseManagement.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}
public interface IBaseCommand
{
}
