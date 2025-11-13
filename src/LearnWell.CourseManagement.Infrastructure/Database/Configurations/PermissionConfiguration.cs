using LearnWell.CourseManagement.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnWell.CourseManagement.Infrastructure.Database.Configurations;
public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(x => x.Id);


        //Seed initial data
        builder.HasData(Permission.CourseRead);
    }
}
