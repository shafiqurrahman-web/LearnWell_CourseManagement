using LearnWell.CourseManagement.Application.Abstractions.Clock;
using LearnWell.CourseManagement.Application.Exceptions;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LearnWell.CourseManagement.Infrastructure.Repositories;
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    //private readonly IPublisher _publisher;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApplicationDbContext(DbContextOptions options, 
        //IPublisher publisher,
        IDateTimeProvider dateTimeProvider) : base(options)
    {
        //_publisher = publisher;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            //First process the domain events and adding them to ChangeTracker as Outbox Messages,
            //then persisting everything in the database in a single transaction "atomic operation" 
            AddDomainEventsAsOutboxMessages();

            var result = await base.SaveChangesAsync(cancellationToken);
            
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception ocurred.", ex);
        }
        
    }

    //private async Task PublishDomainEventsAsync()
    //{
    //   
    //    var domainEvents = ChangeTracker
    //        .Entries<IEntity>()
    //        .Select(entry => entry.Entity)
    //        .SelectMany(entity =>
    //        {
    //            var domainEvents = entity.GetDomainEvents();

    //            entity.ClearDomainEvents();

    //            return domainEvents;
    //        }).ToList();

    //    foreach (var domainEvent in domainEvents) await _publisher.Publish(domainEvent);
    //}

    private void AddDomainEventsAsOutboxMessages()
    {
        var outboxMessages = ChangeTracker
            .Entries<IEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(),
                _dateTimeProvider.UtcNow,
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
            .ToList();

        AddRange(outboxMessages);
    }
}
