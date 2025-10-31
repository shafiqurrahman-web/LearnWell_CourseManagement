﻿namespace LearnWell.CourseManagement.Domain.Entities.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
