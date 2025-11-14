
using LearnWell.CourseManagement.Domain.Entities.Abstractions;

namespace LearnWell.CourseManagement.Domain.Entities.Classes;

public static class ClassErrors
{
    public static readonly Error NotFound = new(
        "Class.NotFound",
        "The Class with the specified identifier was not found");


    public static readonly Error NotCreated = new(
        "Class.NotCreated",
        "The Class is not created");

    public static readonly Error NotUpdated = new(
       "Class.NotUpdated",
       "The Class is not updated");

}
