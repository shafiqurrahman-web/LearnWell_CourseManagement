using LearnWell.CourseManagement.Domain.Entities.StudentCourses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnWell.CourseManagement.Infrastructure.Database.Configurations;


internal sealed class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder.ToTable("student_courses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(studentCourseId => studentCourseId.Value,
            value => new StudentCourseId(value));

        builder.Property(x => x.EnrolledAt)
            .IsRequired();

        builder.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique();

        builder.HasOne(x => x.Student)
            .WithMany(s => s.StudentCourses)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        
        builder.HasOne(x => x.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.StaffUser)
            .WithMany()
            .HasForeignKey(x => x.EnrolledBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
