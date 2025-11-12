using LearnWell.CourseManagement.Domain.Entities.CourseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LearnWell.CourseManagement.Infrastructure.Ddatabase.Configurations;

internal sealed class CourseClassConfiguration : IEntityTypeConfiguration<CourseClass>
{
    public void Configure(EntityTypeBuilder<CourseClass> builder)
    {
        builder.ToTable("course_classes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(courseClassId=>courseClassId.Value,
            value=> new CourseClassId(value));

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => new { x.CourseId, x.ClassId }).IsUnique();

        builder.HasOne(x => x.Course)
            .WithMany(c => c.CourseClasses)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Class)
            .WithMany(c => c.CourseClasses)
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Creator)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


