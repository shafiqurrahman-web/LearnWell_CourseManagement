using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Classes.DeleteClass

{
    public record DeleteClassCommand(
        Guid CourseId) : ICommand<Guid>;


    
}
