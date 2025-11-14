using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Classes.UpdateClass;

public record UpdateClassCommand(
    Guid id,
    string Code,
    string Title,
    string Description,
    Guid UpdatedBy) : ICommand<Guid>;
