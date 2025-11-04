using Microsoft.AspNetCore.Authorization;

namespace LearnWell.CourseManagement.Infrastructure.Authorization;
public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        :base(permission)
    {        
    }
}
