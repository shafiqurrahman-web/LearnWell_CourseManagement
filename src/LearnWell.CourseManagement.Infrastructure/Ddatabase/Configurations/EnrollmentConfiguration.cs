
using LearnWell.CourseManagement.Domain.Entities.Enrollments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LearnWell.CourseManagement.Infrastructure.Ddatabase.Configurations;



internal sealed class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("enrollments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
           .HasConversion(enrollmentId => enrollmentId.Value,
              value => new EnrollmentId(value));

        builder.Property(x => x.EnrolledAt)
            .IsRequired();

        builder.Property(x => x.FromCourseDefault)
            .IsRequired();

        builder.HasIndex(x => new { x.StudentId, x.ClassId }).IsUnique();

        builder.HasOne(x => x.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Class)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.StaffUser)
            .WithMany()
            .HasForeignKey(x => x.EnrolledBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
