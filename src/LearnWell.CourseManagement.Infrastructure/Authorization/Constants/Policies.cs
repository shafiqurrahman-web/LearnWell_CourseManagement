namespace LearnWell.CourseManagement.Infrastructure.Authorization.Constants
{
    public static class Policies
    {
        // Course Policies
        public const string CanCreateCourse = Roles.CourseCreate;
        public const string CanReadCourse = Roles.CourseRead;
        public const string CanUpdateCourse = Roles.CourseUpdate;
        public const string CanDeleteCourse = Roles.CourseDelete;

        // Class Policies
        public const string CanCreateClass = Roles.ClassCreate;
        public const string CanReadClass = Roles.ClassRead;
        public const string CanUpdateClass = Roles.ClassUpdate;
        public const string CanDeleteClass = Roles.ClassDelete;

        // Student Policies
        public const string CanCreateStudent = Roles.StudentCreate;
        public const string CanReadStudent = Roles.StudentRead;
        public const string CanUpdateStudent = Roles.StudentUpdate;
        public const string CanDeleteStudent = Roles.StudentDelete;

        // Enrollment Policies
        public const string CanManageEnrollment = Roles.EnrollmentManage;
        public const string CanViewEnrollment = Roles.EnrollmentView;

        // Student Self-View
        public const string CanViewMyCourses = Roles.MyCoursesRead;
        public const string CanViewMyClasses = Roles.MyClassesRead;
        public const string CanViewClassmates = Roles.ClassmatesRead;
    }
}
