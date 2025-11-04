namespace LearnWell.CourseManagement.Infrastructure.Authorization.Constants
{
    public static class Roles
    {
        // Course Roles
        public const string CourseCreate = "course.create";
        public const string CourseRead = "course.read";
        public const string CourseUpdate = "course.update";
        public const string CourseDelete = "course.delete";

        // Class Roles
        public const string ClassCreate = "class.create";
        public const string ClassRead = "class.read";
        public const string ClassUpdate = "class.update";
        public const string ClassDelete = "class.delete";

        // Student Roles
        public const string StudentCreate = "student.create";
        public const string StudentRead = "student.read";
        public const string StudentUpdate = "student.update";
        public const string StudentDelete = "student.delete";

        // Enrollment Roles
        public const string EnrollmentManage = "enrollment.manage";
        public const string EnrollmentView = "enrollment.view";

        // Student-specific Roles
        public const string MyCoursesRead = "mycourses.read";
        public const string MyClassesRead = "myclasses.read";
        public const string ClassmatesRead = "classmates.read";

        // Realm-level Roles
        public const string Staff = "staff";
        public const string Student = "student";
    }
}
