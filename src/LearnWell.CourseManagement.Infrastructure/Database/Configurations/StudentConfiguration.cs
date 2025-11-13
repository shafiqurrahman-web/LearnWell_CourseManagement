using LearnWell.CourseManagement.Domain.Entities.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnWell.CourseManagement.Infrastructure.Database.Configurations;
public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("students");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
         .HasConversion(studentId => studentId.Value,
            value => new StudentId(value));

        builder.Property(x => x.StudentNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithOne(u => u.StudentProfile)
            .HasForeignKey<Student>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.StudentNumber).IsUnique();


    }
}
