using System.Data;

namespace LearnWell.CourseManagement.Application.Abstractions.Data;
public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
