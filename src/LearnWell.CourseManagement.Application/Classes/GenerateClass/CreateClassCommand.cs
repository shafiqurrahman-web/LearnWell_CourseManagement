using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Classes.GenerateClass;

public record CreateClassCommand(
    
    string Code,
    string Title,
    string Description,
    Guid CreatedBy) : ICommand<Guid>;
